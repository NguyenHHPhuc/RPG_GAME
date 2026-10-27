using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RPG.DoiTuong;
using RPG.Services;
namespace RPG.Controls
{
    public partial class BattleControl : UserControl
    {
        // Tọa độ khói tính từ điểm chân của nhân vật sau khi ngã.
        // X dương: sang phải; Y âm: đi lên trên.
        private const int DeathSmokeOffsetX = 18;
        private const int DeathSmokeOffsetY = -11;

        private GameState gameState;
        private BattleService battleService;
        private Player player;
        private Enemy enemy;
        private ItemService itemService;
        private int designWidth;
        private int playerBarLeft;
        private int enemyBarLeft;
        private int playerBarWidth;
        private int enemyBarWidth;
        private int playerNameOffset;
        private int enemyNameOffset;
        private int playerHpOffset;
        private int playerMpOffset;
        private int enemyHpOffset;
        private readonly System.Windows.Forms.Timer animationTimer;
        private PictureBox? movingCharacter;
        private int animationStep;
        private int animationStartX;
        private int animationStartY;
        private int animationDirection;
        private Action? animationFinished;
        private PictureBox? damagedCharacter;
        private readonly List<Point> deathParticles = new List<Point>();
        private readonly Random particleRandom = new Random();
        private bool targetWasDefeated;
        private bool isArrowAttack;
        private int strikeDistance;
        private Image? arrowImage;
        private Image? tintedImage;
        private Image? damagedOriginalImage;
        private PictureBox? fallenCharacter;
        private Image? fallenImage;
        private float fallAngle;
        private Point fallenPivot;
        private Point smokeOrigin;
        private Point arrowStart;
        private Point arrowEnd;
        private Point arrowPosition;
        private readonly List<FloatingText> floatingTexts = new List<FloatingText>();
        private readonly System.Windows.Forms.Timer floatingTextTimer;
        private int damageToShow;
        // Một lượt gồm hành động của người chơi và đòn đáp trả của quái.
        private int currentTurn = 1;

        private enum BattleOutcome
        {
            None,
            NormalVictory,
            GameVictory,
            PlayerLost
        }

        private BattleOutcome pendingOutcome = BattleOutcome.None;

        public event EventHandler BattleWon;
        public event EventHandler BattleLost;
        public event EventHandler GameWon;
        public BattleControl(GameState gameState)
        {
            InitializeComponent();

            // Events for the skill and item selection overlays.
            lstSkills.SelectedIndexChanged += lstSkills_SelectedIndexChanged;
            btnUseSelectedSkill.Click += btnUseSelectedSkill_Click;
            btnCancelSkill.Click += btnCancelSkill_Click;
            lstBattleItems.SelectedIndexChanged += lstBattleItems_SelectedIndexChanged;
            btnUseSelectedItem.Click += btnUseSelectedItem_Click;
            btnCancelItem.Click += btnCancelItem_Click;

            grpSkillSelection.Visible = false;
            grpItemSelection.Visible = false;

            this.gameState = gameState;
            player = gameState.Player;
            enemy = gameState.CurrentEnemy;
            battleService = new BattleService();
            itemService = new ItemService();
            animationTimer = new System.Windows.Forms.Timer { Interval = 20 };
            animationTimer.Tick += AnimationTimer_Tick;
            floatingTextTimer = new System.Windows.Forms.Timer { Interval = 20 };
            floatingTextTimer.Tick += FloatingTextTimer_Tick;
            LoadBattle();
            // Lấy mốc sau khi nạp tên nhân vật để vị trí nhãn được căn đúng với nội dung thật.
            SaveDesignerPositions();
            SizeChanged += (_, _) => ResizeHealthBars();
            ResizeHealthBars();
        }

        private void SaveDesignerPositions()
        {
            // Lưu vị trí đặt trong Designer để khi đổi cỡ vẫn giữ được cách sắp xếp ban đầu.
            designWidth = ClientSize.Width;
            playerBarLeft = pnlPlayerHP.Left;
            enemyBarLeft = pnlEnemyHP.Left;
            playerBarWidth = pnlPlayerHP.Width;
            enemyBarWidth = pnlEnemyHP.Width;

            playerNameOffset = 0;
            enemyNameOffset = 0;
            playerHpOffset = lblPlayerHP.Left - pnlPlayerHP.Left;
            playerMpOffset = lblPlayerMP.Left - pnlPlayerMP.Left;
            enemyHpOffset = lblEnemyHP.Left - pnlEnemyHP.Left;
        }

        private void ResizeHealthBars()
        {
            // Chia phần chiều rộng mới đều cho hai cột máu, giữ nguyên khoảng cách ở giữa.
            int extraWidth = ClientSize.Width - designWidth;
            int widthChange = extraWidth / 2;
            int newPlayerWidth = Math.Max(120, playerBarWidth + widthChange);
            int newEnemyWidth = Math.Max(120, enemyBarWidth + widthChange);
            int newEnemyLeft = enemyBarLeft + widthChange;

            pnlPlayerHP.SetBounds(playerBarLeft, pnlPlayerHP.Top, newPlayerWidth, pnlPlayerHP.Height);
            pnlPlayerMP.SetBounds(playerBarLeft, pnlPlayerMP.Top, newPlayerWidth, pnlPlayerMP.Height);
            pnlEnemyHP.SetBounds(newEnemyLeft, pnlEnemyHP.Top, newEnemyWidth, pnlEnemyHP.Height);
            pnlPlayerXP.SetBounds(pnlEncounterScene.Left, pnlEncounterScene.Bottom + 18,
                pnlEncounterScene.Width, pnlPlayerXP.Height);
            UpdateBarFill(pnlPlayerHP, pnlPlayerHPFill, player.HP, player.MaxHP);
            UpdateBarFill(pnlPlayerMP, pnlPlayerMPFill, player.MP, player.MaxMP);
            UpdateBarFill(pnlEnemyHP, pnlEnemyHPFill, enemy.HP, enemy.MaxHP);
            UpdateXpBar();

            lblPlayerName.Left = playerBarLeft + (newPlayerWidth - lblPlayerName.Width) / 2 + playerNameOffset;
            lblEnemyName.Left = newEnemyLeft + (newEnemyWidth - lblEnemyName.Width) / 2 + enemyNameOffset;
            lblPlayerHP.Left = playerBarLeft + playerHpOffset;
            lblPlayerMP.Left = playerBarLeft + playerMpOffset;
            lblEnemyHP.Left = newEnemyLeft + enemyHpOffset;
            AlignIconsWithNames();
        }

        private void AlignIconsWithNames()
        {
            // Đặt tâm icon ngay dưới tâm tên nhân vật/quái.
            picBattlePlayer.Left = lblPlayerName.Left + lblPlayerName.Width / 2
                - pnlEncounterScene.Left - picBattlePlayer.Width / 2;
            picBattleEnemy.Left = lblEnemyName.Left + lblEnemyName.Width / 2
                - pnlEncounterScene.Left - picBattleEnemy.Width / 2;
        }

        private void UpdateBarFill(Panel bar, Panel fill, int current, int maximum)
        {
            // Chiều rộng phần màu thay đổi theo tỉ lệ HP hoặc MP hiện tại.
            double ratio = maximum <= 0 ? 0 : (double)current / maximum;
            ratio = Math.Max(0, Math.Min(1, ratio));
            int fillWidth = (int)(bar.ClientSize.Width * ratio);
            fill.SetBounds(0, 0, fillWidth, bar.ClientSize.Height);
        }

        private void LoadIcon(PictureBox pictureBox, string fileName)
        {
            // Các hình được liên kết từ TaiLieu và chép cạnh chương trình khi build.
            string imagePath = Path.Combine(AppContext.BaseDirectory, "Assets", fileName);
            if (!File.Exists(imagePath))
                return;

            using Image source = Image.FromFile(imagePath);
            pictureBox.Image?.Dispose();
            pictureBox.Image = new Bitmap(source);
        }

        private void LoadArrow()
        {
            string imagePath = Path.Combine(AppContext.BaseDirectory, "Assets", "Arrow.png");
            if (!File.Exists(imagePath))
                return;

            using Image source = Image.FromFile(imagePath);
            arrowImage?.Dispose();
            arrowImage = new Bitmap(source);
        }

        private Image MakeDamageTint(Image source)
        {
            // Phủ tông đỏ trong suốt để sprite vẫn nhìn thấy chi tiết khi trúng đòn.
            Bitmap tinted = new Bitmap(source.Width, source.Height);
            using Graphics graphics = Graphics.FromImage(tinted);
            ColorMatrix matrix = new ColorMatrix(new float[][]
            {
                new float[] { 0.65f, 0, 0, 0, 0 },
                new float[] { 0, 0.12f, 0, 0, 0 },
                new float[] { 0, 0, 0.12f, 0, 0 },
                new float[] { 0, 0, 0, 1, 0 },
                new float[] { 0.35f, 0, 0, 0, 1 }
            });
            using ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(matrix);
            graphics.DrawImage(source, new Rectangle(0, 0, tinted.Width, tinted.Height),
                0, 0, source.Width, source.Height, GraphicsUnit.Pixel, attributes);
            return tinted;
        }

        private void AddLog(string message)
        {
            lstBattleLog.Items.Add(message);
            // Cuộn nhật ký xuống dòng mới nhất sau mỗi hành động.
            lstBattleLog.TopIndex = lstBattleLog.Items.Count - 1;
        }
        private bool IsSkeletonEnemy()
        {
            return !string.IsNullOrEmpty(enemy.Name) &&
                enemy.Name.Contains("Skeleton", StringComparison.OrdinalIgnoreCase);
        }
        private string GetEnemySpriteFileName()
        {
            string fileName = enemy.Name?.Trim().ToLowerInvariant() switch
            {
                "slime" => "Slime.png",
                "skeleton" => "Skeleton_Left_Handed.png",
                "zombie" => "Zombie_Adult.png",

                "demon" => "Demon.png",
                "yeti" => "Yeti.png",
                "boss" => "Boss.png",

                _ => "Zombie_Adult.png"
            };

            // Hình thay thế cho các loại quái chưa có sprite riêng
            return fileName;
        }

        public void LoadBattle()
        {
            lblPlayerName.Text = player.Name;
            lblEnemyName.Text = enemy.Name;

            UpdateBarFill(pnlPlayerHP, pnlPlayerHPFill, player.HP, player.MaxHP);
            UpdateBarFill(pnlPlayerMP, pnlPlayerMPFill, player.MP, player.MaxMP);
            UpdateBarFill(pnlEnemyHP, pnlEnemyHPFill, enemy.HP, enemy.MaxHP);
            LoadCatBattleSprite();
            LoadIcon(
                picBattleEnemy,
                GetEnemySpriteFileName()
            );
            LoadArrow();

            UpdateStatsText();
            UpdateXpBar();
            AlignIconsWithNames();

            AddLog($"Trận chiến bắt đầu: {player.Name} đối đầu với {enemy.Name}.");
            AddLog($"HP người chơi: {player.HP}/{player.MaxHP}; MP: {player.MP}/{player.MaxMP}.");
            AddLog($"HP quái vật: {enemy.HP}/{enemy.MaxHP}.");
        }
        private void UpdateStatsText()
        {
            lblPlayerHP.Text = $"Máu: {player.HP} / {player.MaxHP}";
            lblPlayerMP.Text = $"Năng lượng: {player.MP} / {player.MaxMP}";
            lblEnemyHP.Text = $"Máu: {enemy.HP} / {enemy.MaxHP}";
        }

        private void UpdateXpBar()
        {
            int maxExperience = Math.Max(1, player.ExperienceToNextLevel);
            lblPlayerXP.Text = $"Kinh nghiệm: {player.Experience} / {maxExperience} (Cấp {player.Level})";
            UpdateBarFill(pnlPlayerXP, pnlPlayerXPFill, player.Experience, maxExperience);
        }

        private void ShowFloatingText(string message, PictureBox character, Color color)
        {
            // Chữ bay lên từ phía trên nhân vật rồi tự biến mất.
            floatingTexts.Add(new FloatingText
            {
                Message = message,
                Position = new PointF(character.Left + character.Width / 2, character.Top - 18),
                TextColor = color,
                TicksLeft = 40
            });
            floatingTextTimer.Start();
        }

        private void FloatingTextTimer_Tick(object? sender, EventArgs e)
        {
            for (int i = floatingTexts.Count - 1; i >= 0; i--)
            {
                FloatingText floatingText = floatingTexts[i];
                floatingText.Position = new PointF(floatingText.Position.X, floatingText.Position.Y - 0.35f);
                floatingText.TicksLeft--;
                if (floatingText.TicksLeft <= 0)
                    floatingTexts.RemoveAt(i);
            }

            pnlEncounterScene.Invalidate();
            if (floatingTexts.Count == 0) 
                floatingTextTimer.Stop();
        }

        private sealed class FloatingText
        {
            public string Message { get; set; } = "";
            public PointF Position { get; set; }
            public Color TextColor { get; set; }
            public int TicksLeft { get; set; }
        }
        private void lblEnemyName_Click(object sender, EventArgs e)
        {

        }

        private void btnAttack_Click(object sender, EventArgs e)
        {
            int playerDamage = battleService.PlayerAttack(player, enemy);

            AddLog($"Lượt {currentTurn}: {player.Name} gây {playerDamage} sát thương cho {enemy.Name}.");
            UpdateBattleUI();
            AnimateStrike(true, playerDamage, () =>
            {
                if (battleService.IsEnemyDefeated(enemy))
                    EnemyDefeated();
                else
                    EnemyTurn();
            });
        }

        private void AnimateStrike(bool playerStrikes, int damage, Action finished)
        {
            movingCharacter = playerStrikes ? picBattlePlayer : picBattleEnemy;
            damagedCharacter = playerStrikes ? picBattleEnemy : picBattlePlayer;
            animationStartX = movingCharacter.Left;
            animationStartY = movingCharacter.Top;
            animationDirection = playerStrikes ? 1 : -1;
            // Skeleton đứng yên và bắn tên; các đòn cận chiến lao gần sát mục tiêu.
            isArrowAttack = !playerStrikes && IsSkeletonEnemy() && arrowImage != null;
            strikeDistance = animationDirection > 0
                ? damagedCharacter.Left - movingCharacter.Right - 8
                : movingCharacter.Left - damagedCharacter.Right - 8;
            strikeDistance = Math.Max(0, strikeDistance);
            animationStep = 0;
            damageToShow = damage;
            animationFinished = finished;
            targetWasDefeated = playerStrikes
                ? battleService.IsEnemyDefeated(enemy)
                : battleService.IsPlayerDefeated(player);
            damagedOriginalImage = damagedCharacter.Image;
            tintedImage?.Dispose();
            tintedImage = damagedOriginalImage == null ? null : MakeDamageTint(damagedOriginalImage);
            arrowStart = new Point(movingCharacter.Left + movingCharacter.Width / 2,
                movingCharacter.Top + movingCharacter.Height / 2);
            arrowEnd = new Point(damagedCharacter.Left + damagedCharacter.Width / 2,
                damagedCharacter.Top + damagedCharacter.Height / 2);
            arrowPosition = arrowStart;
            fallenCharacter = null;
            fallenImage = null;
            fallAngle = 0;
            deathParticles.Clear();
            btnAttack.Enabled = false;
            btnSkill.Enabled = false;
            btnItem.Enabled = false;
            animationTimer.Start();
        }

        private void AnimationTimer_Tick(object? sender, EventArgs e)
        {
            if (movingCharacter == null || damagedCharacter == null)
                return;

            animationStep++;
            if (isArrowAttack && animationStep <= 8)
            {
                // Bay mũi tên từ phía Skeleton đến chỗ người chơi.
                arrowPosition = new Point(
                    arrowStart.X + (arrowEnd.X - arrowStart.X) * animationStep / 8,
                    arrowStart.Y + (arrowEnd.Y - arrowStart.Y) * animationStep / 8);
            }
            else if (!isArrowAttack && animationStep <= 8)
            {
                // Lao đến sát bên đối thủ thay vì chỉ dịch chuyển vài pixel.
                movingCharacter.Left = animationStartX + animationDirection * strikeDistance * animationStep / 8;
            }
            else if (!isArrowAttack && animationStep <= 16)
            {
                int returnStep = animationStep - 8;
                movingCharacter.Left = animationStartX + animationDirection * strikeDistance
                    * (8 - returnStep) / 8;
            }
            else if (targetWasDefeated && animationStep <= 22)
            {
                // Có một nhịp ngắn sau khi hết máu trước lúc nhân vật bắt đầu ngã.
                damagedCharacter.Image = damagedOriginalImage;
            }
            else if (targetWasDefeated && animationStep <= 30)
            {
                if (animationStep == 23)
                {
                    damagedCharacter.Visible = false;
                    fallenCharacter = damagedCharacter;
                    fallenImage = damagedOriginalImage;
                    fallenPivot = new Point(damagedCharacter.Left + damagedCharacter.Width / 2,
                        damagedCharacter.Bottom);
                    // Khói bốc lên chếch về phía thân quái sau khi ngã.
                    smokeOrigin = new Point(fallenPivot.X + DeathSmokeOffsetX,
                        fallenPivot.Y + DeathSmokeOffsetY);
                }
                fallAngle = (animationStep - 22) * 11.25f;
            }
            else if (targetWasDefeated)
            {
                // Khói trắng xuất hiện sau khi icon đã ngã xong.
                if (animationStep % 2 == 0)
                    deathParticles.Add(new Point(smokeOrigin.X + particleRandom.Next(-12, 13),
                        smokeOrigin.Y - particleRandom.Next(0, 12)));

                for (int i = 0; i < deathParticles.Count; i++)
                {
                    Point particle = deathParticles[i];
                    particle.Y -= 2;
                    particle.X += particleRandom.Next(-1, 2);
                    deathParticles[i] = particle;
                }
            }

            pnlEncounterScene.Invalidate();
            if (animationStep == 8 && tintedImage != null)
            {
                // Hiện hiệu ứng đỏ ngay đúng nhịp đòn đánh chạm mục tiêu.
                damagedCharacter.Image = tintedImage;
                ShowFloatingText($"-{damageToShow}", damagedCharacter, Color.OrangeRed);
            }
            else if (animationStep == 12 && tintedImage != null)
            {
                damagedCharacter.Image = damagedOriginalImage;
            }

            if (animationStep >= (targetWasDefeated ? 55 : 16))
            {
                animationTimer.Stop();
                movingCharacter.Left = animationStartX;
                movingCharacter.Top = animationStartY;
                if (!targetWasDefeated)
                    damagedCharacter.Image = damagedOriginalImage;
                damagedCharacter = null;
                movingCharacter = null;
                tintedImage?.Dispose();
                tintedImage = null;
                deathParticles.Clear();
                pnlEncounterScene.Invalidate();
                btnAttack.Enabled = true;
                btnSkill.Enabled = true;
                btnItem.Enabled = true;
                Action? finished = animationFinished;
                animationFinished = null;
                finished?.Invoke();
            }
        }

        private void pnlEncounterScene_Paint(object? sender, PaintEventArgs e)
        {
            if (isArrowAttack && animationStep <= 8 && arrowImage != null)
            {
                // Xoay mũi tên về bên trái khi Skeleton bắn sang phía người chơi.
                GraphicsState state = e.Graphics.Save();
                e.Graphics.TranslateTransform(arrowPosition.X, arrowPosition.Y);
                if (arrowEnd.X < arrowStart.X)
                    e.Graphics.RotateTransform(180);
                e.Graphics.DrawImage(arrowImage, -16, -6, 32, 12);
                e.Graphics.Restore(state);
            }

            // Vẽ sprite đã ngã quanh chân để tạo cảm giác đổ người.
            if (fallenImage != null && fallenCharacter != null)
            {
                GraphicsState state = e.Graphics.Save();
                e.Graphics.TranslateTransform(fallenPivot.X, fallenPivot.Y);
                e.Graphics.RotateTransform(fallAngle);
                e.Graphics.DrawImage(fallenImage, -fallenCharacter.Width / 2,
                    -fallenCharacter.Height, fallenCharacter.Width, fallenCharacter.Height);
                e.Graphics.Restore(state);
            }

            // Các chấm trắng bay lên như khói sau khi nhân vật đã ngã.
            using SolidBrush smokeBrush = new SolidBrush(Color.FromArgb(180, Color.WhiteSmoke));
            foreach (Point particle in deathParticles)
                e.Graphics.FillEllipse(smokeBrush, particle.X, particle.Y, 7, 7);

            using Font floatingFont = new Font("Segoe UI", 10F, FontStyle.Bold);
            foreach (FloatingText floatingText in floatingTexts)
            {
                int alpha = Math.Min(255, floatingText.TicksLeft * 9);
                Color textColor = Color.FromArgb(alpha, floatingText.TextColor);
                SizeF textSize = e.Graphics.MeasureString(floatingText.Message, floatingFont);
                float x = floatingText.Position.X - textSize.Width / 2;
                float y = floatingText.Position.Y;
                using SolidBrush shadow = new SolidBrush(Color.FromArgb(alpha, Color.Black));
                using SolidBrush textBrush = new SolidBrush(textColor);
                e.Graphics.DrawString(floatingText.Message, floatingFont, shadow, x + 1, y + 1);
                e.Graphics.DrawString(floatingText.Message, floatingFont, textBrush, x, y);
            }
        }


        private void UpdateBattleUI()
        {
            UpdateBarFill(pnlPlayerHP, pnlPlayerHPFill, player.HP, player.MaxHP);
            UpdateBarFill(pnlPlayerMP, pnlPlayerMPFill, player.MP, player.MaxMP);
            UpdateBarFill(pnlEnemyHP, pnlEnemyHPFill, enemy.HP, enemy.MaxHP);

            UpdateStatsText();
            UpdateXpBar();
        }
        private void EnemyDefeated()
        {
            bool isFinalBoss = enemy.IsBoss && enemy.MapId == "DungeonFloor2";

            // Only reward once, after the enemy is successfully removed from the shared state.
            if (gameState.Enemies == null || !gameState.Enemies.Remove(enemy))
                return;

            AddLog("----------------------------------------");
            AddLog($"Bạn đã đánh bại {enemy.Name}!");

            int goldReward = battleService.GetGoldReward(enemy);
            player.Gold += goldReward;
            AddLog($"Bạn nhận được {goldReward} Gold. Tổng Gold: {player.Gold}.");

            int expReward = battleService.GetExperienceReward(enemy);
            List<string> rewardMessages = battleService.GainExperience(player, expReward);
            foreach (string message in rewardMessages)
                AddLog(message);

            ShowFloatingText($"+{expReward} EXP", picBattlePlayer, Color.Gold);
            UpdateBattleUI();
            gameState.CurrentEnemy = null;

            if (isFinalBoss)
            {
                gameState.IsGameWon = true;
                AddLog("Bạn đã chinh phục Dungeon!");
                AddLog("Nhấn nút bên dưới để xem màn hình chiến thắng.");
                FinishBattle(BattleOutcome.GameVictory);
                return;
            }

            AddLog("Trận chiến kết thúc.");
            AddLog("Nhấn nút bên dưới để quay lại bản đồ.");
            FinishBattle(BattleOutcome.NormalVictory);
        }
        private void PlayerDefeated()
        {
            AddLog("----------------------------------------");
            AddLog($"{player.Name} đã bị đánh bại.");
            AddLog("Nhấn nút bên dưới để quay về menu chính.");
            FinishBattle(BattleOutcome.PlayerLost);
        }

        private void FinishBattle(BattleOutcome outcome)
        {
            pendingOutcome = outcome;
            animationTimer.Stop();
            grpSkillSelection.Visible = false;
            grpItemSelection.Visible = false;

            btnAttack.Visible = false;
            btnSkill.Visible = false;
            btnItem.Visible = false;

            // Reuse the skill-button location for the final continue action.
            btnContinue.Bounds = btnSkill.Bounds;
            btnContinue.Text = outcome switch
            {
                BattleOutcome.NormalVictory => "Quay lại bản đồ",
                BattleOutcome.GameVictory => "Xem chiến thắng",
                BattleOutcome.PlayerLost => "Về menu chính",
                _ => "Tiếp tục"
            };
            btnContinue.Visible = true;
            btnContinue.BringToFront();
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            BattleOutcome outcome = pendingOutcome;
            pendingOutcome = BattleOutcome.None;
            btnContinue.Visible = false;

            switch (outcome)
            {
                case BattleOutcome.NormalVictory:
                    BattleWon?.Invoke(this, EventArgs.Empty);
                    break;
                case BattleOutcome.GameVictory:
                    GameWon?.Invoke(this, EventArgs.Empty);
                    break;
                case BattleOutcome.PlayerLost:
                    BattleLost?.Invoke(this, EventArgs.Empty);
                    break;
            }
        }

        private void btnSkill_Click(object sender, EventArgs e)
        {
            if (player.Skills == null || player.Skills.Count == 0)
            {
                AddLog("Bạn chưa có kỹ năng để sử dụng.");
                return;
            }

            LoadSkillList();
            grpItemSelection.Visible = false;
            grpSkillSelection.Visible = true;
            grpSkillSelection.BringToFront();
            SetActionButtonsEnabled(false);
        }

        private void LoadSkillList()
        {
            lstSkills.DataSource = null;
            lstSkills.DisplayMember = nameof(Skill.Name);
            lstSkills.DataSource = player.Skills;
            btnUseSelectedSkill.Enabled = player.Skills != null && player.Skills.Count > 0;

            if (lstSkills.Items.Count > 0)
                lstSkills.SelectedIndex = 0;
            else
                lblSkillInfo.Text = "Bạn chưa có kỹ năng nào.";
        }

        private void lstSkills_SelectedIndexChanged(object sender, EventArgs e)
        {
            Skill skill = lstSkills.SelectedItem as Skill;
            if (skill == null)
            {
                lblSkillInfo.Text = "Hãy chọn một kỹ năng.";
                btnUseSelectedSkill.Enabled = false;
                return;
            }

            lblSkillInfo.Text =
                $"Kỹ năng: {skill.Name}{Environment.NewLine}" +
                $"Sát thương: {skill.MinDamage} - {skill.MaxDamage}{Environment.NewLine}" +
                $"Mana tiêu hao: {skill.ManaCost}{Environment.NewLine}" +
                $"MP hiện tại: {player.MP}/{player.MaxMP}";
            btnUseSelectedSkill.Enabled = true;
        }

        private void btnUseSelectedSkill_Click(object sender, EventArgs e)
        {
            Skill skill = lstSkills.SelectedItem as Skill;
            if (skill == null)
            {
                AddLog("Vui lòng chọn một kỹ năng.");
                return;
            }

            if (!battleService.CanUseSkill(player, skill))
            {
                AddLog($"Không đủ MP để sử dụng {skill.Name}.");
                return;
            }

            int damage = battleService.UseSkill(player, enemy, skill);
            grpSkillSelection.Visible = false;
            AddLog($"Lượt {currentTurn}: {player.Name} dùng {skill.Name}, gây {damage} sát thương cho {enemy.Name}.");
            ShowFloatingText($"-{skill.ManaCost} MP", picBattlePlayer, Color.DeepSkyBlue);
            UpdateBattleUI();

            AnimateStrike(true, damage, () =>
            {
                if (battleService.IsEnemyDefeated(enemy))
                    EnemyDefeated();
                else
                    EnemyTurn();
            });
        }

        private void btnCancelSkill_Click(object sender, EventArgs e)
        {
            grpSkillSelection.Visible = false;
            SetActionButtonsEnabled(true);
        }

        private void SetActionButtonsEnabled(bool enabled)
        {
            btnAttack.Enabled = enabled;
            btnSkill.Enabled = enabled;
            btnItem.Enabled = enabled;
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            LoadBattleItemList();
            if (lstBattleItems.Items.Count == 0)
            {
                AddLog("Bạn không có vật phẩm sử dụng được trong chiến đấu.");
                return;
            }

            grpSkillSelection.Visible = false;
            grpItemSelection.Visible = true;
            grpItemSelection.BringToFront();
            SetActionButtonsEnabled(false);
        }

        private void LoadBattleItemList()
        {
            List<Item> usableItems = player.Inventory?.Items?.Where(item =>
                item.Quantity > 0 &&
                (item.HealAmount > 0 || item.ManaRestore > 0 || item.Damage > 0))
                .ToList() ?? new List<Item>();

            lstBattleItems.DataSource = null;
            lstBattleItems.DisplayMember = nameof(Item.Name);
            lstBattleItems.DataSource = usableItems;
            btnUseSelectedItem.Enabled = usableItems.Count > 0;

            if (usableItems.Count > 0)
                lstBattleItems.SelectedIndex = 0;
            else
                lblBattleItemInfo.Text = "Không có vật phẩm sử dụng được.";
        }

        private void lstBattleItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            Item item = lstBattleItems.SelectedItem as Item;
            if (item == null)
            {
                lblBattleItemInfo.Text = "Hãy chọn một vật phẩm.";
                btnUseSelectedItem.Enabled = false;
                return;
            }

            lblBattleItemInfo.Text =
                $"{item.Name}{Environment.NewLine}" +
                $"{item.Description}{Environment.NewLine}" +
                $"Số lượng: {item.Quantity}{Environment.NewLine}" +
                $"Hồi HP: {item.HealAmount}{Environment.NewLine}" +
                $"Hồi MP: {item.ManaRestore}{Environment.NewLine}" +
                $"Sát thương: {item.Damage}";
            btnUseSelectedItem.Enabled = true;
        }

        private void btnUseSelectedItem_Click(object sender, EventArgs e)
        {
            Item item = lstBattleItems.SelectedItem as Item;
            if (item == null)
            {
                AddLog("Vui lòng chọn một vật phẩm.");
                return;
            }

            if (item.Damage > 0)
            {
                int damage = itemService.UseBomb(player, enemy, item);
                if (damage <= 0)
                {
                    AddLog($"Không thể sử dụng {item.Name}.");
                    LoadBattleItemList();
                    return;
                }

                grpItemSelection.Visible = false;
                AddLog($"Lượt {currentTurn}: {player.Name} sử dụng {item.Name}, gây {damage} sát thương cho {enemy.Name}.");
                UpdateBattleUI();
                AnimateStrike(true, damage, () =>
                {
                    if (battleService.IsEnemyDefeated(enemy))
                        EnemyDefeated();
                    else
                        EnemyTurn();
                });
                return;
            }

            int oldHp = player.HP;
            int oldMp = player.MP;
            bool used = itemService.UseItem(player, item);
            if (!used)
            {
                AddLog($"Không thể sử dụng {item.Name}; HP và MP có thể đã đầy.");
                return;
            }

            grpItemSelection.Visible = false;
            int healedHp = player.HP - oldHp;
            int restoredMp = player.MP - oldMp;
            AddLog($"Lượt {currentTurn}: {player.Name} sử dụng {item.Name}. Hồi {healedHp} HP, {restoredMp} MP.");
            if (healedHp > 0)
                ShowFloatingText($"+{healedHp} HP", picBattlePlayer, Color.LightGreen);
            if (restoredMp > 0)
                ShowFloatingText($"+{restoredMp} MP", picBattlePlayer, Color.DeepSkyBlue);
            UpdateBattleUI();
            EnemyTurn();
        }

        private void btnCancelItem_Click(object sender, EventArgs e)
        {
            grpItemSelection.Visible = false;
            SetActionButtonsEnabled(true);
        }
        private void EnemyTurn()
        {
            int enemyDamage = battleService.EnemyAttack(
                player,
                enemy
            );

            AddLog($"Lượt {currentTurn}: {enemy.Name} gây {enemyDamage} sát thương cho {player.Name}.");

            UpdateBattleUI();
            AnimateStrike(false, enemyDamage, () =>
            {
                if (battleService.IsPlayerDefeated(player))
                {
                    PlayerDefeated();
                    return;
                }

                // Quái đã đánh xong, bắt đầu lượt kế tiếp.
                currentTurn++;
            });
        }
        private void LoadCatBattleSprite()
        {
            // Lấy spritesheet 3 frame từ Resources
            Bitmap spriteSheet =
                global::RPG.Properties.Resources.Cat_idle_1;

            // Chia ảnh thành 3 frame nằm ngang
            int frameCount = 3;
            int frameWidth = spriteSheet.Width / frameCount;
            int frameHeight = spriteSheet.Height;

            // Lấy frame đầu tiên (32 x 32 pixel)
            Rectangle firstFrame = new Rectangle(
                0,
                0,
                frameWidth,
                frameHeight
            );

            Bitmap catFrame = spriteSheet.Clone(
                firstFrame,
                spriteSheet.PixelFormat
            );

            // Giải phóng hình cũ nếu có
            if (picBattlePlayer.Image != null)
            {
                picBattlePlayer.Image.Dispose();
            }

            // Hiển thị mèo trắng trong Battle
            picBattlePlayer.Image = catFrame;
            picBattlePlayer.SizeMode = PictureBoxSizeMode.Zoom;
        }

    }
}

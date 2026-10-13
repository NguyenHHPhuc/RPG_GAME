using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
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
        private bool isVictory;
        public event EventHandler BattleWon;
        public event EventHandler BattleLost;
        public BattleControl(GameState gameState)
        {
            InitializeComponent();
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
        public void LoadBattle()
        {
            lblPlayerName.Text = player.Name;
            lblEnemyName.Text = enemy.Name;

            UpdateBarFill(pnlPlayerHP, pnlPlayerHPFill, player.HP, player.MaxHP);
            UpdateBarFill(pnlPlayerMP, pnlPlayerMPFill, player.MP, player.MaxMP);
            UpdateBarFill(pnlEnemyHP, pnlEnemyHPFill, enemy.HP, enemy.MaxHP);
            LoadIcon(picBattlePlayer, "Steve_Dwarf.png");
            LoadIcon(picBattleEnemy, enemy.MobType == "Skeleton"
                ? "Skeleton_Left_Handed.png"
                : "Zombie_Adult.png");
            LoadArrow();

            UpdateStatsText();
            AlignIconsWithNames();
        }
        private void UpdateStatsText()
        {
            lblPlayerHP.Text = $"Máu: {player.HP} / {player.MaxHP}";
            lblPlayerMP.Text = $"Năng lượng: {player.MP} / {player.MaxMP}";
            lblEnemyHP.Text = $"Máu: {enemy.HP} / {enemy.MaxHP}";
        }

        private void UpdateXpBar()
        {
            int maxExperience = Math.Max(1, player.MaxExperience);
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
            isArrowAttack = !playerStrikes && enemy.MobType == "Skeleton" && arrowImage != null;
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
            int xpGained = enemy.MobType == "Skeleton" ? 25 : 15;
            AddLog($"{enemy.Name} đã bị đánh bại.");
            AddLog($"Nhận {xpGained} XP.");
            player.Experience += xpGained;
            player.MaxExperience = Math.Max(1, player.MaxExperience);
            while (player.Experience >= player.MaxExperience)
            {
                player.Experience -= player.MaxExperience;
                player.Level++;
                player.MaxExperience += 50;
                AddLog($"Lên cấp {player.Level}!");
            }
            UpdateXpBar();
            ShowFloatingText($"+{xpGained} XP", picBattlePlayer, Color.Gold);
            gameState.Enemies.Remove(enemy);
            gameState.CurrentEnemy = null;
            FinishBattle(true);
        }
        private void PlayerDefeated()
        {
            AddLog($"{player.Name} đã bị đánh bại.");
            FinishBattle(false);
        }

        private void FinishBattle(bool won)
        {
            // Tạm giữ màn hình để người chơi đọc được nhật ký của lượt cuối.
            isVictory = won;
            animationTimer.Stop();
            btnAttack.Visible = false;
            btnSkill.Visible = false;
            btnItem.Visible = false;
            // Dùng lại vị trí của nút SKILL sau khi các nút hành động đã được ẩn.
            btnContinue.Bounds = btnSkill.Bounds;
            btnContinue.Text = won ? "Tiếp tục" : "Về menu";
            btnContinue.Visible = true;
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            // Chuyển màn hình sau khi người chơi đã xem xong kết quả trận đấu.
            if (isVictory)
                BattleWon?.Invoke(this, EventArgs.Empty);
            else
                BattleLost?.Invoke(this, EventArgs.Empty);
        }

        private void btnSkill_Click(object sender, EventArgs e)
        {
            if (player.Skills.Count == 0)
            {
                AddLog("Bạn chưa có kỹ năng để sử dụng.");
                return;
            }
            Skill skill = player.Skills[0]; // Sử dụng kỹ năng đầu tiên trong danh sách
            if (!battleService.CanUseSkill(player, skill))
            {
                AddLog("Không đủ năng lượng để sử dụng kỹ năng.");
                return;
            }
            int damage = battleService.UseSkill(
                            player,
                            enemy,
                            skill
                        );

            AddLog($"Lượt {currentTurn}: dùng {skill.Name}, gây {damage} sát thương cho {enemy.Name}.");
            int manaUsed = skill.ManaCost;
            ShowFloatingText($"-{manaUsed} NL", picBattlePlayer, Color.DeepSkyBlue);

            UpdateBattleUI();
            AnimateStrike(true, damage, () =>
            {
                if (battleService.IsEnemyDefeated(enemy))
                    EnemyDefeated();
                else
                    EnemyTurn();
            });
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            Item potion = player.Inventory.GetItem("ITEM_HEALTH_POTION");

            if (potion == null)
            {
                AddLog("Bạn không còn bình hồi máu.");
                return;
            }

            if (player.HP >= player.MaxHP)
            {
                AddLog("HP đang đầy, không cần dùng bình hồi máu.");
                return;
            }

            int oldHP = player.HP;
            bool used = itemService.UseItem(player, potion);

            if (!used)
            {
                AddLog("Không thể sử dụng vật phẩm này.");
                return;
            }

            AddLog($"Lượt {currentTurn}: dùng {potion.Name}, hồi {player.HP - oldHP} HP.");

            UpdateBattleUI();

            EnemyTurn();
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
    }
}

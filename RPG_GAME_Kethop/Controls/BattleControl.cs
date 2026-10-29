using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using RPG.DoiTuong;
using RPG.Services;
namespace RPG.Controls
{
    // Luật chơi và giao diện trận đấu. Phần hoạt ảnh nằm ở BattleControl.Animation.cs.
    public partial class BattleControl : UserControl
    {
        private readonly GameState gameState;
        private readonly BattleService battleService = new BattleService();
        private readonly ItemService itemService = new ItemService();
        private readonly Player player;
        private readonly Enemy enemy;

        // Vị trí gốc trong Designer, dùng khi đổi cỡ cửa sổ
        private int designWidth;
        private int playerBarLeft;
        private int enemyBarLeft;
        private int playerBarWidth;
        private int enemyBarWidth;
        private int playerHpOffset;
        private int playerMpOffset;
        private int enemyHpOffset;

        // Skeleton bắn tên thay vì lao vào cận chiến
        private bool enemyShootsArrows;

        private int currentTurn = 1;

        // Hàng nút kỹ năng, hiện thay chỗ 3 nút hành động
        private readonly FlowLayoutPanel pnlSkillBar = new FlowLayoutPanel
        {
            AutoSize = true,
            WrapContents = false,
            Visible = false
        };
        private readonly ToolTip skillToolTip = new ToolTip();

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

            lstBattleItems.SelectedIndexChanged += lstBattleItems_SelectedIndexChanged;
            btnUseSelectedItem.Click += btnUseSelectedItem_Click;
            btnCancelItem.Click += btnCancelItem_Click;

            grpItemSelection.Visible = false;
            Controls.Add(pnlSkillBar);

            this.gameState = gameState;
            player = gameState.Player;
            enemy = gameState.CurrentEnemy;

            animationTimer = new System.Windows.Forms.Timer { Interval = 20 };
            animationTimer.Tick += AnimationTimer_Tick;
            floatingTextTimer = new System.Windows.Forms.Timer { Interval = 20 };
            floatingTextTimer.Tick += FloatingTextTimer_Tick;

            // Form1 gọi Dispose khi chuyển màn hình
            Disposed += (_, _) => ReleaseResources();

            LoadBattle();
            SaveDesignerPositions();
            SizeChanged += (_, _) => ResizeHealthBars();
            ResizeHealthBars();
        }

        #region Khởi tạo và giao diện

        public void LoadBattle()
        {
            lblPlayerName.Text = player.Name;
            lblEnemyName.Text = enemy.Name;

            LoadCatBattleSprite();

            string enemyType = enemy.Name?.Trim().ToLowerInvariant() ?? "";
            Image? enemySprite = LoadAssetImage(GetEnemySpriteFileName(enemyType));
            if (enemySprite != null)
                ReplaceImage(picBattleEnemy, enemySprite);

            arrowImage = LoadAssetImage("Arrow.png");
            enemyShootsArrows = enemyType == "skeleton" && arrowImage != null;

            UpdateBattleUI();
            AlignIconsWithNames();

            AddLog($"Trận chiến bắt đầu: {player.Name} đối đầu với {enemy.Name}.");
            AddLog($"HP người chơi: {player.HP}/{player.MaxHP}; MP: {player.MP}/{player.MaxMP}.");
            AddLog($"HP quái vật: {enemy.HP}/{enemy.MaxHP}.");
        }

        private static string GetEnemySpriteFileName(string enemyType)
        {
            return enemyType switch
            {
                "slime" => "Slime.png",
                "skeleton" => "Skeleton_Left_Handed.png",
                "zombie" => "Zombie_Adult.png",
                "demon" => "Demon.png",
                "yeti" => "Yeti.png",
                "boss" => "Boss.png",
                _ => "Zombie_Adult.png"
            };
        }

        private static Image? LoadAssetImage(string fileName)
        {
            string imagePath = Path.Combine(AppContext.BaseDirectory, "Assets", fileName);
            if (!File.Exists(imagePath))
                return null;

            // Chép sang Bitmap mới để không khóa file ảnh
            using Image source = Image.FromFile(imagePath);
            return new Bitmap(source);
        }

        private static void ReplaceImage(PictureBox pictureBox, Image image)
        {
            pictureBox.Image?.Dispose();
            pictureBox.Image = image;
        }

        private void LoadCatBattleSprite()
        {
            // Spritesheet 3 frame nằm ngang, chỉ lấy frame đầu
            using Bitmap spriteSheet = global::RPG.Properties.Resources.Cat_idle_1;
            int frameWidth = spriteSheet.Width / 3;
            int frameHeight = spriteSheet.Height;

            Bitmap catFrame = new Bitmap(frameWidth, frameHeight);
            using (Graphics graphics = Graphics.FromImage(catFrame))
            {
                graphics.DrawImage(spriteSheet, new Rectangle(0, 0, frameWidth, frameHeight),
                    new Rectangle(0, 0, frameWidth, frameHeight), GraphicsUnit.Pixel);
            }

            ReplaceImage(picBattlePlayer, catFrame);
            picBattlePlayer.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void SaveDesignerPositions()
        {
            designWidth = ClientSize.Width;
            playerBarLeft = pnlPlayerHP.Left;
            enemyBarLeft = pnlEnemyHP.Left;
            playerBarWidth = pnlPlayerHP.Width;
            enemyBarWidth = pnlEnemyHP.Width;

            playerHpOffset = lblPlayerHP.Left - pnlPlayerHP.Left;
            playerMpOffset = lblPlayerMP.Left - pnlPlayerMP.Left;
            enemyHpOffset = lblEnemyHP.Left - pnlEnemyHP.Left;
        }

        private void ResizeHealthBars()
        {
            // Chia phần rộng thêm đều cho hai cột máu
            int widthChange = (ClientSize.Width - designWidth) / 2;
            int newPlayerWidth = Math.Max(120, playerBarWidth + widthChange);
            int newEnemyWidth = Math.Max(120, enemyBarWidth + widthChange);
            int newEnemyLeft = enemyBarLeft + widthChange;

            pnlPlayerHP.SetBounds(playerBarLeft, pnlPlayerHP.Top, newPlayerWidth, pnlPlayerHP.Height);
            pnlPlayerMP.SetBounds(playerBarLeft, pnlPlayerMP.Top, newPlayerWidth, pnlPlayerMP.Height);
            pnlEnemyHP.SetBounds(newEnemyLeft, pnlEnemyHP.Top, newEnemyWidth, pnlEnemyHP.Height);
            pnlPlayerXP.SetBounds(pnlEncounterScene.Left, pnlEncounterScene.Bottom + 18,
                pnlEncounterScene.Width, pnlPlayerXP.Height);
            UpdateBattleUI();

            lblPlayerName.Left = playerBarLeft + (newPlayerWidth - lblPlayerName.Width) / 2;
            lblEnemyName.Left = newEnemyLeft + (newEnemyWidth - lblEnemyName.Width) / 2;
            lblPlayerHP.Left = playerBarLeft + playerHpOffset;
            lblPlayerMP.Left = playerBarLeft + playerMpOffset;
            lblEnemyHP.Left = newEnemyLeft + enemyHpOffset;
            AlignIconsWithNames();
        }

        private void AlignIconsWithNames()
        {
            // Hình nằm trong pnlEncounterScene nên phải trừ Left của panel
            picBattlePlayer.Left = lblPlayerName.Left + lblPlayerName.Width / 2
                - pnlEncounterScene.Left - picBattlePlayer.Width / 2;
            picBattleEnemy.Left = lblEnemyName.Left + lblEnemyName.Width / 2
                - pnlEncounterScene.Left - picBattleEnemy.Width / 2;
        }

        private void UpdateBattleUI()
        {
            UpdateBarFill(pnlPlayerHP, pnlPlayerHPFill, player.HP, player.MaxHP);
            UpdateBarFill(pnlPlayerMP, pnlPlayerMPFill, player.MP, player.MaxMP);
            UpdateBarFill(pnlEnemyHP, pnlEnemyHPFill, enemy.HP, enemy.MaxHP);

            lblPlayerHP.Text = $"Máu: {player.HP} / {player.MaxHP}";
            lblPlayerMP.Text = $"Năng lượng: {player.MP} / {player.MaxMP}";
            lblEnemyHP.Text = $"Máu: {enemy.HP} / {enemy.MaxHP}";

            int maxExperience = Math.Max(1, player.ExperienceToNextLevel);
            lblPlayerXP.Text = $"Kinh nghiệm: {player.Experience} / {maxExperience} (Cấp {player.Level})";
            UpdateBarFill(pnlPlayerXP, pnlPlayerXPFill, player.Experience, maxExperience);
        }

        private static void UpdateBarFill(Panel bar, Panel fill, int current, int maximum)
        {
            double ratio = maximum <= 0 ? 0 : (double)current / maximum;
            ratio = Math.Clamp(ratio, 0, 1);
            fill.SetBounds(0, 0, (int)(bar.ClientSize.Width * ratio), bar.ClientSize.Height);
        }

        private void SetActionButtonsEnabled(bool enabled)
        {
            btnAttack.Enabled = enabled;
            btnSkill.Enabled = enabled;
            btnItem.Enabled = enabled;
        }

        private void SetActionButtonsVisible(bool visible)
        {
            btnAttack.Visible = visible;
            btnSkill.Visible = visible;
            btnItem.Visible = visible;
        }

        private void AddLog(string message)
        {
            lstBattleLog.Items.Add(message);
            lstBattleLog.TopIndex = lstBattleLog.Items.Count - 1;
        }

        private void ReleaseResources()
        {
            animationTimer.Stop();
            animationTimer.Dispose();
            floatingTextTimer.Stop();
            floatingTextTimer.Dispose();

            picBattlePlayer.Image?.Dispose();
            picBattleEnemy.Image?.Dispose();
            arrowImage?.Dispose();
            tintedImage?.Dispose();
            floatingFont.Dispose();
            smokeBrush.Dispose();
            skillToolTip.Dispose();
        }

        private void lblEnemyName_Click(object sender, EventArgs e)
        {
        }

        #endregion

        #region Lượt chơi

        private void btnAttack_Click(object sender, EventArgs e)
        {
            int damage = battleService.PlayerAttack(player, enemy, out bool hitTwice);
            if (hitTwice)
                AddLog($"Lượt {currentTurn}: Liên Kích! {player.Name} đánh 2 lần, tổng {damage} sát thương cho {enemy.Name}.");
            else
                AddLog($"Lượt {currentTurn}: {player.Name} gây {damage} sát thương cho {enemy.Name}.");
            StrikeEnemy(damage);
        }

        // Dùng chung cho đánh thường, kỹ năng và bom.
        // Đoạn code trong lambda chỉ chạy khi hoạt ảnh kết thúc.
        private void StrikeEnemy(int damage)
        {
            AnimateStrike(true, damage, () =>
            {
                if (enemy.IsDead)
                    EnemyDefeated();
                else
                    EnemyTurn();
            });
        }

        private void EnemyTurn()
        {
            // Chảy máu tính đầu lượt quái, có thể hạ quái luôn
            int bleed = battleService.ApplyBleed(enemy);
            if (bleed > 0)
            {
                AddLog($"{enemy.Name} chảy máu, mất {bleed} HP.");
                ShowFloatingText($"-{bleed}", picBattleEnemy, BleedColor);
                UpdateBattleUI();
                if (enemy.IsDead)
                {
                    picBattleEnemy.Visible = false;
                    EnemyDefeated();
                    return;
                }
            }

            int damage = battleService.EnemyAttack(player, enemy);
            if (damage == 0)
                AddLog($"Lượt {currentTurn}: Khiên Chắn chặn hoàn toàn đòn của {enemy.Name}!");
            else
                AddLog($"Lượt {currentTurn}: {enemy.Name} gây {damage} sát thương cho {player.Name}.");

            // Hồi sinh trước hoạt ảnh để nhân vật không bị ngã
            bool revived = battleService.TryRevive(player);
            if (revived)
                AddLog($"Bất Tử: {player.Name} được hồi sinh với {player.HP} HP!");

            AnimateStrike(false, damage, () =>
            {
                if (player.IsDead)
                {
                    PlayerDefeated();
                    return;
                }

                if (revived)
                    ShowFloatingText("Hồi sinh!", picBattlePlayer, GoldColor);

                int regen = battleService.ApplyRegeneration(player);
                if (regen > 0)
                {
                    AddLog($"Tái Sinh: {player.Name} hồi {regen} HP.");
                    ShowFloatingText($"+{regen} HP", picBattlePlayer, HealColor);
                    UpdateBattleUI();
                }

                currentTurn++;
                SetActionButtonsEnabled(true);
            });
        }

        private void EnemyDefeated()
        {
            // Chặn trao thưởng hai lần
            if (pendingOutcome != BattleOutcome.None)
                return;

            bool isFinalBoss = enemy.IsBoss && enemy.MapId == "DungeonFloor2";
            gameState.Enemies?.Remove(enemy);

            AddLog("----------------------------------------");
            AddLog($"Bạn đã đánh bại {enemy.Name}!");

            int goldReward = battleService.GetGoldReward(enemy);
            player.Gold += goldReward;
            AddLog($"Bạn nhận được {goldReward} Gold. Tổng Gold: {player.Gold}.");

            int expReward = battleService.GetExperienceReward(enemy);
            foreach (string message in battleService.GainExperience(player, expReward))
                AddLog(message);

            ShowFloatingText($"+{expReward} EXP", picBattlePlayer, GoldColor);
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
            pnlSkillBar.Visible = false;
            grpItemSelection.Visible = false;
            SetActionButtonsVisible(false);

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

        #endregion

        #region Kỹ năng

        private void btnSkill_Click(object sender, EventArgs e)
        {
            if (player.Skills.Count == 0)
            {
                AddLog("Bạn chưa có kỹ năng. Hãy mở khóa kỹ năng trong Cây kỹ năng.");
                return;
            }

            ShowSkillBar();
        }

        private void ShowSkillBar()
        {
            // Tạo lại nút mỗi lần mở vì MP thay đổi theo lượt
            while (pnlSkillBar.Controls.Count > 0)
                pnlSkillBar.Controls[0].Dispose();

            foreach (Skill skill in player.Skills)
            {
                bool enoughMana = battleService.CanUseSkill(player, skill);
                Button button = CreateSkillBarButton(enoughMana
                    ? $"{skill.Name}\n{skill.ManaCost} MP"
                    : $"{skill.Name}\nThiếu MP");
                button.Enabled = enoughMana;
                skillToolTip.SetToolTip(button, GetSkillDescription(skill));
                button.Click += (_, _) => UseSkill(skill);
                pnlSkillBar.Controls.Add(button);
            }

            Button cancelButton = CreateSkillBarButton("Hủy");
            cancelButton.Click += (_, _) => HideSkillBar();
            pnlSkillBar.Controls.Add(cancelButton);

            SetActionButtonsVisible(false);
            pnlSkillBar.Anchor = AnchorStyles.Bottom;
            pnlSkillBar.Location = new Point(
                (ClientSize.Width - pnlSkillBar.PreferredSize.Width) / 2, btnAttack.Top);
            pnlSkillBar.Visible = true;
            pnlSkillBar.BringToFront();
        }

        private Button CreateSkillBarButton(string text)
        {
            return new Button
            {
                Text = text,
                Size = btnAttack.Size,
                Margin = new Padding(6, 0, 6, 0)
            };
        }

        private void HideSkillBar()
        {
            pnlSkillBar.Visible = false;
            SetActionButtonsVisible(true);
            SetActionButtonsEnabled(true);
        }

        private static string GetSkillDescription(Skill skill)
        {
            return skill.Type switch
            {
                SkillType.Heal => $"Hồi {skill.HealPercent}% Máu tối đa. Tốn {skill.ManaCost} MP.",
                SkillType.Shield => $"Chặn hoàn toàn đòn kế tiếp của quái. Tốn {skill.ManaCost} MP.",
                _ => $"Gây {skill.MinDamage}-{skill.MaxDamage} sát thương" +
                    (skill.BleedTurns > 0
                        ? $", chảy máu {skill.BleedDamage} HP/lượt trong {skill.BleedTurns} lượt"
                        : "") +
                    $". Tốn {skill.ManaCost} MP."
            };
        }

        private void UseSkill(Skill skill)
        {
            if (!battleService.CanUseSkill(player, skill))
            {
                AddLog($"Không đủ MP để sử dụng {skill.Name}.");
                return;
            }

            pnlSkillBar.Visible = false;
            SetActionButtonsVisible(true);
            SetActionButtonsEnabled(false);

            switch (skill.Type)
            {
                case SkillType.Heal:
                    int healed = battleService.UseHealSkill(player, skill);
                    AddLog($"Lượt {currentTurn}: {player.Name} dùng {skill.Name}, hồi {healed} HP.");
                    ShowFloatingText($"+{healed} HP", picBattlePlayer, HealColor);
                    UpdateBattleUI();
                    EnemyTurn();
                    break;

                case SkillType.Shield:
                    battleService.UseShieldSkill(player, skill);
                    AddLog($"Lượt {currentTurn}: {player.Name} dùng {skill.Name}, sẽ chặn đòn kế tiếp.");
                    ShowFloatingText($"-{skill.ManaCost} MP", picBattlePlayer, ManaColor);
                    UpdateBattleUI();
                    EnemyTurn();
                    break;

                default:
                    int damage = battleService.UseSkill(player, enemy, skill);
                    AddLog($"Lượt {currentTurn}: {player.Name} dùng {skill.Name}, gây {damage} sát thương cho {enemy.Name}.");
                    if (skill.BleedTurns > 0)
                        AddLog($"{enemy.Name} bị chảy máu trong {skill.BleedTurns} lượt.");
                    ShowFloatingText($"-{skill.ManaCost} MP", picBattlePlayer, ManaColor);
                    UpdateBattleUI();
                    StrikeEnemy(damage);
                    break;
            }
        }

        #endregion

        #region Vật phẩm

        private void btnItem_Click(object sender, EventArgs e)
        {
            LoadBattleItemList();
            if (lstBattleItems.Items.Count == 0)
            {
                AddLog("Bạn không có vật phẩm sử dụng được trong chiến đấu.");
                return;
            }

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
            if (lstBattleItems.SelectedItem is not Item item)
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
            if (lstBattleItems.SelectedItem is not Item item)
            {
                AddLog("Vui lòng chọn một vật phẩm.");
                return;
            }

            if (item.Damage > 0)
                UseBombItem(item);
            else
                UseRecoveryItem(item);
        }

        private void UseBombItem(Item item)
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
            StrikeEnemy(damage);
        }

        private void UseRecoveryItem(Item item)
        {
            if (!itemService.UseItem(player, item, out int healedHp, out int restoredMp))
            {
                AddLog($"Không thể sử dụng {item.Name}; HP và MP có thể đã đầy.");
                return;
            }

            grpItemSelection.Visible = false;
            AddLog($"Lượt {currentTurn}: {player.Name} sử dụng {item.Name}. Hồi {healedHp} HP, {restoredMp} MP.");
            if (healedHp > 0)
                ShowFloatingText($"+{healedHp} HP", picBattlePlayer, HealColor);
            if (restoredMp > 0)
                ShowFloatingText($"+{restoredMp} MP", picBattlePlayer, ManaColor);
            UpdateBattleUI();
            EnemyTurn();
        }

        private void btnCancelItem_Click(object sender, EventArgs e)
        {
            grpItemSelection.Visible = false;
            SetActionButtonsEnabled(true);
        }

        #endregion
    }
}

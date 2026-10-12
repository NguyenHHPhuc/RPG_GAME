using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RPG.DoiTuong;
using RPG.Services;
namespace RPG.Controls
{
    public partial class BattleControl : UserControl
    {
        private GameState gameState;
        private BattleService battleService;
        private Player player;
        private Enemy enemy;
        private ItemService itemService;
        public event EventHandler BattleWon;
        public event EventHandler BattleLost;
        public event EventHandler GameWon;
        private enum BattleOutcome
        {
            None,
            NormalVictory,
            GameVictory,
            PlayerLost
        }

        private BattleOutcome pendingOutcome = BattleOutcome.None;
        public BattleControl(GameState gameState)
        {
            InitializeComponent();

            // Đăng ký sự kiện cho danh sách kỹ năng
            lstSkills.SelectedIndexChanged += lstSkills_SelectedIndexChanged;
            btnUseSelectedSkill.Click += btnUseSelectedSkill_Click;
            btnCancelSkill.Click += btnCancelSkill_Click;

            // Đăng ký sự kiện cho danh sách vật phẩm
            lstBattleItems.SelectedIndexChanged += lstBattleItems_SelectedIndexChanged;
            btnUseSelectedItem.Click += btnUseSelectedItem_Click;
            btnCancelItem.Click += btnCancelItem_Click;

            btnContinue.Click += btnContinue_Click;

            this.gameState = gameState;
            player = gameState.Player;
            enemy = gameState.CurrentEnemy;
            battleService = new BattleService();
            itemService = new ItemService();
            LoadBattle();
        }
        public void LoadBattle()
        {
            lblPlayerName.Text = player.Name;
            lblEnemyName.Text = enemy.Name;

            progressPlayerHP.Minimum = 0;
            progressPlayerHP.Maximum = player.MaxHP;
            progressPlayerHP.Value = player.HP;
            progressPlayerMP.Minimum = 0;
            progressPlayerMP.Maximum = player.MaxMP;
            progressPlayerMP.Value = player.MP;

            progressEnemyHP.Minimum = 0;
            progressEnemyHP.Maximum = enemy.MaxHP;
            progressEnemyHP.Value = enemy.HP;

            UpdateStatsText();
            AddBattleLog($"Trận chiến bắt đầu: {player.Name} đối đầu với {enemy.Name}.");

            AddBattleLog($"HP người chơi: {player.HP}/{player.MaxHP}");

            AddBattleLog($"HP quái vật: {enemy.HP}/{enemy.MaxHP}");
        }
        private void LoadSkillList()
        {
            // Xóa nguồn dữ liệu cũ
            lstSkills.DataSource = null;

            // Hiển thị tên của từng kỹ năng
            lstSkills.DisplayMember = "Name";

            // Nạp danh sách kỹ năng của nhân vật
            lstSkills.DataSource = player.Skills;

            if (player.Skills.Count > 0)
            {
                lstSkills.SelectedIndex = 0;
            }
            else
            {
                lblSkillInfo.Text = "Bạn chưa có kỹ năng nào.";
                btnUseSelectedSkill.Enabled = false;
            }
        }
        private void AddBattleLog(string message)
        {
            rtbBattleLog.AppendText(
                message + Environment.NewLine
            );

            // Cuộn xuống dòng mới nhất
            rtbBattleLog.SelectionStart = rtbBattleLog.TextLength;
            rtbBattleLog.ScrollToCaret();
        }
        private void PrepareBattleEnd(BattleOutcome outcome)
        {
            pendingOutcome = outcome;

            // Ẩn các khung lựa chọn đang mở
            grpSkillSelection.Visible = false;
            grpItemSelection.Visible = false;

            // Không cho thực hiện thêm hành động trong trận đã kết thúc
            btnAttack.Enabled = false;
            btnSkill.Enabled = false;
            btnItem.Enabled = false;

            // Hiện nút xác nhận kết thúc
            if (outcome == BattleOutcome.NormalVictory)
            {
                btnContinue.Text = "Quay lại bản đồ";
            }
            else if (outcome == BattleOutcome.GameVictory)
            {
                btnContinue.Text = "Xem chiến thắng";
            }
            else if (outcome == BattleOutcome.PlayerLost)
            {
                btnContinue.Text = "Về menu chính";
            }

            btnContinue.Visible = true;
            btnContinue.BringToFront();
        }
        private void btnContinue_Click(object sender, EventArgs e)
        {
            BattleOutcome outcome = pendingOutcome;

            // Tránh xử lý cùng kết quả nhiều lần
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
        private void UpdateStatsText()
        {
            lblPlayerHP.Text = $"{player.HP} / {player.MaxHP}";
            lblPlayerMP.Text = $"{player.MP} / {player.MaxMP}";
            lblEnemyHP.Text = $"{enemy.HP} / {enemy.MaxHP}";
        }
        private void lblEnemyName_Click(object sender, EventArgs e)
        {

        }

        private void btnAttack_Click(object sender, EventArgs e)
        {
            int playerDamage = battleService.PlayerAttack(player, enemy);

            AddBattleLog(
                $"{player.Name} tấn công {enemy.Name}, " +
                $"gây {playerDamage} sát thương!"
            );

            UpdateBattleUI();

            // Kiểm tra quái vật đã bị đánh bại chưa
            if (battleService.IsEnemyDefeated(enemy))
            {
                EnemyDefeated();
                return;
            }

            // Quái vật thực hiện lượt phản công
            EnemyTurn();
        }


        private void UpdateBattleUI()
        {
            progressPlayerHP.Value = player.HP;
            progressPlayerMP.Value = player.MP;
            progressEnemyHP.Value = enemy.HP;

            UpdateStatsText();
        }
        private void EnemyDefeated()
        {
            bool isFinalBoss =
        enemy.IsBoss &&
        enemy.MapId == "DungeonFloor5";

            // Chỉ thưởng khi xóa được quái khỏi danh sách
            if (!gameState.Enemies.Remove(enemy))
                return;
            AddBattleLog("----------------------------------------");
            AddBattleLog($"Bạn đã đánh bại {enemy.Name}!");

            // Thưởng Gold
            int goldReward = battleService.GetGoldReward(enemy);
            player.Gold += goldReward;

            AddBattleLog(
                $"Bạn nhận được {goldReward} Gold! " +
                $"Tổng Gold: {player.Gold}"
            );

            // Thưởng EXP và kiểm tra lên cấp
            int expReward = battleService.GetExperienceReward(enemy);

            List<string> messages = battleService.GainExperience(
                player,
                expReward
            );

            foreach (string message in messages)
            {
                AddBattleLog(message);
            }

            // Không còn mục tiêu chiến đấu
            gameState.CurrentEnemy = null;

            if (isFinalBoss)
            {
                gameState.IsGameWon = true;

                AddBattleLog("Bạn đã chinh phục Dungeon!");
                AddBattleLog("Nhấn nút bên dưới để xem kết quả chiến thắng.");

                PrepareBattleEnd(BattleOutcome.GameVictory);
                return;
            }

            AddBattleLog("Trận chiến kết thúc.");
            AddBattleLog("Nhấn nút bên dưới để quay lại bản đồ.");

            PrepareBattleEnd(BattleOutcome.NormalVictory);
        }
        private void PlayerDefeated()
        {
            AddBattleLog("----------------------------------------");
            AddBattleLog("Bạn đã bị đánh bại!");
            AddBattleLog("Nhấn nút bên dưới để quay về menu chính.");

            PrepareBattleEnd(BattleOutcome.PlayerLost);
        }

        private void btnSkill_Click(object sender, EventArgs e)
        {
            if (player.Skills == null || player.Skills.Count == 0)
            {
                MessageBox.Show("Bạn chưa có kỹ năng nào!");
                return;
            }

            LoadSkillList();

            grpSkillSelection.Visible = true;
            grpSkillSelection.BringToFront();
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
                $"Kỹ năng: {skill.Name}\r\n" +
                $"Sát thương: {skill.MinDamage} - {skill.MaxDamage}\r\n" +
                $"Mana tiêu hao: {skill.ManaCost}\r\n" +
                $"MP hiện tại: {player.MP}/{player.MaxMP}";

            btnUseSelectedSkill.Enabled = true;
        }
        private void btnUseSelectedSkill_Click(object sender, EventArgs e)
        {
            // Lấy kỹ năng đang được chọn
            Skill skill = lstSkills.SelectedItem as Skill;

            if (skill == null)
            {
                MessageBox.Show("Vui lòng chọn một kỹ năng!");
                return;
            }

            // Kiểm tra người chơi có đủ MP không
            if (!battleService.CanUseSkill(player, skill))
            {
                AddBattleLog($"Không đủ MP để sử dụng {skill.Name}.");
                return;
            }

            // Sử dụng đúng kỹ năng đã chọn
            int damage = battleService.UseSkill(
                player,
                enemy,
                skill
            );

            // Đóng khung chọn kỹ năng
            grpSkillSelection.Visible = false;

            AddBattleLog($"{player.Name} sử dụng {skill.Name}, " + $"gây {damage} sát thương!");

            UpdateBattleUI();

            // Nếu quái đã bị đánh bại, xử lý kết thúc trận chiến
            if (battleService.IsEnemyDefeated(enemy))
            {
                EnemyDefeated();
                return;
            }

            // Nếu quái vẫn còn sống, đến lượt quái tấn công
            EnemyTurn();
        }
        private void btnCancelSkill_Click(object sender, EventArgs e)
        {
            grpSkillSelection.Visible = false;
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            LoadBattleItemList();

            if (lstBattleItems.Items.Count == 0)
            {
                AddBattleLog("Bạn không có vật phẩm sử dụng được trong chiến đấu.");
                return;
            }

            // Đóng khung chọn skill nếu đang mở
            grpSkillSelection.Visible = false;

            // Mở khung chọn vật phẩm
            grpItemSelection.Visible = true;
            grpItemSelection.BringToFront();
        }
        private void LoadBattleItemList()
        {
            var usableItems = player.Inventory.Items
                .Where(item =>
                    item.Quantity > 0 &&
                    (item.HealAmount > 0 || item.ManaRestore > 0 || item.Damage > 0))
                .ToList();

            lstBattleItems.DataSource = null;
            lstBattleItems.DisplayMember = nameof(Item.Name);
            lstBattleItems.DataSource = usableItems;

            btnUseSelectedItem.Enabled = usableItems.Count > 0;

            if (usableItems.Count == 0)
            {
                lblBattleItemInfo.Text =
                    "Không có vật phẩm sử dụng được.";
            }
            else
            {
                lstBattleItems.SelectedIndex = 0;
            }
        }
        private void lstBattleItems_SelectedIndexChanged( object sender, EventArgs e)
        {
            Item item = lstBattleItems.SelectedItem as Item;

            if (item == null)
            {
                lblBattleItemInfo.Text =
                    "Hãy chọn một vật phẩm.";

                btnUseSelectedItem.Enabled = false;
                return;
            }

            lblBattleItemInfo.Text =
                $"{item.Name}\r\n" +
                $"{item.Description}\r\n" +
                $"Số lượng: {item.Quantity}\r\n" +
                $"Hồi HP: {item.HealAmount}\r\n" +
                $"Hồi MP: {item.ManaRestore}\r\n" +
                $"Sát thương: {item.Damage}";

            btnUseSelectedItem.Enabled = true;
        }
        private void btnUseSelectedItem_Click(object sender, EventArgs e)
        {
            Item item = lstBattleItems.SelectedItem as Item;

            if (item == null)
            {
                MessageBox.Show("Vui lòng chọn một vật phẩm!");
                return;
            }

            // Trường hợp sử dụng Bomb
            if (item.Damage > 0)
            {
                int damage = itemService.UseBomb(player, enemy, item);

                if (damage <= 0)
                {
                    AddBattleLog("Không thể sử dụng Bomb.");
                    LoadBattleItemList();
                    return;
                }

                grpItemSelection.Visible = false;

                AddBattleLog($"{player.Name} sử dụng {item.Name}, " + $"gây {damage} sát thương!");

                UpdateBattleUI();

                // Nếu Bomb hạ quái, xử lý kết thúc trận chiến
                if (battleService.IsEnemyDefeated(enemy))
                {
                    EnemyDefeated();
                    return;
                }

                // Cập nhật số lượng vật phẩm còn lại
                LoadBattleItemList();

                // Quái thực hiện lượt tấn công
                EnemyTurn();
                return;
            }

            // Các vật phẩm hồi HP và MP vẫn dùng logic cũ
            bool used = itemService.UseItem(player, item);

            if (!used)
            {
                AddBattleLog("Không thể sử dụng vật phẩm vì HP và MP đã đầy.");
            }

            grpItemSelection.Visible = false;

            AddBattleLog($"{player.Name} sử dụng {item.Name}.");

            UpdateBattleUI();

            // Cập nhật danh sách sau khi tiêu hao vật phẩm
            LoadBattleItemList();

            EnemyTurn();
        }
        private void btnCancelItem_Click(object sender, EventArgs e)
        {
            grpItemSelection.Visible = false;
        }
        private void EnemyTurn()
        {
            int enemyDamage = battleService.EnemyAttack(player,enemy);

            AddBattleLog(
                $"{enemy.Name} tấn công {player.Name}, " +
                $"gây {enemyDamage} sát thương!"
            );

            UpdateBattleUI();

            // Kiểm tra người chơi đã bị đánh bại chưa
            if (battleService.IsPlayerDefeated(player))
            {
                PlayerDefeated();
            }
        }


    }
}

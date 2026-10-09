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
        public BattleControl(GameState gameState)
        {
            InitializeComponent();
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

            MessageBox.Show($"{player.Name} gây {playerDamage} sát thương!");
            UpdateBattleUI();
            if (battleService.IsEnemyDefeated(enemy))
            {
                EnemyDefeated();
                return;
            }

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
            MessageBox.Show("Bạn đã đánh bại " + enemy.Name);
            gameState.Enemies.Remove(enemy);
            gameState.CurrentEnemy = null;

            BattleWon?.Invoke(this, EventArgs.Empty);
        }
        private void PlayerDefeated()
        {
            MessageBox.Show("Bạn đã thua!");
            BattleLost?.Invoke(this, EventArgs.Empty);
        }

        private void btnSkill_Click(object sender, EventArgs e)
        {
            if (player.Skills.Count == 0)
            {
                MessageBox.Show("Bạn không có kỹ năng nào!");
                return;
            }
            Skill skill = player.Skills[0]; // Sử dụng kỹ năng đầu tiên trong danh sách
            if (!battleService.CanUseSkill(player, skill))
            {
                MessageBox.Show("Bạn không đủ mana để sử dụng kỹ năng này!");
                return;
            }
            int damage = battleService.UseSkill(
                            player,
                            enemy,
                            skill
                        );

            MessageBox.Show(
                $"{player.Name} sử dụng {skill.Name} và gây {damage} sát thương!");

            UpdateBattleUI();

            if (battleService.IsEnemyDefeated(enemy))
            {
                EnemyDefeated();
                return;
            }

            EnemyTurn();
        }

        private void btnItem_Click(object sender, EventArgs e)
        {
            Item potion = player.Inventory.GetItem("ITEM_HEALTH_POTION");

            if (potion == null)
            {
                MessageBox.Show("Bạn không còn Potion.");
                return;
            }

            if (player.HP >= player.MaxHP)
            {
                MessageBox.Show("HP đã đầy.");
                return;
            }

            bool used = itemService.UseItem(player, potion);

            if (!used)
            {
                MessageBox.Show("Không thể sử dụng Item.");
                return;
            }

            MessageBox.Show(
                $"{player.Name} sử dụng {potion.Name}!"
            );

            UpdateBattleUI();

            EnemyTurn();
        }
        private void EnemyTurn()
        {
            int enemyDamage = battleService.EnemyAttack(
                player,
                enemy
            );

            MessageBox.Show(
                $"{enemy.Name} gây {enemyDamage} sát thương!"
            );

            UpdateBattleUI();

            if (battleService.IsPlayerDefeated(player))
            {
                PlayerDefeated();
            }
        }
    }
}

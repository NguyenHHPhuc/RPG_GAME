using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RPG.DoiTuong;
namespace RPG.Controls
{
    public partial class HomeControl : UserControl
    {
        private GameState gameState;
        public event EventHandler StartAdventureClicked;
        public event EventHandler InventoryClicked;
        public event EventHandler BackToMainMenuClicked;
        public HomeControl(GameState gameState)
        {
            InitializeComponent();
            this.gameState = gameState;
            LoadPlayerInfo();
        }

        private void LoadPlayerInfo()
        {
            lblPlayerName.Text = $"Player: {gameState.Player.Name}";
            lblPlayerEXP.Text = $"EXP: {gameState.Player.Experience} / " +$"{gameState.Player.ExperienceToNextLevel}";
            lblPlayerLevel.Text = $"Level: {gameState.Player.Level}";
            lblPlayerHP.Text = $"HP: {gameState.Player.HP} / {gameState.Player.MaxHP}";
            lblPlayerMP.Text = $"MP: {gameState.Player.MP} / {gameState.Player.MaxMP}";
            lblPlayerGold.Text = $"Gold: {gameState.Player.Gold}";
        }

        private void btnStartAdventure_Click(object sender, EventArgs e)
        {
            StartAdventureClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            InventoryClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnExitHome_Click(object sender, EventArgs e)
        {
            BackToMainMenuClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}

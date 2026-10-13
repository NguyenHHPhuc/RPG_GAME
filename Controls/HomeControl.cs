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
            lblPlayerName.Text = $"Nhân vật: {gameState.Player.Name} ({gameState.Player.ClassName})";
            lblPlayerLevel.Text = $"Cấp: {gameState.Player.Level}";
            lblPlayerHP.Text = $"Máu: {gameState.Player.HP} / {gameState.Player.MaxHP}";
            lblPlayerMP.Text = $"Năng lượng: {gameState.Player.MP} / {gameState.Player.MaxMP}";
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

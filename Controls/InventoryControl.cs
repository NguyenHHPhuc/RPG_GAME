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
    public partial class InventoryControl : UserControl
    {
        private GameState gameState;
        private ItemService itemService;
        private List<Item> currentItems;
        public event EventHandler CloseClicked;
        public InventoryControl(GameState gameState)
        {
            InitializeComponent();
            this.gameState = gameState;
            this.itemService = new ItemService();

            LoadInventory();
        }
        private void LoadInventory()
        {
            currentItems = gameState.Player.Inventory.Items;

            lstItems.Items.Clear();
            foreach (var item in currentItems)
            {
                lstItems.Items.Add($"{item.Name} x{item.Quantity}");
            }
            lblItemInfo.Text = "Chọn một vật phẩm để xem thông tin.";
        }

        private void lstItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstItems.SelectedIndex < 0)
            {
                lblItemInfo.Text = "";
                return;
            }

            Item item = currentItems[lstItems.SelectedIndex];

            lblItemInfo.Text =
                $"{item.Name}\n" +
                $"Số lượng: {item.Quantity}\n" +
                $"Hồi máu: +{item.HealAmount}\n" +
                $"Hồi năng lượng: +{item.ManaRestore}";
        }

        private void btnUse_Click(object sender, EventArgs e)
        {
            if (lstItems.SelectedIndex < 0)
            {
                MessageBox.Show("Hãy chọn một vật phẩm.");
                return;
            }

            Item item = currentItems[lstItems.SelectedIndex];

            if (gameState.Player.HP >= gameState.Player.MaxHP &&
                gameState.Player.MP >= gameState.Player.MaxMP)
            {
                MessageBox.Show("Không có chỉ số nào cần hồi.");
                return;
            }

            bool used = itemService.UseItem(
                gameState.Player,
                item
            );

            if (!used)
            {
                MessageBox.Show("Không thể sử dụng vật phẩm.");
                return;
            }

            MessageBox.Show(
                $"Đã sử dụng {item.Name}."
            );

            LoadInventory();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}

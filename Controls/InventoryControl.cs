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

        private void LoadInventory(string selectedItemId = null)
        {
            if (gameState.Player.Inventory == null)
            {
                gameState.Player.Inventory = new Inventory();
            }

            if (gameState.Player.Inventory.Items == null)
            {
                gameState.Player.Inventory.Items = new List<Item>();
            }

            currentItems = gameState.Player.Inventory.Items;

            lstItems.Items.Clear();

            foreach (Item item in currentItems)
            {
                string status = IsEquipped(item)
                    ? " (Đang trang bị)"
                    : "";

                lstItems.Items.Add(
                    $"{item.Name} x{item.Quantity}{status}"
                );
            }

            UpdateEquipmentLabels();

            // Giữ lại lựa chọn sau khi trang bị hoặc sử dụng vật phẩm
            if (selectedItemId != null)
            {
                int index = currentItems.FindIndex(
                    item => item.Id == selectedItemId
                );

                if (index >= 0)
                {
                    lstItems.SelectedIndex = index;
                }
            }

            if (lstItems.SelectedIndex < 0)
            {
                lblItemInfo.Text =
                    "Chọn một vật phẩm để xem chi tiết.";

                UpdateActionButtons();
            }
        }

        private bool IsEquipped(Item item)
        {
            if (item == null)
                return false;

            Player player = gameState.Player;

            if (item.Slot == EquipSlot.Weapon)
            {
                return player.EquippedWeapon != null &&
                    player.EquippedWeapon.Id == item.Id;
            }

            if (item.Slot == EquipSlot.Armor)
            {
                return player.EquippedArmor != null &&
                    player.EquippedArmor.Id == item.Id;
            }

            return false;
        }

        private void UpdateEquipmentLabels()
        {
            Player player = gameState.Player;

            // Hiển thị vũ khí đang trang bị
            string weaponName = player.EquippedWeapon == null
                ? "Chưa trang bị"
                : player.EquippedWeapon.Name;

            lblEquippedWeapon.Text = $"Vũ khí: {weaponName}";

            // Hiển thị giáp đang trang bị
            string armorName = player.EquippedArmor == null
                ? "Chưa trang bị"
                : player.EquippedArmor.Name;

            lblEquippedArmor.Text = $"Giáp: {armorName}";

            // Hiển thị sát thương thực tế và chỉ số cơ bản
            lblPlayerDamage.Text =
                $"Sát thương: {player.EffectiveMinDamage} - " +
                $"{player.EffectiveMaxDamage} " +
                $"(Cơ bản: {player.MinDamage} - {player.MaxDamage})";

            // Hiển thị phòng thủ thực tế và chỉ số cơ bản
            lblPlayerDefense.Text =
                $"Phòng thủ: {player.EffectiveDefense} " +
                $"(Cơ bản: {player.Defense})";
        }

        private void UpdateActionButtons()
        {
            bool hasSelection =
                lstItems.SelectedIndex >= 0 &&
                lstItems.SelectedIndex < currentItems.Count;

            btnUse.Enabled = false;
            btnEquip.Enabled = false;
            btnUnequip.Enabled = false;

            if (!hasSelection)
                return;

            Item item = currentItems[lstItems.SelectedIndex];

            // Chỉ Potion hoặc vật phẩm hồi phục mới dùng trực tiếp ở đây
            btnUse.Enabled =
                item.Slot == EquipSlot.None &&
                item.Damage <= 0 &&
                (item.HealAmount > 0 || item.ManaRestore > 0);

            bool equipped = IsEquipped(item);

            // Chỉ trang bị được những món chưa được trang bị
            btnEquip.Enabled =
                item.Slot != EquipSlot.None &&
                !equipped;

            // Chỉ tháo được món đang trang bị
            btnUnequip.Enabled = equipped;
        }

        private void lstItems_SelectedIndexChanged(
            object sender, EventArgs e)
        {
            if (lstItems.SelectedIndex < 0 ||
                lstItems.SelectedIndex >= currentItems.Count)
            {
                lblItemInfo.Text = "";
                UpdateActionButtons();
                return;
            }

            Item item = currentItems[lstItems.SelectedIndex];

            List<string> details = new List<string>
            {
                item.Name,
                $"Số lượng: {item.Quantity}"
            };

            if (!string.IsNullOrWhiteSpace(item.Description))
            {
                details.Add(item.Description);
            }

            if (item.HealAmount > 0)
                details.Add($"Hồi HP: +{item.HealAmount}");

            if (item.ManaRestore > 0)
                details.Add($"Hồi MP: +{item.ManaRestore}");

            if (item.Damage > 0)
                details.Add($"Sát thương vật phẩm: {item.Damage}");

            if (item.Slot == EquipSlot.Weapon)
            {
                details.Add(
                    $"Sát thương tối thiểu: +{item.MinDamageBonus}"
                );

                details.Add(
                    $"Sát thương tối đa: +{item.MaxDamageBonus}"
                );
            }
            else if (item.Slot == EquipSlot.Armor)
            {
                details.Add(
                    $"Phòng thủ: +{item.DefenseBonus}"
                );
            }

            if (IsEquipped(item))
            {
                details.Add("Trạng thái: Đang trang bị");
            }

            lblItemInfo.Text = string.Join(
                Environment.NewLine,
                details
            );

            UpdateActionButtons();
        }

        private void btnEquip_Click(object sender, EventArgs e)
        {
            if (lstItems.SelectedIndex < 0)
            {
                MessageBox.Show("Hãy chọn một trang bị.");
                return;
            }

            Item item = currentItems[lstItems.SelectedIndex];
            Player player = gameState.Player;

            if (item.Slot == EquipSlot.None)
            {
                MessageBox.Show(
                    "Vật phẩm này không phải trang bị."
                );
                return;
            }

            if (item.Slot == EquipSlot.Weapon)
            {
                player.EquippedWeapon = item;
            }
            else if (item.Slot == EquipSlot.Armor)
            {
                player.EquippedArmor = item;
            }

            MessageBox.Show($"Đã trang bị {item.Name}.");

            LoadInventory(item.Id);
        }

        private void btnUnequip_Click(object sender, EventArgs e)
        {
            if (lstItems.SelectedIndex < 0)
            {
                MessageBox.Show("Hãy chọn trang bị muốn tháo.");
                return;
            }

            Item item = currentItems[lstItems.SelectedIndex];
            Player player = gameState.Player;

            if (!IsEquipped(item))
            {
                MessageBox.Show(
                    "Vật phẩm này hiện không được trang bị."
                );
                return;
            }

            if (item.Slot == EquipSlot.Weapon)
            {
                player.EquippedWeapon = null;
            }
            else if (item.Slot == EquipSlot.Armor)
            {
                player.EquippedArmor = null;
            }

            MessageBox.Show($"Đã tháo {item.Name}.");

            LoadInventory(item.Id);
        }

        private void btnUse_Click(object sender, EventArgs e)
        {
            if (lstItems.SelectedIndex < 0)
            {
                MessageBox.Show("Hãy chọn một Item.");
                return;
            }

            Item item = currentItems[lstItems.SelectedIndex];

            if (item.Slot != EquipSlot.None || item.Damage > 0)
            {
                MessageBox.Show(
                    "Vật phẩm này không thể sử dụng trực tiếp trong Inventory."
                );
                return;
            }

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
                MessageBox.Show("Không thể sử dụng Item.");
                return;
            }

            MessageBox.Show($"Đã sử dụng {item.Name}.");

            LoadInventory(item.Id);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}

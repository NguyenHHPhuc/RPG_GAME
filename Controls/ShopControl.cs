using RPG.DoiTuong;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace RPG.Controls
{
    public partial class ShopControl : UserControl
    {
        private GameState gameState;
        public event EventHandler CloseClicked;
        private readonly List<Item> shopItems = new List<Item>
        {
            new Item
            {
                Id = "ITEM_HEALTH_POTION",
                Name = "Health Potion",
                Description = "Hồi phục 50 HP.",
                Price = 25,
                HealAmount = 50,
                ManaRestore = 0
            },

            new Item
            {
                Id = "ITEM_MANA_POTION",
                Name = "Mana Potion",
                Description = "Hồi phục 30 MP.",
                Price = 25,
                HealAmount = 0,
                ManaRestore = 30
            },

            new Item
            {
                Id = "ITEM_LARGE_HEALTH_POTION",
                Name = "Large Health Potion",
                Description = "Hồi phục 80 HP.",
                Price = 50,
                HealAmount = 80,
                ManaRestore = 0
            },

            new Item
            {
                Id = "ITEM_LARGE_MANA_POTION",
                Name = "Large Mana Potion",
                Description = "Hồi phục 50 MP.",
                Price = 50,
                HealAmount = 0,
                ManaRestore = 50
            },

            new Item
            {
                Id = "ITEM_ELIXIR",
                Name = "Elixir",
                Description = "Hồi phục 40 HP và 25 MP.",
                Price = 60,
                HealAmount = 40,
                ManaRestore = 25
            },
            new Item
            {
                Id = "ITEM_BOMB",
                Name = "Bomb",
                Description = "Gây 40 sát thương trực tiếp lên quái vật.",
                Price = 60,
                Quantity = 0,
                HealAmount = 0,
                ManaRestore = 0,
                Damage = 40
            },
                        // ===== WEAPONS =====
            new Item
            {
                Id = "ITEM_WOODEN_SWORD",
                Name = "Kiếm Gỗ",
                Description = "Kiếm gỗ dành cho người mới bắt đầu.",
                Price = 60,
                Quantity = 0,
                Slot = EquipSlot.Weapon,
                MinDamageBonus = 2,
                MaxDamageBonus = 3
            },
            new Item
            {
                Id = "ITEM_IRON_SWORD",
                Name = "Kiem Sắt",
                Description = "Kiếm sắt có sát thương cao hơn.",
                Price = 120,
                Quantity = 0,
                Slot = EquipSlot.Weapon,
                MinDamageBonus = 5,
                MaxDamageBonus = 8
            },

            // ===== ARMOR =====
            new Item
            {
                Id = "ITEM_LEATHER_ARMOR",
                Name = "Áo Giáp Da",
                Description = "Áo giáp da giúp tăng phòng thủ.",
                Price = 70,
                Quantity = 0,
                Slot = EquipSlot.Armor,
                DefenseBonus = 3
            },
            new Item
            {
                Id = "ITEM_IRON_ARMOR",
                Name = "Áo Giáp Sắt",
                Description = "Áo giáp sắt có khả năng phòng thủ cao.",
                Price = 140,
                Quantity = 0,
                Slot = EquipSlot.Armor,
                DefenseBonus = 7
            }

        };
        public ShopControl(GameState gameState)
        {
            InitializeComponent();
            this.gameState = gameState;
            lstShopItems.SelectedIndexChanged += lstShopItems_SelectedIndexChanged;

            btnBuyItem.Click += btnBuyItem_Click;
            LoadShopInfo();
        }
        private void LoadShopInfo()
        {
            lblShopGold.Text = $"Gold: {gameState.Player.Gold}";
            lstShopItems.DataSource = null;
            lstShopItems.DisplayMember = nameof(Item.Name);
            lstShopItems.DataSource = shopItems;

            if (lstShopItems.Items.Count > 0)
            {
                lstShopItems.SelectedIndex = 0;
            }
        }
        private void btnCloseShop_Click(object sender, EventArgs e)
        {
            CloseClicked?.Invoke(this, EventArgs.Empty);
        }
        private void lstShopItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            Item selectedItem = lstShopItems.SelectedItem as Item;

            if (selectedItem == null)
            {
                lblItemDescription.Text = "Chọn một vật phẩm.";
                lblItemPrice.Text = "Giá: -- Gold";
                btnBuyItem.Enabled = false;
                return;
            }

            lblItemDescription.Text = selectedItem.Description;

            string itemStats =
                $"Hồi HP: {selectedItem.HealAmount}\r\n" +
                $"Hồi MP: {selectedItem.ManaRestore}\r\n" +
                $"Sát thương vật phẩm: {selectedItem.Damage}";

            if (selectedItem.Slot == EquipSlot.Weapon)
            {
                itemStats =
                    $"Sát thương tối thiểu: +{selectedItem.MinDamageBonus}\r\n" +
                    $"Sát thương tối đa: +{selectedItem.MaxDamageBonus}";
            }
            else if (selectedItem.Slot == EquipSlot.Armor)
            {
                itemStats = $"Phòng thủ: +{selectedItem.DefenseBonus}";
            }

            lblItemPrice.Text =
                $"Giá: {selectedItem.Price} Gold\r\n" +
                itemStats;

            btnBuyItem.Enabled = true;
        }
        private void btnBuyItem_Click(object sender, EventArgs e)
        {
            Item selectedItem = lstShopItems.SelectedItem as Item;

            if (selectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn vật phẩm cần mua.");
                return;
            }

            Player player = gameState.Player;

            if (player.Gold < selectedItem.Price)
            {
                MessageBox.Show("Bạn không đủ Gold để mua vật phẩm này.");
                return;
            }

            if (player.Inventory == null)
            {
                player.Inventory = new Inventory();
            }

            if (player.Inventory.Items == null)
            {
                player.Inventory.Items = new List<Item>();
            }

            bool isEquipment = selectedItem.Slot != EquipSlot.None;

            Item ownedItem = player.Inventory.Items.FirstOrDefault(
                item => item.Id == selectedItem.Id
            );

            // Không mua thêm bản sao của cùng loại trang bị
            if (isEquipment && ownedItem != null)
            {
                MessageBox.Show("Bạn đã sở hữu trang bị này.");
                return;
            }

            // Trừ tiền sau khi xác nhận giao dịch hợp lệ
            player.Gold -= selectedItem.Price;

            if (!isEquipment && ownedItem != null)
            {
                // Vật phẩm tiêu hao: cộng dồn số lượng
                ownedItem.Quantity++;
            }
            else
            {
                // Trang bị hoặc vật phẩm chưa sở hữu: tạo bản ghi mới
                player.Inventory.Items.Add(new Item
                {
                    Id = selectedItem.Id,
                    Name = selectedItem.Name,
                    Description = selectedItem.Description,
                    Price = selectedItem.Price,
                    Quantity = 1,

                    HealAmount = selectedItem.HealAmount,
                    ManaRestore = selectedItem.ManaRestore,
                    Damage = selectedItem.Damage,

                    Slot = selectedItem.Slot,
                    MinDamageBonus = selectedItem.MinDamageBonus,
                    MaxDamageBonus = selectedItem.MaxDamageBonus,
                    DefenseBonus = selectedItem.DefenseBonus
                });
            }

            lblShopGold.Text = $"Gold: {player.Gold}";

            MessageBox.Show(
                $"Bạn đã mua {selectedItem.Name} thành công!"
            );
        }

        private void sd(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.DoiTuong
{
    public enum EquipSlot
    {
        None,
        Weapon,
        Armor
    }
    public class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }

        public int HealAmount { get; set; }
        public int ManaRestore { get; set; }
        public int Damage { get; set; }
        // Vũ khí hoặc giáp có thể được trang bị vào slot nào
        public EquipSlot Slot { get; set; } = EquipSlot.None;
        // Sát thương cộng thêm từ vũ khí
        public int MinDamageBonus { get; set; }
        public int MaxDamageBonus { get; set; }

        // Phòng thủ cộng thêm từ giáp
        public int DefenseBonus { get; set; }
    }
}

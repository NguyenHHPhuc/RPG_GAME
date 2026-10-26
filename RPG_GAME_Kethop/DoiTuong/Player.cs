using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.DoiTuong
{
    public class Player
    {
        public string Name { get; set; }

        public int Level { get; set; }
        public int Gold { get; set; } = 0;
        public int Experience { get; set; } = 0;
        public int ExperienceToNextLevel { get; set; } = 100;
        public int SkillPoints { get; set; } = 0;
        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int MP { get; set; }
        public int MaxMP { get; set; }

        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int Defense { get; set; }

        // Vị trí của người chơi trên bản đồ
        public int X { get; set; }
        public int Y { get; set; }
        public List<Skill> Skills { get; set; } = new List<Skill>();
        public Inventory Inventory { get; set; } = new Inventory();

        public Item EquippedWeapon { get; set; }
        public int EffectiveMinDamage => MinDamage + (EquippedWeapon == null ? 0 : EquippedWeapon.MinDamageBonus);
        public int EffectiveMaxDamage =>MaxDamage + (EquippedWeapon == null ? 0 : EquippedWeapon.MaxDamageBonus);
        public Item EquippedArmor { get; set; }
        public int EffectiveDefense => Defense + (EquippedArmor == null ? 0 : EquippedArmor.DefenseBonus);
    }
}

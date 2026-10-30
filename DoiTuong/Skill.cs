using System;

namespace RPG.DoiTuong
{
    public enum SkillType
    {
        Damage,
        Heal,
        Shield
    }

    public class Skill
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public SkillType Type { get; set; } = SkillType.Damage;

        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int ManaCost { get; set; }

        // Skill Heal: phần trăm MaxHP được hồi.
        public int HealPercent { get; set; }

        // Skill gây chảy máu: sát thương mỗi lượt và số lượt còn hiệu lực.
        public int BleedDamage { get; set; }
        public int BleedTurns { get; set; }
    }
}

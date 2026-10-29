using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.DoiTuong
{
    // Loại kỹ năng: gây sát thương, hồi máu, hoặc dựng khiên chặn đòn
    public enum SkillType
    {
        Damage,
        Heal,
        Shield
    }

    public class Skill
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public SkillType Type { get; set; } = SkillType.Damage;

        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int ManaCost { get; set; }

        // Kỹ năng hồi máu: hồi bao nhiêu % MaxHP
        public int HealPercent { get; set; }

        // Chảy máu: mỗi lượt của quái mất BleedDamage HP, kéo dài BleedTurns lượt
        public int BleedDamage { get; set; }
        public int BleedTurns { get; set; }
    }
}

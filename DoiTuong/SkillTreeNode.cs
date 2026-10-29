using System.Collections.Generic;

namespace RPG.DoiTuong
{
    public enum SkillBranch
    {
        Root,
        Health,
        Attack,
        Defense
    }

    public enum PassiveSkill
    {
        None,
        Regeneration,   // Tái Sinh
        DoubleStrike,   // Liên Kích
        Revive          // Bất Tử
    }

    public class SkillTreeNode
    {
        public string Name { get; set; }
        public string Description { get; set; } = "";
        public SkillBranch Branch { get; set; }

        // 0 = ô gốc; 1, 2, 3 = tầng trong nhánh
        public int Tier { get; set; }

        public int BonusMaxHP { get; set; }
        public int BonusDamage { get; set; }
        public int BonusDefense { get; set; }

        public Skill UnlockedSkill { get; set; }
        public PassiveSkill Passive { get; set; } = PassiveSkill.None;

        public bool IsLearned { get; set; }

        public string GetDetails()
        {
            List<string> lines = new List<string>();
            if (BonusMaxHP > 0)
                lines.Add($"+{BonusMaxHP} Máu tối đa");
            if (BonusDamage > 0)
                lines.Add($"+{BonusDamage} Tấn công");
            if (BonusDefense > 0)
                lines.Add($"+{BonusDefense} Phòng thủ");
            if (Description != "")
                lines.Add(Description);
            return string.Join("\n", lines);
        }
    }
}

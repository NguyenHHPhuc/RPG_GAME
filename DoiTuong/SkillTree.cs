using System.Collections.Generic;
using System.Linq;

namespace RPG.DoiTuong
{
    // 1 ô gốc + 3 nhánh (Máu, Tấn công, Phòng thủ), mỗi nhánh 3 tầng.
    // Học ô gốc trước, học từ dưới lên, tối đa 2 nhánh.
    public class SkillTree
    {
        public const int MaxBranches = 2;

        public const int RegenerationPercent = 5;
        public const int DoubleStrikeChance = 25;
        public const int RevivePercent = 30;

        public List<SkillTreeNode> Nodes { get; } = CreateNodes();

        public SkillTreeNode Root => Nodes[0];

        public SkillTreeNode GetNode(SkillBranch branch, int tier)
        {
            return Nodes.First(node => node.Branch == branch && node.Tier == tier);
        }

        public bool HasPassive(PassiveSkill passive)
        {
            return Nodes.Any(node => node.IsLearned && node.Passive == passive);
        }

        // Số nhánh đã học tầng 1
        public int CountStartedBranches()
        {
            return Nodes.Count(node => node.Tier == 1 && node.IsLearned);
        }

        // null nghĩa là học được
        public string GetLockReason(SkillTreeNode node, int skillPoints)
        {
            if (node.IsLearned)
                return "Đã học.";
            if (skillPoints <= 0)
                return "Không đủ điểm kỹ năng.";
            if (node.Branch == SkillBranch.Root)
                return null;
            if (!Root.IsLearned)
                return "Phải học ô gốc trước.";
            if (node.Tier > 1 && !GetNode(node.Branch, node.Tier - 1).IsLearned)
                return "Phải học ô bên dưới trước.";
            if (node.Tier == 1 && CountStartedBranches() >= MaxBranches)
                return $"Chỉ được nâng tối đa {MaxBranches} nhánh.";
            return null;
        }

        private static List<SkillTreeNode> CreateNodes()
        {
            return new List<SkillTreeNode>
            {
                // Ô gốc (bắt buộc học đầu tiên)
                new SkillTreeNode
                {
                    Name = "Khởi Đầu", Branch = SkillBranch.Root, Tier = 0,
                    BonusMaxHP = 20, BonusDamage = 2, BonusDefense = 1
                },

                // Nhánh Máu
                new SkillTreeNode
                {
                    Name = "Thể Lực", Branch = SkillBranch.Health, Tier = 1,
                    BonusMaxHP = 30
                },
                new SkillTreeNode
                {
                    Name = "Hồi Phục", Branch = SkillBranch.Health, Tier = 2,
                    BonusMaxHP = 30,
                    Description = "Mở kỹ năng Hồi Phục: hồi 40% Máu tối đa (15 MP).",
                    UnlockedSkill = new Skill
                    {
                        Id = "SKILL_HEAL", Name = "Hồi Phục", Type = SkillType.Heal,
                        HealPercent = 40, ManaCost = 15
                    }
                },
                new SkillTreeNode
                {
                    Name = "Tái Sinh", Branch = SkillBranch.Health, Tier = 3,
                    BonusMaxHP = 40,
                    Description = $"Nội tại Tái Sinh: hồi {RegenerationPercent}% Máu tối đa sau mỗi lượt.",
                    Passive = PassiveSkill.Regeneration
                },

                // Nhánh Tấn công
                new SkillTreeNode
                {
                    Name = "Sức Mạnh", Branch = SkillBranch.Attack, Tier = 1,
                    BonusDamage = 4
                },
                new SkillTreeNode
                {
                    Name = "Chém Rỉ Máu", Branch = SkillBranch.Attack, Tier = 2,
                    BonusDamage = 4,
                    Description = "Mở kỹ năng Chém Rỉ Máu: 20-30 sát thương, quái chảy máu 8 HP mỗi lượt trong 3 lượt (15 MP).",
                    UnlockedSkill = new Skill
                    {
                        Id = "SKILL_BLEED", Name = "Chém Rỉ Máu", Type = SkillType.Damage,
                        MinDamage = 20, MaxDamage = 30, ManaCost = 15,
                        BleedDamage = 8, BleedTurns = 3
                    }
                },
                new SkillTreeNode
                {
                    Name = "Liên Kích", Branch = SkillBranch.Attack, Tier = 3,
                    BonusDamage = 6,
                    Description = $"Nội tại Liên Kích: {DoubleStrikeChance}% đánh thường 2 lần.",
                    Passive = PassiveSkill.DoubleStrike
                },

                // Nhánh Phòng thủ
                new SkillTreeNode
                {
                    Name = "Kiên Cố", Branch = SkillBranch.Defense, Tier = 1,
                    BonusDefense = 3
                },
                new SkillTreeNode
                {
                    Name = "Khiên Chắn", Branch = SkillBranch.Defense, Tier = 2,
                    BonusDefense = 3,
                    Description = "Mở kỹ năng Khiên Chắn: chặn hoàn toàn đòn kế tiếp của quái (10 MP).",
                    UnlockedSkill = new Skill
                    {
                        Id = "SKILL_SHIELD", Name = "Khiên Chắn", Type = SkillType.Shield,
                        ManaCost = 10
                    }
                },
                new SkillTreeNode
                {
                    Name = "Bất Tử", Branch = SkillBranch.Defense, Tier = 3,
                    BonusDefense = 4,
                    Description = $"Nội tại Bất Tử: khi chết được hồi sinh với {RevivePercent}% Máu (1 lần mỗi trận).",
                    Passive = PassiveSkill.Revive
                }
            };
        }
    }
}

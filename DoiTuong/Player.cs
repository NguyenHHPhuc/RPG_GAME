using System;
using System.Collections.Generic;

namespace RPG.DoiTuong
{
    public enum StatType
    {
        Health,
        Attack,
        Defense
    }

    // Name, Level, HP, sát thương, phòng thủ, X/Y, IsDead, Heal... kế thừa từ Character
    public class Player : Character
    {
        public const int StatPointsPerLevel = 5;
        public const int HealthPerPoint = 10;
        public const int AttackPerPoint = 2;
        public const int DefensePerPoint = 1;

        private const int LevelUpMaxHP = 20;
        private const int LevelUpMaxMP = 10;
        private const int LevelUpMinDamage = 2;
        private const int LevelUpMaxDamage = 3;
        private const int LevelUpDefense = 1;

        public Player()
        {
            Name = "Player";
            Level = 1;
        }

        public int Gold { get; set; } = 0;
        public int Experience { get; set; } = 0;
        public int ExperienceToNextLevel { get; set; } = 100;
        public int SkillPoints { get; set; } = 0;
        public int StatPoints { get; set; } = 0;

        public int MP { get; set; }
        public int MaxMP { get; set; }

        // Các kỹ năng được thêm vào danh sách khi mở khóa trong Skill Tree.
        public List<Skill> Skills { get; set; } = new List<Skill>();
        public SkillTree SkillTree { get; set; } = new SkillTree();
        public Inventory Inventory { get; set; } = new Inventory();

        public int AllocatedHealth { get; set; }
        public int AllocatedAttack { get; set; }
        public int AllocatedDefense { get; set; }

        public Item EquippedWeapon { get; set; }
        public Item EquippedArmor { get; set; }

        public int EffectiveMinDamage => MinDamage + (EquippedWeapon?.MinDamageBonus ?? 0);
        public int EffectiveMaxDamage => MaxDamage + (EquippedWeapon?.MaxDamageBonus ?? 0);
        public override int EffectiveDefense => Defense + (EquippedArmor?.DefenseBonus ?? 0);

        public override int RollDamage()
        {
            int minimum = Math.Max(0, EffectiveMinDamage);
            int maximum = Math.Max(minimum, EffectiveMaxDamage);
            return Random.Shared.Next(minimum, maximum + 1);
        }

        public bool TrySpendMana(int amount)
        {
            if (amount < 0 || MP < amount)
                return false;

            MP -= amount;
            return true;
        }

        public int RestoreMana(int amount)
        {
            int restored = Math.Clamp(amount, 0, Math.Max(0, MaxMP - MP));
            MP += restored;
            return restored;
        }

        public bool HasPassive(PassiveSkill passive)
        {
            return SkillTree != null && SkillTree.HasPassive(passive);
        }

        public bool AddStatPoint(StatType stat)
        {
            if (StatPoints <= 0)
                return false;

            switch (stat)
            {
                case StatType.Health:
                    StatPoints--;
                    AllocatedHealth++;
                    MaxHP += HealthPerPoint;
                    HP = Math.Min(MaxHP, HP + HealthPerPoint);
                    return true;

                case StatType.Attack:
                    StatPoints--;
                    AllocatedAttack++;
                    MinDamage += AttackPerPoint;
                    MaxDamage += AttackPerPoint;
                    return true;

                case StatType.Defense:
                    StatPoints--;
                    AllocatedDefense++;
                    Defense += DefensePerPoint;
                    return true;

                default:
                    return false;
            }
        }

        public bool LearnSkillNode(SkillTreeNode node)
        {
            if (node == null || SkillTree == null ||
                SkillTree.GetLockReason(node, SkillPoints) != null)
            {
                return false;
            }

            node.IsLearned = true;
            SkillPoints--;

            MaxHP += node.BonusMaxHP;
            HP = Math.Min(MaxHP, HP + node.BonusMaxHP);
            MinDamage += node.BonusDamage;
            MaxDamage += node.BonusDamage;
            Defense += node.BonusDefense;

            if (node.UnlockedSkill != null && Skills != null &&
                !Skills.Exists(skill => skill.Id == node.UnlockedSkill.Id))
            {
                Skills.Add(node.UnlockedSkill);
            }

            return true;
        }

        public void LevelUp()
        {
            Level++;
            SkillPoints++;
            StatPoints += StatPointsPerLevel;

            MaxHP += LevelUpMaxHP;
            MaxMP += LevelUpMaxMP;
            MinDamage += LevelUpMinDamage;
            MaxDamage += LevelUpMaxDamage;
            Defense += LevelUpDefense;

            HP = MaxHP;
            MP = MaxMP;
            ExperienceToNextLevel = (int)Math.Ceiling(Math.Max(1, ExperienceToNextLevel) * 1.5);
        }
    }
}

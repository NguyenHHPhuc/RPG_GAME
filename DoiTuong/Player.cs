using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.DoiTuong
{
    public enum StatType
    {
        Health,
        Attack,
        Defense
    }

    public class Player : Character
    {
        public const int StatPointsPerLevel = 5;

        // Chỉ số nhận được cho mỗi điểm cộng vào
        public const int HealthPerPoint = 10;
        public const int AttackPerPoint = 2;
        public const int DefensePerPoint = 1;

        // Chỉ số tự tăng mỗi lần lên cấp
        private const int LevelUpMaxHP = 20;
        private const int LevelUpMaxMP = 10;
        private const int LevelUpMinDamage = 2;
        private const int LevelUpMaxDamage = 3;
        private const int LevelUpDefense = 1;

        public int Gold { get; set; } = 0;
        public int Experience { get; set; } = 0;
        public int ExperienceToNextLevel { get; set; } = 100;
        public int MP { get; set; }
        public int MaxMP { get; set; }

        public int SkillPoints { get; set; } = 0;
        public int StatPoints { get; set; } = 0;

        // Số điểm đã cộng vào từng loại, cần cho ResetBuild
        public int AllocatedHealth { get; set; }
        public int AllocatedAttack { get; set; }
        public int AllocatedDefense { get; set; }

        public SkillTree SkillTree { get; set; } = new SkillTree();
        public List<Skill> Skills { get; set; } = new List<Skill>();
        public Inventory Inventory { get; set; } = new Inventory();

        public Item EquippedWeapon { get; set; }
        public int EffectiveMinDamage => MinDamage + (EquippedWeapon == null ? 0 : EquippedWeapon.MinDamageBonus);
        public int EffectiveMaxDamage => MaxDamage + (EquippedWeapon == null ? 0 : EquippedWeapon.MaxDamageBonus);
        public Item EquippedArmor { get; set; }

        public override int EffectiveDefense => Defense + (EquippedArmor == null ? 0 : EquippedArmor.DefenseBonus);

        public override int RollDamage()
        {
            return Random.Shared.Next(EffectiveMinDamage, EffectiveMaxDamage + 1);
        }

        public int RestoreMana(int amount)
        {
            int restored = Math.Clamp(amount, 0, Math.Max(0, MaxMP - MP));
            MP += restored;
            return restored;
        }

        public bool TrySpendMana(int amount)
        {
            if (MP < amount)
                return false;

            MP -= amount;
            return true;
        }

        public bool HasPassive(PassiveSkill passive)
        {
            return SkillTree.HasPassive(passive);
        }

        public bool AddStatPoint(StatType stat)
        {
            if (StatPoints <= 0)
                return false;

            StatPoints--;
            switch (stat)
            {
                case StatType.Health:
                    AllocatedHealth++;
                    MaxHP += HealthPerPoint;
                    HP += HealthPerPoint;
                    break;
                case StatType.Attack:
                    AllocatedAttack++;
                    MinDamage += AttackPerPoint;
                    MaxDamage += AttackPerPoint;
                    break;
                case StatType.Defense:
                    AllocatedDefense++;
                    Defense += DefensePerPoint;
                    break;
            }
            return true;
        }

        public bool LearnSkillNode(SkillTreeNode node)
        {
            if (SkillTree.GetLockReason(node, SkillPoints) != null)
                return false;

            node.IsLearned = true;
            SkillPoints--;

            MaxHP += node.BonusMaxHP;
            HP += node.BonusMaxHP;
            MinDamage += node.BonusDamage;
            MaxDamage += node.BonusDamage;
            Defense += node.BonusDefense;

            if (node.UnlockedSkill != null)
                Skills.Add(node.UnlockedSkill);
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

            ExperienceToNextLevel = (int)Math.Ceiling(ExperienceToNextLevel * 1.5);
        }

        // Admin: reset trước để không bị âm điểm
        public bool LevelDown()
        {
            if (Level <= 1)
                return false;

            ResetBuild();

            Level--;
            SkillPoints = Math.Max(0, SkillPoints - 1);
            StatPoints = Math.Max(0, StatPoints - StatPointsPerLevel);

            MaxHP -= LevelUpMaxHP;
            MaxMP -= LevelUpMaxMP;
            MinDamage -= LevelUpMinDamage;
            MaxDamage -= LevelUpMaxDamage;
            Defense -= LevelUpDefense;

            HP = Math.Min(HP, MaxHP);
            MP = Math.Min(MP, MaxMP);
            Experience = 0;
            ExperienceToNextLevel = (int)Math.Round(ExperienceToNextLevel / 1.5);
            return true;
        }

        // Admin: trả lại toàn bộ điểm đã dùng
        public void ResetBuild()
        {
            MaxHP -= AllocatedHealth * HealthPerPoint;
            MinDamage -= AllocatedAttack * AttackPerPoint;
            MaxDamage -= AllocatedAttack * AttackPerPoint;
            Defense -= AllocatedDefense * DefensePerPoint;
            StatPoints += AllocatedHealth + AllocatedAttack + AllocatedDefense;
            AllocatedHealth = 0;
            AllocatedAttack = 0;
            AllocatedDefense = 0;

            foreach (SkillTreeNode node in SkillTree.Nodes)
            {
                if (!node.IsLearned)
                    continue;

                MaxHP -= node.BonusMaxHP;
                MinDamage -= node.BonusDamage;
                MaxDamage -= node.BonusDamage;
                Defense -= node.BonusDefense;
                if (node.UnlockedSkill != null)
                    Skills.Remove(node.UnlockedSkill);

                node.IsLearned = false;
                SkillPoints++;
            }

            HP = Math.Clamp(HP, 1, MaxHP);
        }
    }
}

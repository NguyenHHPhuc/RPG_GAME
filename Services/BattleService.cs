using System;
using System.Collections.Generic;
using RPG.DoiTuong;

namespace RPG.Services
{
    public class BattleService
    {
        // Trạng thái chỉ tồn tại trong một trận vì BattleControl tạo service mới mỗi trận.
        private bool shieldActive;
        private bool reviveUsed;
        private int bleedDamage;
        private int bleedTurnsLeft;

        #region Đòn đánh

        // Nội tại Liên Kích có thể khiến đòn đánh thường đánh thêm lần thứ hai.
        public int PlayerAttack(Player player, Enemy enemy, out bool hitTwice)
        {
            int damage = Hit(enemy, player.RollDamage());
            hitTwice = player.HasPassive(PassiveSkill.DoubleStrike) &&
                       !enemy.IsDead &&
                       Random.Shared.Next(100) < SkillTree.DoubleStrikeChance;

            if (hitTwice)
                damage += Hit(enemy, player.RollDamage());

            return damage;
        }

        // Trả về 0 nếu đòn bị Khiên Chắn chặn
        public int EnemyAttack(Player player, Enemy enemy)
        {
            if (shieldActive)
            {
                shieldActive = false;
                return 0;
            }

            return Hit(player, enemy.RollDamage());
        }

        // Dùng chung cho mọi đòn đánh, nhận Character nên đánh được cả Player lẫn Enemy.
        // Trừ phòng thủ của mục tiêu, sát thương tối thiểu là 1.
        private static int Hit(Character target, int rawDamage)
        {
            int damage = Math.Max(1, rawDamage - target.EffectiveDefense);
            target.TakeDamage(damage);
            return damage;
        }

        #endregion

        #region Kỹ năng

        public bool CanUseSkill(Player player, Skill skill) =>
            player != null && skill != null && skill.ManaCost >= 0 && player.MP >= skill.ManaCost;

        // Kỹ năng gây sát thương; có thể gắn hiệu ứng chảy máu.
        public int UseSkill(Player player, Enemy enemy, Skill skill)
        {
            if (!CanUseSkill(player, skill) || !player.TrySpendMana(skill.ManaCost))
                return 0;

            if (skill.BleedTurns > 0 && skill.BleedDamage > 0)
            {
                bleedDamage = skill.BleedDamage;
                bleedTurnsLeft = skill.BleedTurns;
            }

            int rawDamage = Random.Shared.Next(skill.MinDamage, Math.Max(skill.MinDamage, skill.MaxDamage) + 1);
            return Hit(enemy, rawDamage);
        }

        public int UseHealSkill(Player player, Skill skill)
        {
            if (!CanUseSkill(player, skill) || !player.TrySpendMana(skill.ManaCost))
                return 0;

            int amount = Math.Max(0, player.MaxHP * skill.HealPercent / 100);
            return player.Heal(amount);
        }

        public bool UseShieldSkill(Player player, Skill skill)
        {
            if (!CanUseSkill(player, skill) || !player.TrySpendMana(skill.ManaCost))
                return false;

            shieldActive = true;
            return true;
        }

        #endregion

        #region Nội tại và hiệu ứng theo lượt

        // Bất Tử: hồi sinh 1 lần mỗi trận
        public bool TryRevive(Player player)
        {
            if (player == null || !player.IsDead || reviveUsed || !player.HasPassive(PassiveSkill.Revive))
                return false;

            reviveUsed = true;
            player.Heal(Math.Max(1, player.MaxHP * SkillTree.RevivePercent / 100));
            return true;
        }

        // Tái Sinh: hồi máu cuối mỗi lượt
        public int ApplyRegeneration(Player player)
        {
            if (player == null || player.IsDead || !player.HasPassive(PassiveSkill.Regeneration))
                return 0;

            return player.Heal(Math.Max(1, player.MaxHP * SkillTree.RegenerationPercent / 100));
        }

        // Chảy máu: quái mất máu đầu mỗi lượt của nó
        public int ApplyBleed(Enemy enemy)
        {
            if (enemy == null || bleedTurnsLeft <= 0 || enemy.IsDead)
                return 0;

            bleedTurnsLeft--;
            return enemy.TakeDamage(bleedDamage);
        }

        #endregion

        #region Phần thưởng

        public int GetExperienceReward(Enemy enemy)
        {
            if (enemy == null)
                return 0;
            return enemy.IsBoss ? 500 : Math.Max(20, enemy.Level * 50);
        }

        public int GetGoldReward(Enemy enemy)
        {
            return enemy == null ? 0 : Math.Max(0, enemy.GoldReward);
        }

        // Cộng EXP, có thể lên nhiều cấp; trả về các dòng thông báo cho nhật ký trận
        public List<string> GainExperience(Player player, int amount)
        {
            List<string> messages = new List<string>();
            if (player == null || amount <= 0)
                return messages;

            if (player.ExperienceToNextLevel <= 0)
                player.ExperienceToNextLevel = 100;

            player.Experience += amount;
            messages.Add($"Bạn nhận được {amount} EXP!");

            while (player.Experience >= player.ExperienceToNextLevel)
            {
                player.Experience -= player.ExperienceToNextLevel;
                player.LevelUp();
                messages.Add($"LÊN CẤP! Bạn đã đạt cấp {player.Level}!");
                messages.Add($"HP và MP đã hồi đầy. Nhận 1 điểm kỹ năng và {Player.StatPointsPerLevel} điểm chỉ số.");
            }

            messages.Add($"EXP hiện tại: {player.Experience}/{player.ExperienceToNextLevel}");
            return messages;
        }

        #endregion
    }
}

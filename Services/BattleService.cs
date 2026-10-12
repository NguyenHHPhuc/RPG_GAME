using System;
using System.Collections.Generic;
using System.Text;
using RPG.DoiTuong;
namespace RPG.Services
{
    public class BattleService
    {
        public int PlayerAttack(Player player, Enemy enemy)
        {
            // Lấy sát thương sau khi tính vũ khí đang trang bị
            int damage = Random.Shared.Next(
                player.EffectiveMinDamage,
                player.EffectiveMaxDamage + 1);
            // Trừ phòng thủ của quái vật
            damage -= enemy.Defense;

            // Sát thương tối thiểu là 1
            if (damage < 1)
                damage = 1;

            enemy.HP -= damage;

            // Không để HP của quái vật xuống dưới 0
            if (enemy.HP < 0)
                enemy.HP = 0;

            return damage;
        }

        public int EnemyAttack(Player player, Enemy enemy)
        {
            int damage = Random.Shared.Next(enemy.MinDamage, enemy.MaxDamage + 1);

            // Trừ phòng thủ của nhân vật sau khi tính giáp
            damage -= player.EffectiveDefense;

            // Sát thương tối thiểu là 1
            if (damage < 1)
                damage = 1;

            player.HP -= damage;

            // Không để HP của nhân vật xuống dưới 0
            if (player.HP < 0)
                player.HP = 0;

            return damage;
        }
        public bool IsEnemyDefeated(Enemy enemy)
        {
            return enemy.HP <= 0;
        }

        public bool IsPlayerDefeated(Player player)
        {
            return player.HP <= 0;
        }
        public bool CanUseSkill(Player player, Skill skill)
        {
            return player.MP >= skill.ManaCost;
        }
        public int UseSkill(Player player, Enemy enemy, Skill skill)
        {
            if (!CanUseSkill(player, skill))
                return 0;

            player.MP -= skill.ManaCost;

            int damage = Random.Shared.Next(skill.MinDamage, skill.MaxDamage + 1);
            damage -= enemy.Defense;

            if (damage < 1)
                damage = 1;

            enemy.HP -= damage;

            if (enemy.HP < 0)
                enemy.HP = 0;

            return damage;
        }
        public int GetExperienceReward(Enemy enemy)
        {
            if (enemy == null)
                return 0;

            // Boss có phần thưởng EXP cố định
            if (enemy.IsBoss)
                return 500;

            // Quái thường nhận EXP theo cấp độ
            return Math.Max(20, enemy.Level * 50);
        }

        // Cộng EXP và xử lý lên cấp
        public List<string> GainExperience(Player player, int amount)
        {
            List<string> messages = new List<string>();

            if (player == null || amount <= 0)
                return messages;

            // Phòng trường hợp cấu hình EXP lên cấp không hợp lệ
            if (player.ExperienceToNextLevel <= 0)
                player.ExperienceToNextLevel = 100;

            // Cộng EXP nhận được
            player.Experience += amount;

            messages.Add($"Bạn nhận được {amount} EXP!");

            // Cho phép lên nhiều cấp nếu EXP nhận được đủ lớn
            while (player.Experience >= player.ExperienceToNextLevel)
            {
                // Trừ EXP dùng để lên cấp
                player.Experience -= player.ExperienceToNextLevel;

                player.Level++;

                // Nhận một điểm kỹ năng
                player.SkillPoints++;

                // Tăng chỉ số nhân vật
                player.MaxHP += 20;
                player.MaxMP += 10;
                player.MinDamage += 2;
                player.MaxDamage += 3;
                player.Defense += 1;

                // Hồi đầy HP và MP khi lên cấp
                player.HP = player.MaxHP;
                player.MP = player.MaxMP;

                // Tăng lượng EXP cần thiết cho cấp tiếp theo
                player.ExperienceToNextLevel = (int)Math.Ceiling(
                    player.ExperienceToNextLevel * 1.5
                );

                messages.Add(
                    $"LÊN CẤP! Bạn đã đạt cấp {player.Level}!"
                );

                messages.Add(
                    "HP và MP đã hồi đầy. Bạn nhận được 1 điểm kỹ năng."
                );
            }

            messages.Add(
                $"EXP hiện tại: {player.Experience}/{player.ExperienceToNextLevel}"
            );

            return messages;
        }
        public int GetGoldReward(Enemy enemy)
        {
            if (enemy == null)
                return 0;

            // Không cho phần thưởng Gold bị âm
            return Math.Max(0, enemy.GoldReward);
        }
    }
}

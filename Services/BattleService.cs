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
            int damage = Random.Shared.Next(player.MinDamage, player.MaxDamage + 1);
            damage -= enemy.Defense;
            if (damage < 1) damage = 1; 
            
            enemy.HP -= damage;

            if(enemy.HP < 0) enemy.HP = 0; 
            return damage;
        }

        public int EnemyAttack(Player player, Enemy enemy)
        {
            int damage = Random.Shared.Next(enemy.MinDamage, enemy.MaxDamage + 1);
            damage -= player.Defense;

            if (damage < 1) damage = 1;

            player.HP -= damage;

            if (player.HP < 0) player.HP = 0;

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
    }
}

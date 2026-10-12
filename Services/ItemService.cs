using System;
using System.Collections.Generic;
using System.Text;
using RPG.DoiTuong;
namespace RPG.Services
{
    public class ItemService
    {
        public bool UseItem(Player player, Item item)
        {
            if (player == null || item == null)
                return false;

            if (item.Quantity <= 0)
                return false;

            bool used = false;

            // Hồi HP
            if (item.HealAmount > 0 && player.HP < player.MaxHP)
            {
                player.HP += item.HealAmount;

                if (player.HP > player.MaxHP)
                    player.HP = player.MaxHP;

                used = true;
            }

            // Hồi MP
            if (item.ManaRestore > 0 && player.MP < player.MaxMP)
            {
                player.MP += item.ManaRestore;

                if (player.MP > player.MaxMP)
                    player.MP = player.MaxMP;

                used = true;
            }

            if (!used)
                return false;

            player.Inventory.RemoveItem(item);

            return true;
        }
        public int UseBomb(Player player, Enemy enemy, Item item)
        {
            if (player == null || enemy == null || item == null)
                return 0;

            if (player.Inventory == null ||
                item.Quantity <= 0 ||
                item.Damage <= 0 ||
                enemy.HP <= 0)
            {
                return 0;
            }

            // Sát thương Bomb gây ra thực tế không vượt quá HP còn lại
            int actualDamage = Math.Min(enemy.HP, item.Damage);

            enemy.HP -= actualDamage;

            // Tiêu hao một Bomb
            player.Inventory.RemoveItem(item);

            return actualDamage;
        }
    }
}

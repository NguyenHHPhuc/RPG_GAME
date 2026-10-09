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
    }
}

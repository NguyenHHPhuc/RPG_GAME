using System;
using System.Collections.Generic;
using System.Text;
using RPG.DoiTuong;
namespace RPG.Services
{
    public class ItemService
    {
        // Bản dùng cho InventoryControl
        public bool UseItem(Player player, Item item)
        {
            return UseItem(player, item, out _, out _);
        }

        // Trả về thêm lượng HP/MP thực sự được hồi để BattleControl hiển thị
        public bool UseItem(Player player, Item item, out int healedHp, out int restoredMp)
        {
            healedHp = 0;
            restoredMp = 0;

            if (player == null || item == null || item.Quantity <= 0)
                return false;

            healedHp = player.Heal(item.HealAmount);
            restoredMp = player.RestoreMana(item.ManaRestore);

            // HP và MP đều đã đầy thì không tốn vật phẩm
            if (healedHp == 0 && restoredMp == 0)
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
                enemy.IsDead)
            {
                return 0;
            }

            // Sát thương Bomb gây ra thực tế không vượt quá HP còn lại
            int actualDamage = enemy.TakeDamage(item.Damage);

            // Tiêu hao một Bomb
            player.Inventory.RemoveItem(item);

            return actualDamage;
        }
    }
}

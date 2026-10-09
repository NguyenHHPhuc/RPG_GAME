using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;
using System.Linq;
namespace RPG.DoiTuong
{
    public class Inventory
    {
        public List<Item> Items { get; set; } = new List<Item>();
        public Item GetItem(string itemId)
        {
            return Items.FirstOrDefault(x => x.Id == itemId);
        }
        public void RemoveItem(Item item)
        {
            if (item == null)
                return;

            if (item.Quantity > 0)
                item.Quantity--;

            if (item.Quantity <= 0)
                Items.Remove(item);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.DoiTuong
{
    public class Item
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public int Quantity { get; set; }

        public int HealAmount { get; set; }
        public int ManaRestore { get; set; }
    }
}

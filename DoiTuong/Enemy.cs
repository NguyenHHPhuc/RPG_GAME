using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.DoiTuong
{
    public class Enemy
    {
        public string Id { get; set; }
        public string MapId { get; set; }
        public string Name { get; set; }
        public string MobType { get; set; } = "Zombie";

        public int Level { get; set; }

        public int HP { get; set; }
        public int MaxHP { get; set; }

        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int Defense { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
    }
}

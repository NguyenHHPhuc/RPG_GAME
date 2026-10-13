using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Generic;
namespace RPG.DoiTuong
{
    public class Player
    {
        public string Name { get; set; }
        public string ClassName { get; set; } = "Chiến binh";

        public int Level { get; set; }
        public int Experience { get; set; }
        public int MaxExperience { get; set; } = 100;

        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int MP { get; set; }
        public int MaxMP { get; set; }

        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int Defense { get; set; }

        // Vị trí của người chơi trên bản đồ
        public int X { get; set; }
        public int Y { get; set; }
        public List<Skill> Skills { get; set; } = new List<Skill>();
        public Inventory Inventory { get; set; } = new Inventory();
    }
}

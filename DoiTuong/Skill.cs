using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.DoiTuong
{
    public class Skill
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int ManaCost { get; set; }
    }
}

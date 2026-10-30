using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.DoiTuong
{
    // Name, Level, HP, sát thương, phòng thủ, X/Y... kế thừa từ Character
    public class Enemy : Character
    {
        public string Id { get; set; }
        public string MapId { get; set; }
        public bool IsBoss { get; set; } = false;
        public int GoldReward { get; set; }
    }
}

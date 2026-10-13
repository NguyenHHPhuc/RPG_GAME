using RPG.DoiTuong;

namespace RPG.Mobs
{
    // Chỉ số cơ bản của zombie, có thể điều chỉnh riêng mà không ảnh hưởng skeleton.
    public class Zombie : Enemy
    {
        public Zombie(string id, string mapId, int x, int y)
        {
            Id = id;
            MapId = mapId;
            Name = "Zombie";
            MobType = "Zombie";
            Level = 1;
            HP = 45;
            MaxHP = 45;
            MinDamage = 6;
            MaxDamage = 11;
            Defense = 3;
            X = x;
            Y = y;
        }
    }
}

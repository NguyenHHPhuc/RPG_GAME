using RPG.DoiTuong;

namespace RPG.Mobs
{
    // Skeleton có ít máu hơn một số quái nhưng gây sát thương nhỉnh hơn zombie.
    public class Skeleton : Enemy
    {
        public Skeleton(string id, string mapId, int x, int y)
        {
            Id = id;
            MapId = mapId;
            Name = "Skeleton";
            MobType = "Skeleton";
            Level = 1;
            HP = 55;
            MaxHP = 55;
            MinDamage = 8;
            MaxDamage = 14;
            Defense = 4;
            X = x;
            Y = y;
        }
    }
}

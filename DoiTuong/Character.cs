using System;

namespace RPG.DoiTuong
{
    // Lớp cha của Player và Enemy
    public abstract class Character
    {
        public string Name { get; set; }
        public int Level { get; set; }

        public int HP { get; set; }
        public int MaxHP { get; set; }

        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int Defense { get; set; }

        // Vị trí trên bản đồ
        public int X { get; set; }
        public int Y { get; set; }

        public bool IsDead => HP <= 0;

        // Player ghi đè để cộng giáp
        public virtual int EffectiveDefense => Defense;

        // Player ghi đè để cộng vũ khí
        public virtual int RollDamage()
        {
            return Random.Shared.Next(MinDamage, MaxDamage + 1);
        }

        // Trả về lượng HP thực sự mất
        public int TakeDamage(int amount)
        {
            int lost = Math.Clamp(amount, 0, Math.Max(0, HP));
            HP -= lost;
            return lost;
        }

        // Trả về lượng HP thực sự được hồi
        public int Heal(int amount)
        {
            int healed = Math.Clamp(amount, 0, Math.Max(0, MaxHP - HP));
            HP += healed;
            return healed;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using RPG.DoiTuong;

namespace RPG.Services
{
    public sealed class GameSaveSnapshot
    {
        public int FormatVersion { get; set; } = 1;

        public string CurrentMapId { get; set; } = "StartArea";
        public int CurrentDungeonFloor { get; set; } = 1;
        public int HighestUnlockedFloor { get; set; } = 1;
        public List<int> ClearedFloors { get; set; } = new();
        public bool IsGameWon { get; set; }

        public PlayerSnapshot Player { get; set; } = new();
        public List<EnemySnapshot> Enemies { get; set; } = new();

        public static GameSaveSnapshot FromGameState(GameState state)
        {
            if (state == null)
                throw new ArgumentNullException(nameof(state));

            if (state.Player == null)
                throw new InvalidOperationException(
                    "GameState chưa có dữ liệu Player.");

            return new GameSaveSnapshot
            {
                CurrentMapId = state.CurrentMapId ?? "StartArea",
                CurrentDungeonFloor = state.CurrentDungeonFloor,
                HighestUnlockedFloor = state.HighestUnlockedFloor,
                ClearedFloors = state.ClearedFloors?.ToList() ?? new(),
                IsGameWon = state.IsGameWon,

                Player = PlayerSnapshot.FromPlayer(state.Player),

                Enemies = state.Enemies?
                    .Select(EnemySnapshot.FromEnemy)
                    .ToList() ?? new()
            };
        }

        public GameState ToGameState()
        {
            return new GameState
            {
                CurrentMapId = CurrentMapId,
                CurrentDungeonFloor = CurrentDungeonFloor,
                HighestUnlockedFloor = HighestUnlockedFloor,
                ClearedFloors = ClearedFloors?.ToList() ?? new(),
                IsGameWon = IsGameWon,

                Player = Player.ToPlayer(),

                Enemies = Enemies?
                    .Select(item => item.ToEnemy())
                    .ToList() ?? new(),

                // Chỉ lưu ở bản đồ, không lưu giữa trận chiến.
                CurrentEnemy = null!
            };
        }
    }

    public sealed class PlayerSnapshot
    {
        public string Name { get; set; } = "Player";
        public int Level { get; set; }
        public int Gold { get; set; }
        public int Experience { get; set; }
        public int ExperienceToNextLevel { get; set; }

        public int SkillPoints { get; set; }
        public int StatPoints { get; set; }

        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int MP { get; set; }
        public int MaxMP { get; set; }

        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int Defense { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public int AllocatedHealth { get; set; }
        public int AllocatedAttack { get; set; }
        public int AllocatedDefense { get; set; }

        public string? EquippedWeaponId { get; set; }
        public string? EquippedArmorId { get; set; }

        public List<SkillSnapshot> Skills { get; set; } = new();
        public List<ItemSnapshot> Items { get; set; } = new();

        // Lưu những ô đã học bằng Branch và Tier.
        public List<LearnedNodeSnapshot> LearnedNodes { get; set; } = new();

        public static PlayerSnapshot FromPlayer(Player player)
        {
            return new PlayerSnapshot
            {
                Name = player.Name,
                Level = player.Level,
                Gold = player.Gold,
                Experience = player.Experience,
                ExperienceToNextLevel = player.ExperienceToNextLevel,

                SkillPoints = player.SkillPoints,
                StatPoints = player.StatPoints,

                HP = player.HP,
                MaxHP = player.MaxHP,
                MP = player.MP,
                MaxMP = player.MaxMP,

                MinDamage = player.MinDamage,
                MaxDamage = player.MaxDamage,
                Defense = player.Defense,

                X = player.X,
                Y = player.Y,

                AllocatedHealth = player.AllocatedHealth,
                AllocatedAttack = player.AllocatedAttack,
                AllocatedDefense = player.AllocatedDefense,

                EquippedWeaponId = player.EquippedWeapon?.Id,
                EquippedArmorId = player.EquippedArmor?.Id,

                Skills = player.Skills?
                    .Select(SkillSnapshot.FromSkill)
                    .ToList() ?? new(),

                Items = player.Inventory?.Items?
                    .Select(ItemSnapshot.FromItem)
                    .ToList() ?? new(),

                LearnedNodes = player.SkillTree?.Nodes?
                    .Where(node => node.IsLearned)
                    .Select(node => new LearnedNodeSnapshot
                    {
                        Branch = node.Branch,
                        Tier = node.Tier
                    })
                    .ToList() ?? new()
            };
        }

        public Player ToPlayer()
        {
            Inventory inventory = new Inventory
            {
                Items = Items?
                    .Select(item => item.ToItem())
                    .ToList() ?? new()
            };

            Player player = new Player
            {
                Name = Name,
                Level = Level,
                Gold = Gold,
                Experience = Experience,
                ExperienceToNextLevel = ExperienceToNextLevel,

                SkillPoints = SkillPoints,
                StatPoints = StatPoints,

                HP = HP,
                MaxHP = MaxHP,
                MP = MP,
                MaxMP = MaxMP,

                MinDamage = MinDamage,
                MaxDamage = MaxDamage,
                Defense = Defense,

                X = X,
                Y = Y,

                AllocatedHealth = AllocatedHealth,
                AllocatedAttack = AllocatedAttack,
                AllocatedDefense = AllocatedDefense,

                Inventory = inventory,
                Skills = Skills?
                    .Select(skill => skill.ToSkill())
                    .ToList() ?? new(),

                SkillTree = new SkillTree()
            };

            // Khôi phục trạng thái các ô Skill Tree đã học.
            // Không gọi LearnSkillNode(), vì chỉ số đã được khôi phục ở trên.
            foreach (LearnedNodeSnapshot savedNode in LearnedNodes ?? new())
            {
                SkillTreeNode? node = player.SkillTree.Nodes
                    .FirstOrDefault(item =>
                        item.Branch == savedNode.Branch &&
                        item.Tier == savedNode.Tier);

                if (node != null)
                    node.IsLearned = true;
            }

            // Gắn trang bị về đúng Item trong Inventory.
            // Nhờ vậy trang bị vẫn dùng chung đối tượng với túi đồ.
            if (!string.IsNullOrWhiteSpace(EquippedWeaponId))
            {
                player.EquippedWeapon = inventory.Items
                    .FirstOrDefault(item => item.Id == EquippedWeaponId);
            }

            if (!string.IsNullOrWhiteSpace(EquippedArmorId))
            {
                player.EquippedArmor = inventory.Items
                    .FirstOrDefault(item => item.Id == EquippedArmorId);
            }

            return player;
        }
    }

    public sealed class LearnedNodeSnapshot
    {
        public SkillBranch Branch { get; set; }
        public int Tier { get; set; }
    }

    public sealed class SkillSnapshot
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public SkillType Type { get; set; }

        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int ManaCost { get; set; }

        public int HealPercent { get; set; }
        public int BleedDamage { get; set; }
        public int BleedTurns { get; set; }

        public static SkillSnapshot FromSkill(Skill skill)
        {
            return new SkillSnapshot
            {
                Id = skill.Id,
                Name = skill.Name,
                Type = skill.Type,
                MinDamage = skill.MinDamage,
                MaxDamage = skill.MaxDamage,
                ManaCost = skill.ManaCost,
                HealPercent = skill.HealPercent,
                BleedDamage = skill.BleedDamage,
                BleedTurns = skill.BleedTurns
            };
        }

        public Skill ToSkill()
        {
            return new Skill
            {
                Id = Id,
                Name = Name,
                Type = Type,
                MinDamage = MinDamage,
                MaxDamage = MaxDamage,
                ManaCost = ManaCost,
                HealPercent = HealPercent,
                BleedDamage = BleedDamage,
                BleedTurns = BleedTurns
            };
        }
    }

    public sealed class ItemSnapshot
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";

        public int Price { get; set; }
        public int Quantity { get; set; }

        public int HealAmount { get; set; }
        public int ManaRestore { get; set; }
        public int Damage { get; set; }

        public EquipSlot Slot { get; set; }

        public int MinDamageBonus { get; set; }
        public int MaxDamageBonus { get; set; }
        public int DefenseBonus { get; set; }

        public static ItemSnapshot FromItem(Item item)
        {
            return new ItemSnapshot
            {
                Id = item.Id ?? "",
                Name = item.Name ?? "",
                Description = item.Description ?? "",

                Price = item.Price,
                Quantity = item.Quantity,

                HealAmount = item.HealAmount,
                ManaRestore = item.ManaRestore,
                Damage = item.Damage,

                Slot = item.Slot,

                MinDamageBonus = item.MinDamageBonus,
                MaxDamageBonus = item.MaxDamageBonus,
                DefenseBonus = item.DefenseBonus
            };
        }

        public Item ToItem()
        {
            return new Item
            {
                Id = Id,
                Name = Name,
                Description = Description,

                Price = Price,
                Quantity = Quantity,

                HealAmount = HealAmount,
                ManaRestore = ManaRestore,
                Damage = Damage,

                Slot = Slot,

                MinDamageBonus = MinDamageBonus,
                MaxDamageBonus = MaxDamageBonus,
                DefenseBonus = DefenseBonus
            };
        }
    }

    public sealed class EnemySnapshot
    {
        public string Id { get; set; } = "";
        public string MapId { get; set; } = "";
        public string Name { get; set; } = "";

        public bool IsBoss { get; set; }

        public int Level { get; set; }
        public int GoldReward { get; set; }

        public int HP { get; set; }
        public int MaxHP { get; set; }

        public int MinDamage { get; set; }
        public int MaxDamage { get; set; }
        public int Defense { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public static EnemySnapshot FromEnemy(Enemy enemy)
        {
            return new EnemySnapshot
            {
                Id = enemy.Id,
                MapId = enemy.MapId,
                Name = enemy.Name,

                IsBoss = enemy.IsBoss,

                Level = enemy.Level,
                GoldReward = enemy.GoldReward,

                HP = enemy.HP,
                MaxHP = enemy.MaxHP,

                MinDamage = enemy.MinDamage,
                MaxDamage = enemy.MaxDamage,
                Defense = enemy.Defense,

                X = enemy.X,
                Y = enemy.Y
            };
        }

        public Enemy ToEnemy()
        {
            return new Enemy
            {
                Id = Id,
                MapId = MapId,
                Name = Name,

                IsBoss = IsBoss,

                Level = Level,
                GoldReward = GoldReward,

                HP = HP,
                MaxHP = MaxHP,

                MinDamage = MinDamage,
                MaxDamage = MaxDamage,
                Defense = Defense,

                X = X,
                Y = Y
            };
        }
    }
}

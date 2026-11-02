using System;
using System.Collections.Generic;
using System.Text;

namespace RPG.DoiTuong
{
    public class GameState
    {
        public Player Player { get; set; }
        public List<Enemy> Enemies { get; set; } 
        public Enemy CurrentEnemy { get; set; }
        public string CurrentMapId { get; set; }
        public int CurrentDungeonFloor { get; set; } = 1;

        public int HighestUnlockedFloor { get; set; } = 1;

        public List<int> ClearedFloors { get; set; } = new List<int>();

        public bool IsGameWon { get; set; }

        // Phương thức tĩnh để tạo một trò chơi mới với trạng thái mặc định
        // trả về một đối tượng GameState mới với các giá trị mặc định cho Player và CurrentMapId

        //static để có thể gọi phương thức mà không cần tạo một thể hiện của lớp GameState
        public static GameState CreateNewGame()
        {
            return new GameState
            {

                CurrentMapId = "StartArea",

                CurrentDungeonFloor = 1,
                HighestUnlockedFloor = 1,

                ClearedFloors = new List<int>(),

                IsGameWon = false,
                Player = new Player
                {
                    Name = "Player1",
                    Level = 1,
                    Gold = 200,
                    HP = 100,
                    MaxHP = 100,
                    MP = 50,
                    MaxMP = 50,
                    MinDamage = 20,
                    MaxDamage = 50,
                    Defense = 10,
                    X = 170, //145, 135
                    Y = 200, 
                    Skills = new List<Skill>(),
                    Inventory = new Inventory
                    {
                        Items = new List<Item>
                        {
                            new Item
                            {
                                Id = "ITEM_HEALTH_POTION",
                                Name = "Health Potion",
                                Quantity = 5,
                                HealAmount = 50,
                                ManaRestore = 0
                            },
                            new Item
                            {
                                Id = "ITEM_MANA_POTION",
                                Name = "Mana Potion",
                                Quantity = 3,
                                HealAmount = 0,
                                ManaRestore = 30
                            }
                        }
                    }

                },
                Enemies = new List<Enemy>
                {
                    new Enemy
                        {
                            Id = "SLIME_01",
                            MapId = "DungeonFloor1",
                            Name = "Slime",
                            Level = 1,
                            GoldReward = 10,
                            HP = 50,
                            MaxHP = 50,
                            MinDamage = 8,
                            MaxDamage = 15,
                            Defense = 5,
                            X = 380,
                            Y = 250
                        },
                    new Enemy
                    {
                        Id = "SKELETON_01",
                            MapId = "DungeonFloor1",
                            Name = "Skeleton",
                            Level = 1,
                            GoldReward = 15,
                            HP = 60,
                            MaxHP = 60,
                            MinDamage = 10,
                            MaxDamage = 18,
                            Defense = 6,
                            X = 680,
                            Y = 400
                    },
                    new Enemy
                        {
                            Id = "SLIME_02",
                            MapId = "DungeonFloor1",
                            Name = "Slime",
                            Level = 1,
                            GoldReward = 10,
                            HP = 50,
                            MaxHP = 50,
                            MinDamage = 8,
                            MaxDamage = 15,
                            Defense = 5,
                            X = 950,
                            Y = 270
                        },
                    new Enemy
                    {
                        Id = "ZOMBIE_01",
                            MapId = "DungeonFloor1",
                            Name = "Zombie",
                            Level = 1,
                            GoldReward = 15,
                            HP = 60,
                            MaxHP = 60,
                            MinDamage = 10,
                            MaxDamage = 18,
                            Defense = 6,
                            X = 1200,
                            Y = 410
                    },

                    new Enemy
                        {
                            Id = "YETI_01",
                            MapId = "DungeonFloor2",
                            Name = "Yeti",
                            Level = 2,
                            GoldReward = 20,
                            HP = 80,
                            MaxHP = 80,
                            MinDamage = 12,
                            MaxDamage = 20,
                            Defense = 8,
                            X = 350,
                            Y = 230
                        },
                    new Enemy
                        {
                            Id = "YETI_02",
                            MapId = "DungeonFloor2",
                            Name = "Yeti",
                            Level = 2,
                            GoldReward = 20,
                            HP = 80,
                            MaxHP = 80,
                            MinDamage = 12,
                            MaxDamage = 20,
                            Defense = 8,
                            X = 1030,
                            Y = 550
                        },
                    new Enemy
                        {
                            Id = "Demon_01",
                            MapId = "DungeonFloor2",
                            Name = "Demon",
                            Level = 2,
                            GoldReward = 20,
                            HP = 80,
                            MaxHP = 80,
                            MinDamage = 12,
                            MaxDamage = 20,
                            Defense = 8,
                            X = 685,
                            Y = 490
                        },
                    new Enemy
                    {
                            Id = "BOSS_01",
                            MapId = "DungeonFloor2",
                            Name = "boss",
                            IsBoss = true,
                            Level = 2,
                            GoldReward = 25,
                            HP = 90,
                            MaxHP = 90,
                            MinDamage = 14,
                            MaxDamage = 22,
                            Defense = 9,
                            X = 1200,
                            Y = 130
                    }

                     
                },
                CurrentEnemy = null
            };
        }
    }
}

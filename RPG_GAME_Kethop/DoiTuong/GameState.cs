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
                    MinDamage = 90,
                    MaxDamage = 150,
                    Defense = 10,
                    X = 100,
                    Y = 100,
                    // Kỹ năng được mở khóa dần qua Cây kỹ năng
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
                            Id = "E001",
                            MapId = "DungeonFloor1",
                            Name = "Slime",
                            Level = 1,
                            GoldReward = 10,
                            HP = 50,
                            MaxHP = 50,
                            MinDamage = 8,
                            MaxDamage = 15,
                            Defense = 5,
                            X = 400,
                            Y = 250
                        },
                    new Enemy
                    {
                        Id = "E004",
                            MapId = "DungeonFloor1",
                            Name = "Skeleton",
                            Level = 1,
                            GoldReward = 15,
                            HP = 60,
                            MaxHP = 60,
                            MinDamage = 10,
                            MaxDamage = 18,
                            Defense = 6,
                            X = 300,
                            Y = 200
                    },

                    new Enemy
                        {
                            Id = "E002",
                            MapId = "DungeonFloor2",
                            Name = "Yeti",
                            Level = 2,
                            GoldReward = 20,
                            HP = 80,
                            MaxHP = 80,
                            MinDamage = 12,
                            MaxDamage = 20,
                            Defense = 8,
                            X = 400,
                            Y = 250
                        },
                    new Enemy
                    {
                        Id = "E005",
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
                            X = 350,
                            Y = 220
                    }

                     
                },
                CurrentEnemy = null
            };
        }
    }
}

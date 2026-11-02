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
                    MinDamage = 15,
                    MaxDamage = 30,
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
                                Quantity = 2,
                                HealAmount = 30,
                                ManaRestore = 0
                            },
                            new Item
                            {
                                Id = "ITEM_MANA_POTION",
                                Name = "Mana Potion",
                                Quantity = 2,
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
                            Level = 2,
                            GoldReward = 40,
                            HP = 50,
                            MaxHP = 50,
                            MinDamage = 15,
                            MaxDamage = 20,
                            Defense = 5,
                            X = 380,
                            Y = 250
                        },
                    new Enemy
                    {
                        Id = "SKELETON_01",
                            MapId = "DungeonFloor1",
                            Name = "Skeleton",
                            Level = 3,
                            GoldReward = 50,
                            HP = 80,
                            MaxHP = 80,
                            MinDamage = 20,
                            MaxDamage = 25,
                            Defense = 10,
                            X = 680,
                            Y = 400
                    },
                    new Enemy
                        {
                            Id = "SLIME_02",
                            MapId = "DungeonFloor1",
                            Name = "Slime",
                            Level = 7,
                            GoldReward = 60,
                            HP = 100,
                            MaxHP = 100,
                            MinDamage = 25,
                            MaxDamage = 33,
                            Defense = 13,
                            X = 950,
                            Y = 270
                        },
                    new Enemy
                    {
                        Id = "ZOMBIE_01",
                            MapId = "DungeonFloor1",
                            Name = "Zombie",
                            Level = 10,
                            GoldReward = 70,
                            HP = 140,
                            MaxHP = 140,
                            MinDamage = 30,
                            MaxDamage = 40,
                            Defense = 18,
                            X = 1200,
                            Y = 410
                    },

                    new Enemy
                        {
                            Id = "YETI_01",
                            MapId = "DungeonFloor2",
                            Name = "Yeti",
                            Level = 17,
                            GoldReward = 100,
                            HP = 160,
                            MaxHP = 160,
                            MinDamage = 25,
                            MaxDamage = 35,
                            Defense = 20,
                            X = 350,
                            Y = 230
                        },
                    new Enemy
                        {
                            Id = "YETI_02",
                            MapId = "DungeonFloor2",
                            Name = "Yeti",
                            Level = 39,
                            GoldReward = 20,
                            HP = 200,
                            MaxHP = 200,
                            MinDamage = 25,
                            MaxDamage = 35,
                            Defense = 23,
                            X = 1030,
                            Y = 550
                        },
                    new Enemy
                        {
                            Id = "Demon_01",
                            MapId = "DungeonFloor2",
                            Name = "Demon",
                            Level = 25,
                            GoldReward = 20,
                            HP = 180,
                            MaxHP = 180,
                            MinDamage = 35,
                            MaxDamage = 40,
                            Defense = 22,
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
                            HP = 300,
                            MaxHP = 300,
                            MinDamage = 30,
                            MaxDamage = 60,
                            Defense = 35,
                            X = 1200,
                            Y = 130
                    }

                     
                },
                CurrentEnemy = null
            };
        }
    }
}

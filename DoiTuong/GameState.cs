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

        // Phương thức tĩnh để tạo một trò chơi mới với trạng thái mặc định
        // trả về một đối tượng GameState mới với các giá trị mặc định cho Player và CurrentMapId

              //static để có thể gọi phương thức mà không cần tạo một thể hiện của lớp GameState
        public static GameState CreateNewGame()
        {
            return new GameState
            {

                CurrentMapId = "Map01",
                Player = new Player
                {
                    Name = "Player1",
                    Level = 1,
                    HP = 100,
                    MaxHP = 100,
                    MP = 50,
                    MaxMP = 50,
                    MinDamage = 5,
                    MaxDamage = 15,
                    Defense = 10,
                    X = 100,
                    Y = 100,
                    Skills = new List<Skill>
                    {
                        new Skill
                        {
                            Id = "SKILL_FIREBALL",
                            Name = "Fireball",
                            MinDamage = 30,
                            MaxDamage = 50,
                            ManaCost = 15
                        },
                        new Skill
                        {
                            Id = "SKILL_ICE_SHARD",
                            Name = "Ice Shard",
                            MinDamage = 25,
                            MaxDamage = 45,
                            ManaCost = 5
                        }
                    },
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
                            MapId = "Map01",
                            Name = "Slime",
                            Level = 1,
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
                            Id = "E002",
                            MapId = "Map02",
                            Name = "Goblin",
                            Level = 2,
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
                            Id = "E003",
                            MapId = "Map03",
                            Name = "Bat",
                            Level = 3,
                            HP = 100,
                            MaxHP = 100,
                            MinDamage = 15,
                            MaxDamage = 25,
                            Defense = 10,
                            X = 400,
                            Y = 250
                        }
                },
                CurrentEnemy = null
            };
        }
    }
}

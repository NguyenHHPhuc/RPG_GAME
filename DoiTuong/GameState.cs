using System;
using System.Collections.Generic;
using System.Text;
using RPG.Mobs;

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
        public static GameState CreateNewGame(string playerName = "Player1", string className = "Chiến binh")
        {
            // Mỗi lớp có bộ chỉ số khởi đầu riêng để lựa chọn nhân vật có ý nghĩa.
            int maxHP = 100;
            int maxMP = 50;
            int minDamage = 5;
            int maxDamage = 15;
            int defense = 10;

            switch (className)
            {
                case "Cung thủ":
                    maxHP = 85;
                    maxMP = 60;
                    minDamage = 10;
                    maxDamage = 20;
                    defense = 6;
                    break;
                case "Đỡ đòn":
                    maxHP = 150;
                    maxMP = 35;
                    minDamage = 4;
                    maxDamage = 11;
                    defense = 18;
                    break;
                default: // Chiến binh
                    maxHP = 120;
                    maxMP = 45;
                    minDamage = 8;
                    maxDamage = 17;
                    defense = 11;
                    break;
            }

            return new GameState
            {

                CurrentMapId = "Map01",
                Player = new Player
                {
                    Name = playerName,
                    ClassName = className,
                    Level = 1,
                    HP = maxHP,
                    MaxHP = maxHP,
                    MP = maxMP,
                    MaxMP = maxMP,
                    MinDamage = minDamage,
                    MaxDamage = maxDamage,
                    Defense = defense,
                    X = 100,
                    Y = 100,
                    // Nhân vật mới chưa học kỹ năng nào.
                    Skills = new List<Skill>(),
                    Inventory = new Inventory
                    {
                        Items = new List<Item>
                        {
                            new Item
                            {
                                Id = "ITEM_HEALTH_POTION",
                                Name = "Bình máu",
                                Quantity = 5,
                                HealAmount = 50,
                                ManaRestore = 0
                            },
                            new Item
                            {
                                Id = "ITEM_MANA_POTION",
                                Name = "Bình năng lượng",
                                Quantity = 3,
                                HealAmount = 0,
                                ManaRestore = 30
                            }
                        }
                    }

                },
                // Map 1 cần hạ đủ năm quái trước khi cổng được mở.
                Enemies = CreateStartingEnemies(),
                CurrentEnemy = null
            };
        }

        public void ResetCurrentMapEnemies()
        {
            // Chỉ tạo lại quái của bản đồ đang mở, giữ nguyên tiến độ và nhân vật.
            List<Enemy> startingEnemies = CreateStartingEnemies();
            Enemies.RemoveAll(enemy => enemy.MapId == CurrentMapId);
            Enemies.AddRange(startingEnemies.Where(enemy => enemy.MapId == CurrentMapId));
            CurrentEnemy = null;
        }

        private static List<Enemy> CreateStartingEnemies()
        {
            return new List<Enemy>
            {
                new Zombie("Z001", "Map01", 180, 150),
                new Skeleton("S001", "Map01", 680, 150),
                new Zombie("Z002", "Map01", 650, 370),
                new Skeleton("S002", "Map01", 170, 420),
                new Zombie("Z003", "Map01", 540, 500),
                new Zombie("Z004", "Map02", 400, 260),
                new Skeleton("S003", "Map03", 600, 260)
            };
        }
    }
}

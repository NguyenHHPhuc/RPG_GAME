using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;
using RPG.DoiTuong;
namespace RPG.Services
{
    public class MapService
    {
        private readonly List<MapData> maps = new List<MapData>
        {
            new MapData
            {
                Id = "StartArea",
                Name = "START AREA",
                IsDungeon = false,
                FloorNumber = 0,
                BackgroundColor = Color.LightBlue,
                TextColor = Color.Black,
                // Danh sách các chướng ngại vật trong Map.
                ExitVerticalRatio = 0.5f,
                NextMapId = "DungeonFloor1",
                EntryX = 30,
                EntryY = 200
            },
            new MapData
            {
                Id = "DungeonFloor1",
                Name = "FLOOR 1 - FOREST",
                IsDungeon = true,
                FloorNumber = 1,

                BackgroundColor = Color.DarkSeaGreen,
                TextColor = Color.Black,
                Obstacles = new List<MapObstacle>
                {
                    new MapObstacle
                    {
                        Id = "ForestRock01",
                        X = 180,
                        Y = 90,
                        Width = 100,
                        Height = 45,
                        Color = Color.DarkOliveGreen
                    },

                    new MapObstacle
                    {
                        Id = "ForestTree01",
                        X = 280,
                        Y = 260,
                        Width = 60,
                        Height = 90,
                        Color = Color.ForestGreen
                    },

                    new MapObstacle
                    {
                        Id = "ForestRock02",
                        X = 520,
                        Y = 130,
                        Width = 120,
                        Height = 45,
                        Color = Color.SaddleBrown
                    }
                },

                ExitVerticalRatio = 0.5f,

                NextMapId = "DungeonFloor2",

                EntryX = 30,
                EntryY = 200
            },

            new MapData
            {
                Id = "DungeonFloor2",
                Name = "FLOOR 2 - MOUNTAIN",
                IsDungeon = true,
                FloorNumber = 2,

                BackgroundColor = Color.LightSteelBlue,
                TextColor = Color.Black,
                Obstacles = new List<MapObstacle>
                {
                    new MapObstacle
                    {
                        Id = "MountainRock01",
                        X = 160,
                        Y = 130,
                        Width = 80,
                        Height = 80,
                        Color = Color.Gray
                    },

                    new MapObstacle
                    {
                        Id = "MountainRock02",
                        X = 300,
                        Y = 80,
                        Width = 45,
                        Height = 150,
                        Color = Color.DarkGray
                    },

                    new MapObstacle
                    {
                        Id = "MountainRock03",
                        X = 450,
                        Y = 280,
                        Width = 130,
                        Height = 45,
                        Color = Color.DimGray
                    }
                },

                ExitVerticalRatio = 0.2f,

                NextMapId = "DungeonFloor3",

                EntryX = 30,
                EntryY = 100
            },

            new MapData
            {
                Id = "DungeonFloor3",
                Name = "FLOOR 3 - CAVE",
                IsDungeon = true,
                FloorNumber = 3,

                BackgroundColor = Color.DimGray,
                TextColor = Color.White,
                Obstacles = new List<MapObstacle>
                {
                    new MapObstacle
                    {
                        Id = "CaveWall01",
                        X = 180,
                        Y = 100,
                        Width = 220,
                        Height = 35,
                        Color = Color.DarkSlateGray
                    },

                    new MapObstacle
                    {
                        Id = "CaveWall02",
                        X = 180,
                        Y = 135,
                        Width = 35,
                        Height = 105,
                        Color = Color.DarkSlateGray
                    },

                    new MapObstacle
                    {
                        Id = "CaveWall03",
                        X = 180,
                        Y = 240,
                        Width = 190,
                        Height = 35,
                        Color = Color.DarkSlateGray
                    },

                    new MapObstacle
                    {
                        Id = "CaveWall04",
                        X = 430,
                        Y = 200,
                        Width = 140,
                        Height = 35,
                        Color = Color.DarkSlateGray
                    }
                },

                ExitVerticalRatio = 0.8f,

                NextMapId = "DungeonFloor4",

                EntryX = 30,
                EntryY = 200
            },
            new MapData
            {
                Id = "DungeonFloor4",
                Name = "FLOOR 4 - LAVA CITADEL",

                IsDungeon = true,
                FloorNumber = 4,

                BackgroundColor = Color.Sienna,
                TextColor = Color.White,

                Obstacles = new List<MapObstacle>
                {
                    new MapObstacle
                    {
                        Id = "LavaRock01",
                        X = 160,
                        Y = 100,
                        Width = 90,
                        Height = 55,
                        Color = Color.DarkRed
                    },

                    new MapObstacle
                    {
                        Id = "LavaRock02",
                        X = 280,
                        Y = 290,
                        Width = 120,
                        Height = 40,
                        Color = Color.OrangeRed
                    },

                    new MapObstacle
                    {
                        Id = "LavaPillar01",
                        X = 520,
                        Y = 140,
                        Width = 45,
                        Height = 130,
                        Color = Color.DarkSlateGray
                    }
                },

                ExitVerticalRatio = 0.35f,
                NextMapId = "DungeonFloor5",

                EntryX = 30,
                EntryY = 200
            },
            new MapData
            {
                Id = "DungeonFloor5",
                Name = "FLOOR 5 - DRAGON'S LAIR",

                IsDungeon = true,
                FloorNumber = 5,

                BackgroundColor = Color.Maroon,
                TextColor = Color.White,

                Obstacles = new List<MapObstacle>
                {
                    new MapObstacle
                    {
                        Id = "DragonRock01",
                        X = 150,
                        Y = 100,
                        Width = 100,
                        Height = 45,
                        Color = Color.DarkSlateGray
                    },

                    new MapObstacle
                    {
                        Id = "DragonRock02",
                        X = 250,
                        Y = 310,
                        Width = 90,
                        Height = 50,
                        Color = Color.Firebrick
                    },

                    new MapObstacle
                    {
                        Id = "DragonPillar01",
                        X = 390,
                        Y = 100,
                        Width = 55,
                        Height = 100,
                        Color = Color.DarkRed
                    }
                },

                ExitVerticalRatio = 0.5f,
                NextMapId = null,

                EntryX = 30,
                EntryY = 200
            }

        };

        public MapData GetMap(string mapId)
        {
            return maps.FirstOrDefault(
                map => map.Id == mapId
            );
        }
    }
}

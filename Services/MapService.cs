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
                Obstacles = new List<MapObstacle>
                {
                    new MapObstacle
                    {
                        Id = "StartArea_rightup01",
                        X = 600,
                        Y = 0,
                        Width = 400,
                        Height = 200,
                        Color = Color.Gray
                    },
                    new MapObstacle
                    {
                        Id = "StartArea_rightup02",
                        X = 700,
                        Y = 200,
                        Width = 330,
                        Height = 50,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "StartArea_rightup03",
                        X = 750,
                        Y = 250,
                        Width = 280,
                        Height = 50,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "StartArea_rightup04",
                        X = 780,
                        Y = 270,
                        Width = 250,
                        Height = 50,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "StartArea_Shop",
                        X = 60,
                        Y = 230,
                        Width = 430,
                        Height = 20,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "StartArea_Shop2",
                        X = 50,
                        Y = 250,
                        Width = 430,
                        Height = 20,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "StartArea_Shop3",
                        X = 350,
                        Y = 270,
                        Width = 20,
                        Height = 60,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "StartArea_Shop4",
                        X = 200,
                        Y = 270,
                        Width = 20,
                        Height = 300,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "StartArea_Down",
                        X = 200,
                        Y = 570,
                        Width = 170,
                        Height = 20,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "StartArea_Down2",
                        X = 290,
                        Y = 440,
                        Width = 20,
                        Height = 200,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "StartArea_Down3",
                        X = 300,
                        Y = 460,
                        Width = 130,
                        Height = 20,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "StartArea_Down4",
                        X = 400,
                        Y = 460,
                        Width = 20,
                        Height = 200,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "StartArea_Down5",
                        X = 380,
                        Y = 550,
                        Width = 150,
                        Height = 200,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "Well",
                        X = 555,
                        Y = 280,
                        Width = 60,
                        Height = 80,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "House_right",
                        X = 800,
                        Y = 390,
                        Width = 400,
                        Height = 180,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "House_right2",
                        X = 720,
                        Y = 455,
                        Width = 100,
                        Height = 20,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "rightwall",
                        X = 1150,
                        Y = 0,
                        Width = 200,
                        Height = 200,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "rightwall2",
                        X = 1150,
                        Y = 200,
                        Width = 200,
                        Height = 400,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "House_Left",
                        X = 220,
                        Y = 0,
                        Width = 220,
                        Height = 180,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "wall_left",
                        X = 0,
                        Y = 0,
                        Width = 100,
                        Height = 200,
                        Color = Color.Green
                    }
                },
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

                NextMapId = null,

                EntryX = 30,
                EntryY = 100
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

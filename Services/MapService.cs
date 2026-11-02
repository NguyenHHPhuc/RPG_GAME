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
                        X = 680,
                        Y = 0,
                        Width = 450,
                        Height = 130,
                        Color = Color.Gray
                    },
                    
                    
                    new MapObstacle
                    {
                        Id = "StartArea_rightup02",
                        X = 820,
                        Y = 270,
                        Width = 250,
                        Height = 90,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "StartArea_rightup03",
                        X = 880,
                        Y = 400,
                        Width = 230,
                        Height = 50,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "StartArea_Shop",
                        X = 80,
                        Y = 310,
                        Width = 430,
                        Height = 20,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "StartArea_Shop2",
                        X = 80,
                        Y = 330,
                        Width = 430,
                        Height = 20,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "StartArea_Shop3",
                        X = 400,
                        Y = 350,
                        Width = 20,
                        Height = 90,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "StartArea_Shop4",
                        X = 200,
                        Y = 350,
                        Width = 20,
                        Height = 300,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "StartArea_Down",
                        X = 200,
                        Y = 730,
                        Width = 170,
                        Height = 20,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "StartArea_Down2",
                        X = 240,
                        Y = 600,
                        Width = 20,
                        Height = 200,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "StartArea_Down3",
                        X = 510,
                        Y = 770,
                        Width = 120,
                        Height = 150,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "StartArea_Down4",
                        X = 335,
                        Y = 600,
                        Width = 20,
                        Height = 200,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "StartArea_Down5",
                        X = 350,
                        Y = 600,
                        Width = 200,
                        Height = 200,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "Well",
                        X = 630,
                        Y = 370,
                        Width = 80,
                        Height = 110,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "House_right",
                        X = 900,
                        Y = 530,
                        Width = 400,
                        Height = 250,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "House_right2",
                        X = 830,
                        Y = 625,
                        Width = 100,
                        Height = 20,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "rightwall",
                        X = 1280,
                        Y = 0,
                        Width = 200,
                        Height = 200,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "rightwall2",
                        X = 1280,
                        Y = 200,
                        Width = 200,
                        Height = 400,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "rightwall3",
                        X = 1280,
                        Y = 600,
                        Width = 200,
                        Height = 400,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "House_Left",
                        X = 250,
                        Y = 0,
                        Width = 250,
                        Height = 230,
                        Color = Color.Green
                    },
                    new MapObstacle
                    {
                        Id = "wall_left",
                        X = 0,
                        Y = 0,
                        Width = 130,
                        Height = 300,
                        Color = Color.Green
                    }
                },
                ExitVerticalRatio = 0.5f,
                NextMapId = "DungeonFloor1",
                EntryX = 590,
                EntryY = 60
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
                        Id = "Rock_up01",
                        X = 0,
                        Y = 0,
                        Width = 210,
                        Height = 300,
                        Color = Color.DarkOliveGreen
                    },

                    new MapObstacle
                    {
                        Id = "Rock_up02",
                        X = 210,
                        Y = 0,
                        Width = 1000,
                        Height = 180,
                        Color = Color.ForestGreen
                    },

                    new MapObstacle
                    {
                        Id = "Rock_up03",
                        X = 1210,
                        Y = 0,
                        Width = 300,
                        Height = 300,
                        Color = Color.SaddleBrown
                    },
                    new MapObstacle
                    {
                        Id = "Rock_Down04",
                        X = 0,
                        Y = 540,
                        Width = 280,
                        Height = 300,
                        Color = Color.ForestGreen
                    },
                    new MapObstacle
                    {
                        Id = "Rock_Down05",
                        X = 280,
                        Y = 600,
                        Width = 1200,
                        Height = 200,
                        Color = Color.DarkOliveGreen
                    },
                    new MapObstacle
                    {
                        Id = "Rock01",
                        X = 625,
                        Y = 280,
                        Width = 30,
                        Height = 30,
                        Color = Color.ForestGreen
                    },
                    new MapObstacle
                    {
                        Id = "Rock02",
                        X = 730,
                        Y = 250,
                        Width = 30,
                        Height = 50,
                        Color = Color.ForestGreen
                    },
                },

                ExitVerticalRatio = 0.4f,

                NextMapId = "DungeonFloor2",

                EntryX = 110,
                EntryY = 380
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
                        Id = "Rock_left01",
                        X = 0,
                        Y = 0,
                        Width = 200,
                        Height = 640,
                        Color = Color.Gray
                    },

                    new MapObstacle
                    {
                        Id = "Rock_left02",
                        X = 200,
                        Y = 360,
                        Width = 100,
                        Height = 190,
                        Color = Color.DarkGray
                    },

                    new MapObstacle
                    {
                        Id = "Rock_left03",
                        X = 300,
                        Y = 450,
                        Width = 60,
                        Height = 100,
                        Color = Color.DimGray
                    },
                    new MapObstacle
                    {
                        Id = "Rock_left04",
                        X = 369,
                        Y = 520,
                        Width = 70,
                        Height = 40
                    },
                    new MapObstacle
                    {
                        Id = "wall_up01",
                        X = 200,
                        Y = 0,
                        Width = 900,
                        Height = 90
                    },
                    new MapObstacle
                    {
                        Id = "wall_up02",
                        X = 470,
                        Y = 90,
                        Width = 630,
                        Height = 90
                    },
                    new MapObstacle
                    {
                        Id = "wall_up03",
                        X = 720,
                        Y = 180,
                        Width = 370,
                        Height = 90
                    },
                    new MapObstacle
                    {
                        Id = "wall_right01",
                        X = 1400,
                        Y = 200,
                        Width = 150,
                        Height = 900
                    },
                    new MapObstacle
                    {
                        Id = "wall_right02",
                        X = 1100,
                        Y = 760,
                        Width = 300,
                        Height = 90
                    },
                    new MapObstacle
                    {
                        Id = "wall_right03",
                        X = 1280,
                        Y = 600,
                        Width = 150,
                        Height = 160
                    },
                    new MapObstacle
                    {
                        Id = "wall_down01",
                        X = 310,
                        Y = 750,
                        Width = 570,
                        Height = 90
                    },
                    new MapObstacle
                    {
                        Id = "wall_down02",
                        X = 430,
                        Y = 640,
                        Width = 120,
                        Height = 110
                    },
                    new MapObstacle
                    {
                        Id = "Rock",
                        X = 930,
                        Y = 340,
                        Width = 70,
                        Height = 50
                    }
                },

                ExitVerticalRatio = 0.2f,

                NextMapId = null,

                EntryX = 130,
                EntryY = 700
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

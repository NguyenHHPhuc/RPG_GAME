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

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
namespace RPG.DoiTuong
{
    public class MapData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsDungeon { get; set; }

        public int FloorNumber { get; set; }
        public Color BackgroundColor { get; set; }
        public Color TextColor { get; set; }

        // Danh sách các chướng ngại vật trong Map.
        public List<MapObstacle> Obstacles { get; set; } = new List<MapObstacle>();

        // Vị trí cửa ra theo chiều dọc,
        // tính theo tỷ lệ chiều cao của Map.
        public float ExitVerticalRatio { get; set; }

        // Map tiếp theo khi đi qua cửa ra.
        public string NextMapId { get; set; }

        // Vị trí xuất hiện khi đi vào Map này.
        public int EntryX { get; set; }
        public int EntryY { get; set; }
    }
}

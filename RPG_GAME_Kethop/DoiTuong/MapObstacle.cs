using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
namespace RPG.DoiTuong
{
    public class MapObstacle
    {
        public string Id { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public Color Color { get; set; }
    }
}

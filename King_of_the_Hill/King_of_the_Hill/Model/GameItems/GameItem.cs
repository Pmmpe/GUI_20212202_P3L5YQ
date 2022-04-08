using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace King_of_the_Hill.Model.GameItems
{
    public abstract class GameItem
    {
        public string Name { get; set; }
        public double PosX { get; set; }
        public double PosY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)PosX, (int)PosY, Width, Height);
            }
        }

        public GameItem(string name, double posX, double posY)
        {
            Name = name;
            PosX = posX;
            PosY = posY;
            Width = 25;
            Height = 25;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace King_of_the_Hill.Model.GameItems
{
    public class Arrow
    {
        public double PosX { get; set; }
        public double PosY { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public Arrow(double posX, double posY, double width, double height)
        {
            PosX = posX;
            PosY = posY;
            Width = width;
            Height = height;
            InterSected = false;
        }
        public bool InterSected { get; set; }
        public Rectangle arrowRect
        {
            get
            {
                return new Rectangle((int)PosX, (int)PosY, (int)Width, (int)Height);
            }
        }
    }
}

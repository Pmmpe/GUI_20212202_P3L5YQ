using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace King_of_the_Hill.Logic
{
    public class Ground : IMapItem
    {
        public Point Center { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public Ground(int x, int y, int Width, int Height)
        {
            Center = new Point(x, y);
            this.Width = Width;
            this.Height = Height;
        }
    }
}

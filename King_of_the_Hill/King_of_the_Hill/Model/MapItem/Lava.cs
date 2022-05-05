using System.Drawing;

namespace King_of_the_Hill.Model.MapItem
{
    class Lava : IMapItem
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
        public Rectangle Rectangle { get; set; }

        public Lava(int x, int y, int Width, int Height)
        {
            X = x;
            Y = y;
            this.Width = Width;
            this.Height = Height;
            Rectangle = new Rectangle(x, y, Width, Height);
        }
    }
}

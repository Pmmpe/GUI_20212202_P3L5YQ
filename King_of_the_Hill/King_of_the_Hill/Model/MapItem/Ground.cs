namespace King_of_the_Hill.Model.MapItem
{
    using System.Drawing;

    public class Ground : IMapItem
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public Rectangle Rectangle { get; set; }

        public Ground(int x, int y, int Width, int Height)
        {
            X = x;
            Y = y;
            this.Width = Width;
            this.Height = Height;
            Rectangle = new Rectangle(x, y, Width, Height);
        }
    }
}

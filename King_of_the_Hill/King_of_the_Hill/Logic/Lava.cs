namespace King_of_the_Hill.Logic
{
    using System.Drawing;
    class Lava : IMapItem
    {
        public Point Center { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
        public Rectangle Rectangle { get; set; }

        public Lava(int x, int y, int Width, int Height)
        {
            Center = new Point(x, y);
            this.Width = Width;
            this.Height = Height;
            Rectangle = new Rectangle(x, y, Width, Height); 
        }
    }
}

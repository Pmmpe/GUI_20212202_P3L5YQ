namespace King_of_the_Hill.Logic
{
    using System.Drawing;
    public interface IMapItem
    {
        public Point Center { get; set; }

        Rectangle Rectangle { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}

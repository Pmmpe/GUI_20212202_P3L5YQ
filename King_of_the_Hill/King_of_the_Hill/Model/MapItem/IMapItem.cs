using System.Drawing;

namespace King_of_the_Hill.Model.MapItem
{
    public interface IMapItem
    {
        public int X { get; set; }

        public int Y { get; set; }

        Rectangle Rectangle { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}

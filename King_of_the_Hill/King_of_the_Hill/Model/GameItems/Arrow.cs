using System.Drawing;

namespace King_of_the_Hill.Model.GameItems
{
    public class Arrow
    {
        public double PosX { get; set; }
        public double PosY { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public bool DirectionIsLeft { get; set; }
        public double ArrowDamage { get; set; }

        public Arrow(double arrowDamage, double posX, double posY, double width, double height, bool directionIsLeft)
        {
            PosX = posX;
            PosY = posY;
            Width = width;
            Height = height;
            DirectionIsLeft = directionIsLeft;
            ArrowDamage = arrowDamage;
        }
        public Rectangle arrowRect
        {
            get
            {
                return new Rectangle((int)PosX, (int)PosY, (int)Width, (int)Height);
            }
        }
    }
}

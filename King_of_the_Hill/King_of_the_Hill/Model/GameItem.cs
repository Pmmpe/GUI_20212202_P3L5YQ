namespace King_of_the_Hill.Model
{
    using System.Drawing;
    public class GameItem
    {
        public Point Center { get; set; }
        public int ItemRadius { get; set; }
        public int SpeedX { get; set; }
        public int SpeedY { get; set; }
        public Size GameArea { get; set; }
        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle(
                Center.X - ItemRadius,
                Center.Y - ItemRadius,
                ItemRadius * 2, ItemRadius * 2
                );
            }
        }

        public GameItem(Size gameArea, int itemRadius)
        {
            GameArea = gameArea;
            ItemRadius = itemRadius;
        }
    }
}

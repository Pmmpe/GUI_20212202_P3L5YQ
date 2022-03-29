using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace King_of_the_Hill.Model
{
    public class Charachter
    {
        public string Name { get; set; }

        public double Health { get; set; }

        public double Armour { get; set; }
        public double Speed { get; private set; }

        public Charachter(double Health, double Armour, double Speed, Size gameArea, int itemRadius)
        {
            this.Health = Health;
            this.Armour = Armour;
            this.Speed = Speed;
            GameArea = gameArea;
            ItemRadius = itemRadius;
        }

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

        public bool Move()
        {
            Point newCenter = new Point(
                Center.X + SpeedX,
                Center.Y + SpeedY);
            if (newCenter.X >= 0 && newCenter.Y >= 0
                && newCenter.X < GameArea.Width
                && newCenter.Y < GameArea.Height)
            {
                Center = newCenter;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
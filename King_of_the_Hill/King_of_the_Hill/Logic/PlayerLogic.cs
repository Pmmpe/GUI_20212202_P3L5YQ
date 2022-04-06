namespace King_of_the_Hill.Logic
{
    using King_of_the_Hill.Model;
    using System;
    using System.Drawing;
    using System.Windows;

    public class PlayerLogic
    {
        public enum Controls
        {
            A, D, W, S, Space, Q, E
        }

        public Player plyr;
        public PlayerLogic()
        {
            plyr = new Player(100, 100, 0, 0, 50, 50);
        }

        public double Weight { get; set; }

        public Rectangle playerRect
        {
            get
            {
                return new Rectangle((int)plyr.PosX, (int)plyr.PosY, (int)plyr.Width, (int)plyr.Height);
            }
        }

        public void Control(Controls control)
        {
            switch (control)
            {
                case Controls.A:
                    plyr.PosX -= 5;
                    break;
                case Controls.D:
                    plyr.PosX += 5;
                    break;
                case Controls.W:
                    plyr.PosY -= 5;
                    break;
                case Controls.S:
                    plyr.PosY += 5;
                    break;
                case Controls.Space:
                    plyr.PosY = plyr.PosY - 25 + Weight;
                    break;
                case Controls.Q:
                    MessageBox.Show("Armour Restored");
                    break;
                case Controls.E:
                    MessageBox.Show("HP Restored");
                    break;
            }
        }

        public void Gravity(double Weight)
        {
           plyr.PosY = plyr.PosY + Weight;
        }
    }
}

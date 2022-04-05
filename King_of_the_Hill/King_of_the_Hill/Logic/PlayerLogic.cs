namespace King_of_the_Hill.Logic
{
    using King_of_the_Hill.Model;
    using System;
    using System.Drawing;
    using System.Windows;

    public class PlayerLogic : IGameModel
    {
        public enum Controls
        {
            A, D, W, S, Space, Q, E
        }

        public event EventHandler Changed;

        public Player plyr;
        public PlayerLogic()
        {
            plyr = new Player(100,100,0,0,50,50);
        }

        public double Weight { get; set; }

        public Rectangle playerRect
        {
            get
            {
                return new Rectangle((int)plyr.PosX, (int)plyr.PosY, (int)plyr.Width, (int)plyr.Height);
            }
        }
        public System.Windows.Size gameArea { get; set; }

        public void Control(Controls control)
        {
            if (control == Controls.A /*&& (gameArea.Width < PosX - 20)*/)
            {
                plyr.PosX = plyr.PosX - 20;
            }
            if (control == Controls.D /*&& (PosX + 20) < gameArea.Width*/)
            {
                plyr.PosX = plyr.PosX + 20;
            }
            if (control == Controls.W /*&& gameArea.Height > (PosY) - 20*/)
            {
                plyr.PosY = plyr.PosY - 20;
            }
            if (control == Controls.S /*&& (PosY + 20) < gameArea.Height*/)
            {
                plyr.PosY = plyr.PosY + 20;
            }
            if (control == Controls.Space)
            {
                plyr.PosY = plyr.PosY - (plyr.PosY * Weight);
            }
            if (control == Controls.E)
            {
                MessageBox.Show("HP Restored");
            }
            if (control == Controls.Q)
            {
                MessageBox.Show("Armour Restored");
            }
            Changed?.Invoke(this, null);
        }

        public void Gravity(double Weight)
        {
           plyr.PosY = plyr.PosY + Weight;
        }
    }
}

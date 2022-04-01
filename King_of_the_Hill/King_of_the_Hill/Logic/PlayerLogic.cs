namespace King_of_the_Hill.Logic
{
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
        public double PosX { get; set; }
        public double PosY { get; set; }

        public double Weight { get; set; }

        public Rectangle playerRect { get; set; }

        public System.Windows.Size gameArea { get; set; }

        public void Control(Controls control)
        {
            if (control == Controls.A /*&& (gameArea.Width < PosX - 20)*/)
            {
                PosX = PosX - 20;
            }
            if (control == Controls.D /*&& (PosX + 20) < gameArea.Width*/)
            {
                PosX = PosX + 20;
            }
            if (control == Controls.W /*&& gameArea.Height > (PosY) - 20*/)
            {
                PosY = PosY - 20;
            }
            if (control == Controls.S /*&& (PosY + 20) < gameArea.Height*/)
            {
                PosY = PosY + 20;
            }
            if (control == Controls.Space)
            {
                PosY = PosY - (PosY * Weight);
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
            if (gameArea.Height > 100 && 0 <= gameArea.Height)//Could be determined later when we could get an optimal exact value;
            {
                PosY = PosY + (PosY * Weight);
            }
        }
    }
}

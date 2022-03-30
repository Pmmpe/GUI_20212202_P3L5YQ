using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace King_of_the_Hill.Logic
{
    public class PlayerLogic : IGameModel
    {
        public enum Controls
        {
            A, D, W, S, Space, Q, E
        }

        public event EventHandler Changed;
        public double PosX { get; set; }
        public double PosY { get; set; }

        public void Control(Controls control)
        {
            if (control == Controls.A)
            {
                PosX = PosX - 20;
            }
            else if (control == Controls.D)
            {
                PosX = PosX + 20;
            }
            else if (control == Controls.W)
            {
                PosY = PosY - 20;
            }
            else if (control == Controls.S)
            {
                PosY = PosY + 20;
            }           
            Changed?.Invoke(this, null);
        }
    }
}

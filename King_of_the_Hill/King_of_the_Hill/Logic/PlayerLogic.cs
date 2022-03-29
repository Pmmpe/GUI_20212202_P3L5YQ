using King_of_the_Hill.Logic.Controller;
using King_of_the_Hill.Logic.LogicModelInterface;
using King_of_the_Hill.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace King_of_the_Hill.Logic
{
    public class PlayerLogic : ICharachterController, IPlayerModel
    {
        public Player player { get; set; }

        public event EventHandler Changed;

        public void Attack()
        {
            MessageBox.Show("Attacked");
        }

        public void JumpJet()
        {
            MessageBox.Show("Jump Jetted");
        }

        public void Rotate(double angle)
        {
            player.Angle += angle;
            Changed?.Invoke(this, null);
        }

        public void Shoot()
        {
            MessageBox.Show("Shooted");
        }
    }
}

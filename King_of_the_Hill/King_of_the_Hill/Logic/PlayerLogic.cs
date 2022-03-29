﻿using King_of_the_Hill.Logic.Controller;
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

        public enum Controls
        {
            A, W, S, D, E, Q, Space
        }

        public event EventHandler Changed;

        public void Attack()
        {
            MessageBox.Show("Attacked");
        }

        public void JumpJet()
        {
            MessageBox.Show("Jump Jetted");
        }

        public void Control(Controls controls)
        {
            switch(controls)
            {
                case Controls.A:
                    player.PosX -= 5;
                    break;
                case Controls.D:
                    player.PosX += 5;
                    break;
                case Controls.W:
                    player.PosY += 5;
                    break;
                case Controls.S:
                    player.PosX -= 5;
                    break;
                case Controls.E:
                    //HP potion
                    break;
                case Controls.Q:
                    //Armour kit
                    break;
                case Controls.Space:
                    player.PosY += 15;
                    break;
            }
        }

        public void Rotate(double angle)
        {
            //player.Angle += angle;
            //Changed?.Invoke(this, null);
        }

        public void Shoot()
        {
            MessageBox.Show("Shooted");
        }
    }
}
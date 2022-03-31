﻿namespace King_of_the_Hill.Model
{
    using System.Drawing;
    public class Player : Character
    {
        public Player(double Health, double Armour, double Speed, Size gameArea, int itemRadius) : base(Health, Armour, Speed, gameArea, 75)
        {
            Center = new Point(gameArea.Width / 2, gameArea.Height / 2);
        }
        public double PosX { get; set; }
        public double PosY { get; set; }

    }
}

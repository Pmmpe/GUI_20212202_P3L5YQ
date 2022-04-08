namespace King_of_the_Hill.Model
{
    using King_of_the_Hill.Model.GameItems;
    using System.Collections.Generic;
    using System.Drawing;
    public class Player : Character
    {
        public int Weight { get; set; }

        public List<Weapon> Weapons { get; set; }

        public Jetpack Jetpack { get; set; }

        public Player(double Health, double Armour, double PosX, double PosY, double Width, double Height, int Weight) : base(PosX, PosY, Width, Height, Health, Armour)
        {
            this.Weight = Weight;
            Weapons = new List<Weapon>();
        }
        

    }
}

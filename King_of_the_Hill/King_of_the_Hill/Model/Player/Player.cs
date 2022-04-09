namespace King_of_the_Hill.Model
{
    using King_of_the_Hill.Model.GameItems;
    using System.Collections.Generic;
    using System.Drawing;
    public class Player : Character
    {
        public int Weight { get; set; }

        public Weapon PrimaryWeapon { get; set; }

        public Bow Bow { get; set; }

        public Jetpack Jetpack { get; set; }

        public Player(double Health, double Armour, double PosX, double PosY, double Width, double Height, int Weight) : base(PosX, PosY, Width, Height, Health, Armour)
        {
            this.Weight = Weight;
            PrimaryWeapon = new Weapon(0, "DELETED", 0, 0, 0, 0); ////nem tudom ki null-ozni szóval ha nincs a playernek fegyverek akkor átírjuk a fegyverének a nevét DELETED-re
            Bow = new Bow(25, "Bow", 0, 0, 0); //itt más a helyzet mert Íj az mindig van a játékosnál, csak nyilakat kell felvennie.
            Jetpack = new Jetpack("Jetpack", 0, 0); //teljesen mindegy, hogy ez miként van itt létrehozva csak a Fuel változik úgyis.
        }
        

    }
}

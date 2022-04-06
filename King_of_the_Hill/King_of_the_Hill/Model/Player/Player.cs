namespace King_of_the_Hill.Model
{
    using System.Drawing;
    public class Player : Character
    {
        public int Weight { get; set; }

        public Player(double Health, double Armour, double PosX, double PosY, double Width, double Height, int Weight) : base(PosX, PosY, Width, Height, Health, Armour)
        {
            this.Weight = Weight;
        }
        

    }
}

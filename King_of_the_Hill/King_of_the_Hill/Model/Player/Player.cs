namespace King_of_the_Hill.Model
{
    using System.Drawing;
    public class Player : Character
    {
        public double PosX { get; set; }
        public double PosY { get; set; }
        public double Width { get; }
        public double Height { get; }
        public int Weight { get; set; }

        public Player(double Health, double Armour, double PosX, double PosY, double Width, double Height, int Weight) : base(Health, Armour)
        {
            this.PosX = PosX;
            this.PosY = PosY;
            this.Width = Width;
            this.Height = Height;
            this.Weight = Weight;
        }
        

    }
}

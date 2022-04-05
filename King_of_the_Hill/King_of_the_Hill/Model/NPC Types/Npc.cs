namespace King_of_the_Hill.Model
{
    using System.Drawing;
    public class Npc : Character
    {
        public Npc(double Health, double Armour, double PosX, double PosY, double Width, double Height) : base(Health, Armour)
        {
            this.PosX = PosX;
            this.PosY = PosY;
            this.Width = Width;
            this.Height = Height;
        }
        public double PosX { get; set; }
        public double PosY { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
    }
}

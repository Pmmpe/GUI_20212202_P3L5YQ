namespace King_of_the_Hill.Model
{
    public class Character
    {
        public double PosX { get; set; }
        public double PosY { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public double Health { get; set; }
        public double Armour { get; set; }

        public Character(double posX, double posY, double width, double height, double health, double armour)
        {
            PosX = posX;
            PosY = posY;
            Width = width;
            Height = height;
            Health = health;
            Armour = armour;
        }
    }
}
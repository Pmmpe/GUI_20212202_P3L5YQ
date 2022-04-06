namespace King_of_the_Hill.Model
{
    public class Character
    {
        public double Health { get; set; }

        public double Armour { get; set; }

        public Character(double Health, double Armour)
        {
            this.Health = Health;
            this.Armour = Armour;
        }
    }
}
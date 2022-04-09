namespace King_of_the_Hill.Model
{
    using King_of_the_Hill.Model.GameItems;
    public class Player : Character
    {
        public int Weight { get; set; }

        public Player(double Health, double Armour, double PosX, double PosY, double Width, double Height, int Weight) : base(PosX, PosY, Width, Height, Health, Armour)
        {
            this.Weight = Weight;
        }
        
        public Weapon weapon { get; set; }

        public Bow bow { get; set; }

        public double returnDamage()
        {
            if (weapon == null)
            {
                return 50;
            }
            else
            {
                return Weight * weapon.WeaponDamage;
            }
        }
    }
}

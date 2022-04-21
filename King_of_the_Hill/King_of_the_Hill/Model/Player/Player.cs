namespace King_of_the_Hill.Model
{
    using King_of_the_Hill.Model.GameItems;

    public class Player : Character
    {
        public int Weight { get; set; }

        public Weapon PrimaryWeapon { get; set; }

        public Bow Bow { get; set; }

        public Jetpack Jetpack { get; set; }

        public HealPotion HealPotion { get; set; }

        public Armor ArmorRepairKit { get; set; }

        public Player(double Health, double Armour, double PosX, double PosY, double Width, double Height, int Weight) : base(PosX, PosY, Width, Height, Health, Armour)
        {
            this.Weight = Weight;
            PrimaryWeapon = null;
            Bow = null;
            Jetpack = new Jetpack("Jetpack", 0, 0);

            HealPotion = new HealPotion("HP poti", 0, 0);
            ArmorRepairKit = new Armor("Armor kit", 0, 0);
        }

        public double returnDamage()
        {
            if (PrimaryWeapon == null)
            {
                return 10;
            }
            else
            {
                return Weight * PrimaryWeapon.WeaponDamage;
            }
        }
    }
}

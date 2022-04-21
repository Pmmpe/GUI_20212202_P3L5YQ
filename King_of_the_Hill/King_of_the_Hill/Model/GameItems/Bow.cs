namespace King_of_the_Hill.Model.GameItems
{
    public class Bow : Weapon
    {
        public int NumberOfArrows { get; set; }

        public Bow(double weaponDamage, string weaponName, double attackSpeed, int numberOfArrows, double PosX, double PosY) : base(weaponDamage, weaponName, attackSpeed, PosX, PosY)
        {
            WeaponDamage = weaponDamage;
            NumberOfArrows = numberOfArrows;
        }
    }
}

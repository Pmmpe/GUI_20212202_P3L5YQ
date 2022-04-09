
namespace King_of_the_Hill.Model.GameItems
{
    public class Bow : GameItem
    {
        public int NumberOfArrows { get; set; }
        public int WeaponDamage { get; set; }

        public Bow(int weaponDamage, string weaponName, int numberOfArrows, double PosX, double PosY) : base(weaponName, PosX, PosY)
        {
            WeaponDamage = weaponDamage;
            NumberOfArrows = numberOfArrows;
        }
    }
}

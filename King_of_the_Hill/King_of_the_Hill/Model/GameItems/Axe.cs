namespace King_of_the_Hill.Model.GameItems
{
    using System.Drawing;
    public class Axe : Weapon
    {
        public Axe(double weaponDamage, string weaponName, double durability, double attackSpeed, double PosX, double PosY) : base(weaponDamage, weaponName, durability, attackSpeed, PosX, PosY)
        {
            this.WeaponDamage = 50;
            this.WeaponName = "Axe";
            this.Durability = 1;
            this.PosX = PosX;
            this.PosY = PosY;   
        }
    }
}

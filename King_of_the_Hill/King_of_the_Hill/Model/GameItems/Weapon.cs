namespace King_of_the_Hill.Model.GameItems
{
    using System.Drawing;

    public class Weapon : GameItem
    {
        public double WeaponDamage { get; set; }
        public double Durability { get; set; }
        public double AttackSpeed { get; set; }

        public Weapon(double weaponDamage, string weaponName, double durability, double attackSpeed, double PosX, double PosY) : base(weaponName, PosX, PosY)
        {
            WeaponDamage = weaponDamage;
            Name = weaponName;
            Durability = durability;
            AttackSpeed = attackSpeed;
        }

        
    }
}

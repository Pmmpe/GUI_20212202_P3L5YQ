namespace King_of_the_Hill.Model.GameItems
{
    using System.Drawing;

    public class Weapon : GameItem
    {
        public double WeaponDamage { get; set; }
        public int AttackSpeed { get; set; }

        public Weapon(double weaponDamage, string weaponName, int attackSpeed, double PosX, double PosY) : base(weaponName, PosX, PosY)
        {
            WeaponDamage = weaponDamage;
            Name = weaponName;
            AttackSpeed = attackSpeed;
        }

        
    }
}

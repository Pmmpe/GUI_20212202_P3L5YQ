namespace King_of_the_Hill.Model.GameItems
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Weapon
    {
        public Weapon(double weaponDamage, string weaponName, double durability, double attackSpeed, double PosX, double PosY)
        {
            WeaponDamage = weaponDamage;
            WeaponName = weaponName;
            Durability = durability;
            AttackSpeed = 1.0;
            this.PosX = PosX;
            this.PosY = PosY;
        }

        public double WeaponDamage { get; set; }
        public string WeaponName { get; set; }
        public double Durability { get; set; }
        public double AttackSpeed { get; set; }
        public double PosX { get; set; }
        public double PosY { get; set; }
        public Rectangle WeaponRect { get; set; }
        public static Weapon ReturnRandomWeapon(List<Weapon> weapons)
        {
            Random rnd = new Random();
            int index = rnd.Next(0, weapons.Count);
            return weapons[index];
        }
    }
}

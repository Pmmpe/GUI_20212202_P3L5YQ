using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace King_of_the_Hill.Model.GameItems
{
    public class Sword : Weapon
    {
        public Sword(double weaponDamage, string weaponName, double durability, double attackSpeed, double PosX, double PosY) : base(weaponDamage, weaponName, durability, attackSpeed, PosX, PosY)
        {
            this.WeaponDamage = 25;
            this.WeaponName = "Sword";
            this.Durability = 1;
            this.PosX = PosX;
            this.PosY = PosY;
        }
    }
}

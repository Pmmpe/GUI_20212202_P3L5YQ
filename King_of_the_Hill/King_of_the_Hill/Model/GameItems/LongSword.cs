﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace King_of_the_Hill.Model.GameItems
{
    public class LongSword : Weapon
    {
        public LongSword(double weaponDamage, string weaponName, double durability, double attackSpeed, double PosX, double PosY) : base(weaponDamage, weaponName, durability, attackSpeed, PosX, PosY)
        {

        }
    }
}
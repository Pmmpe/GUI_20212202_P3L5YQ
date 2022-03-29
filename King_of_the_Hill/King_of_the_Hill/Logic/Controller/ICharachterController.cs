﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace King_of_the_Hill.Logic.Controller
{
    public interface ICharachterController
    {
        void Rotate(double angle);

        void Shoot();

        void Attack();

        void JumpJet();
    }
}
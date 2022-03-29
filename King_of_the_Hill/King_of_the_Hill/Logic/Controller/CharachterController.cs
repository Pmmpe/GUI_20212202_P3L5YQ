using King_of_the_Hill.Logic.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace King_of_the_Hill.Logic
{
    public class CharachterController : ICharachterController
    {
        ICharachterController charachterController;
        public CharachterController(ICharachterController charachterController)
        {
            this.charachterController = charachterController;
        }
        public void Attack()
        {
            throw new NotImplementedException();
        }

        public void JumpJet()
        {
            throw new NotImplementedException();
        }

        public void MoveGameitem()
        {
            throw new NotImplementedException();
        }

        public void Shoot()
        {
            throw new NotImplementedException();
        }
    }
}

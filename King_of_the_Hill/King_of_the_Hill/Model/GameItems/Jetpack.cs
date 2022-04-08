using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace King_of_the_Hill.Model.GameItems
{
    public class Jetpack : GameItem
    {
        public int Fuel { get; set; }

        public Jetpack(string name, double posX, double posY) : base(name, posX, posY)
        {
            Fuel = 0;
        }
    }
}

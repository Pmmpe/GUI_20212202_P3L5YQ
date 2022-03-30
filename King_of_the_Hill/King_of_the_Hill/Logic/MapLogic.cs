using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace King_of_the_Hill.Logic
{
    public class MapLogic
    {
        public List<IMapItem> Grounds { get; set; }

        public void SetupSizes()
        {
            Grounds = new List<IMapItem>();
        }
    }
}

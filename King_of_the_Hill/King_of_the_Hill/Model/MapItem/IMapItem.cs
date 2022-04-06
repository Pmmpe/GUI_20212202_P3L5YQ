using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace King_of_the_Hill.Model.MapItem
{
    public interface IMapItem
    {
        public Point Center { get; set; }

        Rectangle Rectangle { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}

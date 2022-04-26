using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace King_of_the_Hill.Renderer
{
    class CharacterBrush
    {
        public ImageBrush Brush { get; set; }
        public bool IsFlipped { get; set; }

        public CharacterBrush(ImageBrush brush)
        {
            Brush = brush;
            IsFlipped = false;
        }
    }
}

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
        public ImageBrush DefaultBrush { get; set; }

        public ImageBrush CurrentBrush { get; set; }
        public bool IsFlipped { get; set; }

        public CharacterBrush(ImageBrush brush)
        {
            DefaultBrush = brush;
            IsFlipped = false;
            CurrentBrush = brush;
        }
    }
}

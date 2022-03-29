using King_of_the_Hill.Logic.Controller;
using King_of_the_Hill.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace King_of_the_Hill.Logic.LogicModelInterface
{
    public interface IPlayerModel : ICharachterModel
    {
        Player player { get; set; }
    }
}

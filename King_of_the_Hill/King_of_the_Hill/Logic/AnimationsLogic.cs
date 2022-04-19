using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace King_of_the_Hill.Logic
{
    public delegate void JetpackAnimEventHandler();
    public delegate void PlayerAnimEventHandler();
    public class AnimationsLogic
    {
        //delegate

        public PlayerAnimEventHandler Fight;
        public JetpackAnimEventHandler Jetpack;
        public JetpackAnimEventHandler StopJetpack;

        public void FightAnimations() //ha meghivod 5mp megjeleniti a harcolo player image-t
        {
            Fight?.Invoke();
        }

        public void StartJetpackAnimation()
        {
            Jetpack?.Invoke();
        }
    }
}

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
        public JetpackAnimEventHandler StartJetpack; 
        public JetpackAnimEventHandler StopJetpack;

        public void FightAnimations()
        {
            Fight?.Invoke();
        }

        public void StartJetpackAnimation()
        {

        }
    }
}

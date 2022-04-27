using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace King_of_the_Hill.Logic
{
    public delegate void JetpackAnimEventHandler(string action);
    public delegate void PlayerAnimEventHandler(string action);
    public delegate void MovementAnimEventHandler(bool leftOrientation);
    public class AnimationsLogic
    {
        //delegate

        public PlayerAnimEventHandler Fight;
        public JetpackAnimEventHandler Jetpack;
        public MovementAnimEventHandler MoveRight;
        public MovementAnimEventHandler MoveLeft;
        //public JetpackAnimEventHandler StopJetpack;

        public void StartFightAnimations() 
        {
            Fight?.Invoke("start");
        }

        public void StopFightAnimations()
        {
            Fight?.Invoke("stop");
        }


        public void StartJetpackAnimation()
        {
            Jetpack?.Invoke("start");
        }

        public void StopJetpackAnimation()
        {
            Jetpack?.Invoke("stop");
        }

        public void MoveRightAnimation(bool leftOrientation)
        {
            MoveRight?.Invoke(leftOrientation);
        }

        public void MoveLeftAnimation(bool leftOrientation)
        {
            MoveLeft?.Invoke(leftOrientation);
        }

        
    }
}

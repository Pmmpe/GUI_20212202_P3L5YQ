using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace King_of_the_Hill.Logic
{
    public delegate void JetpackAnimEventHandler(string action);
    public delegate void PlayerAnimEventHandler(string action);
    public delegate void MovementAnimEventHandler(bool leftOrientation, string direction, string action);
    public class AnimationsLogic
    {
        //delegate

        public PlayerAnimEventHandler Fight;
        public PlayerAnimEventHandler BowShoot;
        public JetpackAnimEventHandler Jetpack;
        public MovementAnimEventHandler Move;
        public PlayerAnimEventHandler Fall;
//        public MovementAnimEventHandler MoveLeft;
        

        public void StartFightAnimations() 
        {
            Fight?.Invoke("start");
        }

        public void StopFightAnimations()
        {
            Fight?.Invoke("stop");
        }

        public void StartBowShootAnimations()
        {
            BowShoot?.Invoke("start");
        }

        public void StopBowShootAnimations()
        {
            BowShoot?.Invoke("stop");
        }




        public void StartJetpackAnimation()
        {
            Jetpack?.Invoke("start");
        }

        public void StopJetpackAnimation()
        {
            Jetpack?.Invoke("stop");
        }

        //public void StartPlayerMoveRightAnimation(bool leftOrientation)
        //{
        //    MoveRight?.Invoke(leftOrientation, "start");
        //}

        //public void StopPlayerMoveLeftAnimation(bool leftOrientation)
        //{
        //    MoveLeft?.Invoke(leftOrientation, "start");
        //}

        public void StartPlayerMoveAnimation(bool leftOrientation, string direction)
        {
            Move?.Invoke(leftOrientation, direction, "start");
        }
        public void StopPlayerMoveAnimation(bool leftOrientation, string direction)
        {
            Move?.Invoke(leftOrientation, direction, "stop");
        }

        public void StartPlayerFallAnimation()
        {
            Fall?.Invoke("start");
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace King_of_the_Hill.Logic
{
    
    public class AnimationsLogic
    {
        //delegate

        public Action<string> Fight;
        public Action<string> BowShoot;
        public Action<string> Jetpack;
        public Action<bool,string,string> Move;
        public Action<string> Fall;
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

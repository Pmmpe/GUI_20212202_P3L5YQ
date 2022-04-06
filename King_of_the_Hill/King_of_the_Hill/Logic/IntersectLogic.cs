using King_of_the_Hill.Model.MapItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace King_of_the_Hill.Logic
{
    public class IntersectLogic
    {
        PlayerLogic playerLogic;
        MapLogic mapLogic;
        EnemyLogic enemyLogic;
        int width;
        int height;

        public IntersectLogic(PlayerLogic playerLogic, MapLogic mapLogic, EnemyLogic enemyLogic)
        {
            this.playerLogic = playerLogic;
            this.mapLogic = mapLogic;
            this.enemyLogic = enemyLogic;
        }

        public void SetSizes(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public bool IsPlayerAndMapIntersect()
        {
            foreach (var ground in mapLogic.Grounds)
            {
                if (playerLogic.playerRect.IntersectsWith(ground.Rectangle))
                {
                    return true;
                }
            }
            playerLogic.Gravity(2);
            return false;
        }

        public void SetPlayerInTheMap()
        {
            if (playerLogic.plyr.PosX < 0)
            {
                playerLogic.plyr.PosX = 0;
            }
            if (playerLogic.plyr.PosY < 0)
            {
                playerLogic.plyr.PosY = 0;
            }
            if (playerLogic.plyr.PosX + playerLogic.plyr.Width > width)
            {
                playerLogic.plyr.PosX = width - playerLogic.plyr.Width;
            }
            if (playerLogic.plyr.PosY + playerLogic.plyr.Height > height)
            {
                playerLogic.plyr.PosY = height - playerLogic.plyr.Height;
            }
        }
    }
}

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

        public IntersectLogic(PlayerLogic playerLogic, MapLogic mapLogic, EnemyLogic enemyLogic)
        {
            this.playerLogic = playerLogic;
            this.mapLogic = mapLogic;
            this.enemyLogic = enemyLogic;
        }
    }
}

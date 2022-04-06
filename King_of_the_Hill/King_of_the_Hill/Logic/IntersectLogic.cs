using King_of_the_Hill.Model;
using King_of_the_Hill.Model.MapItem;
using King_of_the_Hill.Model.NPC_Types;
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
        static Random random = new Random();

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

        //már megírt gravitációs leesés
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

        //játékos nem tud kimenni a pályáról
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

        //ellenség random generálása
        public void GenerateEnemiesPositons()
        {
            bool ok;
            foreach (var enemy in enemyLogic.enemies)
            {
                ok = false;
                if (enemy is Archer)
                {
                    while (!ok)
                    {
                        enemy.PosX = random.Next(0, (int)(width - enemy.Width));
                        enemy.PosY = random.Next(0, height - 500);
                        foreach (var platform in mapLogic.Grounds)
                        {
                            if (!ok && platform is Platform && enemy.enemyRect.IntersectsWith(platform.Rectangle))
                            {
                                ok = true;
                                PutTopOfAPlatform(platform, enemy);
                            }
                        }
                    }
                }
                else
                {
                    while (!ok)
                    {
                        enemy.PosX = random.Next(0, (int)(width - enemy.Width));
                        enemy.PosY = random.Next(height - 200, (int)(height - enemy.Height));
                        foreach (var ground in mapLogic.Grounds)
                        {
                            if (!ok && ground is Ground && enemy.enemyRect.IntersectsWith(ground.Rectangle))
                            {
                                ok = true;
                                foreach (var lava in mapLogic.Grounds)
                                {
                                    if (lava is Lava && enemy.enemyRect.IntersectsWith(lava.Rectangle))
                                    {
                                        ok = false;
                                    }
                                }
                                foreach (var otherEnemies in enemyLogic.enemies)
                                {
                                    if (!enemy.Equals(otherEnemies) && enemy.enemyRect.IntersectsWith(otherEnemies.enemyRect))
                                    {
                                        ok = false;
                                    }
                                }
                            }
                        }
                    }
                }
                
            }
        }

        //játékos kezdő platformra helyezése
        public void PutPlayerOnTheStartPlatform()
        {
            foreach (var platform in mapLogic.Grounds)
            {
                if (platform is StartPlatform)
                {
                    PutTopOfAPlatform(platform, playerLogic.plyr);
                }
            }
        }

        //Bármilyen character, legyen az játékos vagy ellenség a megadott platformra helyezése
        private void PutTopOfAPlatform(IMapItem platform, Character character)
        {
            if (character.PosX < platform.X)
            {
                character.PosX = platform.X;
            }
            if (character.PosX > platform.X + platform.Width)
            {
                character.PosX = platform.X + platform.Width - character.Width;
            }
            character.PosY = platform.Y - character.Height;
        }

        //beállítja, hogy merre menjen az ellenség
        public void SetEnemyDirection()
        {
            foreach (var enemy in enemyLogic.enemies)
            {
                if (enemy is Archer)
                {
                    foreach (var platform in mapLogic.Grounds)
                    {
                        if (platform is Platform)
                        {
                            if (enemy.PosX < platform.X)
                            {
                                enemy.Direction = false;
                            }
                            else if (enemy.PosX > platform.X + platform.Width - enemy.Width)
                            {
                                enemy.Direction = true;
                            }
                        }
                    }
                }
                else
                {
                    if (enemy.PosX <= 0)
                    {
                        enemy.Direction = false;
                    }
                    else if (enemy.PosX >= width)
                    {
                        enemy.Direction = true;
                    }
                    foreach (var lava in mapLogic.Grounds)
                    {
                        if (lava is Lava && enemy.enemyRect.IntersectsWith(lava.Rectangle))
                        {
                            enemy.Direction = !enemy.Direction;
                        }
                    }
                }
            }
        }
    }
}

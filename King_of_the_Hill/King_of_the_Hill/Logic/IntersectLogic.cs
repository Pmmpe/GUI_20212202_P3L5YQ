namespace King_of_the_Hill.Logic
{
    using King_of_the_Hill.Model;
    using King_of_the_Hill.Model.GameItems;
    using King_of_the_Hill.Model.MapItem;
    using King_of_the_Hill.Model.NPC_Types;
    using System;
    public class IntersectLogic
    {
        PlayerLogic playerLogic;
        MapLogic mapLogic;
        EnemyLogic enemyLogic;
        ItemLogic itemLogic;
        int width;
        int height;
        static Random random = new Random();

        int canArcherShootCounter;
        bool canArcherShoot;

        public Action<string, double> CausedDamageEvent;

        public IntersectLogic(PlayerLogic playerLogic, MapLogic mapLogic, EnemyLogic enemyLogic, ItemLogic itemLogic)
        {
            this.playerLogic = playerLogic;
            this.mapLogic = mapLogic;
            this.enemyLogic = enemyLogic;
            this.itemLogic = itemLogic;
            canArcherShootCounter = 0;
            canArcherShoot = true;
        }

        public void SetSizes(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        //Returns true if the player is in intersect with any of the given NPCs in the current EnemyLogic
        public bool isPlayerIntersectWithAnyNPC()
        {
            bool intersect = false;
            foreach (var npc in enemyLogic.enemies)
            {
                if (playerLogic.playerRect.IntersectsWith(npc.enemyRect))
                {
                    intersect =  true;
                }
            }
            return intersect;
        }

        //Returns the current npc that is intersecting with the player object.
        //BEAWARE if you call it in none intersect situation, it could and will return null. (Handled in PlayerLogic)
        public Npc PlayerIntersectWithThat()
        {
            foreach (var npc in enemyLogic.enemies)
            {
                if (playerLogic.playerRect.IntersectsWith(npc.enemyRect))
                {
                    return npc;
                }
            }
            return null;
        }

        public void RemoveDeadEnemies()
        {
            Npc enemyToBeDeleted = null;
            foreach (var enemy in enemyLogic.enemies)
            {
                if (enemy.Health <= 0)
                {
                    enemyToBeDeleted = enemy;
                    if (enemy is Archer)
                    {
                        playerLogic.plyr.Health += 50;
                    }
                }
            }
            if (enemyToBeDeleted != null)
            {
                enemyLogic.enemies.Remove(enemyToBeDeleted);
            }
        }

        public void ArcherShoot()
        {
            if (canArcherShoot)
            {
                foreach (var enemy in enemyLogic.enemies)
                {
                    if (enemy is Archer && enemy.PosY - 50 < playerLogic.plyr.PosY && enemy.PosY + 50 > playerLogic.plyr.PosY)
                    {
                        bool directionIsLeft = false;
                        if (playerLogic.plyr.PosX < enemy.PosX)
                        {
                            directionIsLeft = true;
                        }
                        enemyLogic.arrows.Add(new Arrow(enemy.Damage, enemy.PosX + enemy.Width / 2, enemy.PosY + enemy.Height / 2, 10, 10, directionIsLeft));
                    }
                }
                canArcherShoot = false;
            }
            else
            {
                canArcherShootCounter++;
                if (canArcherShootCounter == 150)
                {
                    canArcherShootCounter = 0;
                    canArcherShoot = true;
                }
            }
        }

        public void ArrowIntersected()
        {
            Arrow toBeRemoved = null;
            foreach (var enemy in enemyLogic.enemies)
            {
                foreach (var arrow in playerLogic.Arrows)
                {
                    if (arrow.PosX < 0 || arrow.PosX > 2500)
                    {
                        toBeRemoved = arrow;
                    }
                    else if (arrow.arrowRect.IntersectsWith(enemy.enemyRect))
                    {
                        toBeRemoved = arrow;
                        enemy.Health -= arrow.ArrowDamage;
                        CausedDamageEvent(playerLogic.plyr.Bow.Name, arrow.ArrowDamage);
    }
                }
            }
            if (toBeRemoved != null)
            {
                playerLogic.Arrows.Remove(toBeRemoved);
                toBeRemoved = null;
            }
            foreach (var arrow in enemyLogic.arrows)
            {
                if (arrow.PosX < 0 || arrow.PosX > 2500)
                {
                    toBeRemoved = arrow;
                }
                else if (arrow.arrowRect.IntersectsWith(playerLogic.playerRect))
                {
                    toBeRemoved = arrow;
                    playerLogic.SufferDamage(arrow.ArrowDamage);
                }
            }
            if (toBeRemoved != null)
            {
                enemyLogic.arrows.Remove(toBeRemoved);
            }
        }

        public void PlayerIntersectWithLava()
        {
            foreach (var lava in mapLogic.Grounds)
            {
                if (lava is Lava && playerLogic.playerRect.IntersectsWith(lava.Rectangle))
                {
                    playerLogic.plyr.Health--;
                }
            }
        }

        //The already stated player gravity function for further description see in the xaml.cs!
        public bool IsPlayerAndMapIntersect()
        {
            foreach (var ground in mapLogic.Grounds)
            {
                if (playerLogic.playerRect.IntersectsWith(ground.Rectangle))
                {
                    return true;
                }
            }
            playerLogic.Gravity();
            return false;
        }

        //Prevents the player to leave the playable area aka the game map!
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

        //Random enemy generating function!
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
                        enemy.PosY = random.Next(0, (int)(height - enemy.Height));
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

        //Localising the player at the starter platform or area!
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

        //It places every single character (could be player or npc aswell) to a platform!
        private void PutTopOfAPlatform(IMapItem platform, Character character)
        {
            if (character.PosX < platform.X)
            {
                character.PosX = platform.X;
            }
            if (character.PosX > platform.X)
            {
                character.PosX = platform.X + platform.Width - character.Width;
            }
            character.PosY = platform.Y - character.Height + 1;
        }

        //It sets the enemies moving direction / facing direction!
        public void SetEnemyDirection()
        {
            foreach (var enemy in enemyLogic.enemies)
            {
                if (enemy is Archer)
                {
                    foreach (var platform in mapLogic.Grounds)
                    {
                        if (platform is Platform && enemy.enemyRect.IntersectsWith(platform.Rectangle))
                        {
                            if (enemy.PosX < platform.X)
                            {
                                enemy.DirectionIsLeft = false;
                            }
                            else if (enemy.PosX > platform.X + platform.Width - enemy.Width)
                            {
                                enemy.DirectionIsLeft = true;
                            }
                        }
                    }
                }
                else
                {
                    if (enemy.PosX <= 0)
                    {
                        enemy.DirectionIsLeft = false;
                    }
                    else if (enemy.PosX >= width - enemy.Width)
                    {
                        enemy.DirectionIsLeft = true;
                    }
                    foreach (var lava in mapLogic.Grounds)
                    {
                        if (lava is Lava && enemy.enemyRect.IntersectsWith(lava.Rectangle))
                        {
                            enemy.DirectionIsLeft = !enemy.DirectionIsLeft;
                        }
                    }
                }
            }
        }

        #region Items

        public void GenerateItemsPositions()
        {
            bool ok;
            foreach (var item in itemLogic.items)
            {
                ok = false;
                while (!ok)
                {
                    item.PosX = random.Next(0, width - item.Width);
                    item.PosY = random.Next(height - 200, height - item.Height);
                    foreach (var ground in mapLogic.Grounds)
                    {
                        if (!ok && ground is Ground && item.Rectangle.IntersectsWith(ground.Rectangle))
                        {
                            ok = true;
                            foreach (var lava in mapLogic.Grounds)
                            {
                                if (lava is Lava && item.Rectangle.IntersectsWith(lava.Rectangle))
                                {
                                    ok = false;
                                }
                            }
                            foreach (var otherItem in itemLogic.items)
                            {
                                if (!item.Equals(otherItem) && item.Rectangle.IntersectsWith(otherItem.Rectangle))
                                {
                                    ok = false;
                                }
                            }
                        }
                    }
                }

            }
        }

        public void PlayerIntersectWithItem()
        {
            GameItem itemToRemove = new Axe(0, "temp", 0, 0, 0);
            bool needRemove = false;
            foreach (var item in itemLogic.items)
            {
                if (item.Rectangle.IntersectsWith(playerLogic.playerRect))
                {
                    if (item is Bow)
                    {
                        itemToRemove = item;
                        needRemove = true;
                        playerLogic.plyr.Bow = (Bow)item; //player megkapja az íjat.
                    }
                    else if (item is Weapon && playerLogic.plyr.PrimaryWeapon == null)
                    {
                        itemToRemove = item;
                        needRemove = true;
                        playerLogic.plyr.PrimaryWeapon = (Weapon)item; //player megkapja a fegyverét
                    }
                    else if (item is HealPotion)
                    {
                        itemToRemove = item;
                        needRemove = true;
                        playerLogic.plyr.HealPotion.Amount++;
                    }
                    else if (item is Armor)
                    {
                        itemToRemove = item;
                        needRemove = true;
                        playerLogic.plyr.ArmorRepairKit.Amount++;
                    }
                    else if (item is Jetpack)
                    {
                        itemToRemove = item;
                        needRemove = true;
                        playerLogic.plyr.Jetpack.Fuel += 100; //kap 100 üzemanyagot
                    }
                    else if (item is Charon)
                    {
                        itemToRemove = item;
                        needRemove = true;
                        playerLogic.plyr.Charon = 1;
                    }

                }
            }
            if (needRemove)
            {
                itemLogic.items.Remove(itemToRemove);
            }
        }

        #endregion
    }
}

namespace King_of_the_Hill.Logic
{
    using King_of_the_Hill.Model;
    using King_of_the_Hill.Model.GameItems;
    using King_of_the_Hill.Model.NPC_Types;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class PlayerLogic
    {
        string difficulty;
        static Random random = new Random();
        bool directionIsLeft;

        public Action<string, double> CausedDamageEvent;

        public List<Arrow> Arrows { get; set; }
        public enum Controls
        {
            A, D, W, S, Space, Q, E, R
        }

        public Player plyr;
        public PlayerLogic()
        {
            plyr = new Player(100, 50, 0, 0, 75, 75);
            directionIsLeft = false;
            Arrows = new List<Arrow>();
        }

        public Rectangle playerRect
        {
            get
            {
                return new Rectangle((int)plyr.PosX, (int)plyr.PosY, (int)plyr.Width, (int)plyr.Height);
            }
        }

        public void Control(Controls control)
        {
            switch (control)
            {
                case Controls.A:
                    plyr.PosX -= 5;
                    directionIsLeft = true;
                    break;
                case Controls.D:
                    plyr.PosX += 5;
                    directionIsLeft = false;
                    break;
                case Controls.W:
                    plyr.PosY -= 5;
                    break;
                case Controls.S:
                    plyr.PosY += 5;
                    break;
                case Controls.Space:
                    plyr.PosY -= 25;
                    break;
                case Controls.Q:
                    if (plyr.HealPotion.Amount > 0)
                    {
                        plyr.HealPotion.Amount--;
                        plyr.Health += 25;
                    }
                    break;
                case Controls.E:
                    if (plyr.ArmorRepairKit.Amount > 0)
                    {
                        plyr.ArmorRepairKit.Amount--;
                        plyr.Armour += 25;
                    }
                    break;
                case Controls.R:
                    Shoot();
                    break;
            }
        }

        public bool IsPlayerDead()
        {
            if (plyr.Health < 1)
            {
                if (plyr.Charon == 1)
                {
                    plyr.Health = 100;
                    plyr.Charon = 0;
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        public void Attack(Npc enemy)
        {
            if (enemy != null)
            {
                enemy.Health -= CauseDamage();
            }
        }

        public void HitPlayer(Npc enemy)
        {
            if (enemy != null && enemy is not Archer)
            {
                SufferDamage(enemy.Damage);
            }
        }

        public void Shoot()
        {
            if (plyr.Bow != null && plyr.Bow.NumberOfArrows != 0)
            {
                Arrows.Add(new Arrow(plyr.Bow.WeaponDamage, plyr.PosX + plyr.Width / 2, plyr.PosY + plyr.Height / 2, 10, 10, directionIsLeft));
                plyr.Bow.NumberOfArrows--;
            }
        }

        public void SufferDamage(double damage)
        {
            if (plyr.Armour > 0)
            {
                damage *= 0.5;
            }
            plyr.Armour -= damage;
            if (plyr.Armour < 0)
            {
                plyr.Health -= (int)plyr.Armour * -1;
                plyr.Armour = 0;
            }
        }

        public double CauseDamage()
        {
            if (plyr.PrimaryWeapon == null)
            {
                CausedDamageEvent("Hand", 10.0);
                return 10.0;
            }
            else
            {
                CausedDamageEvent(plyr.PrimaryWeapon.Name ,plyr.PrimaryWeapon.WeaponDamage);
                return plyr.PrimaryWeapon.WeaponDamage;
            }
        }

        public void ArrowFly()
        {
            foreach (var arrow in Arrows)
            {
                if (arrow.DirectionIsLeft)
                {
                    arrow.PosX -= 15;
                }
                else
                {
                    arrow.PosX += 15;
                }
                if (random.Next(0, 2) == 1)
                {
                    arrow.PosY += 2;
                }
                else
                {
                    arrow.PosY -= 1;
                }
            }
        }

        public void DropItems()
        {
            if (difficulty == "Easy")
            {
                //no drop
            }
            else if (difficulty == "Medium")
            {
                for (int i = 0; i < 2; i++)
                {
                    DropOneItem();
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    DropOneItem();
                }
            }
        }

        private void DropOneItem()
        {
            switch (random.Next(0, 5))
            {
                case 0:
                    if (plyr.HealPotion.Amount > 0)
                    {
                        plyr.HealPotion.Amount--;
                    }
                    break;
                case 1:
                    if (plyr.ArmorRepairKit.Amount > 0)
                    {
                        plyr.ArmorRepairKit.Amount--;
                    }
                    break;
                case 2:
                    plyr.PrimaryWeapon = null;
                    break;
                case 3:
                    plyr.Bow = null;
                    break;
                case 4:
                    plyr.Jetpack.Fuel -= 50;
                    if (plyr.Jetpack.Fuel < 0)
                    {
                        plyr.Jetpack.Fuel = 0;
                    }
                    break;
                default:
                    break;
            }
        }

        public void SetDifficulty(string difficulty)
        {
            this.difficulty = difficulty;
        }
        public void Gravity()
        {
           plyr.PosY += 2;
        }
    }
}

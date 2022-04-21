namespace King_of_the_Hill.Logic
{
    using King_of_the_Hill.Model;
    using King_of_the_Hill.Model.GameItems;
    using King_of_the_Hill.Model.MapItem;
    using King_of_the_Hill.Model.NPC_Types;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows;

    public class PlayerLogic
    {
        string difficulty;
        static Random random = new Random();
        bool directionIsLeft;

        public List<Arrow> Arrows { get; set; }
        public enum Controls
        {
            A, D, W, S, Space, Q, E, R, T
        }

        public Player plyr;
        public PlayerLogic()
        {
            plyr = new Player(100, 100, 0, 0, 75, 75, 1);
            //plyr.Bow.NumberOfArrows = 100;//TODO remove, only for test
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
                    plyr.PosY = plyr.PosY - 25 + plyr.Weight;
                    break;
                case Controls.Q:
                    if (plyr.HealPotion.Amount > 0)
                    {
                        plyr.HealPotion.Amount--;
                        plyr.Health += 50;
                    }
                    break;
                case Controls.E:
                    if (plyr.ArmorRepairKit.Amount > 0)
                    {
                        plyr.ArmorRepairKit.Amount--;
                        plyr.Armour += 50;
                    }
                    break;
                case Controls.T:
                    Shoot();
                    break;
            }
        }

        public void Attack(Npc enemy)
        {
            if (enemy != null)
            {
                enemy.Health -= plyr.returnDamage();
            }
        }

        public void HitPlayer(Npc enemy)
        {
            if (enemy != null)
            {
                plyr.Armour -= enemy.Damage;
                if (plyr.Armour < 0)
                {
                    plyr.Health -= (int)plyr.Armour * -1;
                    plyr.Armour = 0;
                }
            }
        }


        public void Shoot()
        {
            if (plyr.Bow.NumberOfArrows != 0)
            {
                Arrows.Add(new Arrow(plyr.PosX, plyr.PosY, 10, 10, directionIsLeft));
                plyr.Bow.NumberOfArrows--;
            }
        }

        public void ArrowFly()
        {
            foreach (var arrow in Arrows)
            {
                if (arrow.InterSected == false && arrow != null)
                {
                    if (arrow.DirectionIsLeft)
                    {
                        arrow.PosX -= 15;
                    }
                    else
                    {
                        arrow.PosX += 15;
                    }
                    if (random.Next(0,2) == 1)
                    {
                        arrow.PosY += 2;
                    }
                    else
                    {
                        arrow.PosY -= 1;
                    }
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
                    plyr.Bow.NumberOfArrows = 0;
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

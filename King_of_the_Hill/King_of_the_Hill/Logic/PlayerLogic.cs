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
        public List<Arrow> Arrows { get; set; }
        public enum Controls
        {
            A, D, W, S, Space, Q, E, R, T
        }

        public Player plyr;
        public PlayerLogic()
        {
            plyr = new Player(0, 0, 0, 0, 75, 75, 1);
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
                    break;
                case Controls.D:
                    plyr.PosX += 5;
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
                    MessageBox.Show("Armour Restored");
                    break;
                case Controls.E:
                    MessageBox.Show("HP Restored");
                    break;
                case Controls.T:
                    Shoot();
                    break;
            }
        }

        public void Attack(bool couldAttack, Npc Enemy)
        {
            if (Enemy != null)
            {
                Enemy.Health = Enemy.Health - plyr.returnDamage();
            }
        }
        public void Shoot()
        {
            if (!(plyr.bow.Durability == 0))
            {
                Arrows.Add(new Arrow(plyr.PosX, plyr.PosY, 10, 10));
                plyr.bow.Durability -= 1;
            }
        }
        public void ArrowIntersected(List<Npc> Enemies, List<IMapItem> Grounds) //Dont place arrow remove inside loops it causes dataStream error.
        {                                                                       //C# cant handle data looping and modifying (list.remove for example) at the same time.
            Arrow toBeRomved = null;
            foreach (var arrow in Arrows)
            {
                foreach (var ground in Grounds)
                {
                    if (arrow.arrowRect.IntersectsWith(ground.Rectangle))
                    {
                        toBeRomved = arrow;
                    }
                }
            }
            Arrows.Remove(toBeRomved);
            foreach (var arrow in Arrows)
            {
                foreach (var enemy in Enemies)
                {
                    if (arrow.arrowRect.IntersectsWith(enemy.enemyRect))
                    {
                        toBeRomved = arrow;
                        enemy.Health -= plyr.bow.WeaponDamage;
                    }
                }
            }
            if (toBeRomved != null)
            {
                Arrows.Remove(toBeRomved);
            }
        }
        public void ArrowFly()
        {
            foreach (var arrow in Arrows)
            {
                if (arrow.InterSected == false && arrow != null)
                {
                    arrow.PosX += 15;
                    Random rnd = new Random();
                    if (rnd.Next(0,2) == 1)
                    {
                        arrow.PosY += 1.75;
                    }
                    else
                    {
                        arrow.PosY -= 1.75;
                    }
                }
            }
        }
        public void Gravity(double Weight)
        {
           plyr.PosY = plyr.PosY + Weight;
        }
    }
}

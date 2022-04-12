namespace King_of_the_Hill.Logic
{
    using King_of_the_Hill.Model;
    using King_of_the_Hill.Model.NPC_Types;
    using System;
    using System.Drawing;
    using System.Windows;

    public class PlayerLogic
    {
        public Action<int> InventoryAddHPFromLogic;
        public Action<int> InventoryAddArmorFromLogic;
        public Action<int> InventoryAddHealPotionFromLogic;
        public Action<int> InventoryAddArmorReapirKitFromLogic;

        public enum Controls
        {
            A, D, W, S, Space, Q, E
        }

        public Player plyr;
        public PlayerLogic()
        {
            plyr = new Player(100, 100, 0, 0, 75, 75, 1);
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
                    if (plyr.HealPotion.Amount > 0)
                    {
                        plyr.HealPotion.Amount--;
                        plyr.Health += 50;
                        InventoryAddHPFromLogic((int)plyr.Health);
                        InventoryAddHealPotionFromLogic(plyr.HealPotion.Amount);
                    }
                    break;
                case Controls.E:
                    if (plyr.ArmorRepairKit.Amount > 0)
                    {
                        plyr.ArmorRepairKit.Amount--;
                        plyr.Armour += 50;
                        InventoryAddArmorFromLogic((int)plyr.Armour);
                        InventoryAddArmorReapirKitFromLogic(plyr.ArmorRepairKit.Amount);
                    }
                    break;
            }
        }

        public void Attack(bool couldAttack, Npc Enemy)
        {
            if (Enemy != null)
            {
                MessageBox.Show($"Before Hit calc the HP is: {Enemy.Health}");
                Enemy.Health = Enemy.Health - plyr.returnDamage();
                MessageBox.Show($"After Hit calc the HP is: {Enemy.Health}");
            }
        }

        public void Gravity(double Weight)
        {
           plyr.PosY = plyr.PosY + Weight;
        }
    }
}

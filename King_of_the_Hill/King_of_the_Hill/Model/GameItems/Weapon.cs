namespace King_of_the_Hill.Model.GameItems
{
    using System.Drawing;

    public class Weapon
    {
        public double WeaponDamage { get; set; }
        public string WeaponName { get; set; }
        public double Durability { get; set; }
        public double AttackSpeed { get; set; }
        public double PosX { get; set; }
        public double PosY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Rectangle WeaponRect
        {
            get
            {
                return new Rectangle((int)PosX, (int)PosY, Width, Height);
            }
        }

        public Weapon(double weaponDamage, string weaponName, double durability, double attackSpeed, double PosX, double PosY)
        {
            WeaponDamage = weaponDamage;
            WeaponName = weaponName;
            Durability = durability;
            AttackSpeed = attackSpeed;
            this.PosX = PosX;
            this.PosY = PosY;
            Width = 50;
            Height = 50;
        }

        
    }
}

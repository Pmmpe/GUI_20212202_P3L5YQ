namespace King_of_the_Hill.Model.NPC_Types
{
    using System.Drawing;
    public class Npc : Character
    {
        public bool DirectionIsLeft { get; set; } // true - bal, false - jobb
        public double Damage { get; }

        public Npc(double Health, double Armour, double PosX, double PosY, double Width, double Height, double damage) : base(PosX, PosY, Width, Height, Health, Armour)
        {
            DirectionIsLeft = true;
            Damage = damage;
        }
        

        public Rectangle enemyRect
        {
            get
            {
                return new Rectangle((int)PosX, (int)PosY, (int)Width, (int)Height);
            }
        }
    }
}

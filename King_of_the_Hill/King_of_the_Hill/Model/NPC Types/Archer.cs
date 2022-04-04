namespace King_of_the_Hill.Model.NPC_Types
{
    using System.Drawing;
    public class Archer : Npc
    {
        public Archer(double Health, double Armour, double Speed, Size gameArea, int itemRadius) : base(Health, Armour, Speed, gameArea, itemRadius)
        {
        }
    }
}

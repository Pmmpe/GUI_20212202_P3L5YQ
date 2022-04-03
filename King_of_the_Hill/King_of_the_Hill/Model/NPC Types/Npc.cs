namespace King_of_the_Hill.Model
{
    using System.Drawing;
    public class Npc : Character
    {
        public Npc(double Health, double Armour, double Speed, Size gameArea, int itemRadius) : base(Health, Armour, Speed, gameArea, itemRadius)
        {
        }
    }
}

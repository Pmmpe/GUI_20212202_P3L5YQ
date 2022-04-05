namespace King_of_the_Hill.Model.NPC_Types
{
    using System.Drawing;
    public class ShieldBearer : Npc //just and idea nees further disscussion.
                                    //Could only be damaged from the opposite side to his shield
                                    //or only from upward.
    {
        public ShieldBearer(double Health, double Armour) : base(Health, Armour)
        {
        }
    }
}

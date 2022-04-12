namespace King_of_the_Hill.Model.GameItems
{
    public class HealPotion : GameItem
    {
        public int Amount { get; set; }
        public HealPotion(string name, double posX, double posY) : base(name, posX, posY)
        {
            Amount = 0;
        }
    }
}

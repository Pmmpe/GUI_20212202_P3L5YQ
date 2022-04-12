namespace King_of_the_Hill.Model.GameItems
{
    public class Armor : GameItem
    {
        public int Amount { get; set; }
        public Armor(string name, double posX, double posY) : base(name, posX, posY)
        {
            Amount = 0;
        }
    }
}

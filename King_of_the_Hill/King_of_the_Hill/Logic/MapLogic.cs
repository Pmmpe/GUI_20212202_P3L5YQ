namespace King_of_the_Hill.Logic
{
    using King_of_the_Hill.Model.MapItem;
    using System.Collections.Generic;
    using System.Drawing;
    public class MapLogic
    {
        public List<IMapItem> Grounds { get; set; }
        string difficulty;

        Size area;

        public MapLogic()
        {
            difficulty = "";
            Grounds = new List<IMapItem>();
        }

        public void SetupSizes(Size area)
        {
            this.area = area;
        }

        public void CreateMap()
        {
            if (difficulty == "Easy")
            {
                Grounds.Add(new Ground(0, area.Height - 100, 1200, 100));
                Grounds.Add(new Lava(1200, area.Height - 101, area.Width, 101));
                Grounds.Add(new Platform(1000, 250, 500, 25));
                Grounds.Add(new Platform(450, 400, 900, 25));
                Grounds.Add(new StartPlatform(100, 155, 100, 25));
            } 
            else if(difficulty == "Medium")
            {
                Grounds.Add(new Ground(0, area.Height - 100, 400, 100));
                Grounds.Add(new Ground(600, area.Height - 100, 800, 100));
                Grounds.Add(new Lava(400, area.Height - 101, 200, 101));
                Grounds.Add(new Lava(1400, area.Height - 101, area.Width, 101));
                Grounds.Add(new Platform(1000, 250, 500, 25));
                Grounds.Add(new StartPlatform(100, 155, 100, 25));
            }
            else
            {

            }
        }

        public void SetDifficulty(string difficulty)
        {
            this.difficulty = difficulty;
        }
    }
}

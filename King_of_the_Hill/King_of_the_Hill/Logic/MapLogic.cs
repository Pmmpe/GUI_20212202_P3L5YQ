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
                Grounds.Add(new Lava(1200, area.Height - 101, area.Width - 1200, 101));
                Grounds.Add(new Platform(1000, 250, 500, 25));
                Grounds.Add(new Platform(450, 400, 900, 25));
                Grounds.Add(new StartPlatform(100, 155, 100, 25));
            } 
            else if(difficulty == "Medium")
            {
                Grounds.Add(new Ground(0, area.Height - 100, 400, 100));
                Grounds.Add(new Ground(600, area.Height - 100, 800, 100));
                Grounds.Add(new Lava(400, area.Height - 101, 200, 101));
                Grounds.Add(new Lava(1400, area.Height - 101, area.Width - 1400, 101));
                Grounds.Add(new Platform(1000, 250, 500, 25));
                Grounds.Add(new StartPlatform(100, 155, 100, 25));
            }
            else
            {
                Grounds.Add(new Ground(200, area.Height - 100, 600, 100));
                Grounds.Add(new Ground(1200, area.Height - 100, area.Width - 1200, 100));
                Grounds.Add(new Lava(0, area.Height - 101, 200, 101));
                Grounds.Add(new Lava(800, area.Height - 101, 400, 101));
                Grounds.Add(new Platform(500, 150, 250, 25));
                Grounds.Add(new Platform(850, 200, 250, 25));
                Grounds.Add(new Platform(1050, 300, area.Width - 1050 - 100, 25));
                Grounds.Add(new Platform(800, 450, 350, 25));
                Grounds.Add(new StartPlatform(100, 155, 100, 25));
            }
        }

        public void SetDifficulty(string difficulty)
        {
            this.difficulty = difficulty;
        }
    }
}

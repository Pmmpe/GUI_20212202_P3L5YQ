namespace King_of_the_Hill.Logic
{
    using King_of_the_Hill.Model.MapItem;
    using System.Collections.Generic;
    using System.Drawing;
    public class MapLogic
    {
        public List<IMapItem> Grounds { get; set; }

        int maxMapNumber;
        int actualMapNumber;
        string difficulty;

        Size area;

        public MapLogic()
        {
            actualMapNumber = 0;
            Grounds = new List<IMapItem>();
        }

        public void SetupSizes(Size area)
        {
            this.area = area;
        }

        public void NextMap()
        {
            actualMapNumber++;
            if (actualMapNumber <= maxMapNumber)
            {
                CreateMap();
            }
        }

        private void CreateMap()
        {
            switch (actualMapNumber)
            {
                case 1:
                    Grounds.Add(new Ground(0, area.Height - 100, 400, 100));
                    Grounds.Add(new Ground(600, area.Height - 100, 800, 100));
                    Grounds.Add(new Lava(400, area.Height - 101, 200, 101));
                    Grounds.Add(new Lava(1400, area.Height - 101, area.Width, 101));
                    Grounds.Add(new Platform(1000, 250, 500, 25));
                    Grounds.Add(new Platform(100, 155, 100, 25));

                    //ha jól be lesznek állítva a player adatai akkor jó lesz ez a platform. Addig csak be van rakva alá egy.
                    //mapLogic.Grounds.Add(new Platform((int)playerModel.player.PosX, (int)playerModel.player.PosY + 60, 100, 25));




                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
            }
        }

        public void SetDifficulty(string difficulty)
        {
            this.difficulty = difficulty;
            if (difficulty == "Easy")
            {
                maxMapNumber = 2;
            }
            else if (difficulty == "Medium")
            {
                maxMapNumber = 4;
            }
            else
            {
                maxMapNumber = 5;
            }
        }
    }
}

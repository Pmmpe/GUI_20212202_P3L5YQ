using King_of_the_Hill.Model.NPC_Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace King_of_the_Hill.Logic
{
    public class EnemyLogic
    {
        int actualWaveNumber;
        int maxWaveNumber;
        string difficulty;
        public List<Npc> enemies; //ellenségek listája

        public EnemyLogic()
        {
            actualWaveNumber = 0;
            enemies = new List<Npc>();
            difficulty = "";
        }

        public void NextWave()
        {
            actualWaveNumber++;
            if (actualWaveNumber <= maxWaveNumber)
            {
                CreateEnemies();
            }
        }

        //Hullámonként ide tudod beírni, hogy hány ellenség spawnoljon
        private void CreateEnemies()
        {
            switch (actualWaveNumber)
            {
                case 1:
                    enemies.Add(new Grunt(50, 0, 0, 0, 50, 50));
                    enemies.Add(new Grunt(50, 0, 0, 0, 50, 50));
                    enemies.Add(new Brute(100, 0, 0, 0, 50, 50));
                    enemies.Add(new Brute(100, 0, 0, 0, 50, 50));
                    enemies.Add(new Brute(100, 0, 0, 0, 50, 50));
                    enemies.Add(new Archer(30, 0, 0, 0, 50, 50));
                    enemies.Add(new HeavyBrute(250, 0, 0, 0, 75, 75));
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    break;
                case 11:
                    break;
                case 12:
                    break;
                case 13:
                    break;
                case 14:
                    break;
                case 15:
                    break;
            }
        }

        public void Move()
        {
            foreach (var enemy in enemies)
            {
                if (enemy.DirectionIsLeft)
                {
                    enemy.PosX--;
                }
                else
                {
                    enemy.PosX++;
                }
            }
        }

        //beállítja hány hullám legyen
        public void SetDifficulty(string difficulty)
        {
            this.difficulty = difficulty;
            if (difficulty == "Easy")
            {
                maxWaveNumber = 5;
            }
            else if (difficulty == "Medium")
            {
                maxWaveNumber = 10;
            }
            else
            {
                maxWaveNumber = 15;
            }
        }
    }
}

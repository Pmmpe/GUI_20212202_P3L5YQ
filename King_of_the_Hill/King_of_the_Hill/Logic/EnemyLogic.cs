namespace King_of_the_Hill.Logic
{
    using King_of_the_Hill.Model;
    using King_of_the_Hill.Model.GameItems;
    using King_of_the_Hill.Model.NPC_Types;
    using System;
    using System.Collections.Generic;

    public class EnemyLogic
    {
        int actualWaveNumber;
        int maxWaveNumber;
        string difficulty;
        double attackDamageMultiplier;
        public List<Npc> enemies; //List of every used current enemy npc unit in the current play session!
        public List<Arrow> arrows;

        public string AchievedScore { get { return difficulty + " -> " + actualWaveNumber + " Pont"; } }


        static Random random = new Random();

        public EnemyLogic()
        {
            actualWaveNumber = 0;
            enemies = new List<Npc>();
            arrows = new List<Arrow>();
            difficulty = "";
        }

        public void ArrowFly()
        {
            foreach (var arrow in arrows)
            {
                if (arrow.DirectionIsLeft)
                {
                    arrow.PosX -= 15;
                }
                else
                {
                    arrow.PosX += 15;
                }
                if (random.Next(0, 2) == 1)
                {
                    arrow.PosY += 2;
                }
                else
                {
                    arrow.PosY -= 1;
                }
            }
        }

        public void NextWave()
        {
            actualWaveNumber++;
            if (actualWaveNumber <= maxWaveNumber)
            {
                CreateEnemies();
            }
        }

        //It spawns every single different enemy wave in the game.
        //You could determinate what kind of enemies and how many of
        //them to come in every single round, per wave by wave!
        private void CreateEnemies()
        {
            GenerateRandomEnemies();
            if (actualWaveNumber == 5 || actualWaveNumber == 10 || actualWaveNumber == 15)
            {
                enemies.Add(new HeavyBrute(250, 0, 0, 0, 75, 75, 25 * attackDamageMultiplier));
            }
        }

        private void GenerateRandomEnemies()
        {
            if (difficulty == "Easy")
            {
                for (int i = 0; i < random.Next(1, 4); i++)
                {
                    enemies.Add(new Grunt(50, 0, 0, 0, 50, 50, 10 * attackDamageMultiplier));
                }
                for (int i = 0; i < random.Next(1, 3); i++)
                {
                    enemies.Add(new Brute(100, 0, 0, 0, 50, 50, 12 * attackDamageMultiplier));
                }
                for (int i = 0; i < random.Next(0, 2); i++)
                {
                    enemies.Add(new Archer(30, 0, 0, 0, 50, 50, 15 * attackDamageMultiplier));
                }
            }
            else if (difficulty == "Medium")
            {
                for (int i = 0; i < random.Next(1, 5); i++)
                {
                    enemies.Add(new Grunt(50, 0, 0, 0, 50, 50, 10 * attackDamageMultiplier));
                }
                for (int i = 0; i < random.Next(1, 3); i++)
                {
                    enemies.Add(new Brute(100, 0, 0, 0, 50, 50, 12 * attackDamageMultiplier));
                }
                for (int i = 0; i < random.Next(1, 3); i++)
                {
                    enemies.Add(new Archer(30, 0, 0, 0, 50, 50, 15 * attackDamageMultiplier));
                }
            }
            else
            {
                for (int i = 0; i < random.Next(1, 5); i++)
                {
                    enemies.Add(new Grunt(50, 0, 0, 0, 50, 50, 10 * attackDamageMultiplier));
                }
                for (int i = 0; i < random.Next(1, 4); i++)
                {
                    enemies.Add(new Brute(100, 0, 0, 0, 50, 50, 12 * attackDamageMultiplier));
                }
                for (int i = 0; i < random.Next(2, 3); i++)
                {
                    enemies.Add(new Archer(30, 0, 0, 0, 50, 50, 15 * attackDamageMultiplier));
                }
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

        //It sets in the actual number of waves int the current
        //game session according to the given difficulity!
        public void SetDifficulty(string difficulty)
        {
            this.difficulty = difficulty;
            if (difficulty == "Easy")
            {
                maxWaveNumber = 5;
                attackDamageMultiplier = 0.5;
            }
            else if (difficulty == "Medium")
            {
                maxWaveNumber = 10;
                attackDamageMultiplier = 0.75;
            }
            else
            {
                maxWaveNumber = 15;
                attackDamageMultiplier = 1.0;
            }
        }

        public bool IsOnlyArcher()
        {
            foreach (var enemy in enemies)
            {
                if (enemy is not Archer)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsEndGame()
        {
            if (difficulty == "Easy")
            {
                return actualWaveNumber == 5;
            }
            else if (difficulty == "Medium")
            {
                return actualWaveNumber == 10;
            }
            else
            {
                return actualWaveNumber == 15;
            }
        }
    }
}

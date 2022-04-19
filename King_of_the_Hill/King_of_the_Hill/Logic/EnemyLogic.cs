﻿namespace King_of_the_Hill.Logic
{
    using King_of_the_Hill.Model.NPC_Types;
    using System.Collections.Generic;

    public class EnemyLogic
    {
        int actualWaveNumber;
        int maxWaveNumber;
        string difficulty;
        public List<Npc> enemies; //List of every used current enemy npc unit in the current play session!

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

        //It spawns every single different enemy wave in the game.
        //You could determinate what kind of enemies and how many of
        //them to come in every single round, per wave by wave!
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
                    enemies.Add(new Heavy_Brute(250, 0, 0, 0, 75, 75));
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
                if (enemy.Direction)
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

        public void RemoveDeadEnemies()
        {
            Npc enemyToBeDeleted = null;
            foreach (var enemy in enemies)
            {
                if (enemy.Health <= 0)
                {
                    enemyToBeDeleted = enemy;
                }
            }
            if (enemyToBeDeleted != null)
            {
                enemies.Remove(enemyToBeDeleted);
            }
        }
    }
}

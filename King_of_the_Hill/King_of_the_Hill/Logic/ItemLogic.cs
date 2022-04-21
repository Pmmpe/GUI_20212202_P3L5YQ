using King_of_the_Hill.Model.GameItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace King_of_the_Hill.Logic
{
    public class ItemLogic
    {
        int actualWaveNumber;
        int maxWaveNumber;
        string difficulty;
        double attackDamageMultiplier;
        public List<GameItem> items; //itemek listája

        static Random random = new Random();

        public ItemLogic()
        {
            actualWaveNumber = 0;
            items = new List<GameItem>();
            difficulty = "";
        }

        public void NextWave()
        {
            actualWaveNumber++;
            if (actualWaveNumber <= maxWaveNumber)
            {
                CreateItems();
            }
        }

        //Hullámonként ide tudod beírni, hogy hány item spawnoljon
        private void CreateItems()
        {
            //Ez azért van ide kirakva mert minden körben generálódik 2 fegyver.
            GenerateRandomWeapons();
            GenerateRandomWeapons();

            //Jetpack mindig 50% al van
            if (random.Next(0, 2) == 0)
            {
                items.Add(new Jetpack("Jetpack", 0, 0));
            }

            //Potik amik csak wave számától függően generálódik
            switch (actualWaveNumber)
            {
                case 1:
                    if (difficulty == "Easy")
                    {
                        items.Add(new HealPotion("HP", 0, 0));
                    }
                    else if (difficulty == "Medium")
                    {

                    }
                    else //Hard
                    {

                    }
                    break;
                case 2:
                    if (difficulty == "Easy")
                    {
                        items.Add(new Armor("Armor", 0, 0));
                    }
                    else if (difficulty == "Medium")
                    {
                        items.Add(new HealPotion("HP", 0, 0));
                    }
                    else //Hard
                    {

                    }
                    break;
                case 3:
                    if (difficulty == "Easy")
                    {
                        items.Add(new HealPotion("HP", 0, 0));
                    }
                    else if (difficulty == "Medium")
                    {

                    }
                    else //Hard
                    {

                    }
                    break;
                case 4:
                    if (difficulty == "Easy")
                    {
                        items.Add(new Armor("Armor", 0, 0));
                    }
                    else if (difficulty == "Medium")
                    {
                        items.Add(new Armor("Armor", 0, 0));
                    }
                    else //Hard
                    {

                    }
                    break;
                case 5:
                    if (difficulty == "Easy")
                    {
                        items.Add(new HealPotion("HP", 0, 0));
                    }
                    else if (difficulty == "Medium")
                    {

                    }
                    else //Hard
                    {
                        items.Add(new HealPotion("HP", 0, 0));
                    }
                    break;
                case 6:
                    if (difficulty == "Medium")
                    {
                        items.Add(new HealPotion("HP", 0, 0));
                    }
                    else //Hard
                    {

                    }
                    break;
                case 7:
                    if (difficulty == "Medium")
                    {

                    }
                    else //Hard
                    {

                    }
                    break;
                case 8:
                    if (difficulty == "Medium")
                    {
                        items.Add(new Armor("Armor", 0, 0));
                    }
                    else //Hard
                    {

                    }
                    break;
                case 9:
                    if (difficulty == "Medium")
                    {

                    }
                    else //Hard
                    {

                    }
                    break;
                case 10:
                    if (difficulty == "Medium")
                    {
                        items.Add(new HealPotion("HP", 0, 0));
                    }
                    else //Hard
                    {
                        items.Add(new HealPotion("HP", 0, 0));
                        items.Add(new Armor("Armor", 0, 0));
                    }
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
                    items.Add(new HealPotion("HP", 0, 0));
                    items.Add(new Armor("Armor", 0, 0));
                    break;
            }
        }

        //Fegyverek random generálása
        //TODO: attackspeed és durability beállítása
        private void GenerateRandomWeapons()
        {
            switch (random.Next(0, 4))
            {
                case 0:
                    items.Add(new Sword(25 * attackDamageMultiplier, "Sword", 1, 0, 0));
                    break;
                case 1:
                    items.Add(new LongSword(35 * attackDamageMultiplier, "LongSword", 2, 0, 0));
                    break;
                case 2:
                    items.Add(new Axe(25 * attackDamageMultiplier, "Axe", 3, 0, 0));
                    break;
                case 3:
                    //Az íjnál kell nyíl darabszám beállítás is nehézségi szinttől függően ezért kell az if
                    if (difficulty == "Easy")
                    {
                        items.Add(new Bow(25 * attackDamageMultiplier, "Bow", 5, 15, 0, 0));
                    }
                    else if (difficulty == "Medium")
                    {
                        items.Add(new Bow(25 * attackDamageMultiplier, "Bow", 5, 10, 0, 0));
                    }
                    else //Hard
                    {
                        items.Add(new Bow(25 * attackDamageMultiplier, "Bow", 5, 5, 0, 0));
                    }
                    break;
            }
        }

        public void SetDifficulty(string difficulty)
        {
            this.difficulty = difficulty;
            if (difficulty == "Easy")
            {
                maxWaveNumber = 5;
                attackDamageMultiplier = 1.5;
            }
            else if (difficulty == "Medium")
            {
                maxWaveNumber = 10;
                attackDamageMultiplier = 1.25;
            }
            else
            {
                maxWaveNumber = 15;
                attackDamageMultiplier = 1.0;
            }
        }
    }
}

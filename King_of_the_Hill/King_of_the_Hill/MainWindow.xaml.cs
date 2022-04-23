namespace King_of_the_Hill
{
    using King_of_the_Hill.Logic;
    using King_of_the_Hill.Model;
    using King_of_the_Hill.Model.GameItems;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;

    /// <summary>
    /// Main logical part of the project it checks every component and it does every single move
    /// or calculation from frame by frame according to the DispatcherTimers tickrate.
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Initialization
        PlayerLogic playerLogic;
        EnemyLogic enemyLogic;
        ItemLogic itemLogic;
        MapLogic mapLogic;
        IntersectLogic intersectLogic;
        SoundLogic soundplayer; //sound
        AnimationsLogic animationsLogic; //animation

        DispatcherTimer timer;

        double lastAttacksTime;

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            playerLogic = new PlayerLogic();
            enemyLogic = new EnemyLogic();
            itemLogic = new ItemLogic();
            mapLogic = new MapLogic();
            intersectLogic = new IntersectLogic(playerLogic, mapLogic, enemyLogic, itemLogic);
            // animation
            animationsLogic = new AnimationsLogic();

            lastAttacksTime = 0.0;

            soundplayer = new SoundLogic(); //sound
            
            
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += Timer_Tick;

            
        }

        //Returns if enough time is elapsed since last attack or not.
        private static bool isTimeElapsed(DispatcherTimer timer, double lastAttack)
        {
            //if (lastAttack + 500 < timer.Interval.TotalMilliseconds)
            //{
            //    return true;
            //}
            //return false;
            return true;
        }

        //Button check for attacking, used in Timer_Tick, calling PlayerLogics attack function.
        //Uses isTimeElapsed function to check the time elapsed between the button presses, to
        //prevent constant damaging!
        private bool isAttackButtonDown(PlayerLogic playerLogic, EnemyLogic enemyLogic, IntersectLogic intersectLogic)
        {
            if (Keyboard.IsKeyDown(Key.R))
            {
                if (isTimeElapsed(timer, lastAttacksTime))
                {
                    playerLogic.Attack(intersectLogic.isPlayerIntersectWithAnyNPC(playerLogic, enemyLogic), intersectLogic.PlayerIntersectWithThat(playerLogic, enemyLogic));
                }
                return true;
            }
            return false;
        }
        private void Timer_Tick(object? sender, EventArgs e)
        {
            display.InvalidateVisual();
            intersectLogic.IsPlayerAndMapIntersect();   // Summarized at implementation! 
            intersectLogic.SetPlayerInTheMap();         // Summarized at implementation!
             intersectLogic.SetEnemyDirection();        // Summarized at implementation!
             enemyLogic.Move();                         //NPC moving function!
            intersectLogic.PlayerIntersectWithItem();   //item pick up
            

            //Chain functions: The player is attacking if the first checker function returns true for NPC intersecting and the "K" has been pressed down,
            //then the second function returns the NPC that is currently intersecting with the player! The player will then causes damage to this npc equal to
            //his or her Weight * (Weapon) weapons.WeaponDamage;
            isAttackButtonDown(playerLogic, enemyLogic, intersectLogic);

            isSpaceButtonDown(animationsLogic);
        }

        private void isSpaceButtonDown(AnimationsLogic animationsLogic) //jetpackAnimation
        {
            if (Keyboard.IsKeyDown(Key.Space))
            {
                if (true) //ToDo
                {
                    animationsLogic.StartJetpackAnimation();
                    
                }
                
            }
            else
            {
                animationsLogic.StopJetpackAnimation();
            }
            
        }

        #region CharMoving



        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.W) && intersectLogic.IsPlayerAndMapIntersect())
            {
                playerLogic.Control(PlayerLogic.Controls.W);
                
            }
            if (Keyboard.IsKeyDown(Key.S) && intersectLogic.IsPlayerAndMapIntersect())
            {
                playerLogic.Control(PlayerLogic.Controls.S);
                
            }
            if (Keyboard.IsKeyDown(Key.A))
            {
                playerLogic.Control(PlayerLogic.Controls.A);
                playerLogic.plyr.LeftOrientation = true;
                //animationsLogic.MoveLeftAnimation(playerLogic.plyr.LeftOrientation);
            }
            if (Keyboard.IsKeyDown(Key.D))
            {
                playerLogic.Control(PlayerLogic.Controls.D);
                playerLogic.plyr.LeftOrientation = false;
                //animationsLogic.MoveRightAnimation(playerLogic.plyr.LeftOrientation);
            }
            if (Keyboard.IsKeyDown(Key.E))
            {
                playerLogic.Control(PlayerLogic.Controls.E);
            }
            if (Keyboard.IsKeyDown(Key.Q))
            {
                playerLogic.Control(PlayerLogic.Controls.Q);
            }
            if (Keyboard.IsKeyDown(Key.Space))
            {
                if (playerLogic.plyr.Jetpack.Fuel > 0 || true) //change for test
                {
                    playerLogic.plyr.Jetpack.Fuel--;
                    InventorySetJetpackFuel(playerLogic.plyr.Jetpack.Fuel);
                    playerLogic.Control(PlayerLogic.Controls.Space);
                    
                }
                
            }
            if (Keyboard.IsKeyDown(Key.NumPad1))
            {
                //playerLogic.plyr.PrimaryWeapon.Name = "DELETED"; //nem tudom ki null-ozni szóval ha nincs a playernek fegyverek akkor átírjuk a fegyverének a nevét DELETED-re
                playerLogic.plyr.PrimaryWeapon = null;
                InventorySetWeaponName("N/A");
            }
            if (Keyboard.IsKeyDown(Key.NumPad2))
            {
                if (playerLogic.plyr.Bow.NumberOfArrows > 0)
                {
                    playerLogic.plyr.Bow.NumberOfArrows--;
                    InventorySetArrowNumber(playerLogic.plyr.Bow.NumberOfArrows);
                }
            }
            if (Keyboard.IsKeyDown(Key.Enter)) //indítja az ellenségek spawnolását, azaz az új hullámot
            {
                if (enemyLogic.enemies.Count == 0)
                {
                    enemyLogic.NextWave();
                    intersectLogic.GenerateEnemiesPositons(); //ellenségek legenerálása véletlenszerű helyekre
                    display.InvalidateVisual();
                }
            }
        }
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mapLogic.SetDifficulty("Easy"); //alapból Easy beállítása.
            enemyLogic.SetDifficulty("Easy"); //alapból Easy beállítása.
            itemLogic.SetDifficulty("Easy"); //alapból Easy beállítása.
            mapLogic.SetupSizes(new System.Drawing.Size((int)display.ActualWidth, (int)display.ActualHeight));
            intersectLogic.SetSizes((int)display.ActualWidth, (int)display.ActualHeight); //It gives the current sizes!
            display.SetupAllLogic(mapLogic, playerLogic, enemyLogic, itemLogic, animationsLogic); //It passes through the logics!
            
            //Main Menu theme song!
            soundplayer.BackgroundMusicMenu("start");
            //menu zene

            //esemény feliratkoztatása
            intersectLogic.InventoryAddWeaponFromLogic = InventorySetWeaponName;
            intersectLogic.InventoryAddArrowsFromLogic = InventorySetArrowNumber;
            intersectLogic.InventoryAddCharonFromLogic = InventorySetCharon;
            intersectLogic.InventoryAddJetpackFuelFromLogic = InventorySetJetpackFuel;
            intersectLogic.InventoryAddHPFromLogic = InventorySetHP;
            intersectLogic.InventoryAddArmorFromLogic = InventorySetArmor;

            
            
        }

        #region InventoryAndMenu
        
        public void InventorySetWeaponName(string weaponName)
        {
            label_slotOne.Content = weaponName;
        }

        public void InventorySetArrowNumber(int numberOfArrows)
        {
            label_slotTwo.Content = numberOfArrows;
        }

        public void InventorySetCharon(int number)
        {
            label_slotThree.Content = number;
        }

        public void InventorySetJetpackFuel(int fuel)
        {
            label_slotFour.Content = fuel;
        }

        public void InventorySetHP(int numberOfHP)
        {
            progressbar_hp.Value = numberOfHP;
        }

        public void InventorySetArmor(int numberOfAmor)
        {
            progressbar_armor.Value = numberOfAmor;
        }


        private void Play_Click(object sender, RoutedEventArgs e)
        {
            menu.Visibility = Visibility.Hidden;
            gamegrid.Visibility = Visibility.Visible;
            mapLogic.NextMap(); //következő pálya indítása, jelen esetben az első pálya indul.
            itemLogic.NextWave(); //következő hullám indítása, jelen esetben az első hullám indul.
            intersectLogic.GenerateItemsPositions(); //itemek legenerálása random helyekre
            intersectLogic.PutPlayerOnTheStartPlatform();
            
            timer.Start();

            soundplayer.PlayActionSound(SoundLogic.MenusSounds.game_start);
            //game zene
            soundplayer.BackgroundMusicMenu("stop");
            soundplayer.BackgroundMusicGame("start");
            //game zene

            display.InvalidateVisual();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            soundplayer.PlayActionSound(SoundLogic.MenusSounds.button_click); //sound
            menu.Visibility = Visibility.Hidden;
            difficulty_select.Visibility = Visibility.Visible;
        }

        private void Easy_Click(object sender, RoutedEventArgs e)
        {
            soundplayer.PlayActionSound(SoundLogic.MenusSounds.button_click); //sound
            label_difficulty.Content = "Current Difficulty: Easy";
            mapLogic.SetDifficulty("Easy");
            enemyLogic.SetDifficulty("Easy");
            itemLogic.SetDifficulty("Easy");
        }

        private void Medium_Click(object sender, RoutedEventArgs e)
        {
            soundplayer.PlayActionSound(SoundLogic.MenusSounds.button_click); //sound
            label_difficulty.Content = "Current Difficulty: Medium";
            mapLogic.SetDifficulty("Medium");
            enemyLogic.SetDifficulty("Medium");
            itemLogic.SetDifficulty("Medium");
        }

        private void Hard_Click(object sender, RoutedEventArgs e)
        {
            soundplayer.PlayActionSound(SoundLogic.MenusSounds.button_click); //sound
            label_difficulty.Content = "Current Difficulty: Hard";
            mapLogic.SetDifficulty("Hard");
            enemyLogic.SetDifficulty("Hard");
            itemLogic.SetDifficulty("Hard");
        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            difficulty_select.Visibility = Visibility.Hidden;
            menu.Visibility = Visibility.Visible;
        }
        #endregion
    }
}

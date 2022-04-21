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

        DispatcherTimer timer;

        int counter = 0;
        int canAttackCounter = 0;

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            playerLogic = new PlayerLogic();
            enemyLogic = new EnemyLogic();
            itemLogic = new ItemLogic();
            mapLogic = new MapLogic();
            intersectLogic = new IntersectLogic(playerLogic, mapLogic, enemyLogic, itemLogic);

            soundplayer = new SoundLogic(); //sound
            
            
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += Timer_Tick;
        }

        

        private void Timer_Tick(object? sender, EventArgs e)
        {
            display.InvalidateVisual();
            intersectLogic.IsPlayerAndMapIntersect();   // Summarized at implementation! 
            intersectLogic.SetPlayerInTheMap();         // Summarized at implementation!
            intersectLogic.SetEnemyDirection();        // Summarized at implementation!
            intersectLogic.PlayerIntersectWithItem();   //item pick up
            enemyLogic.RemoveDeadEnemies();
            enemyLogic.Move();                         //NPC moving function!


            playerLogic.ArrowFly();
            intersectLogic.ArrowIntersected();

            if (intersectLogic.isPlayerIntersectWithAnyNPC())
            {
                if (canAttackCounter == 50)
                {
                    playerLogic.HitPlayer(intersectLogic.PlayerIntersectWithThat());
                    canAttackCounter = 0;
                }
                canAttackCounter++;
            }
            else
            {
                canAttackCounter = 0;
            }
            
            if (enemyLogic.IsOnlyArcher())
            {
                if (counter == 50)
                {
                    label_enter.Visibility = Visibility.Visible;
                    label_next.Visibility = Visibility.Visible;
                }
                if (counter == 100)
                {
                    label_enter.Visibility = Visibility.Hidden;
                    label_next.Visibility = Visibility.Hidden;
                    counter = 0;
                }
                counter++;
            }
            InventoryDataChanged();
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
            }
            if (Keyboard.IsKeyDown(Key.D))
            {
                playerLogic.Control(PlayerLogic.Controls.D);
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
                if (playerLogic.plyr.Jetpack.Fuel > 0)
                {
                    playerLogic.plyr.Jetpack.Fuel--;
                    playerLogic.Control(PlayerLogic.Controls.Space);
                }              
            }
            if (Keyboard.IsKeyDown(Key.NumPad1))
            {
                playerLogic.plyr.PrimaryWeapon = null;
            }
            if (Keyboard.IsKeyDown(Key.NumPad2))
            {
                if (playerLogic.plyr.Bow.NumberOfArrows > 0)
                {
                    playerLogic.plyr.Bow.NumberOfArrows--;
                }
                //temp
                enemyLogic.enemies.Clear();
                itemLogic.items.Clear();
            }
            if (Keyboard.IsKeyDown(Key.R))
            {
                playerLogic.Attack(intersectLogic.PlayerIntersectWithThat());
            }
            if (Keyboard.IsKeyDown(Key.T))
            {
                playerLogic.Control(PlayerLogic.Controls.T);
            }
            if (Keyboard.IsKeyDown(Key.Enter)) //indítja az ellenségek spawnolását, azaz az új hullámot
            {
                label_enter.Visibility = Visibility.Hidden;
                label_next.Visibility = Visibility.Hidden;
                if (enemyLogic.enemies.Count == 0 || enemyLogic.IsOnlyArcher())
                {
                    if (enemyLogic.IsEndGame())
                    {
                        MessageBox.Show("Nyertél!");
                    }
                    enemyLogic.enemies.Clear();
                    itemLogic.items.Clear();
                    enemyLogic.NextWave();
                    itemLogic.NextWave();
                    playerLogic.DropItems();
                    intersectLogic.GenerateItemsPositions(); //itemek legenerálása random helyekre
                    intersectLogic.PutPlayerOnTheStartPlatform();
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
            playerLogic.SetDifficulty("Easy"); //alapból Easy beállítása.
            mapLogic.SetupSizes(new System.Drawing.Size((int)display.ActualWidth, (int)display.ActualHeight));
            intersectLogic.SetSizes((int)display.ActualWidth, (int)display.ActualHeight); //It gives the current sizes!
            display.SetupAllLogic(mapLogic, playerLogic, enemyLogic, itemLogic); //It passes through the logics!
            
            //Main Menu theme song!
            soundplayer.BackgroundMusicMenu("start");
            //menu zene

            




        }

        #region InventoryAndMenu
        
        public void InventoryDataChanged()
        {
            if (playerLogic.plyr.PrimaryWeapon == null)
            {
                label_slotOne.Content = "N/A";
            }
            else
            {
                label_slotOne.Content = playerLogic.plyr.PrimaryWeapon.Name;
            }
            label_slotTwo.Content = playerLogic.plyr.Bow.NumberOfArrows;
            label_slotThree.Content = 0;//TODO: charon
            label_slotFour.Content = playerLogic.plyr.Jetpack.Fuel;
            label_hp.Content = playerLogic.plyr.Health;
            label_armor.Content = playerLogic.plyr.Armour;
            label_hp_potion.Content = playerLogic.plyr.HealPotion.Amount;
            label_armor_repairkit.Content = playerLogic.plyr.ArmorRepairKit.Amount;

        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            menu.Visibility = Visibility.Hidden;
            gamegrid.Visibility = Visibility.Visible;
            mapLogic.CreateMap(); //nehézségi szintnek megfelelő pálya indul
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
            playerLogic.SetDifficulty("Easy");
        }

        private void Medium_Click(object sender, RoutedEventArgs e)
        {
            soundplayer.PlayActionSound(SoundLogic.MenusSounds.button_click); //sound
            label_difficulty.Content = "Current Difficulty: Medium";
            mapLogic.SetDifficulty("Medium");
            enemyLogic.SetDifficulty("Medium");
            itemLogic.SetDifficulty("Medium");
            playerLogic.SetDifficulty("Medium");
        }

        private void Hard_Click(object sender, RoutedEventArgs e)
        {
            soundplayer.PlayActionSound(SoundLogic.MenusSounds.button_click); //sound
            label_difficulty.Content = "Current Difficulty: Hard";
            mapLogic.SetDifficulty("Hard");
            enemyLogic.SetDifficulty("Hard");
            itemLogic.SetDifficulty("Hard");
            playerLogic.SetDifficulty("Hard");
        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            difficulty_select.Visibility = Visibility.Hidden;
            menu.Visibility = Visibility.Visible;
        }
        #endregion
    }
}

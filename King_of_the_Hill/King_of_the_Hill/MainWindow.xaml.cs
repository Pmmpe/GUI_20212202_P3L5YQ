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
    /// Interaction logic for MainWindow.xaml
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
        

        InventorySlot[] inv = new InventorySlot[5];
        Brush defaultInventoryBackground = Brushes.Aqua;

        DispatcherTimer timer;

        ProgressBar HPprogressBar;
        ProgressBar ShieldprogressBar;

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
            intersectLogic.IsPlayerAndMapIntersect(); //megvalósításnál részletezve
            intersectLogic.SetPlayerInTheMap(); //megvalósításnál részletezve
            intersectLogic.SetEnemyDirection(); //megvalósításnál részletezve
            enemyLogic.Move(); //mozgatja az ellenséget
            intersectLogic.PlayerIntersectWithItem(); //item felvétele
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
                playerLogic.Control(PlayerLogic.Controls.Space);
            }
            if (Keyboard.IsKeyDown(Key.NumPad1))
            {
                playerLogic.plyr.PrimaryWeapon.Name = "DELETED"; //nem tudom ki null-ozni szóval ha nincs a playernek fegyverek akkor átírjuk a fegyverének a nevét DELETED-re
                InventoryAddWeapon("N/A");
            }
            if (Keyboard.IsKeyDown(Key.NumPad2))
            {
                playerLogic.plyr.Bow.NumberOfArrows--;
                InventoryAddArrow(playerLogic.plyr.Bow.NumberOfArrows);
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
            intersectLogic.SetSizes((int)display.ActualWidth, (int)display.ActualHeight); //méret átadása
            display.SetupAllLogic(mapLogic, playerLogic, enemyLogic, itemLogic); //Logicok átadása
            //menu zene
            soundplayer.BackgroundMusicMenu("start");
            //menu zene

            //esemény feliratkoztatása
            intersectLogic.InventoryAddWeaponFromLogic = InventoryAddWeapon;
            intersectLogic.InventoryAddArrowsFromLogic = InventoryAddArrow;
            intersectLogic.InventoryAddCharonFromLogic = InventoryAddCharon;

            
            
        }

        #region InventoryAndMenu
        
        public void InventoryAddWeapon(string weaponName)
        {
            label_slotOne.Content = weaponName;
        }

        public void InventoryAddArrow(int numberOfArrows)
        {
            label_slotTwo.Content = numberOfArrows + int.Parse(label_slotTwo.Content+"");
        }

        public void InventoryAddCharon(int number)
        {
            label_slotThree.Content = number;
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
        }

        private void Medium_Click(object sender, RoutedEventArgs e)
        {
            soundplayer.PlayActionSound(SoundLogic.MenusSounds.button_click); //sound
            label_difficulty.Content = "Current Difficulty: Medium";
            mapLogic.SetDifficulty("Medium");
            enemyLogic.SetDifficulty("Medium");
        }

        private void Hard_Click(object sender, RoutedEventArgs e)
        {
            soundplayer.PlayActionSound(SoundLogic.MenusSounds.button_click); //sound
            label_difficulty.Content = "Current Difficulty: Hard";
            mapLogic.SetDifficulty("Hard");
            enemyLogic.SetDifficulty("Hard");
        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            difficulty_select.Visibility = Visibility.Hidden;
            menu.Visibility = Visibility.Visible;
        }
        #endregion
    }
}

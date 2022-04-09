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
        MapLogic mapLogic;
        IntersectLogic intersectLogic;
        SoundLogic soundplayer; //sound
        List<Weapon> Weapons; //For random generating weapons on the ground / round. 

        InventorySlot[] inv = new InventorySlot[5];
        Brush defaultInventoryBackground = Brushes.Aqua;

        DispatcherTimer timer;

        double lastAttacksTime;

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            playerLogic = new PlayerLogic();
            enemyLogic = new EnemyLogic();
            mapLogic = new MapLogic();
            intersectLogic = new IntersectLogic(playerLogic, mapLogic, enemyLogic);

            lastAttacksTime = 0.0;

            soundplayer = new SoundLogic(); //sound

            //erre a fegyveres cucra ma nem vo
            Weapons = new List<Weapon>();

            #region Weapons
            Weapons.Add(new Weapon(50, "Axe", 1.0, 1.0, 0, 0));
            Weapons.Add(new Weapon(24, "Sword", 1.0, 1.0, 0, 0));
            Weapons.Add(new Weapon(35, "LongSword", 1.0, 1.0, 0, 0));
            Weapons.Add(new Weapon(15, "Bow", 10.0, 1.0, 0, 0));
            #endregion
            //Last 0,0s are the X : Y cordinates of the weapons needed for later use.
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
            intersectLogic.IsPlayerAndMapIntersect(); //megvalósításnál részletezve
            intersectLogic.SetPlayerInTheMap(); //megvalósításnál részletezve
            intersectLogic.SetEnemyDirection(); //megvalósításnál részletezve
            enemyLogic.Move(); //mozgatja az ellenséget
            
            //Chain functions: The player is attacking if the first checker function returns true for NPC intersecting and the "K" has been pressed down,
            //then the second function returns the NPC that is currently intersecting with the player! The player will then causes damage to this npc equal to
            //his or her Weight * (Weapon) weapons.WeaponDamage;
            isAttackButtonDown(playerLogic, enemyLogic, intersectLogic);
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
            mapLogic.SetupSizes(new System.Drawing.Size((int)display.ActualWidth, (int)display.ActualHeight));
            intersectLogic.SetSizes((int)display.ActualWidth, (int)display.ActualHeight); //méret átadása
            display.SetupAllLogic(mapLogic, playerLogic, enemyLogic); //Logicok átadása
            //menu zene
            soundplayer.BackgroundMusicMenu("start");
            //menu zene

            for (int i = 0; i < inv.Length; i++)
            {
                inv[i] = new InventorySlot();
                Label label = new Label();
                label.Content = 0;
                label.Background = defaultInventoryBackground;
                label.Padding = new Thickness(0, 30, 40, 0);
                label.Margin = new Thickness(15, 0, 0, 0);
                label.FontSize = 50;
                label.Tag = i;
                stackpanel.Children.Add(label);
                inv[i].Count = 0;
                inv[i].Label = label;
            }
            ProgressBar ShieldprogressBar = new ProgressBar();
            ShieldprogressBar.Maximum = 100;
            ShieldprogressBar.Value = 50;
            ShieldprogressBar.Width = 300;
            ShieldprogressBar.Margin = new Thickness(300, 10, 0, 10);
            stackpanel.Children.Add(ShieldprogressBar);
            Label ShieldLabel = new Label();
            ShieldLabel.Content = "Shield";
            ShieldLabel.FontSize = 50;
            ShieldLabel.Padding = new Thickness(0, 10, 0, 0);
            ShieldLabel.Margin = new Thickness(15, 0, 0, 0);
            stackpanel.Children.Add(ShieldLabel);
            ProgressBar HPprogressBar = new ProgressBar();
            HPprogressBar.Maximum = 100;
            HPprogressBar.Value = 50;
            HPprogressBar.Width = 300;
            HPprogressBar.Margin = new Thickness(15, 10, 0, 10);
            stackpanel.Children.Add(HPprogressBar);
            Label HPLabel = new Label();
            HPLabel.Content = "HP";
            HPLabel.FontSize = 50;
            HPLabel.Padding = new Thickness(0, 10, 0, 0);
            HPLabel.Margin = new Thickness(15, 0, 0, 0);
            stackpanel.Children.Add(HPLabel);
        }

        #region InventoryAndMenu
        private void InventoryItemAdd(int number, int amount)
        {
            bool done = false;
            for (int i = 0; i < inv.Length; i++)
            {
                if (!done && (int)((Label)inv[i].Label).Tag == number)
                {
                    inv[i].Count = amount;
                    ((Label)inv[i].Label).Content = inv[i].Count;
                    done = true;
                }
            }
            if (!done)
            {
                for (int i = 0; i < inv.Length; i++)
                {
                    if (!done && (int)((Label)inv[i].Label).Tag == number)
                    {
                        inv[i].Count = amount;
                        ((Label)inv[i].Label).Content = inv[i].Count;
                        done = true;
                    }
                }
            }
        }

        private void InventoryItemDelete(int number)
        {
            for (int i = 0; i < inv.Length; i++)
            {
                if ((int)((Label)inv[i].Label).Tag == number)
                {
                    if (inv[i].Count > 1)
                    {
                        inv[i].Count--;
                        ((Label)inv[i].Label).Content = inv[i].Count;
                    }
                    else
                    {
                        inv[i].Count = 0;
                        ((Label)inv[i].Label).Content = 0;
                        ((Label)inv[i].Label).Background = defaultInventoryBackground;
                    }
                }
            }
        }

        private void InventoryAllItemDelete(int number)
        {
            for (int i = 0; i < inv.Length; i++)
            {
                if ((int)((Label)inv[i].Label).Tag == number)
                {
                    inv[i].Count = 0;
                    ((Label)inv[i].Label).Content = 0;
                    ((Label)inv[i].Label).Background = defaultInventoryBackground;
                }
            }
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            menu.Visibility = Visibility.Hidden;
            gamegrid.Visibility = Visibility.Visible;
            mapLogic.NextMap(); //következő pálya indítása, jelent esetben az első pálya indul.
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

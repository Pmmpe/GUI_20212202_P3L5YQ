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

        private int nextWaveLabelCounter;
        private int canEnemyAttackCounter;
        private int canPlayerAttackCounter;
        private bool canPlayerAttack;
        private int canPlayerShootCounter;
        private bool canPlayerShoot;

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

            soundplayer = new SoundLogic(); //sound

            nextWaveLabelCounter = 0;
            canEnemyAttackCounter = 0;
            canPlayerAttackCounter = 0;
            canPlayerAttack = true;
            canPlayerShootCounter = 0;
            canPlayerShoot = true;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += Timer_Tick;

            
        }

        

        private void Timer_Tick(object? sender, EventArgs e)
        {
            display.InvalidateVisual();
            intersectLogic.IsPlayerAndMapIntersect();   // Summarized at implementation! 
            intersectLogic.PlayerIntersectWithLava();
            intersectLogic.SetPlayerInTheMap();         // Summarized at implementation!
            intersectLogic.SetEnemyDirection();        // Summarized at implementation!
            intersectLogic.PlayerIntersectWithItem();   //item pick up
            intersectLogic.ArcherShoot();
            intersectLogic.RemoveDeadEnemies();
            enemyLogic.Move();                         //NPC moving function!


            playerLogic.ArrowFly();
            enemyLogic.ArrowFly();
            intersectLogic.ArrowIntersected();

            if (intersectLogic.isPlayerIntersectWithAnyNPC())
            {
                if (canEnemyAttackCounter == 50)
                {
                    playerLogic.HitPlayer(intersectLogic.PlayerIntersectWithThat());
                    canEnemyAttackCounter = 0;
                }
                canEnemyAttackCounter++;
            }
            else
            {
                canEnemyAttackCounter = 0;
            }

            if (!canPlayerAttack)
            {
                canPlayerAttackCounter++;
                progressbar_hit.Value++;
                if (canPlayerAttackCounter == 25 * (playerLogic.plyr.PrimaryWeapon == null ? 1 : playerLogic.plyr.PrimaryWeapon.AttackSpeed))
                {
                    canPlayerAttackCounter = 0;
                    canPlayerAttack = true;
                    label_slotOne.Visibility = Visibility.Visible;
                    progressbar_hit.Visibility = Visibility.Hidden;
                    progressbar_hit.Value = 0;
                    animationsLogic.StopFightAnimations(); //it works!
                }
            }

            if (!canPlayerShoot)
            {
                canPlayerShootCounter++;
                progressbar_shoot.Value++;
                if (canPlayerShootCounter == 25 * (playerLogic.plyr.Bow == null ? 1 : playerLogic.plyr.Bow.AttackSpeed))
                {
                    canPlayerShootCounter = 0;
                    canPlayerShoot = true;
                    label_slotTwo.Visibility = Visibility.Visible;
                    progressbar_shoot.Visibility = Visibility.Hidden;
                    progressbar_shoot.Value = 0;
                    animationsLogic.StopBowShootAnimations();
                }
            }

            if (enemyLogic.IsOnlyArcher())
            {
                label_damageBy.Visibility = Visibility.Hidden;
                label_damageValue.Visibility = Visibility.Hidden;
                if (nextWaveLabelCounter == 50)
                {
                    label_enter.Visibility = Visibility.Visible;
                    label_next.Visibility = Visibility.Visible;
                }
                if (nextWaveLabelCounter == 100)
                {
                    label_enter.Visibility = Visibility.Hidden;
                    label_next.Visibility = Visibility.Hidden;
                    nextWaveLabelCounter = 0;
                }
                nextWaveLabelCounter++;
            }
            InventoryDataChanged();
            if (playerLogic.IsPlayerDead())
            {
                MessageBox.Show("Vesztettél!\n" + enemyLogic.AchievedScore);
                timer.Stop();
            }

            
        }

        #region CharMoving



        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.IsKeyDown(Key.W) || Keyboard.IsKeyDown(Key.Up)) && intersectLogic.IsPlayerAndMapIntersect())
            {
                playerLogic.Control(PlayerLogic.Controls.W);
                //animationsLogic.StopJetpackAnimation();

            }
            if ((Keyboard.IsKeyDown(Key.S) || Keyboard.IsKeyDown(Key.Down)) && intersectLogic.IsPlayerAndMapIntersect())
            {
                playerLogic.Control(PlayerLogic.Controls.S);
                //animationsLogic.StopJetpackAnimation();

            }
            if (Keyboard.IsKeyDown(Key.A) || Keyboard.IsKeyDown(Key.Left))
            {

                playerLogic.plyr.LeftOrientation = true;
                playerLogic.Control(PlayerLogic.Controls.A);
                

                animationsLogic.StartPlayerMoveAnimation(playerLogic.plyr.LeftOrientation, "left"); // kell a left?
            }
            if (Keyboard.IsKeyDown(Key.D) || Keyboard.IsKeyDown(Key.Right))
            {
                
                playerLogic.plyr.LeftOrientation = false;
                playerLogic.Control(PlayerLogic.Controls.D);

                animationsLogic.StartPlayerMoveAnimation(playerLogic.plyr.LeftOrientation, "right"); // kell a left?
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
                    animationsLogic.StartJetpackAnimation();
                }
            }
            if (Keyboard.IsKeyDown(Key.NumPad1))
            {
                playerLogic.plyr.PrimaryWeapon = null;
            }
            if (Keyboard.IsKeyDown(Key.NumPad2))
            {
                if (playerLogic.plyr.Bow != null && playerLogic.plyr.Bow.NumberOfArrows > 0)
                {
                    playerLogic.plyr.Bow.NumberOfArrows--;
                }
            }
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                if (canPlayerAttack)
                {
                    playerLogic.Attack(intersectLogic.PlayerIntersectWithThat());
                    label_slotOne.Visibility = Visibility.Hidden;
                    progressbar_hit.Visibility = Visibility.Visible;
                    progressbar_hit.Maximum = 25 * (playerLogic.plyr.PrimaryWeapon == null ? 1 : playerLogic.plyr.PrimaryWeapon.AttackSpeed);
                    animationsLogic.StartFightAnimations();
                    canPlayerAttack = false;
                }
            }
            if (Keyboard.IsKeyDown(Key.R))
            {
                if (canPlayerShoot)
                {
                    playerLogic.Control(PlayerLogic.Controls.R);
                    label_slotTwo.Visibility = Visibility.Hidden;
                    progressbar_shoot.Visibility = Visibility.Visible;
                    progressbar_shoot.Maximum = 25 * (playerLogic.plyr.Bow == null ? 1 : playerLogic.plyr.Bow.AttackSpeed);
                    animationsLogic.StartBowShootAnimations();
                    canPlayerShoot = false;
                }
                
            }
            if (Keyboard.IsKeyDown(Key.Enter)) //indítja az ellenségek spawnolását, azaz az új hullámot
            {
                label_enter.Visibility = Visibility.Hidden;
                label_next.Visibility = Visibility.Hidden;
                if (enemyLogic.enemies.Count == 0 || enemyLogic.IsOnlyArcher())
                {
                    if (enemyLogic.IsEndGame())
                    {
                        MessageBox.Show("Nyertél!\n" + enemyLogic.AchievedScore);
                        timer.Stop();
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

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyUp(Key.Space))
            {
                if (playerLogic.plyr.Jetpack.Fuel > 0)
                {
                    animationsLogic.StopJetpackAnimation();
                }
            }
            if (Keyboard.IsKeyUp(Key.A) || Keyboard.IsKeyUp(Key.D))
            {
                animationsLogic.StopPlayerMoveAnimation(playerLogic.plyr.LeftOrientation, "left");
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
            display.SetupAllLogic(mapLogic, playerLogic, enemyLogic, itemLogic, animationsLogic); //It passes through the logics!
            
            //Main Menu theme song!
            soundplayer.BackgroundMusicMenu("start");
            //menu zene

            playerLogic.CausedDamageEvent = DamageToDisplay;
            intersectLogic.CausedDamageEvent = DamageToDisplay;



        }

        #region InventoryAndMenu
        
        public void DamageToDisplay(string weaponName, double damage)
        {
            label_damageBy.Visibility = Visibility.Visible;
            label_damageValue.Visibility = Visibility.Visible;
            label_damageBy.Content = weaponName;
            label_damageValue.Content = damage;
        }

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
            if (playerLogic.plyr.Bow == null)
            {
                label_slotTwo.Content = 0;
            }
            else
            {
                label_slotTwo.Content = playerLogic.plyr.Bow.NumberOfArrows;
            }
            label_slotThree.Content = playerLogic.plyr.Charon;
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

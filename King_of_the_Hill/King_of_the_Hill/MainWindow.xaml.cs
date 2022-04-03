namespace King_of_the_Hill
{
    using King_of_the_Hill.Logic;
    using King_of_the_Hill.Model;
    using King_of_the_Hill.Model.GameItems;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Initialization
        PlayerLogic playerLogic;
        MapLogic mapLogic;
        List<Weapon> Weapons; //For random generating weapons on the ground / round. 

        InventorySlot[] inv = new InventorySlot[5];
        Brush defaultInventoryBackground = Brushes.Aqua;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            playerLogic = new PlayerLogic();
            display.SetupModel(playerLogic);
            playerLogic.gameArea = new Size(gamegrid.ActualWidth, gamegrid.ActualHeight);
            Weapons = new List<Weapon>();
            #region Weapons
            Weapons.Add(new Weapon(50, "Axe", 1.0, 1.0, 0,0));
                Weapons.Add(new Weapon(24, "Sword", 1.0, 1.0, 0, 0));
                Weapons.Add(new Weapon(35, "LongSword", 1.0, 1.0, 0, 0));
                Weapons.Add(new Weapon(15, "Bow", 10.0, 1.0, 0, 0));
            #endregion
            //Last 0,0s are the X : Y cordinates of the weapons needed for later use.
        }

        #region CharMoving
        private static bool isItemIntersect(PlayerLogic playerLogic, MapLogic mapLogic)
        {
            foreach (var ground in mapLogic.Grounds)
            {
                if (playerLogic.playerRect.IntersectsWith(ground.Rectangle))
                {
                    return true;
                }
            }
            return false;
        }
        private void Window_KeyDown(object sender, KeyEventArgs e) //If we find a solution that the player could move on after impacting an object, uncomment the Intersect checks.
        {
            if (Keyboard.IsKeyDown(Key.W) && !isItemIntersect(playerLogic, mapLogic))
            {
                playerLogic.Control(PlayerLogic.Controls.W);
            }
            if (Keyboard.IsKeyDown(Key.S) && !isItemIntersect(playerLogic, mapLogic))
            {
                playerLogic.Control(PlayerLogic.Controls.S);
            }
            if (Keyboard.IsKeyDown(Key.A)/* && isItemIntersect(playerLogic, mapLogic)*/)
            {
                playerLogic.Control(PlayerLogic.Controls.A);
            }
            if (Keyboard.IsKeyDown(Key.D)/* && isItemIntersect(playerLogic, mapLogic)*/)
            {
                playerLogic.Control(PlayerLogic.Controls.D);
            }
            if (Keyboard.IsKeyDown(Key.E)/* && isItemIntersect(playerLogic, mapLogic)*/)
            {
                playerLogic.Control(PlayerLogic.Controls.E);
            }
            if (Keyboard.IsKeyDown(Key.Q)/* && isItemIntersect(playerLogic, mapLogic)*/)
            {
                playerLogic.Control(PlayerLogic.Controls.Q);
            }
            if (Keyboard.IsKeyDown(Key.Space))
            {
                playerLogic.Weight = 0.5;
                playerLogic.Control(PlayerLogic.Controls.Space);
            }       
        }
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            display.SetupSizes(new Size(gamegrid.ActualWidth, gamegrid.ActualHeight));

            mapLogic = new MapLogic();
            mapLogic.SetDifficulty("Easy"); //alapból Easy beállítása.
            mapLogic.SetupSizes(new System.Drawing.Size((int)display.ActualWidth, (int)display.ActualHeight));
            display.SetupMapLogic(mapLogic);
            display.SetupPlayerLogic(playerLogic);

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
            display.InvalidateVisual();
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            menu.Visibility = Visibility.Hidden;
            difficulty_select.Visibility = Visibility.Visible;
        }

        private void Easy_Click(object sender, RoutedEventArgs e)
        {
            label_difficulty.Content = "Current Difficulty: Easy";
            mapLogic.SetDifficulty("Easy");
        }

        private void Medium_Click(object sender, RoutedEventArgs e)
        {
            label_difficulty.Content = "Current Difficulty: Medium";
            mapLogic.SetDifficulty("Medium");
        }

        private void Hard_Click(object sender, RoutedEventArgs e)
        {
            label_difficulty.Content = "Current Difficulty: Hard";
            mapLogic.SetDifficulty("Hard");
        }

        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            difficulty_select.Visibility = Visibility.Hidden;
            menu.Visibility = Visibility.Visible;
        }
        #endregion

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            display.SetupSizes(new Size(gamegrid.ActualWidth, gamegrid.ActualHeight));
        }
    }
}

namespace King_of_the_Hill
{
    using King_of_the_Hill.Logic;
    using King_of_the_Hill.Model;
    using System;
    using System.IO;
    using System.Media;
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
        MediaPlayer soundplayer;

        InventorySlot[] inv = new InventorySlot[5];
        Brush defaultInventoryBackground = Brushes.Aqua;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            playerLogic = new PlayerLogic();
            soundplayer = new MediaPlayer();
            display.SetupModel(playerLogic);
        }

        #region CharMoving
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A)
            {
                playerLogic.Control(PlayerLogic.Controls.A);
            }
            else if (e.Key == Key.D)
            {
                playerLogic.Control(PlayerLogic.Controls.D);
            }
            else if (e.Key == Key.W)
            {
                playerLogic.Control(PlayerLogic.Controls.W);
            }
            else if (e.Key == Key.S)
            {
                playerLogic.Control(PlayerLogic.Controls.S);
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
            //menu zene
            soundplayer.Open(new Uri(Path.Combine("Sources", "Sounds", "hatterzene_lol.mp3"), UriKind.RelativeOrAbsolute));
            soundplayer.Play();
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

            //menu zene
            soundplayer.Stop();
            soundplayer.Close();
            soundplayer.Open(new Uri(Path.Combine("Sources", "Sounds", "game_bcg_music.mp3"), UriKind.RelativeOrAbsolute));
            soundplayer.Play();
            //menu zene

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

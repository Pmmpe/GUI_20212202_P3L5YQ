using King_of_the_Hill.Logic;
using King_of_the_Hill.Logic.Controller;
using King_of_the_Hill.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace King_of_the_Hill
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PlayerLogic playerLogic;
        ICharachterController charachterController;
        InventorySlot[] inv = new InventorySlot[5];
        Brush defaultInventoryBackground = Brushes.Aqua;

        public MainWindow()
        {
            InitializeComponent();
            PlayerLogic playerLogic = new PlayerLogic();
            display.SetupModel(playerLogic);
        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    playerLogic.Control(PlayerLogic.Controls.A);
                    break;
                case Key.Up:
                    playerLogic.Control(PlayerLogic.Controls.W);
                    break;

                case Key.Right:
                    playerLogic.Control(PlayerLogic.Controls.D);
                    break;

                case Key.Down:
                    playerLogic.Control(PlayerLogic.Controls.S);
                    break;       
                    
                case Key.A:
                    playerLogic.Control(PlayerLogic.Controls.A);
                    break;
               
                case Key.D:
                    playerLogic.Control(PlayerLogic.Controls.D);
                    break;

                case Key.E:
                    playerLogic.Control(PlayerLogic.Controls.E);
                    break;
                
                case Key.Q:
                    playerLogic.Control(PlayerLogic.Controls.Q);
                    break;

                case Key.S:
                    playerLogic.Control(PlayerLogic.Controls.S);
                    break;

                case Key.W:
                    playerLogic.Control(PlayerLogic.Controls.W);
                    break;

                case Key.Space:
                    playerLogic.Control(PlayerLogic.Controls.Space);
                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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


            playerLogic = new PlayerLogic();
            playerLogic.SetupSize((int)display.ActualWidth,
                (int)display.ActualHeight);
            charachterController = new CharachterController(playerLogic);
            display.SetupModel(playerLogic); //de itt nem null b+;

            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(100);
            dt.Tick += Dt_Tick;
            dt.Start();
        }

        private void Dt_Tick(object sender, EventArgs e)
        {
            //playerLogic.MoveGameItems();
        }

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
    }
}

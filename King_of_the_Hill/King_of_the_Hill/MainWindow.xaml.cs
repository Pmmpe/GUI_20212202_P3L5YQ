using King_of_the_Hill.Logic;
using King_of_the_Hill.Logic.Controller;
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

                case (Key.W):
                    playerLogic.Control(PlayerLogic.Controls.W);
                    break;

                case (Key.Space):
                    playerLogic.Control(PlayerLogic.Controls.Space);
                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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
    }
}

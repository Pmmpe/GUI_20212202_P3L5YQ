using King_of_the_Hill.Logic;
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
        public MainWindow()
        {
            InitializeComponent();
            playerLogic = new PlayerLogic();
            display.SetupModel(playerLogic);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            display.SetupSizes(new Size(grid.ActualWidth, grid.ActualHeight));
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            display.SetupSizes(new Size(grid.ActualWidth, grid.ActualHeight));
        }

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
    }
}

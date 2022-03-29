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
            PlayerLogic playerLogic = new PlayerLogic();
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
    }
}

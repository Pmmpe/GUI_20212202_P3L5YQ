using King_of_the_Hill.Logic.Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace King_of_the_Hill.Display
{
    public class Display : FrameworkElement
    {
        ICharachterController charachterController;
        Brush playerBrush;
        Brush npcBrush;
        Brush backgroundBrush;
        Brush backgroundTileset1Brush;
        Brush backgroundTileset2Brush;
        Brush arrowBrush;

        public Display()
        {
            backgroundBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "bckgrnd1.png"), UriKind.RelativeOrAbsolute)));
            backgroundTileset1Brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "bckgrnd2.png"), UriKind.RelativeOrAbsolute)));
            backgroundTileset2Brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "bckgrnd3.png"), UriKind.RelativeOrAbsolute)));
            arrowBrush = Brushes.Red;
        }

        public void SetupModel(ICharachterController charachterController)
        {
            this.charachterController = charachterController;
            this.charachterController.Changed +=
                (sender, args) => this.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (charachterController != null && ActualWidth > 0 && ActualHeight > 0)
            {
                //világűr (háttér)
                drawingContext.DrawRectangle(backgroundBrush, null,
                    new Rect(0, 0, ActualWidth, ActualHeight));
                //Nyíl
                foreach (var item in charachterController.Lasers)
                {
                    drawingContext.DrawEllipse(laserBrush, null,
                        new Point(item.Center.X, item.Center.Y),
                        item.ItemRadius, item.ItemRadius);
                }
                //űrhajó
                drawingContext.PushTransform(
                    new RotateTransform(charachterController.Ship.Angle,
                    charachterController.Ship.Center.X, charachterController.Ship.Center.Y));
                var r = charachterController.Ship.Rectangle;
                drawingContext.DrawRectangle(shipBrush, null,
                    new Rect(r.X, r.Y, r.Width, r.Height));
                drawingContext.Pop();

            }
        }
    }
}

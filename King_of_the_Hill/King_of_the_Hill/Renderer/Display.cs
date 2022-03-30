using King_of_the_Hill.Logic;
using King_of_the_Hill.Logic.Controller;
using King_of_the_Hill.Logic.LogicModelInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace King_of_the_Hill.Renderer.Display
{
    public class Display : FrameworkElement
    {
        IPlayerModel playerModel;
        Size area;
        Brush playerBrush;
        Brush npcBrush;
        Brush backgroundBrush;
        Brush backgroundTileset1Brush;
        Brush backgroundTileset2Brush;
        Brush arrowBrush;

        MapLogic mapLogic;
        

        public Display()
        {
            backgroundBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "bckgrnd1.png"), UriKind.RelativeOrAbsolute)));
            backgroundTileset1Brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "bckgrnd2.png"), UriKind.RelativeOrAbsolute)));
            backgroundTileset2Brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "bckgrnd3.png"), UriKind.RelativeOrAbsolute)));
            playerBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Her.png"), UriKind.RelativeOrAbsolute)));
            arrowBrush = Brushes.Red;
        }

        public void SetupModel(IPlayerModel playerModel)
        {
            this.playerModel = playerModel;
            //this.charachterController.Changed +=
            //    (sender, args) => this.InvalidateVisual();
        }

        public void SetupMapLogic(MapLogic mapLogic)
        {
            this.mapLogic = mapLogic;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (playerModel != null && mapLogic != null && ActualWidth > 0 && ActualHeight > 0)
            {
                //null reference
                //drawingContext.PushTransform(
                //    new TranslateTransform(playerModel.player.PosX, playerModel.player.PosY));
                //(háttér)
                drawingContext.DrawRectangle(backgroundBrush, null,
                    new Rect(0, 0, ActualWidth, area.Height));
                drawingContext.DrawRectangle(backgroundTileset1Brush, null,
                    new Rect(0, 0, ActualWidth, ActualHeight));
                drawingContext.DrawRectangle(backgroundTileset2Brush, null,
                    new Rect(0, 0, ActualWidth, ActualHeight));
                drawingContext.DrawRectangle(playerBrush, null,
                    new Rect(0, 0, ActualWidth / 5, ActualHeight / 5));
                ////Nyíl
                //foreach (var item in charachterController.Lasers)
                //{
                //    drawingContext.DrawEllipse(laserBrush, null,
                //        new Point(item.Center.X, item.Center.Y),
                //        item.ItemRadius, item.ItemRadius);
                //}

                //var r = charachterController.Ship.Rectangle;
                //drawingContext.DrawRectangle(shipBrush, null,
                //    new Rect(r.X, r.Y, r.Width, r.Height));
                //drawingContext.Pop();

                foreach (var item in mapLogic.Grounds)
                {
                    if (item is Ground)
                    {
                        drawingContext.DrawRectangle(Brushes.Green, null, new Rect(item.Center.X, item.Center.Y, item.Width, item.Height));
                    }
                    else if (item is Lava)
                    {
                        drawingContext.DrawRectangle(Brushes.Red, null, new Rect(item.Center.X, item.Center.Y, item.Width, item.Height));
                    }
                    else if (item is Platform)
                    {
                        drawingContext.DrawRectangle(Brushes.LightBlue, null, new Rect(item.Center.X, item.Center.Y, item.Width, item.Height));
                    }
                }

            }
        }
    }
}

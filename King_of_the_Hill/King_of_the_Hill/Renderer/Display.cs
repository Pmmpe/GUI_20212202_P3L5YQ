namespace King_of_the_Hill.Renderer.Display
{
    using King_of_the_Hill.Logic;
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    public class Display : FrameworkElement
    {
        Size area;

        public Brush playerBrush;
        public Brush npcBrush;
        public Brush backgroundBrush;
        public Brush backgroundTileset1Brush;
        public Brush backgroundTileset2Brush;
        public Brush arrowBrush;

        MapLogic mapLogic;
        PlayerLogic playerLogic;


        public Display()
        {
            backgroundBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "bckgrnd1.png"), UriKind.RelativeOrAbsolute)));
            backgroundTileset1Brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "bckgrnd2.png"), UriKind.RelativeOrAbsolute)));
            backgroundTileset2Brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "bckgrnd3.png"), UriKind.RelativeOrAbsolute)));
            playerBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Her.png"), UriKind.RelativeOrAbsolute)));
            arrowBrush = Brushes.Red;
        }

        public void SetupMapLogic(MapLogic mapLogic)
        {
            this.mapLogic = mapLogic;
        }

        public void SetupPlayerLogic(PlayerLogic playerLogic)
        {
            this.playerLogic = playerLogic;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (playerLogic != null && mapLogic != null)
            {
                drawingContext.DrawRectangle(backgroundBrush, null,
                    new Rect(0, 0, ActualWidth, ActualHeight));
                drawingContext.DrawRectangle(backgroundTileset1Brush, null,
                    new Rect(0, 0, ActualWidth, ActualHeight));
                drawingContext.DrawRectangle(backgroundTileset2Brush, null,
                    new Rect(0, 0, ActualWidth, ActualHeight));


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
                drawingContext.DrawRectangle(Brushes.Black, null, new Rect(playerLogic.plyr.PosX, playerLogic.plyr.PosY, playerLogic.plyr.Width, playerLogic.plyr.Height));
            }
        }
    }
}

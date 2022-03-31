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
        IGameModel model;
        public Brush playerBrush;
        public Brush npcBrush;
        public Brush backgroundBrush;
        public Brush backgroundTileset1Brush;
        public Brush backgroundTileset2Brush;
        public Brush arrowBrush;

        MapLogic mapLogic;
        

        public Display()
        {
            backgroundBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "bckgrnd1.png"), UriKind.RelativeOrAbsolute)));
            backgroundTileset1Brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "bckgrnd2.png"), UriKind.RelativeOrAbsolute)));
            backgroundTileset2Brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "bckgrnd3.png"), UriKind.RelativeOrAbsolute)));
            playerBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Her.png"), UriKind.RelativeOrAbsolute)));
            arrowBrush = Brushes.Red;
        }

        public void SetupModel(IGameModel model)
        {
            this.model = model;
            this.model.Changed
                += (sender, eventargs) => this.InvalidateVisual();
        }

        public void SetupSizes(Size Area)
        {
            this.area = Area;
            this.InvalidateVisual();
        }

        public void SetupMapLogic(MapLogic mapLogic)
        {
            this.mapLogic = mapLogic;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (model != null && mapLogic != null && ActualWidth > 0 && ActualHeight > 0)
            {

                drawingContext.DrawRectangle(backgroundBrush, null,
                    new Rect(0, 0, ActualWidth, area.Height));
                drawingContext.DrawRectangle(backgroundTileset1Brush, null,
                    new Rect(0, 0, ActualWidth, ActualHeight));
                drawingContext.DrawRectangle(backgroundTileset2Brush, null,
                    new Rect(0, 0, ActualWidth, ActualHeight));


                drawingContext.PushTransform(new TranslateTransform(model.PosX, model.PosY));
                drawingContext.DrawRectangle(playerBrush, null,
                    new Rect(0, 0, ActualWidth / 5, ActualHeight / 5));
                drawingContext.Pop();

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

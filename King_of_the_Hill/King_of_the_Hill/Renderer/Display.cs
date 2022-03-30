using King_of_the_Hill.Logic;
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
        Size area;
        IGameModel model;
        public Brush playerBrush;
        public Brush npcBrush;
        public Brush backgroundBrush;
        public Brush backgroundTileset1Brush;
        public Brush backgroundTileset2Brush;
        public Brush arrowBrush;

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

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (ActualWidth > 0 && ActualHeight > 0 && model != null)
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
            }
        }
    }
}

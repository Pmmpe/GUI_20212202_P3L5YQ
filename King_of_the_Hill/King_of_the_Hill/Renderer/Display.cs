namespace King_of_the_Hill.Renderer.Display
{
    using King_of_the_Hill.Logic;
    using King_of_the_Hill.Model.MapItem;
    using King_of_the_Hill.Model.NPC_Types;
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
        EnemyLogic enemyLogic;


        public Display()
        {
            backgroundBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "bckgrnd1.png"), UriKind.RelativeOrAbsolute)));
            backgroundTileset1Brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "bckgrnd2.png"), UriKind.RelativeOrAbsolute)));
            backgroundTileset2Brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "bckgrnd3.png"), UriKind.RelativeOrAbsolute)));
            playerBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Her.png"), UriKind.RelativeOrAbsolute)));
            arrowBrush = Brushes.Red;
        }

        public void SetupAllLogic(MapLogic mapLogic, PlayerLogic playerLogic, EnemyLogic enemyLogic)
        {
            this.mapLogic = mapLogic;
            this.playerLogic = playerLogic;
            this.enemyLogic = enemyLogic;
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
                        drawingContext.DrawRectangle(Brushes.Green, null, new Rect(item.X, item.Y, item.Width, item.Height));
                    }
                    else if (item is Lava)
                    {
                        drawingContext.DrawRectangle(Brushes.Red, null, new Rect(item.X, item.Y, item.Width, item.Height));
                    }
                    else if (item is Platform || item is StartPlatform)
                    {
                        drawingContext.DrawRectangle(Brushes.LightBlue, null, new Rect(item.X, item.Y, item.Width, item.Height));
                    }
                }

                foreach (var item in enemyLogic.enemies)
                {
                    if (item is Grunt)
                    {
                        drawingContext.DrawRectangle(Brushes.White, null, new Rect(item.PosX, item.PosY, item.Width, item.Height));
                    }
                    if (item is Brute)
                    {
                        drawingContext.DrawRectangle(Brushes.Crimson, null, new Rect(item.PosX, item.PosY, item.Width, item.Height));
                    }
                    if (item is Archer)
                    {
                        drawingContext.DrawRectangle(Brushes.Aqua, null, new Rect(item.PosX, item.PosY, item.Width, item.Height));
                    }
                    if (item is Heavy_Brute)
                    {
                        drawingContext.DrawRectangle(Brushes.Yellow, null, new Rect(item.PosX, item.PosY, item.Width, item.Height));
                    }
                }

                drawingContext.DrawRectangle(Brushes.Black, null, new Rect(playerLogic.plyr.PosX, playerLogic.plyr.PosY, playerLogic.plyr.Width, playerLogic.plyr.Height));
            }
        }
    }
}

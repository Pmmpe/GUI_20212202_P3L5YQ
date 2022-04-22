namespace King_of_the_Hill.Renderer.Display
{
    using King_of_the_Hill.Logic;
    using King_of_the_Hill.Model.GameItems;
    using King_of_the_Hill.Model.MapItem;
    using King_of_the_Hill.Model.NPC_Types;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;
    using WpfAnimatedGif;

    //public delegate void DtTimerTickEventHandler(DispatcherTimer timer); //animation
    public class Display : FrameworkElement
    {
        #region Brushes

        #endregion

        //player 50x50px
        Brush playerBrush;

        //map
        Brush backgroundBrush;
        Brush backgroundTileset1Brush;
        Brush backgroundTileset2Brush;

        Brush groundBrush;
        Brush lavaBrush;
        Brush platformBrush;
        

        //enemies 50x50px
        Brush archerBrush;
        Brush bruteBrush;
        Brush gruntBrush;
        Brush heavyBruteBrush;

        //items 25x25px
        Brush armorBrush;
        Brush axeBrush;
        Brush bowBrush;
        Brush healPotionBrush;
        Brush jetpackBrush;
        Brush longSwordBrush;
        Brush swordBrush;

        Brush arrowBrush;


        MapLogic mapLogic;
        PlayerLogic playerLogic;
        EnemyLogic enemyLogic;
        ItemLogic itemLogic;

        //animations
        //AnimationsLogic animationsLogic;
        List<DispatcherTimer> dispatcherTimers;


        public Display()
        {
            backgroundBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "bckgrnd1.png"), UriKind.RelativeOrAbsolute)));
            backgroundTileset1Brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "bckgrnd2.png"), UriKind.RelativeOrAbsolute)));
            backgroundTileset2Brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "bckgrnd3.png"), UriKind.RelativeOrAbsolute)));
            
            //playert cserélni kell, mert nem jó a kép, megbeszéltük ugye.
            playerBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Her.png"), UriKind.RelativeOrAbsolute)));
            //Ideiglenesen fekete:
//          playerBrush = Brushes.Black;
            arrowBrush = Brushes.Red;

            //TODO képeket berakni a Brush-okra.
            //Ideiglenes Brushok, ezeket majd törölni kell:
            groundBrush = Brushes.Green;
            lavaBrush = Brushes.Red;
            platformBrush = Brushes.LightBlue;

            gruntBrush = Brushes.White;
            bruteBrush = Brushes.Crimson;
            archerBrush = Brushes.Aqua;
            heavyBruteBrush = Brushes.Yellow;

            armorBrush = Brushes.Aqua;
            axeBrush = Brushes.Orange;
            bowBrush = Brushes.Orange;
            healPotionBrush = Brushes.Red;
            jetpackBrush = Brushes.Black;
            longSwordBrush = Brushes.Orange;
            swordBrush = Brushes.Orange;

            dispatcherTimers = new List<DispatcherTimer>();

        }

        public void SetupAllLogic(MapLogic mapLogic, PlayerLogic playerLogic, EnemyLogic enemyLogic, ItemLogic itemLogic, AnimationsLogic animationsLogic)
        {
            this.mapLogic = mapLogic;
            this.playerLogic = playerLogic;
            this.enemyLogic = enemyLogic;
            this.itemLogic = itemLogic;

            #region Animations Subscribe
            animationsLogic.Fight += FightAnimations;
            animationsLogic.Jetpack += JetpackAnimation;
            animationsLogic.MoveLeft += MoveLeftAnimation;
            animationsLogic.MoveRight += MoveRightAnimation;
            #endregion
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
                        drawingContext.DrawRectangle(groundBrush, null, new Rect(item.X, item.Y, item.Width, item.Height));
                    }
                    else if (item is Lava)
                    {
                        drawingContext.DrawRectangle(lavaBrush, null, new Rect(item.X, item.Y, item.Width, item.Height));
                    }
                    else if (item is Platform || item is StartPlatform)
                    {
                        drawingContext.DrawRectangle(platformBrush, null, new Rect(item.X, item.Y, item.Width, item.Height));
                    }
                }

                foreach (var item in itemLogic.items)
                {
                    if (item is Armor)
                    {
                        drawingContext.DrawRectangle(armorBrush, null, new Rect(item.PosX, item.PosY, item.Width, item.Height));
                    }
                    else if (item is Axe)
                    {
                        drawingContext.DrawRectangle(axeBrush, null, new Rect(item.PosX, item.PosY, item.Width, item.Height));
                    }
                    else if (item is Bow)
                    {
                        drawingContext.DrawRectangle(bowBrush, null, new Rect(item.PosX, item.PosY, item.Width, item.Height));
                    }
                    else if (item is HealPotion)
                    {
                        drawingContext.DrawRectangle(healPotionBrush, null, new Rect(item.PosX, item.PosY, item.Width, item.Height));
                    }
                    else if (item is Jetpack)
                    {
                        drawingContext.DrawRectangle(jetpackBrush, null, new Rect(item.PosX, item.PosY, item.Width, item.Height));
                    }
                    else if (item is LongSword)
                    {
                        drawingContext.DrawRectangle(longSwordBrush, null, new Rect(item.PosX, item.PosY, item.Width, item.Height));
                    }
                    else if (item is Sword)
                    {
                        drawingContext.DrawRectangle(swordBrush, null, new Rect(item.PosX, item.PosY, item.Width, item.Height));
                    }
                }

                foreach (var item in enemyLogic.enemies)
                {
                    if (item is Grunt)
                    {
                        drawingContext.DrawRectangle(gruntBrush, null, new Rect(item.PosX, item.PosY, item.Width, item.Height));
                    }
                    else if (item is Brute)
                    {
                        drawingContext.DrawRectangle(bruteBrush, null, new Rect(item.PosX, item.PosY, item.Width, item.Height));
                    }
                    else if (item is Archer)
                    {
                        drawingContext.DrawRectangle(archerBrush, null, new Rect(item.PosX, item.PosY, item.Width, item.Height));
                    }
                    else if (item is HeavyBrute)
                    {
                        drawingContext.DrawRectangle(heavyBruteBrush, null, new Rect(item.PosX, item.PosY, item.Width, item.Height));
                    }
                }

                drawingContext.DrawRectangle(playerBrush, null, new Rect(playerLogic.plyr.PosX, playerLogic.plyr.PosY, playerLogic.plyr.Width, playerLogic.plyr.Height));
            }
        }

        public void FightAnimations()
        {
            playerBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "knight", "knight 3 idle.png"), UriKind.RelativeOrAbsolute))); //TODO képcsere

            ObjectAnimationUsingKeyFrames anim = new ObjectAnimationUsingKeyFrames();
            anim.Duration = TimeSpan.FromSeconds(3);
            anim.FillBehavior = FillBehavior.Stop;
            ImageSource[] images = new ImageSource[]
            {
                  new BitmapImage(new Uri(Path.Combine("Sources", "Her.png"), UriKind.RelativeOrAbsolute)),
                  new BitmapImage(new Uri(Path.Combine("Sources", "knight", "knight 3 idle.png"), UriKind.RelativeOrAbsolute))
            };
            anim.KeyFrames.Add(new DiscreteObjectKeyFrame(images[0]));
            anim.KeyFrames.Add(new DiscreteObjectKeyFrame(images[1]));
            playerBrush.BeginAnimation(ImageBrush.ImageSourceProperty, anim);



            //// Set the target of the animation
            //Storyboard.SetTarget(anim, playerBrush);
            //Storyboard.SetTargetProperty(anim, new PropertyPath("ImageSourceProperty"));

            //// Kick the animation off
            //sb.Children.Add(anim);
            //sb.Begin();

            //var animation = new BrushAnimation
            //{
            //    From = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Her.png"), UriKind.RelativeOrAbsolute))),
            //    To = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "knight", "knight 3 idle.png"), UriKind.RelativeOrAbsolute))),
            //    Duration = new Duration(TimeSpan.FromSeconds(5))
            //};
            //playerBrush.BeginAnimation(,);


            //animation
            //var dispatcherTimerInstance = new DispatcherTimer();
            //dispatcherTimerInstance.Tick += RevertBackToDefPlayerBrush;
            //dispatcherTimerInstance.Interval = new TimeSpan(0, 0, 5);
            //dispatcherTimerInstance.Start();
            //dispatcherTimers.Add(dispatcherTimerInstance);

        }

        private void RevertBackToDefPlayerBrush(object? sender, EventArgs e)
        {
            if ((sender as DispatcherTimer).IsEnabled)
            {
                playerBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Her.png"), UriKind.RelativeOrAbsolute))); //visszacsere
                (sender as DispatcherTimer).Stop();
                dispatcherTimers.Remove(sender as DispatcherTimer);
            }
        }

        public void IdleAnimation(Rect playerRect) //imageBrush nem tud animált gif-et megjeleníteni úgy tűnik
        {
            var image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(Path.Combine("Sources", "knight", "knight idle.gif"), UriKind.RelativeOrAbsolute);
            image.EndInit();
            //ImageBehavior.SetAnimatedSource(, (playerBrush as ImageBrush).ImageSource);
        }

        private void JetpackAnimation() //just player
        {
            var img = new BitmapImage(new Uri(Path.Combine("Sources", "knight", "knight 3 idle.png"), UriKind.RelativeOrAbsolute));
            playerBrush = new ImageBrush(new TransformedBitmap(img, new ScaleTransform(-1,1)));
            var dispatcherTimerInstance = new DispatcherTimer();
            dispatcherTimerInstance.Tick += RevertBackToDefPlayerBrush;
            dispatcherTimerInstance.Interval = new TimeSpan(0, 0, 2);
            dispatcherTimerInstance.Start();
            dispatcherTimers.Add(dispatcherTimerInstance);
        }

        private void MoveLeftAnimation(bool leftOrientation) //just player currently
        {
            if (!leftOrientation)
            {
                var img = (BitmapSource)((ImageBrush)playerBrush).ImageSource;
                var mirrorredImage = new TransformedBitmap(img, new ScaleTransform(-1, 1));
                ((ImageBrush)playerBrush).ImageSource = mirrorredImage;
                playerLogic.plyr.LeftOrientation = true;
            }
            
        }

        private void MoveRightAnimation(bool leftOrientation) //just player currently
        {
            if (leftOrientation)
            {
                var img = (BitmapSource)((ImageBrush)playerBrush).ImageSource;
                var mirrorredImage = new TransformedBitmap(img, new ScaleTransform(-1, 1));
                ((ImageBrush)playerBrush).ImageSource = mirrorredImage;
                playerLogic.plyr.LeftOrientation = false;
            }
        }

    }
}

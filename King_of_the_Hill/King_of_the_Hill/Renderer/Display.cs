﻿namespace King_of_the_Hill.Renderer.Display
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

    
    public class Display : FrameworkElement
    {
        #region Brushes

        #endregion

        //player 50x50px
        CharacterBrush playerBrush;
        CharacterBrush playerBrushRun;
        CharacterBrush playerBrushJetpack;
        CharacterBrush playerBrushSword;
        CharacterBrush playerBrushBow;
        CharacterBrush playerBrushIdle;
        CharacterBrush playerBrushFall;

        //map
        Brush backgroundBrush;
        Brush backgroundTileset1Brush;
        Brush backgroundTileset2Brush;

        Brush groundBrush;
        Brush lavaBrush;
        Brush platformBrush;
        

        //enemies 50x50px
        CharacterBrush archerBrush;
        CharacterBrush archerBrushAttack;
        CharacterBrush bruteBrush;
        CharacterBrush bruteBrushAttack;
        CharacterBrush gruntBrush;
        CharacterBrush gruntBrushAttack;
        CharacterBrush heavyBruteBrush;
        CharacterBrush heavyBruteBrushAttack;

        //items 25x25px
        Brush armorBrush;
        Brush axeBrush;
        Brush bowBrush;
        Brush healPotionBrush;
        Brush jetpackBrush;
        Brush longSwordBrush;
        Brush swordBrush;
        Brush charonBrush;

        //arrow 10x10px
        Brush arrowBrush;


        MapLogic mapLogic;
        PlayerLogic playerLogic;
        EnemyLogic enemyLogic;
        ItemLogic itemLogic;

        int enemyhitcounter;



        public Display()
        {
            backgroundBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "bckgrnd1.png"), UriKind.RelativeOrAbsolute)));
            backgroundTileset1Brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "bckgrnd2.png"), UriKind.RelativeOrAbsolute)));
            backgroundTileset2Brush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "bckgrnd3.png"), UriKind.RelativeOrAbsolute)));
            
            //playert cserélni kell, mert nem jó a kép, megbeszéltük ugye.
            playerBrush = new CharacterBrush(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "knight_idle.png"), UriKind.RelativeOrAbsolute))));
            playerBrushJetpack = new CharacterBrush(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "knight_JetPack.png"), UriKind.RelativeOrAbsolute))));
            playerBrushBow = new CharacterBrush(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "knight_bowshoot.png"), UriKind.RelativeOrAbsolute))));
            playerBrushRun = new CharacterBrush(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "knight_Run.png"), UriKind.RelativeOrAbsolute))));
            playerBrushSword = new CharacterBrush(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "knight_Attack.png"), UriKind.RelativeOrAbsolute))));
            playerBrushIdle = new CharacterBrush(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "knight_idle.png"), UriKind.RelativeOrAbsolute))));
            playerBrushFall = new CharacterBrush(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Her.png"), UriKind.RelativeOrAbsolute))));

            //playerBrush = Brushes.Black;
            arrowBrush = Brushes.Red;

            //TODO képeket berakni a Brush-okra.
            //Ideiglenes Brushok, ezeket majd törölni kell:
            groundBrush = Brushes.Green;
            lavaBrush = Brushes.Red;
            platformBrush = Brushes.LightBlue;

            gruntBrush = new CharacterBrush(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Her.png"), UriKind.RelativeOrAbsolute))));
            gruntBrushAttack = new CharacterBrush(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "knight_Attack.png"), UriKind.RelativeOrAbsolute))));
            bruteBrush = new CharacterBrush(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Her.png"), UriKind.RelativeOrAbsolute))));
            bruteBrushAttack = new CharacterBrush(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "knight_Attack.png"), UriKind.RelativeOrAbsolute))));
            archerBrush = new CharacterBrush(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Her.png"), UriKind.RelativeOrAbsolute))));
            archerBrushAttack = new CharacterBrush(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "knight_bowshoot.png"), UriKind.RelativeOrAbsolute))));
            heavyBruteBrush = new CharacterBrush(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Her.png"), UriKind.RelativeOrAbsolute))));
            heavyBruteBrushAttack = new CharacterBrush(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "knight_Attack.png"), UriKind.RelativeOrAbsolute))));

            armorBrush = Brushes.Aqua;
            axeBrush = Brushes.Orange;
            bowBrush = Brushes.Orange;
            healPotionBrush = Brushes.Red;
            jetpackBrush = Brushes.Black;
            longSwordBrush = Brushes.Orange;
            swordBrush = Brushes.Orange;
            charonBrush = Brushes.Gold;

            enemyhitcounter = 0;

        }

        //internal void EnemyHit(Npc npc)
        //{
        //    if (enemyhitcounter == 0)
        //    {
        //        //    if (npc is Grunt)
        //        //    {

        //        //    }
        //        //    else if (npc is Brute)
        //        //    {

        //        //    }
        //        //    else if (npc is Archer)
        //        //    {

        //        //    }
        //        //    else if (npc is HeavyBrute)
        //        //    {

        //        //    }

        //        //animation start

        //        enemyhitcounter++;
        //    }
        //    else if (enemyhitcounter < 25)
        //    {
        //        enemyhitcounter++;

        //    }
        //    else if(enemyhitcounter == 25)
        //    {
        //        enemyhitcounter = 0;
        //        npc.IsAttacking = false;
        //        //stop
        //    }
        //}

        public void SetupAllLogic(MapLogic mapLogic, PlayerLogic playerLogic, EnemyLogic enemyLogic, ItemLogic itemLogic)
        {
            this.mapLogic = mapLogic;
            this.playerLogic = playerLogic;
            this.enemyLogic = enemyLogic;
            this.itemLogic = itemLogic;

            
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


                setupCharacterOrientation(playerLogic.plyr.LeftOrientation, playerBrush);
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
                    else if (item is Charon)
                    {
                        drawingContext.DrawRectangle(charonBrush, null, new Rect(item.PosX, item.PosY, item.Width, item.Height));
                    }
                }

                foreach (var item in enemyLogic.enemies)
                {
                    if (item is Grunt)
                    {
                        if (item.IsAttacking)
                        {
                            setupCharacterOrientation(item.DirectionIsLeft, playerBrushSword);
                            drawingContext.DrawRectangle(playerBrushSword.CurrentBrush, null, new Rect(item.PosX, item.PosY, item.Width, item.Height));
                        }
                        else
                        {
                            setupCharacterOrientation(item.DirectionIsLeft, gruntBrush);
                            drawingContext.DrawRectangle(gruntBrush.CurrentBrush, null, new Rect(item.PosX, item.PosY, item.Width, item.Height));
                        }
                        
                    }
                    else if (item is Brute)
                    {
                        if (item.IsAttacking)
                        {
                            setupCharacterOrientation(item.DirectionIsLeft, bruteBrushAttack);
                            drawingContext.DrawRectangle(bruteBrushAttack.CurrentBrush, null, new Rect(item.PosX, item.PosY, item.Width, item.Height));
                        }
                        else
                        {
                            setupCharacterOrientation(item.DirectionIsLeft, bruteBrush);
                            drawingContext.DrawRectangle(bruteBrush.CurrentBrush, null, new Rect(item.PosX, item.PosY, item.Width, item.Height));
                        }
                    }
                    else if (item is Archer)
                    {
                        if (item.IsAttacking)
                        {
                            setupCharacterOrientation(item.DirectionIsLeft, archerBrushAttack);
                            drawingContext.DrawRectangle(archerBrushAttack.CurrentBrush, null, new Rect(item.PosX, item.PosY, item.Width, item.Height));
                        }
                        else
                        {
                            setupCharacterOrientation(item.DirectionIsLeft, archerBrush);
                            drawingContext.DrawRectangle(archerBrush.CurrentBrush, null, new Rect(item.PosX, item.PosY, item.Width, item.Height));
                        }
                        
                    }
                    else if (item is HeavyBrute)
                    {
                        if (item.IsAttacking)
                        {
                            setupCharacterOrientation(item.DirectionIsLeft, heavyBruteBrushAttack);
                            drawingContext.DrawRectangle(heavyBruteBrushAttack.CurrentBrush, null, new Rect(item.PosX, item.PosY, item.Width, item.Height));
                        }
                        else
                        {
                            setupCharacterOrientation(item.DirectionIsLeft, heavyBruteBrush);
                            drawingContext.DrawRectangle(heavyBruteBrush.CurrentBrush, null, new Rect(item.PosX, item.PosY, item.Width, item.Height));
                        }

                        
                    }
                }

                
                drawingContext.DrawRectangle(playerBrush.CurrentBrush, null, new Rect(playerLogic.plyr.PosX, playerLogic.plyr.PosY, playerLogic.plyr.Width, playerLogic.plyr.Height));
                
                foreach (var arrow in playerLogic.Arrows)
                {
                    drawingContext.DrawRectangle(arrowBrush, null, new Rect(arrow.PosX, arrow.PosY, arrow.Width, arrow.Height));
                }
                foreach (var arrow in enemyLogic.arrows)
                {
                    drawingContext.DrawRectangle(arrowBrush, null, new Rect(arrow.PosX, arrow.PosY, arrow.Width, arrow.Height));
                }


            }
        }

        private void setupCharacterOrientation(bool directionIsLeft, CharacterBrush characterBrush)
        {
            //alapbol minden kép balra néz. --> .Flipped = false
            //ha a karakter nem balra néz + a kép .Flipped = false => flip
            if (directionIsLeft)
            {
                MoveLeftAnimation(characterBrush);
                
            }
            else
            {
                MoveRightAnimation(characterBrush);
            }

            
        }

        public void PlyrMoveAnimation(bool leftOrientation, string action)
        {
            if (action == "start")
            {
                //playerBrush.CurrentBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "knight_Run.png"), UriKind.RelativeOrAbsolute)));
                playerBrush = playerBrushRun;
            }
            if (action == "stop")
            {
                //playerBrush.CurrentBrush = playerBrush.DefaultBrush;
                playerBrush = playerBrushIdle;
            }
        }

        public void FallAnimation(string action)
        {
            if (action == "start")
            {
                playerBrush = playerBrushFall;
            }
            //todo?
        }

        public void FightAnimations(string action)
        {
            if (action == "start")
            {
                playerBrush = playerBrushSword;
            }
            if (action == "stop")
            {
                playerBrush = playerBrushIdle;
            }


            #region obsolete
            //ObjectAnimationUsingKeyFrames anim = new ObjectAnimationUsingKeyFrames();
            //anim.Duration = TimeSpan.FromSeconds(3);
            //anim.FillBehavior = FillBehavior.Stop;
            //ImageSource[] images = new ImageSource[]
            //{
            //      new BitmapImage(new Uri(Path.Combine("Sources", "Her.png"), UriKind.RelativeOrAbsolute)),
            //      new BitmapImage(new Uri(Path.Combine("Sources", "knight", "knight 3 idle.png"), UriKind.RelativeOrAbsolute))
            //};
            //anim.KeyFrames.Add(new DiscreteObjectKeyFrame(images[1]));
            //anim.KeyFrames.Add(new DiscreteObjectKeyFrame(images[0]));
            //playerBrush.BeginAnimation(ImageBrush.ImageSourceProperty, anim);



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

            //var animation = new BrushAnimation
            //{
            //    From = Brushes.Red,
            //    To = new LinearGradientBrush(Colors.Green, Colors.Yellow, 45),
            //    Duration = new Duration(TimeSpan.FromSeconds(5)),
            //};
            //animation.Completed += new EventHandler(animation_Completed);
            //Storyboard.SetTarget(animation, border);
            //Storyboard.SetTargetProperty(animation, new PropertyPath("Background"));

            //var sb = new Storyboard();
            //sb.Children.Add(animation);
            //sb.Begin();

            #endregion

        }

        public void BowShootAnimation(string action)
        {
            if (action == "start")
            {
                playerBrush = playerBrushBow;
            }
            if (action == "stop")
            {
                playerBrush = playerBrushIdle;
            }
        }

        //public void IdleAnimation() //imageBrush nem tud animált gif-et megjeleníteni úgy tűnik
        //{
        //    var image = new BitmapImage();
        //    image.BeginInit();
        //    image.UriSource = new Uri(Path.Combine("Sources", "knight", "knight idle.gif"), UriKind.RelativeOrAbsolute);
        //    image.EndInit();
        //    System.Windows.Controls.Image img = new System.Windows.Controls.Image();
        //    img.Source = (playerBrush.CurrentBrush).ImageSource;
        //    ImageBehavior.SetAnimatedSource(img,image);
        //}

        public void JetpackAnimation(string action) //just player
        {
            if (action == "start")
            {
                playerBrush = playerBrushJetpack;
            }
            if (action == "stop") 
            {
                playerBrush = playerBrushIdle;
            }

            #region obsolete
            //var img = new BitmapImage(new Uri(Path.Combine("Sources", "knight", "knight 3 idle.png"), UriKind.RelativeOrAbsolute));
            //playerBrush = new ImageBrush(new TransformedBitmap(img, new ScaleTransform(-1,1)));
            //var dispatcherTimerInstance = new DispatcherTimer();
            //dispatcherTimerInstance.Tick += RevertBackToDefPlayerBrush;
            //dispatcherTimerInstance.Interval = new TimeSpan(0, 0, 2);
            //dispatcherTimerInstance.Start();
            //dispatcherTimers.Add(dispatcherTimerInstance);

            //ObjectAnimationUsingKeyFrames anim = new ObjectAnimationUsingKeyFrames();
            //anim.Duration = TimeSpan.FromSeconds(10);
            //anim.FillBehavior = FillBehavior.Stop;
            //ImageSource[] images = new ImageSource[]
            //{
            //      (playerBrush as ImageBrush).ImageSource,
            //      new BitmapImage(new Uri(Path.Combine("Sources", "knight", "knight 3 idle.png"), UriKind.RelativeOrAbsolute))
            //};
            //anim.KeyFrames.Add(new DiscreteObjectKeyFrame(images[0]));
            //anim.KeyFrames.Add(new DiscreteObjectKeyFrame(images[1]));
            //playerBrush.BeginAnimation(ImageBrush.ImageSourceProperty, anim);
            #endregion
        }

        private void MoveLeftAnimation(CharacterBrush characterBrush)
        {
            if (characterBrush.IsFlipped)
            {
                var img = (BitmapSource)characterBrush.CurrentBrush.ImageSource;
                var mirrorredImage = new TransformedBitmap(img, new ScaleTransform(-1, 1));
                characterBrush.CurrentBrush.ImageSource = mirrorredImage;
                characterBrush.IsFlipped = false;
            }

        }

        private void MoveRightAnimation(CharacterBrush characterBrush)
        {
            if (!characterBrush.IsFlipped)
            {
                var img = (BitmapSource)characterBrush.CurrentBrush.ImageSource;
                var mirrorredImage = new TransformedBitmap(img, new ScaleTransform(-1, 1));
                characterBrush.CurrentBrush.ImageSource = mirrorredImage;
                characterBrush.IsFlipped = true;
            }
        }

    }
}

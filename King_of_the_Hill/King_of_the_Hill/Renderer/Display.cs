namespace King_of_the_Hill.Renderer.Display
{
    using King_of_the_Hill.Logic;
    using King_of_the_Hill.Model.GameItems;
    using King_of_the_Hill.Model.MapItem;
    using King_of_the_Hill.Model.NPC_Types;
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    
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
            backgroundBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Textures", "Map", "Background9Layered.png"), UriKind.RelativeOrAbsolute)));

            playerBrush = new CharacterBrush(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Textures", "Player" ,"knight_idle.png"), UriKind.RelativeOrAbsolute))));
            playerBrushJetpack = new CharacterBrush(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Textures", "Player", "knight_JetPack.png"), UriKind.RelativeOrAbsolute))));
            playerBrushBow = new CharacterBrush(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Textures", "Player", "knight_bowshoot2.png"), UriKind.RelativeOrAbsolute))));
            playerBrushRun = new CharacterBrush(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Textures", "Player", "knight_Run.png"), UriKind.RelativeOrAbsolute))));
            playerBrushSword = new CharacterBrush(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Textures", "Player", "knight_Attack.png"), UriKind.RelativeOrAbsolute))));
            playerBrushIdle = new CharacterBrush(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Textures", "Player", "knight_idle.png"), UriKind.RelativeOrAbsolute))));
            playerBrushFall = new CharacterBrush(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Textures", "Player", "Her.png"), UriKind.RelativeOrAbsolute))));
            arrowBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Textures", "Items", "weapon_arrow.png"), UriKind.RelativeOrAbsolute)));
            groundBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Textures", "Map", "ground.png"), UriKind.RelativeOrAbsolute)));
            lavaBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Textures", "Map", "lava.png"), UriKind.RelativeOrAbsolute)));
            platformBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Textures", "Map", "PlatformHuge.png"), UriKind.RelativeOrAbsolute)));

            gruntBrush = new CharacterBrush(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Textures", "Enemy", "grunt_idle.png"), UriKind.RelativeOrAbsolute))));
            gruntBrushAttack = new CharacterBrush(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Textures", "Enemy", "grunt_attack.png"), UriKind.RelativeOrAbsolute))));
            bruteBrush = new CharacterBrush(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Textures", "Enemy", "brute_idle.png"), UriKind.RelativeOrAbsolute))));
            bruteBrushAttack = new CharacterBrush(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Textures", "Enemy", "brute_attack.png"), UriKind.RelativeOrAbsolute))));
            archerBrush = new CharacterBrush(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Textures", "Enemy", "archer_idle.png"), UriKind.RelativeOrAbsolute))));
            archerBrushAttack = new CharacterBrush(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Textures", "Enemy", "archer_attack.png"), UriKind.RelativeOrAbsolute))));
            heavyBruteBrush = new CharacterBrush(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Textures", "Enemy", "heavyBrute_idle.png"), UriKind.RelativeOrAbsolute))));
            heavyBruteBrushAttack = new CharacterBrush(new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Textures", "Enemy", "heavyBrute_attack.png"), UriKind.RelativeOrAbsolute))));

            armorBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Textures", "Items", "armor.png"), UriKind.RelativeOrAbsolute)));
            axeBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Textures", "Items", "axe.png"), UriKind.RelativeOrAbsolute)));
            bowBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Textures", "Items", "bow.png"), UriKind.RelativeOrAbsolute)));
            healPotionBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Textures", "Items", "healpotion.png"), UriKind.RelativeOrAbsolute)));
            jetpackBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Textures", "Items", "jetpack.png"), UriKind.RelativeOrAbsolute)));
            longSwordBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Textures", "Items", "longsword.png"), UriKind.RelativeOrAbsolute)));
            swordBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Textures", "Items", "sword.png"), UriKind.RelativeOrAbsolute)));
            charonBrush = new ImageBrush(new BitmapImage(new Uri(Path.Combine("Sources", "Textures", "Items", "charon.png"), UriKind.RelativeOrAbsolute)));
        }

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
                            
                            drawingContext.DrawRectangle( item.DirectionIsLeft ? gruntBrushAttack.CurrentBrush : FlippedBrush(gruntBrushAttack.CurrentBrush), null, new Rect(item.PosX, item.PosY, item.Width, item.Height));
                        }
                        else
                        {
                            
                            drawingContext.DrawRectangle(item.DirectionIsLeft ? gruntBrush.CurrentBrush : FlippedBrush(gruntBrush.CurrentBrush), null, new Rect(item.PosX, item.PosY, item.Width, item.Height));
                        }
                        
                    }
                    else if (item is Brute)
                    {
                        if (item.IsAttacking)
                        {
                            
                            drawingContext.DrawRectangle(item.DirectionIsLeft ? bruteBrushAttack.CurrentBrush : FlippedBrush(bruteBrushAttack.CurrentBrush), null, new Rect(item.PosX, item.PosY, item.Width, item.Height));
                        }
                        else
                        {
                            
                            drawingContext.DrawRectangle(item.DirectionIsLeft ? bruteBrush.CurrentBrush : FlippedBrush(bruteBrush.CurrentBrush), null, new Rect(item.PosX, item.PosY, item.Width, item.Height));
                        }
                    }
                    else if (item is Archer)
                    {
                        if (item.IsAttacking)
                        {
                            
                            drawingContext.DrawRectangle(item.DirectionIsLeft ? archerBrushAttack.CurrentBrush : FlippedBrush(archerBrushAttack.CurrentBrush), null, new Rect(item.PosX, item.PosY, item.Width, item.Height));
                        }
                        else
                        {
                            
                            drawingContext.DrawRectangle(item.DirectionIsLeft ? archerBrush.CurrentBrush : FlippedBrush(archerBrush.CurrentBrush), null, new Rect(item.PosX, item.PosY, item.Width, item.Height));
                        }
                        
                    }
                    else if (item is HeavyBrute)
                    {
                        if (item.IsAttacking)
                        {
                            
                            drawingContext.DrawRectangle(item.DirectionIsLeft? heavyBruteBrushAttack.CurrentBrush : FlippedBrush(heavyBruteBrushAttack.CurrentBrush), null, new Rect(item.PosX, item.PosY, item.Width, item.Height));
                        }
                        else
                        {
                            
                            drawingContext.DrawRectangle(item.DirectionIsLeft ? heavyBruteBrush.CurrentBrush : FlippedBrush(heavyBruteBrush.CurrentBrush), null, new Rect(item.PosX, item.PosY, item.Width, item.Height));
                        }

                        
                    }
                }


                if (playerBrush == playerBrushSword) //hotfix
                {
                    drawingContext.DrawRectangle(playerBrush.CurrentBrush, null, new Rect(playerLogic.plyr.PosX, playerLogic.plyr.PosY - 35, playerLogic.plyr.Width*1.5, playerLogic.plyr.Height*1.5));
                }
                else
                {
                    drawingContext.DrawRectangle(playerBrush.CurrentBrush, null, new Rect(playerLogic.plyr.PosX, playerLogic.plyr.PosY, playerLogic.plyr.Width, playerLogic.plyr.Height));
                }
                
                
                foreach (var arrow in playerLogic.Arrows)
                {
                    drawingContext.DrawRectangle(arrow.DirectionIsLeft ? arrowBrush : FlippedBrush((ImageBrush)arrowBrush), null, new Rect(arrow.PosX, arrow.PosY, arrow.Width, arrow.Height));
                }
                foreach (var arrow in enemyLogic.arrows)
                {
                    drawingContext.DrawRectangle(arrow.DirectionIsLeft ? arrowBrush : FlippedBrush((ImageBrush)arrowBrush), null, new Rect(arrow.PosX, arrow.PosY, arrow.Width, arrow.Height));
                }


            }
        }

        private void setupCharacterOrientation(bool directionIsLeft, CharacterBrush characterBrush) //just player
        {
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
                playerBrush = playerBrushRun;
            }
            if (action == "stop")
            {
                playerBrush = playerBrushIdle;
            }
        }

        public void FallAnimation(string action)
        {
            if (action == "start")
            {
                playerBrush = playerBrushFall;
            }
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

        private ImageBrush FlippedBrush(ImageBrush imgbrush)
        {
            var img = (BitmapSource)imgbrush.ImageSource;
            var mirrorredImage = new TransformedBitmap(img, new ScaleTransform(-1, 1));
            return new ImageBrush(mirrorredImage);
        }
    }
}

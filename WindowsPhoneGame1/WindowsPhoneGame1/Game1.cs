using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
using Microsoft.Devices.Sensors;
using WindowsPhoneGame1.Model;
using Microsoft.Devices;

namespace WindowsPhoneGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        enum GameState
        {
            TitleScreen,
            GameStarted,
            GameEnded
        }

        private GameState _currentGameState;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D texture1;
        Texture2D texture2;

        Texture2D _background;
        Texture2D _background2;

        Rectangle _mainFrame;
        Rectangle _mainFrame2;

        List<Texture2D> _listOfMainCharacters = new List<Texture2D>();
        List<Texture2D> _listOfMainCharacterDying = new List<Texture2D>();

        Texture2D _enemy1;
        Vector2 _enemy1Position;
        List<Texture2D> _listOfEnemy1 = new List<Texture2D>();

        Texture2D _enemy2;
        Vector2 _enemyPosition2;
        List<Texture2D> _listOfEnemy2 = new List<Texture2D>();

        //Dictionary<int, Enemy> _enemies = new Dictionary<int, Enemy>();

        Vector2 spritePosition1;
        Vector2 spritePosition2;
        Vector2 _backgroundPosition1;
        Vector2 _backgroundPosition2;

        Vector2 spriteSpeed1 = new Vector2(50.0f, 50.0f);
        Vector2 spriteSpeed2 = new Vector2(100.0f, 100.0f);
        int sprite1Height;
        int sprite1Width;
        int sprite2Height;
        int sprite2Width;

        SoundEffect soundEffect;

        Accelerometer _sensor;
        Vector3 _sensorReading = new Vector3();

        //Power Bar
        SpriteBatch _healthBarSpriteBatch;
        Texture2D _healthBarTexture2D;
        int _currentHealth = 100;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);

            TouchPanel.EnabledGestures = GestureType.Tap;

            _sensor = new Accelerometer();
            _sensor.CurrentValueChanged += _sensor_CurrentValueChanged;
            _sensor.Start();

            _currentGameState = GameState.GameEnded;
        }

        void _sensor_CurrentValueChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e)
        {
            _sensorReading.X = e.SensorReading.Acceleration.X;
            _sensorReading.Y = e.SensorReading.Acceleration.Y;
            _sensorReading.Z = e.SensorReading.Acceleration.Z;

            if (_sensorReading.Y > .5)
            {
                _sensorReading.Y = .5f * -1;
            }

            if (_sensorReading.Y < -.5)
            {
                _sensorReading.Y = .5f * -2;
            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            texture1 = Content.Load<Texture2D>("KnightAssetUserFlying0001");
            _listOfMainCharacters.Add(Content.Load<Texture2D>("KnightAssetUserFlying0001"));
            _listOfMainCharacters.Add(Content.Load<Texture2D>("KnightAssetUserFlying0002"));
            _listOfMainCharacters.Add(Content.Load<Texture2D>("KnightAssetUserFlying0003"));
            _listOfMainCharacters.Add(Content.Load<Texture2D>("KnightAssetUserFlying0004"));
            _listOfMainCharacters.Add(Content.Load<Texture2D>("KnightAssetUserFlying0005"));
            _listOfMainCharacters.Add(Content.Load<Texture2D>("KnightAssetUserFlying0006"));
            _listOfMainCharacters.Add(Content.Load<Texture2D>("KnightAssetUserFlying0007"));
            _listOfMainCharacters.Add(Content.Load<Texture2D>("KnightAssetUserFlying0008"));
            _listOfMainCharacters.Add(Content.Load<Texture2D>("KnightAssetUserFlying0009"));

            _listOfMainCharacterDying.Add(Content.Load<Texture2D>("KnightDying/KnightAssetUser_HIT0001"));
            _listOfMainCharacterDying.Add(Content.Load<Texture2D>("KnightDying/KnightAssetUser_HIT0002"));
            _listOfMainCharacterDying.Add(Content.Load<Texture2D>("KnightDying/KnightAssetUser_HIT0003"));
            _listOfMainCharacterDying.Add(Content.Load<Texture2D>("KnightDying/KnightAssetUser_HIT0004"));
            _listOfMainCharacterDying.Add(Content.Load<Texture2D>("KnightDying/KnightAssetUser_HIT0005"));
            _listOfMainCharacterDying.Add(Content.Load<Texture2D>("KnightDying/KnightAssetUser_HIT0006"));
            _listOfMainCharacterDying.Add(Content.Load<Texture2D>("KnightDying/KnightAssetUser_HIT0007"));
            _listOfMainCharacterDying.Add(Content.Load<Texture2D>("KnightDying/KnightAssetUser_HIT0008"));
            _listOfMainCharacterDying.Add(Content.Load<Texture2D>("KnightDying/KnightAssetUser_HIT0009"));
            _listOfMainCharacterDying.Add(Content.Load<Texture2D>("KnightDying/KnightAssetUser_HIT0010"));
            _listOfMainCharacterDying.Add(Content.Load<Texture2D>("KnightDying/KnightAssetUser_HIT0011"));
            _listOfMainCharacterDying.Add(Content.Load<Texture2D>("KnightDying/KnightAssetUser_HIT0012"));
            _listOfMainCharacterDying.Add(Content.Load<Texture2D>("KnightDying/KnightAssetUser_HIT0013"));

            _enemy1 = Content.Load<Texture2D>("Enemy1/Enemy01Assset0001");
            _listOfEnemy1.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0001"));
            _listOfEnemy1.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0002"));
            _listOfEnemy1.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0003"));
            _listOfEnemy1.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0004"));
            _listOfEnemy1.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0005"));
            _listOfEnemy1.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0006"));
            _listOfEnemy1.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0007"));
            _listOfEnemy1.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0008"));
            _listOfEnemy1.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0009"));
            _listOfEnemy1.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0010"));
            _listOfEnemy1.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0011"));
            _listOfEnemy1.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0012"));
            _listOfEnemy1.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0013"));
            _listOfEnemy1.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0014"));
            _listOfEnemy1.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0015"));
            _listOfEnemy1.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0016"));
            _listOfEnemy1.Add(Content.Load<Texture2D>("Enemy1/Enemy01Assset0017"));

            _background = Content.Load<Texture2D>("backgroundassetSimplified20485");
            _background2 = Content.Load<Texture2D>("backgroundassetSimplified20485");

            _healthBarSpriteBatch = new SpriteBatch(this.graphics.GraphicsDevice);
            _healthBarTexture2D = Content.Load<Texture2D>("HealthBarEdited");

            //for (int i = 0; i <= 4; i++)
            //{
            //    _enemies.Add(i, new Enemy(new Vector2(graphics.GraphicsDevice.Viewport.Width - _enemy1.Width, graphics.GraphicsDevice.Viewport.Height / 2))); 
            //}

            _backgroundPosition1 = new Vector2(0, 0);
            _backgroundPosition2 = new Vector2(graphics.PreferredBackBufferWidth, 0);

            _enemy1Position = new Vector2(graphics.GraphicsDevice.Viewport.Width - _enemy1.Width, graphics.GraphicsDevice.Viewport.Height / 2);

            _mainFrame = new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width * 2, graphics.GraphicsDevice.Viewport.Height);
            _mainFrame2 = new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width * 2, graphics.GraphicsDevice.Viewport.Height );
            int screenHeight = graphics.GraphicsDevice.Viewport.Height;
            int screenWidth = graphics.GraphicsDevice.Viewport.Width;

            soundEffect = Content.Load<SoundEffect>("Ding");

            spritePosition1.X = 0;
            spritePosition1.Y = 0;

            spritePosition2.X = graphics.GraphicsDevice.Viewport.Width - texture1.Width;
            spritePosition2.Y = graphics.GraphicsDevice.Viewport.Height - texture1.Height;

            sprite1Height = texture1.Bounds.Height;
            sprite1Width = texture1.Bounds.Width;

            //sprite2Height = texture2.Bounds.Height;
            //sprite2Width = texture2.Bounds.Width;

        }

        VibrateController _vibrate = VibrateController.Default;

        private int _collissions = 0;
        void CheckForCollision()
        {
            BoundingBox bb1 = new BoundingBox(
                new Vector3(spritePosition1.X - ((sprite1Width / 2) - 20), spritePosition1.Y - ((sprite1Height / 2) - 20), 0),
                new Vector3(spritePosition1.X + ((sprite1Width / 2) - 20), spritePosition1.Y + ((sprite1Height / 2) -20), 0));

            BoundingBox bb2 = new BoundingBox(
                new Vector3(_enemy1Position.X - (_enemy1.Width / 2), _enemy1Position.Y - (_enemy1.Height / 2), 0),
                new Vector3(_enemy1Position.X + (_enemy1.Width / 2), _enemy1Position.Y + (_enemy1.Height / 2), 0));

            if (bb1.Intersects(bb2))
            {
                soundEffect.Play();
                _collissions = _collissions + 1;

                _currentHealth -= 1;
                _currentHealth = (int)MathHelper.Clamp(_currentHealth, 0, 100);


                _vibrate.Start(TimeSpan.FromSeconds(.1));
                //Debug.WriteLine(_collissions);
            }
        }


        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allow the game to exit.
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back ==
                ButtonState.Pressed)
                this.Exit();

            if (TouchPanel.IsGestureAvailable)
            {
                var gesture = TouchPanel.ReadGesture();
                try
                {
                    CheckForTapOnBox(gesture.Position.X, gesture.Position.Y);
                }
                catch (Exception ex)
                {

                }
            }

            // Move the sprite around.
            UpdateSprite(gameTime, ref spritePosition1, ref spriteSpeed1);
            //UpdateSprite(gameTime, ref spritePosition2, ref spriteSpeed2);
            CheckForCollision();

            UpdateBackground();



            base.Update(gameTime);

        }

        private void UpdateBackground()
        {
            if (_mainFrame2.X == 0)
            {
                _mainFrame2.X = graphics.GraphicsDevice.Viewport.Width;
            }

            if (_mainFrame.X == -graphics.GraphicsDevice.Viewport.Width)
            {
                _mainFrame.X = 0;
            }

            _mainFrame.X -= 1;
            _mainFrame2.X -= 1;

        }

        void CheckForTapOnBox(float x, float y)
        {
            BoundingBox bb1 = new BoundingBox(
                new Vector3(spritePosition1.X - (sprite1Width / 2), spritePosition1.Y - (sprite1Height / 2), 0),
                new Vector3(spritePosition1.X + (sprite1Width / 2), spritePosition1.Y + (sprite1Height / 2), 0));

            BoundingBox bb2 = new BoundingBox(
                new Vector3(spritePosition2.X - (sprite2Width / 2), spritePosition2.Y - (sprite2Height / 2), 0),
                new Vector3(spritePosition2.X + (sprite2Width / 2), spritePosition2.Y + (sprite2Height / 2), 0));

            BoundingBox tapBox = new BoundingBox(new Vector3(x), new Vector3(y));


            float leftX = spritePosition1.X - (sprite1Width / 2);
            float rightX = spritePosition1.X + (sprite1Width / 2);
            float topY = spritePosition1.Y + (sprite1Height / 2);
            float bottomY = spritePosition1.Y - (sprite1Height / 2);

            if ((x >= leftX && x <= rightX) && (y <= topY && y >= bottomY))
            {
                Debug.WriteLine("Box Tapped");
            }
        }

        TimeSpan _frameLength;
        TimeSpan _enemyFrameLength;

        private int _currentFrame = 0;
        private int GetCurrentFrame()
        {
            int returnValue = 0;

            if (_currentFrame == 8)
            {
                _currentFrame = 0;
            }
            else
            {
                _currentFrame = _currentFrame + 1;
                returnValue = _currentFrame;
            }

            return returnValue;
        }

        private int _mainCharacterDyingFrame = 0;
        private int GetMainCharacterDyingFrame()
        {
            int returnValue = 0;

            if (_mainCharacterDyingFrame == 12)
            {
                _mainCharacterDyingFrame = 0;
            }
            else
            {
                _mainCharacterDyingFrame += 1;
                returnValue = _mainCharacterDyingFrame;
            }

            return returnValue;
        }

        private int _currentEnemyFrame = 0;
        private int GetCurrentEnemy1Frame()
        {
            int returnValue = 0;

            if (_currentEnemyFrame == 16)
            {
                _currentEnemyFrame = 0;
            }
            else
            {
                _currentEnemyFrame += 1;
                returnValue = _currentEnemyFrame;
            }

            return returnValue;
        }

        void UpdateSprite(GameTime gameTime, ref Vector2 spritePosition, ref Vector2 spriteSpeed)
        {
            _frameLength += gameTime.ElapsedGameTime;
            _enemyFrameLength += gameTime.ElapsedGameTime;

            if (_frameLength.TotalSeconds > 0.1d)
            {
                _frameLength -= TimeSpan.FromSeconds(.1);

                if (_currentHealth > 0)
                {
                    texture1 = _listOfMainCharacters[GetCurrentFrame()];
                }
                else
                {
                    texture1 = _listOfMainCharacterDying[GetMainCharacterDyingFrame()];
                }
            }

            if (_enemyFrameLength.TotalSeconds > 0.1d)
            {
                _enemyFrameLength -= TimeSpan.FromSeconds(.1);
                _enemy1 = _listOfEnemy1[GetCurrentEnemy1Frame()];
            }

            // Move the sprite by speed, scaled by elapsed time.
            //spritePosition +=
            //    spriteSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            spritePosition += spriteSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            //int MaxX =
            //    graphics.GraphicsDevice.Viewport.Width - texture1.Width;
            //int MinX = 0;
            int MaxY =
                graphics.GraphicsDevice.Viewport.Height - texture1.Height;

            int MinY = 0;

            int MaxX = graphics.GraphicsDevice.Viewport.Width - texture1.Width;
            int MinX = 0;

            //// Check for bounce.
            //if (spritePosition.X > MaxX)
            //{
            //    spriteSpeed.X *= -1;
            //    spritePosition.X = MaxX;
            //}

            //else if (spritePosition.X < MinX)
            //{
            //    spriteSpeed.X *= -1;
            //    spritePosition.X = MinX;
            //}

            spritePosition.Y = spritePosition.Y + (spritePosition.Y * (_sensorReading.X * .5f));
            spritePosition.X = spritePosition.X + (spritePosition.X * (_sensorReading.Y * .5f));


            if (spritePosition.Y > MaxY)
            {
                spriteSpeed.Y *= -1;
                spritePosition.Y = MaxY;
            }
            else if (spritePosition.Y < MinY)
            {
                spriteSpeed.Y *= -1;
                spritePosition.Y = MinY;
            }

            if (spritePosition.X > MaxX)
            {
                spriteSpeed.X *= -1;
                spritePosition.X = MaxX;
            }
            else if (spritePosition.X < MinX)
            {
                spriteSpeed.X *= -1;
                spritePosition.X = MinX;
            }

            //enemy positioning
            if (_enemy1Position.X > 0)
            {
                _enemy1Position.X -= 10;
            }
            else
            {
                _enemy1Position.X = graphics.GraphicsDevice.Viewport.Width - _enemy1.Width;
                _enemy1Position.Y = GetRandomEnemy1YPosition();
            }

        }

        private Random _randomEnemy1 = new Random();
        private float GetRandomEnemy1YPosition()
        {
            float returnValue = 0;

            returnValue = _randomEnemy1.Next(0, graphics.GraphicsDevice.Viewport.Height - _enemy1.Height); 

            return returnValue;
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            //if (_currentGameState == GameState.GameStarted)
            //{
                graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

                spriteBatch.Begin();
                spriteBatch.Draw(_background, _mainFrame, Color.White);
                spriteBatch.End();

                spriteBatch.Begin();
                spriteBatch.Draw(_background2, _mainFrame2, Color.White);
                spriteBatch.End();

                _healthBarSpriteBatch.Begin();

                //Draw the box around the health bar            
                _healthBarSpriteBatch.Draw(_healthBarTexture2D, new Rectangle(this.Window.ClientBounds.Width / 2 - _healthBarTexture2D.Width / 2,
                    30, _healthBarTexture2D.Width, 44), new Rectangle(0, 0, _healthBarTexture2D.Width, 44), Color.White);

                //Draw the health for the health bar
                //Draw the current health level based on the current Health           
                _healthBarSpriteBatch.Draw(_healthBarTexture2D, new Rectangle(this.Window.ClientBounds.Width / 2 - _healthBarTexture2D.Width / 2,
                    30, (int)(_healthBarTexture2D.Width * ((double)_currentHealth / 100)), 44),
                    new Rectangle(0, 45, _healthBarTexture2D.Width, 44), Color.Red);

                _healthBarSpriteBatch.End();

                // Draw the sprite.
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                spriteBatch.Draw(texture1, spritePosition1, Color.White);
                spriteBatch.End();

                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
                spriteBatch.Draw(_enemy1, _enemy1Position, Color.White);
                spriteBatch.End();

                //try the new enemies drawing
                List<int> positionsToDrawEnemy = new List<int>();

                int screenHeight = graphics.GraphicsDevice.Viewport.Height;
                //int heightOfEnemy = _enemies[0].ListOfEnemy[0].Height;

                //int numberOfEnemies = (int)screenHeight / heightOfEnemy;

            //}


            base.Draw(gameTime);
        }
    }
}

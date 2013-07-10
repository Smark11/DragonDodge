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
using Microsoft.Devices.Sensors;
using Microsoft.Devices;

namespace DragonDodge
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SoundEffect _soundEffect;
        VibrateController _vibrateController = VibrateController.Default;

        #region properties

        #region accelerometer

        Accelerometer _sensor;
        Vector3 _sensorReading;

        #endregion accelerometer

        #region background

        Texture2D _background;
        Texture2D _background2;
        Vector2 _backgroundPosition1;
        Vector2 _backgroundPosition2;
        Rectangle _mainFrame;
        Rectangle _mainFrame2;

        #endregion background

        #region maincharacter

        Texture2D _mainCharacterTexture;
        Vector2 _mainCharacterPosition;
        Vector2 _mainCharacterSpeed = new Vector2(50.0f, 50.0f);
        TimeSpan _mainCharacterFrameLength;
        int _currentHealth = 100;

        List<Texture2D> _listOfMainCharacters = new List<Texture2D>();
        List<Texture2D> _listOfMainCharacterDying = new List<Texture2D>();
        List<Texture2D> _listOfMainCharacterBigFist = new List<Texture2D>();

        #endregion maincharacter

        #region enemy

        Texture2D _enemyTexture;
        Vector2 _enemyPosition;
        Vector2 _enemySpeed = new Vector2(50.0f, 50.0f);
        TimeSpan _enemyFrameLength;

        List<Texture2D> _listOfEnemies = new List<Texture2D>();
        List<Texture2D> _listOfEnemiesDying = new List<Texture2D>();

        #endregion enemy

        #region healthbar

        SpriteBatch _healthBarSpriteBatch;
        Texture2D _healthBarTexture2D;

        #endregion healthbar

        #endregion properties

        public Game1()
        {
            try
            {
                graphics = new GraphicsDeviceManager(this);
                _currentHealth = 100;
                Content.RootDirectory = "Content";

                _listOfMainCharacterBigFist = new List<Texture2D>();
                _listOfMainCharacterDying = new List<Texture2D>();
                _listOfMainCharacters = new List<Texture2D>();
                _listOfEnemies = new List<Texture2D>();
                _listOfEnemiesDying = new List<Texture2D>();

                _sensor = new Accelerometer();
                _sensor.CurrentValueChanged += _sensor_CurrentValueChanged;
                _sensor.Start();

                // Frame rate is 30 fps by default for Windows Phone.
                TargetElapsedTime = TimeSpan.FromTicks(333333);

                // Extend battery life under lock.
                InactiveSleepTime = TimeSpan.FromSeconds(1);
            }
            catch (Exception ex)
            {

            }
        }

        void _sensor_CurrentValueChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e)
        {
            try
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
            catch (Exception ex)
            {

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
            try
            {
                // TODO: Add your initialization logic here
                TouchPanel.EnabledGestures = GestureType.Tap;

                _soundEffect = Content.Load<SoundEffect>("Sounds/Ding");

                base.Initialize();
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            try
            {
                // Create a new SpriteBatch, which can be used to draw textures.
                spriteBatch = new SpriteBatch(GraphicsDevice);

                //Background
                LoadBackground();

                //LoadMainCharacters
                LoadMainCharacter();

                //LoadEnemy
                LoadEnemy();

                //HealthBar
                _healthBarSpriteBatch = new SpriteBatch(this.graphics.GraphicsDevice);
                _healthBarTexture2D = Content.Load<Texture2D>("HealthBar/HealthBarEdited");
            }
            catch (Exception ex)
            {

            }
        }

        #region MainCharacter

        private void LoadMainCharacter()
        {
            _mainCharacterTexture = Content.Load<Texture2D>("KnightNormal/KnightAssetUserFlying0001");
            _listOfMainCharacters.Add(Content.Load<Texture2D>("KnightNormal/KnightAssetUserFlying0001"));
            _listOfMainCharacters.Add(Content.Load<Texture2D>("KnightNormal/KnightAssetUserFlying0002"));
            _listOfMainCharacters.Add(Content.Load<Texture2D>("KnightNormal/KnightAssetUserFlying0003"));
            _listOfMainCharacters.Add(Content.Load<Texture2D>("KnightNormal/KnightAssetUserFlying0004"));
            _listOfMainCharacters.Add(Content.Load<Texture2D>("KnightNormal/KnightAssetUserFlying0005"));
            _listOfMainCharacters.Add(Content.Load<Texture2D>("KnightNormal/KnightAssetUserFlying0006"));
            _listOfMainCharacters.Add(Content.Load<Texture2D>("KnightNormal/KnightAssetUserFlying0007"));
            _listOfMainCharacters.Add(Content.Load<Texture2D>("KnightNormal/KnightAssetUserFlying0008"));
            _listOfMainCharacters.Add(Content.Load<Texture2D>("KnightNormal/KnightAssetUserFlying0009"));

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

            _listOfMainCharacterBigFist.Add(Content.Load<Texture2D>("KnightBigFist/KnightAssetUser0001"));
            _listOfMainCharacterBigFist.Add(Content.Load<Texture2D>("KnightBigFist/KnightAssetUser0002"));
            _listOfMainCharacterBigFist.Add(Content.Load<Texture2D>("KnightBigFist/KnightAssetUser0003"));
            _listOfMainCharacterBigFist.Add(Content.Load<Texture2D>("KnightBigFist/KnightAssetUser0004"));
            _listOfMainCharacterBigFist.Add(Content.Load<Texture2D>("KnightBigFist/KnightAssetUser0005"));
        }

        private int _mainCharacterNormalFrame = 0;
        private int GetMainCharacterNormalFrame()
        {
            int returnValue = 0;

            if (_mainCharacterNormalFrame == 8)
            {
                _mainCharacterNormalFrame = 0;
            }
            else
            {
                _mainCharacterNormalFrame = _mainCharacterNormalFrame + 1;
                returnValue = _mainCharacterNormalFrame;
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

        private int _mainCharacterFistFrame = 0;
        private int GetMainCharacterFistFrame()
        {
            int returnValue = 0;

            if (_mainCharacterFistFrame == 4)
            {
                _mainCharacterFistFrame = 0;
            }
            else
            {
                _mainCharacterFistFrame += 1;
                returnValue = _mainCharacterFistFrame;
            }

            return returnValue;
        }

        private bool _fistOut;
        private void UpdateMainCharacter(GameTime gameTime, ref Vector2 _mainCharacterPosition, ref Vector2 _mainCharacterSpeed)
        {
            _mainCharacterFrameLength += gameTime.ElapsedGameTime;

            if (_mainCharacterFrameLength.TotalSeconds > 0.1d)
            {
                _mainCharacterFrameLength -= TimeSpan.FromSeconds(.1);

                if (_currentHealth > 0)
                {
                    if (!_fistOut)
                    {
                        _mainCharacterTexture = _listOfMainCharacters[GetMainCharacterNormalFrame()];
                    }
                    else
                    {
                        int fistFrame = GetMainCharacterFistFrame();
                        _mainCharacterTexture = _listOfMainCharacterBigFist[fistFrame];
                        if (fistFrame == 4)
                        {
                            _fistOut = false;
                        }
                    }
                }
                else
                {
                    _mainCharacterTexture = _listOfMainCharacterDying[GetMainCharacterDyingFrame()];
                }


                _mainCharacterPosition += _mainCharacterSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                int MaxY =
                    graphics.GraphicsDevice.Viewport.Height - _mainCharacterTexture.Height;

                int MinY = 0;

                int MaxX = graphics.GraphicsDevice.Viewport.Width - _mainCharacterTexture.Width;
                int MinX = 0;

                _mainCharacterPosition.Y = _mainCharacterPosition.Y + (_mainCharacterPosition.Y * (_sensorReading.X * .5f));
                _mainCharacterPosition.X = _mainCharacterPosition.X + (_mainCharacterPosition.X * (_sensorReading.Y * .5f));


                if (_mainCharacterPosition.Y > MaxY)
                {
                    _mainCharacterSpeed.Y *= -1;
                    _mainCharacterPosition.Y = MaxY;
                }
                else if (_mainCharacterPosition.Y < MinY)
                {
                    _mainCharacterSpeed.Y *= -1;
                    _mainCharacterPosition.Y = MinY;
                }

                if (_mainCharacterPosition.X > MaxX)
                {
                    _mainCharacterSpeed.X *= -1;
                    _mainCharacterPosition.X = MaxX;
                }
                else if (_mainCharacterPosition.X < MinX)
                {
                    _mainCharacterSpeed.X *= -1;
                    _mainCharacterPosition.X = MinX;
                }
            }

            CheckForFistOut();
        }

        private void CheckForFistOut()
        {
            try
            {
                if (TouchPanel.IsGestureAvailable)
                {
                    var gesture = TouchPanel.ReadGesture();

                    if (gesture.Position.X > 0 && gesture.Position.Y > 0)
                    {
                        _fistOut = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        #endregion MainCharacter

        #region Enemy

        private void LoadEnemy()
        {
            _enemyTexture = Content.Load<Texture2D>("EnemyNormal/Enemy01Assset0001");
            _listOfEnemies.Add(Content.Load<Texture2D>("EnemyNormal/Enemy01Assset0001"));
            _listOfEnemies.Add(Content.Load<Texture2D>("EnemyNormal/Enemy01Assset0002"));
            _listOfEnemies.Add(Content.Load<Texture2D>("EnemyNormal/Enemy01Assset0003"));
            _listOfEnemies.Add(Content.Load<Texture2D>("EnemyNormal/Enemy01Assset0004"));
            _listOfEnemies.Add(Content.Load<Texture2D>("EnemyNormal/Enemy01Assset0005"));
            _listOfEnemies.Add(Content.Load<Texture2D>("EnemyNormal/Enemy01Assset0006"));
            _listOfEnemies.Add(Content.Load<Texture2D>("EnemyNormal/Enemy01Assset0007"));
            _listOfEnemies.Add(Content.Load<Texture2D>("EnemyNormal/Enemy01Assset0008"));
            _listOfEnemies.Add(Content.Load<Texture2D>("EnemyNormal/Enemy01Assset0009"));
            _listOfEnemies.Add(Content.Load<Texture2D>("EnemyNormal/Enemy01Assset0010"));
            _listOfEnemies.Add(Content.Load<Texture2D>("EnemyNormal/Enemy01Assset0011"));
            _listOfEnemies.Add(Content.Load<Texture2D>("EnemyNormal/Enemy01Assset0012"));
            _listOfEnemies.Add(Content.Load<Texture2D>("EnemyNormal/Enemy01Assset0013"));
            _listOfEnemies.Add(Content.Load<Texture2D>("EnemyNormal/Enemy01Assset0014"));
            _listOfEnemies.Add(Content.Load<Texture2D>("EnemyNormal/Enemy01Assset0015"));
            _listOfEnemies.Add(Content.Load<Texture2D>("EnemyNormal/Enemy01Assset0016"));
            _listOfEnemies.Add(Content.Load<Texture2D>("EnemyNormal/Enemy01Assset0017"));

            _listOfEnemiesDying.Add(Content.Load<Texture2D>("EnemyDying/Enemy01Asset_HIT_POP0001"));
            _listOfEnemiesDying.Add(Content.Load<Texture2D>("EnemyDying/Enemy01Asset_HIT_POP0002"));
            _listOfEnemiesDying.Add(Content.Load<Texture2D>("EnemyDying/Enemy01Asset_HIT_POP0003"));
            _listOfEnemiesDying.Add(Content.Load<Texture2D>("EnemyDying/Enemy01Asset_HIT_POP0004"));
            _listOfEnemiesDying.Add(Content.Load<Texture2D>("EnemyDying/Enemy01Asset_HIT_POP0005"));
            _listOfEnemiesDying.Add(Content.Load<Texture2D>("EnemyDying/Enemy01Asset_HIT_POP0006"));
            _listOfEnemiesDying.Add(Content.Load<Texture2D>("EnemyDying/Enemy01Asset_HIT_POP0007"));
            _listOfEnemiesDying.Add(Content.Load<Texture2D>("EnemyDying/Enemy01Asset_HIT_POP0008"));
            _listOfEnemiesDying.Add(Content.Load<Texture2D>("EnemyDying/Enemy01Asset_HIT_POP0009"));
            _listOfEnemiesDying.Add(Content.Load<Texture2D>("EnemyDying/Enemy01Asset_HIT_POP0010"));
            _listOfEnemiesDying.Add(Content.Load<Texture2D>("EnemyDying/Enemy01Asset_HIT_POP0011"));
        }

        private int _normalEnemyFrame = 0;
        private int GetNormalEnemyFrame()
        {
            int returnValue = 0;

            if (_normalEnemyFrame == 16)
            {
                _normalEnemyFrame = 0;
            }
            else
            {
                _normalEnemyFrame += 1;
                returnValue = _normalEnemyFrame;
            }

            return returnValue;
        }

        private int _enemyDyingFrame = 0;
        private int GetEnemyDyingFrame()
        {
            int returnValue = 0;

            if (_enemyDyingFrame == 10)
            {
                _enemyDyingFrame = 0;
            }
            else
            {
                _enemyDyingFrame += 1;
                returnValue = _enemyDyingFrame;
            }

            return returnValue;
        }

        private Random _randomEnemy1 = new Random();
        private float GetRandomEnemy1YPosition()
        {
            float returnValue = 0;

            returnValue = _randomEnemy1.Next(0, graphics.GraphicsDevice.Viewport.Height - _enemyTexture.Height);

            return returnValue;
        }

        private bool _enemyDead = false;

        private void UpdateEnemy(GameTime gameTime, ref Vector2 _mainCharacterPosition, ref Vector2 _mainCharacterSpeed)
        {
            _enemyFrameLength += gameTime.ElapsedGameTime;

            if (_enemyFrameLength.TotalSeconds > 0.1d)
            {
                _enemyFrameLength -= TimeSpan.FromSeconds(.1);

                if (!_enemyDead)
                {
                    _enemyTexture = _listOfEnemies[GetNormalEnemyFrame()];
                }
                else
                {
                    int dyingFrame = GetEnemyDyingFrame();
                    _enemyTexture = _listOfEnemiesDying[GetEnemyDyingFrame()];

                    if (dyingFrame == 10)
                    {
                        _enemyPosition.X = -100;
                        _enemyDead = false;
                    }
                }
            }

            if (_enemyPosition.X > 0)
            {
                _enemyPosition.X -= 10;
            }
            else
            {
                _enemyDead = false;
                _enemyPosition.X = graphics.GraphicsDevice.Viewport.Width - _enemyTexture.Width;
                _enemyPosition.Y = GetRandomEnemy1YPosition();
            }
        }

        #endregion Enemy

        #region Collisions

        private void CheckForCollision()
        {
            BoundingBox bb1 = new BoundingBox(
                new Vector3(_mainCharacterPosition.X - ((_mainCharacterTexture.Width / 2) - 20), _mainCharacterPosition.Y - ((_mainCharacterTexture.Height / 2) - 20), 0),
                new Vector3(_mainCharacterPosition.X + ((_mainCharacterTexture.Width / 2) - 20), _mainCharacterPosition.Y + ((_mainCharacterTexture.Height / 2) - 20), 0));

            BoundingBox bb2 = new BoundingBox(
                new Vector3(_enemyPosition.X - (_enemyTexture.Width / 2), _enemyPosition.Y - (_enemyTexture.Height / 2), 0),
                new Vector3(_enemyPosition.X + (_enemyTexture.Width / 2), _enemyPosition.Y + (_enemyTexture.Height / 2), 0));

            if (bb1.Intersects(bb2))
            {
                _soundEffect.Play();

                if (!_fistOut)
                {
                    _currentHealth -= 1;
                    _currentHealth = (int)MathHelper.Clamp(_currentHealth, 0, 100);
                }
                else
                {
                    _enemyDead = true;
                }

                _vibrateController.Start(TimeSpan.FromSeconds(.1));
                //Debug.WriteLine(_collissions);
            }
        }

        #endregion Collisions

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
            try
            {
                // Allows the game to exit
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    this.Exit();

                // TODO: Add your update logic here

                base.Update(gameTime);

                //Background
                UpdateBackground();

                //Move Main Character Around
                UpdateMainCharacter(gameTime, ref _mainCharacterPosition, ref _mainCharacterSpeed);

                //Update Enemy
                UpdateEnemy(gameTime, ref _mainCharacterPosition, ref _mainCharacterSpeed);

                //Check For Collisions
                CheckForCollision();
            }
            catch (Exception ex)
            {

            }
        }

        #region Background

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

        private void LoadBackground()
        {
            _background = Content.Load<Texture2D>("Background/backgroundassetSimplified20485");
            _background2 = Content.Load<Texture2D>("Background/backgroundassetSimplified20485");
            _backgroundPosition1 = new Vector2(0, 0);
            _backgroundPosition2 = new Vector2(graphics.PreferredBackBufferWidth, 0);
            _mainFrame = new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width * 2, graphics.GraphicsDevice.Viewport.Height);
            _mainFrame2 = new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width * 2, graphics.GraphicsDevice.Viewport.Height);
        }

        #endregion Background

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            try
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);

                //Background
                spriteBatch.Begin();
                spriteBatch.Draw(_background, _mainFrame, Color.White);
                spriteBatch.End();
                spriteBatch.Begin();
                spriteBatch.Draw(_background2, _mainFrame2, Color.White);
                spriteBatch.End();

                //HealthBar
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

                //Main Character
                spriteBatch.Begin();
                spriteBatch.Draw(_mainCharacterTexture, _mainCharacterPosition, Color.White);
                spriteBatch.End();

                //Enemy
                spriteBatch.Begin();
                spriteBatch.Draw(_enemyTexture, _enemyPosition, Color.White);
                spriteBatch.End();

                // TODO: Add your drawing code here

                base.Draw(gameTime);
            }
            catch (Exception ex)
            {

            }
        }
    }
}

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

namespace StupidBird
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D bg_game1;
        Texture2D bg_game;

        SoundEffect soundWing;
        SoundEffect soundPoint;
        SoundEffect soundDie;
        SoundEffect soundhit;
        
        Footer footer;
        PipeControl pipeControl;
        Texture2D bird1;
        bool isReady = false;
        bool isGameOver = false;
        Bird StupidBird;
        double timerBird, timerPipe;
        public bool isTouchGround = false;
        public SpriteFont fontScore;
        public int score = 0;
        Texture2D GameoverTexture;
        Texture2D BtnRetry;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.SupportedOrientations = DisplayOrientation.Portrait;
            graphics.PreferredBackBufferWidth = 480;
            graphics.PreferredBackBufferHeight = 800;
            

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(33333);    ///333333
           
            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);
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

            // TODO: use this.Content to load your game content here
            bg_game1 = Content.Load<Texture2D>("img/bg_game1");
            bg_game = Content.Load<Texture2D>("img/bg_game");

            soundPoint = Content.Load<SoundEffect>("sound/sfx_point");
            soundWing = Content.Load<SoundEffect>("sound/sfx_wing");
            soundDie = Content.Load<SoundEffect>("sound/sfx_die");
            soundhit = Content.Load<SoundEffect>("sound/sfx_hit");


            footer = new Footer(Content, spriteBatch);
            pipeControl = new PipeControl(Content, spriteBatch);
            bird1 = Content.Load<Texture2D>("img/bird1");
            fontScore = Content.Load<SpriteFont>("SpriteFont1");
            StupidBird = new Bird(Content, spriteBatch);
            BtnRetry = Content.Load<Texture2D>("img/btn-play");
            GameoverTexture = Content.Load<Texture2D>("img/gameover");
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            checkTouch();
            if (!isGameOver && !isTouchGround)
            {
                footer.Update();
                
                if (isReady)
                {

                    timerPipe += gameTime.ElapsedGameTime.TotalSeconds;
                    if (timerPipe >= 0.10000000149011612)
                    {
                        pipeControl.Update();
                    }
                    if (pipeControl.CheckCollideWithBird(StupidBird))
                    {
                        soundhit.Play();
                        soundDie.Play();
                        isGameOver = true;
                    }
                    isTouchGround = StupidBird.isTouchGround;
                    if (isTouchGround)
                    {
                        soundhit.Play();
                    }
                    foreach (Pipe pipe in pipeControl.q_pipe2)
                    {
                        if (pipe.CanScore && pipe.Position.X <= 100)
                        {
                            soundPoint.Play();
                            score++;
                            pipe.CanScore = false;
                        }
                    }
                }
                
            }
            
            if (isReady)
            {
                timerBird += gameTime.ElapsedGameTime.TotalSeconds;
                if (timerBird >= 0.029999999329447746)
                {
                    StupidBird.Move();
                    
                }
            }
            base.Update(gameTime);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            if (!isReady)
            {
                spriteBatch.Draw(bg_game1, GraphicsDevice.Viewport.Bounds, Color.White);
                spriteBatch.Draw(bird1, new Vector2(100f, 360f), Color.White);
            }
            else
            {
                spriteBatch.Draw(bg_game, GraphicsDevice.Viewport.Bounds, Color.White);
                pipeControl.Draw();
                StupidBird.Draw();
                if (isGameOver || isTouchGround)
                {
                    spriteBatch.Draw(BtnRetry, new Vector2((480-BtnRetry.Width)/2, 400), Color.White);
                    spriteBatch.Draw(GameoverTexture, new Vector2((480-GameoverTexture.Width)/2, 300), Color.White);
                }

            }
            footer.Draw();
            spriteBatch.DrawString(fontScore, score.ToString(), new Vector2(200f, 100f), Color.White);
            spriteBatch.End();


            base.Draw(gameTime);
        }

        public void checkTouch()
        {
            foreach(TouchLocation location in TouchPanel.GetState())
            {
                if (location.State == TouchLocationState.Pressed && !isReady && !isGameOver && !isTouchGround)
                {
                    isReady = true;
                    StupidBird.Move();
                }
                if (location.State == TouchLocationState.Pressed && isReady && !isGameOver && !isTouchGround)
                {

                    soundWing.Play();
                    StupidBird.isUp = true;
                    StupidBird.ResetMove();
                }
                Vector2 Position = location.Position;
                if ((isGameOver || isTouchGround) && (Position.Y >= 400) && (Position.Y <= 400 + BtnRetry.Height) && (Position.X >= (480 - BtnRetry.Width) / 2) && (Position.X <= (480 + BtnRetry.Width) / 2))
                {
                    isReady = false;
                    isGameOver = false;
                    isTouchGround = false;
                    score = 0;
                    StupidBird.Position = new Vector2(100f, 360f);
                    StupidBird.ResetMove();
                    StupidBird.isTouchGround = false;
                    pipeControl = new PipeControl(Content, spriteBatch);
                }
            }
        }
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace BrickBreaker
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Ball ball;
        Brick brick;
        Paddle paddle;
        SpriteFont font1;
        Texture2D pixel;
        int lives = 3;
        List<Brick> bricks = new List<Brick>();
        bool gameover = false;
        int brickdeestroyed = 0;
        bool lose = false;
        bool win = false;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            IsMouseVisible = true;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData( new Color[]{ Color.White } );

            Texture2D texture1 = Content.Load<Texture2D>("goodball");
            Texture2D texture2 = Content.Load<Texture2D>("goodpaddle");
            Texture2D texture3 = Content.Load<Texture2D>("goodbrick");
             font1 = Content.Load<SpriteFont>("font1");

            Vector2 position1 = new Vector2(300, 350);
            Vector2 position2 = new Vector2(300, 450);
            Vector2 position3 = new Vector2(0, 0);
            Vector2 speed1 = new Vector2(8, 8);
            

            ball = new Ball(texture1, position1, Color.White, speed1);
            brick = new Brick(texture3, position3, Color.White);
            paddle = new Paddle(texture2, position2, Color.White, speed1);


           
            for (int i = 0; i < 6; i++)
            {
                bricks.Add(new Brick(texture3, brick.position, Color.White));


                for (int j = 0; j < 12; j++)
                {
                    brick.position.X += brick.texture.Width;
                    bricks.Add(new Brick(texture3, brick.position, Color.White));
                }
                brick.position.Y += brick.texture.Height;
                brick.position.X = 0;
            }
            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            ball.Bounce(GraphicsDevice);
            // TODO: Add your update logic here
            if (paddle.position.X + paddle.texture.Width < GraphicsDevice.Viewport.Width)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    paddle.position.X += paddle.speed.X;

                }
            }

            if (paddle.position.X> 0)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    paddle.position.X -= paddle.speed.X;

                }
            }
            for (int i = 0; i < bricks.Count; i++)
            {
                if (ball.hitbox.Intersects(bricks[i].hibox))
                {
                    bricks.Remove(bricks[i]);
                    ball.speed.Y *= -1;
                    brickdeestroyed++;
                }
            }
            




            if (ball.hitbox.Intersects(paddle.hitbox))
            {
                ball.speed.Y = -Math.Abs(ball.speed.Y);
            }

            if(ball.position.Y > GraphicsDevice.Viewport.Height)
            {
                lives--;
                ball.position = new Vector2(300, 350);
                ball.speed.X = 0;
                ball.speed.Y = 0;
                
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                ball.speed.X = 8;
                ball.speed.Y = 8;
            }

            if(lives == 0)
            {
                lose = true;
                gameover = true;
            }

            if (gameover == true)
            {
                ball.position = new Vector2(300, 350);

                ball.speed.X = 0;
                ball.speed.Y = 0;

                if (Keyboard.GetState().IsKeyDown(Keys.R))
                {
                   
                    bricks.Clear();
                    brick.position = new Vector2(0, 0);
                    for (int i = 0; i < 6; i++)
                    {
                        bricks.Add(new Brick(brick.texture, brick.position, Color.White));


                        for (int j = 0; j < 12; j++)
                        {
                            brick.position.X += brick.texture.Width;
                            bricks.Add(new Brick(brick.texture, brick.position, Color.White));
                        }
                        brick.position.Y += brick.texture.Height;
                        brick.position.X = 0;
                    }
                    gameover = false;
                    lose = false;
                    win = false;
                    brickdeestroyed = 0;
                    lives = 3;
                }
                
            }

            if(brickdeestroyed == 78)
            {
                gameover = true;
                win = true;
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
            spriteBatch.Begin();

            // TODO: Add your drawing code here
            spriteBatch.Draw(ball.texture, ball.position, ball.tint);
            spriteBatch.Draw(paddle.texture, paddle.position, paddle.tint);
            for (int i = 0; i < bricks.Count; i++)
            {
                bricks[i].Draw(spriteBatch);
            }


            //spriteBatch.Draw(ball.hitbox);
            spriteBatch.DrawString(font1, "bricks left", new Vector2(600, 440), Color.White);
            spriteBatch.DrawString(font1, $"{78-brickdeestroyed}", new Vector2(750, 440), Color.White);

            spriteBatch.DrawString(font1, "lives:", new Vector2(5, 450), Color.White);
            spriteBatch.DrawString(font1, $"{lives}", new Vector2(70, 452), Color.White);

            //spriteBatch.Draw(pixel, paddle.hitbox, Color.Red);
            if(lose == true)
            {
                spriteBatch.DrawString(font1, "you lose", new Vector2(GraphicsDevice.Viewport.Width/2, GraphicsDevice.Viewport.Height/2), Color.White);
                spriteBatch.DrawString(font1, "Press R to restart", new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2+30), Color.White);

            }
            if (win == true)
            {
                spriteBatch.DrawString(font1, "you won!", new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), Color.White);
                spriteBatch.DrawString(font1, "Press R to restart", new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2 + 30), Color.White);

            }


            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

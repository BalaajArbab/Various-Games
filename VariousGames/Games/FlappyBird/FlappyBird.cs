using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;

namespace VariousGames.Games
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    /// 
    

    public class FlappyBird : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        const int screenWidth = 800;
        const int screenHeight = 500;
        

        Texture2D line;
        Texture2D bird;
        Texture2D background;
        Texture2D reddot;
        Texture2D pipeBot;
        Texture2D pipeTop;
        Texture2D andromeda;
        Texture2D YouDied;

        SpriteFont font;

        AnimatedSprite galaxy;

        static int bckCounter = 0;
        static int galaxyCounter = 0;

        Vector2[] positions = new Vector2[2];
        static float bckX = 0f;
        static float bckX2 = 1600f;
        static float bckSpeed = 2f;
        static int score = 0;
        static int hiScore = 0;

        Bird ting;
        Pipe pipe;

        StreamWriter writer;
        StreamReader reader;
            
        public FlappyBird()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            positions[0] = new Vector2(bckX, 0);
            positions[1] = new Vector2(bckX2, 0);
            IsMouseVisible = true;
            
        }

        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            graphics.ApplyChanges();
            Window.Title = "FlappyThonk";

            try
            {
                reader = new StreamReader("highscore.txt");
            }
            catch (Exception e)
            {
                StreamWriter writer = new StreamWriter("highscore.txt");
                writer.WriteLine("0");
                writer.Close();
                reader = new StreamReader("highscore.txt");
            }
            hiScore = int.Parse(reader.ReadLine());
            reader.Close();

            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);            
            line = Content.Load<Texture2D>("line");
            bird = Content.Load<Texture2D>("bird");
            background = Content.Load<Texture2D>("background");
            reddot = Content.Load<Texture2D>("reddot");
            pipeBot = Content.Load<Texture2D>("pipeBot");
            pipeTop = Content.Load<Texture2D>("pipeTop");
            andromeda = Content.Load<Texture2D>("andromeda");           
            YouDied = Content.Load<Texture2D>("YOUDIED");


            font = Content.Load<SpriteFont>("font");


            galaxy = new AnimatedSprite(andromeda, 2, 1);


            ting = new Bird(bird, screenWidth / 4, screenHeight / 3);
            pipe = new Pipe(bckSpeed, pipeTop, pipeBot);
                       
        }

        
        protected override void UnloadContent()
        {
            
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {                
                Exit();
            }

            ting.move();

            pipe.initializePipeY();
            pipe.pipePlacer();
            pipe.collisions(ting);
            pipe.scoreCounter(ref score, ting);

            hiScoreUpdate();
            
           

            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);            
            spriteBatch.Begin();

            if (ting.birdState == BirdState.Alive)
            {
                drawBackground();
                drawGalaxy();

                pipe.pipeDraw(spriteBatch);
                ting.draw(spriteBatch);

                spriteBatch.DrawString(font, "" + score, new Vector2(screenWidth / 2 - 18, 30), Color.Yellow);
                spriteBatch.DrawString(font, "High Score: " + hiScore, new Vector2(20, screenHeight - 50), Color.AliceBlue);
            }
            gameOver();
            

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void drawBackground()
        {
            positions[0] = new Vector2(bckX, 0);
            positions[1] = new Vector2(bckX2, 0);

            if (bckCounter >= 120)
            {
                spriteBatch.Draw(reddot, positions[0], Color.White);
                spriteBatch.Draw(reddot, positions[1], Color.White);
                if (bckCounter == 126) bckCounter = 0;
            } else
            {
                spriteBatch.Draw(background, positions[0], Color.White);
                spriteBatch.Draw(background, positions[1], Color.White);
            }

            bckCounter++;
            bckX -= 1;
            bckX2 -= 1;
             if(bckX <= -1598)
            {
                bckX = 1600;
            }
             if(bckX2 <= -1598)
            {
                bckX2 = 1600;
            }

            

        }

        public void drawGalaxy()
        {            
            galaxy.Draw(spriteBatch, new Vector2(bckX + 1018, 120), scaling:5);
            galaxyCounter++;
            if (galaxyCounter == 5)
            {
                galaxy.Update();
                galaxyCounter = 0;
            }
        }

        public void gameOver()
        {
            if (ting.birdState == BirdState.Dead)
            {
                GraphicsDevice.Clear(Color.Black);
                ting.draw(spriteBatch);
                spriteBatch.Draw(YouDied, new Rectangle(screenWidth / 2, screenHeight / 2, 250, 150), null, Color.White, 0, new Vector2(YouDied.Width / 2, YouDied.Height / 2), SpriteEffects.None, 1);
                spriteBatch.DrawString(font, "High Score: " + hiScore, new Vector2(20, screenHeight - 50), Color.AliceBlue);
                spriteBatch.DrawString(font, "" + score, new Vector2(screenWidth / 2 - 18, 30), Color.Yellow);
            }
        }

        public void hiScoreUpdate()
        {            
            if (score > hiScore)
            {
                writer = new StreamWriter("highscore.txt");
                writer.WriteLine("" + score);
                hiScore = score;
                writer.Close();
            }

        }

        public static void tong()
        {

        }

       
    }

    
    
}

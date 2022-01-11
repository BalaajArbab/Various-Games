using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VariousGames.Games
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Pong : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D line;
        SpriteFont font;

        const int MAX_HEIGHT = 480;
        const int MAX_WIDTH = 800;
        const int LINE_THICKNESS = 12;

        int boardTop = 0 + LINE_THICKNESS;
        int boardBot = MAX_HEIGHT - LINE_THICKNESS;

        const int paddleHeight = 60;

        Color boardColor = Color.White;

        KeyboardState keyboardState = new KeyboardState();

        Paddle paddle1 = new Paddle(LINE_THICKNESS, paddleHeight, LINE_THICKNESS, (MAX_HEIGHT - paddleHeight) / 2, 5);
        Paddle paddle2 = new Paddle(LINE_THICKNESS, paddleHeight, MAX_WIDTH - (2 * LINE_THICKNESS), (MAX_HEIGHT - paddleHeight) / 2, 2);

        Ball ball = new Ball((MAX_WIDTH - Ball.ballWidth) / 2, MAX_HEIGHT / 4);

        int scoreP = 0;
        int scoreCPU = 0;


        public Pong()
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
            Window.Title = "Pong";

            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            line = Content.Load<Texture2D>("line");

            font = Content.Load<SpriteFont>("font");

            
        }

        
        protected override void UnloadContent()
        {
            
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            keyboardState = Keyboard.GetState();

            paddle1.paddleMove(keyboardState, boardBot, boardTop);
            paddle2.AIMove(ball, boardTop, boardBot);


            if (ball.ballPosition().Intersects(paddle1.paddlePosition()))
            {
                Ball.ballSpeedX *= -1;
                
            }
            else if (ball.ballPosition().Intersects(paddle2.paddlePosition()))
            {
                Ball.ballSpeedX *= -1;                
            }

            if (ball.ballY <= (boardTop))
            {
                Ball.ballSpeedY *= -1;
            }
            else if (ball.ballY >= (boardBot - Ball.ballHeight))
            {
                Ball.ballSpeedY *= -1;
            }

            ball.ballX += Ball.ballSpeedX;
            ball.ballY += Ball.ballSpeedY;

            score(ball);
            
            
            
            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            spriteBatch.Draw(line, new Rectangle(0, 0, MAX_WIDTH, LINE_THICKNESS), boardColor);
            spriteBatch.Draw(line, new Rectangle(0, 0, LINE_THICKNESS, MAX_HEIGHT), boardColor);
            spriteBatch.Draw(line, new Rectangle(0, MAX_HEIGHT - LINE_THICKNESS, MAX_WIDTH, LINE_THICKNESS), boardColor);
            spriteBatch.Draw(line, new Rectangle(MAX_WIDTH - LINE_THICKNESS, 0, LINE_THICKNESS, MAX_HEIGHT), boardColor);
            spriteBatch.Draw(line, new Rectangle((MAX_WIDTH - LINE_THICKNESS) / 2, 0, LINE_THICKNESS, MAX_HEIGHT), boardColor);

            spriteBatch.Draw(line, paddle1.paddlePosition(), Color.Magenta);
            spriteBatch.Draw(line, paddle2.paddlePosition(), Color.Gold);
            spriteBatch.Draw(line, ball.ballPosition(), Color.RoyalBlue);

            spriteBatch.DrawString(font, "" + scoreP, new Vector2((MAX_WIDTH / 2) - 40, 20), Color.Yellow);
            spriteBatch.DrawString(font, "" + scoreCPU, new Vector2((MAX_WIDTH / 2) + 24, 20), Color.Yellow);



            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void score(Ball ball)
        {
            
            if (ball.ballX < 2)
            {
                scoreCPU++;
                System.Threading.Thread.Sleep(200);
                ball.ballX = (MAX_WIDTH + Ball.ballWidth + 15) / 2;
                ball.ballY = MAX_HEIGHT / 4;
                Ball.ballSpeedX = -6;
                Ball.ballSpeedY = 2;

            }
            else if (ball.ballX > MAX_WIDTH - LINE_THICKNESS + (LINE_THICKNESS - 2))
            {
                scoreP++;
                System.Threading.Thread.Sleep(200);
                ball.ballX = (MAX_WIDTH - Ball.ballWidth - 15) / 2;
                ball.ballY = MAX_HEIGHT / 4;
                Ball.ballSpeedX = 6;
                Ball.ballSpeedY = 2;

            }
            
        }
    }

    public class Paddle
    {
        int paddleSizeX;
        public int paddleSizeY;
        int paddleX;
        public int paddleY;       

        public int paddleSpeed = 2;

        public Rectangle paddleRect;

        public Paddle(int sizeX, int sizeY, int posX, int posY, int speed)
        {
            paddleSizeX = sizeX;
            paddleSizeY = sizeY;
            paddleX = posX;
            paddleY = posY;
            paddleSpeed = speed;
        }

        public void paddleMove(KeyboardState state, int boardBot, int boardTop)
        {
            if (state.IsKeyDown(Keys.Down) && paddleY < boardBot - paddleSizeY)
            {
                paddleY += paddleSpeed;
            } 
            else if (state.IsKeyDown(Keys.Up) && paddleY > boardTop)
            {
                paddleY -= paddleSpeed;
            }
        }

        public Rectangle paddlePosition()
        {
            return new Rectangle(paddleX, paddleY, paddleSizeX, paddleSizeY);
        }
        

        public void AIMove(Ball ball, int boardTop, int boardBot)
        {
            if (paddleY > ball.ballY && (paddleY > boardTop))
            {
                paddleY -= paddleSpeed;
            }
            else if (paddleY < ball.ballY && (paddleY < (boardBot - paddleSizeY)))
            {
                paddleY += paddleSpeed;
            }
        }

    }

    public class Ball
    {
        public static int ballWidth = 15;
        public static int ballHeight = 15;
        public static int ballSpeedX = -6;
        public static int ballSpeedY = 2;

        public int ballX;
        public int ballY;

        public Ball(int startX, int startY)
        {
            ballX = startX;
            ballY = startY;
        }


        public Rectangle ballPosition()
        {
            return new Rectangle(ballX, ballY, ballWidth, ballHeight);
        }


    }
}

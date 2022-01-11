using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VariousGames.Games
{
    
    public class SnakeGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        const int SCREEN_WIDTH = 800;
        const int SCREEN_HEIGHT = 800;

        public const int BLOCK_SIZE = 20;
        
        Texture2D line;

        SpriteFont font;

        Snake ting;

        Veggie veggie;

        bool justStarted = true;

        public SnakeGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        
        protected override void Initialize()
        {
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.ApplyChanges();

            Window.Title = "ThonkSnake";

            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);

            line = Content.Load<Texture2D>("line");

            font = Content.Load<SpriteFont>("font");


            ting = new Snake(line);
            veggie = new Veggie(line);

        }

        
        protected override void UnloadContent()
        {
            
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (justStarted)
            {
                veggie.placement();
                justStarted = false;

            }

            if (ting.snakeState == SnakeState.Alive)
            {

                ting.checkInput();
                ting.move();
                ting.eat(veggie);
                ting.checkCollisions();

            }

            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            
            ting.drawSnake(spriteBatch);
            veggie.drawVeggie(spriteBatch);

            drawBorder();

            gameOver();

            spriteBatch.DrawString(font, "SnakeSize: " + (ting.snakeSize + 1), new Vector2(0, 0), Color.MonoGameOrange);

            spriteBatch.End();
            base.Draw(gameTime);

        }

        public void drawBorder()
        {
            spriteBatch.Draw(line, new Rectangle(0, 0, SCREEN_WIDTH, BLOCK_SIZE), Color.White);
            spriteBatch.Draw(line, new Rectangle(0, SCREEN_HEIGHT - BLOCK_SIZE, SCREEN_WIDTH, BLOCK_SIZE), Color.White);
            spriteBatch.Draw(line, new Rectangle(0, 0, BLOCK_SIZE, SCREEN_HEIGHT), Color.White);
            spriteBatch.Draw(line, new Rectangle(SCREEN_WIDTH - BLOCK_SIZE, 0, BLOCK_SIZE, SCREEN_HEIGHT), Color.White);

        }

        public void gameOver()
        {
            if (ting.snakeState == SnakeState.Dead)
            {

                spriteBatch.Draw(line, new Rectangle(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT), new Color(100, 100, 100, 100));
                spriteBatch.DrawString(font, "YOU DIED", new Vector2(350, 250), Color.Red);

            }
        }
    }
}

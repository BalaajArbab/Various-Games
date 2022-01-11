using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VariousGames.Games
{
    
    public class TicTacToe : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont font;
        SpriteFont tong;

        Texture2D line;
        Texture2D circle;
        Texture2D cross;
        Texture2D border;

        Logic ting;

        string[] board = new string[10];

        int squareWidth = 600;
        int squareHeight = 600;

        bool win = false;
        

        public TicTacToe()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        
        protected override void Initialize()
        {
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1200;
            graphics.ApplyChanges();
            Window.Title = "Rathalos Tic Tac Toe";
            

            base.Initialize();
        }

        
        protected override void LoadContent()
        {
            
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("font");
            tong = Content.Load<SpriteFont>("tong");

            line = Content.Load<Texture2D>("line");
            cross = Content.Load<Texture2D>("cross");
            circle = Content.Load<Texture2D>("circle");
            border = Content.Load<Texture2D>("border");

            ting = new Logic(board, cross, circle, line, spriteBatch, border);
        }

       
        protected override void UnloadContent()
        {
            
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (win == false)
            {
                ting.move();
            }

            if (ting.winCheck())
            {
                win = true;
                if (ting.playAgain())
                {
                    ting.turnCounter = 1;
                    win = false;
                }
                
            }

            base.Update(gameTime);
        }

        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            drawBoard();

            ting.drawPieces();

            spriteBatch.DrawString(tong, "Tic \nTac \nToe \nKnockoff", new Vector2(10, 10), Color.White);

            if (win == false)
            {
                spriteBatch.DrawString(font, "Player " + ting.whoseTurn() + "'s turn", new Vector2(420, 10), Color.White);
                ting.drawMove();
            }

            if (win == true)
            {
                spriteBatch.DrawString(font, "Player " + ting.whoseTurn() + " won. Press Enter to play again", new Vector2(80, 720), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void drawBoard()
        {

            spriteBatch.Draw(line, new Rectangle(300, 100 + squareHeight/3, squareWidth, 20), Color.White);
            spriteBatch.Draw(line, new Rectangle(300, 100 + (2 * squareHeight / 3), squareWidth, 20), Color.White);
            spriteBatch.Draw(line, new Rectangle(300 + squareWidth / 3, 100, 20, squareHeight), Color.White);
            spriteBatch.Draw(line, new Rectangle(300 + (2 * squareWidth / 3), 100, 20, squareHeight), Color.White);

        }

        
    }
}

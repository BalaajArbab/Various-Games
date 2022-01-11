using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VariousGames.Games
{

    public class Tetris : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        const int SCREEN_WIDTH = 320;
        const int SCREEN_HEIGHT = 640;

        Texture2D block;
        Texture2D line;
        Texture2D sword;
        Texture2D galaxy;

        SpriteFont font;

        Board board;

        int stepTime = 300;
        int elapsedTime = 0;
        int keyboardElapsedTime = 0;
        double elapsedTime2 = 0;
        int milli = 0;


        bool start = true;

        GameState gameState;

        KeyboardState keyboardState;

        public Tetris()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }


        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = 480;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            graphics.ApplyChanges();
            Window.Title = "Tetorisu";
            IsMouseVisible = true;

            base.Initialize();
        }


        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);

            block = Content.Load<Texture2D>("block");
            line = Content.Load<Texture2D>("line");
            sword = Content.Load<Texture2D>("SwordOfRupture");
            galaxy = Content.Load<Texture2D>("galaxy");

            font = Content.Load<SpriteFont>("font");

            board = new Board(block, galaxy);
        }


        protected override void UnloadContent()
        {

        }


        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
            milli += gameTime.ElapsedGameTime.Milliseconds;
            keyboardElapsedTime += gameTime.ElapsedGameTime.Milliseconds;
            elapsedTime2 = milli / 1000.0;



            if (start)
            {

                board.spawn();
                start = false;

            }

            if (gameState == GameState.Alive)
            {

                if (elapsedTime > stepTime)
                {

                    board.currentY++;

                    PlaceStates ps = board.canBePlaced(board.currentX, board.currentY);

                    if (ps != PlaceStates.CAN_PLACE)
                    {

                        board.Place();
                        board.spawn();

                        ps = board.canBePlaced(board.currentX, board.currentY);

                        if (ps == PlaceStates.BLOCKED)
                        {
                            gameState = GameState.Dead;
                        }
                    }


                    elapsedTime = 0;
                }

                keyboardState = Keyboard.GetState();


                if (keyboardElapsedTime > 150)
                {

                    if (keyboardState.IsKeyDown(Keys.Up))
                    {
                        int[,] p = board.rotate();

                        if (board.canBePlaced(p) == PlaceStates.CAN_PLACE)
                        {
                            board.currentPiece = p;
                        }

                        keyboardElapsedTime = 0;
                    }

                    PlaceStates ps = board.canBePlaced(board.currentX - 1, board.currentY);

                    if (ps == PlaceStates.CAN_PLACE)
                    {

                        if (keyboardState.IsKeyDown(Keys.Left))
                        {

                            board.currentX -= 1;

                        }
                        keyboardElapsedTime = 50;
                    }

                    ps = board.canBePlaced(board.currentX + 1, board.currentY);

                    if (ps == PlaceStates.CAN_PLACE)
                    {

                        if (keyboardState.IsKeyDown(Keys.Right))
                        {

                            board.currentX += 1;

                        }
                        keyboardElapsedTime = 50;
                    }


                    if (keyboardState.IsKeyDown(Keys.Down))
                    {
                        elapsedTime = stepTime + 1;
                        keyboardElapsedTime = 100;
                    }

                    if (keyboardState.IsKeyDown(Keys.Space))
                    {

                        for (int y = 19; y > 0; y--)
                        {
                            if (board.canBePlaced(board.currentX, board.currentY + y) == PlaceStates.CAN_PLACE)
                            {
                                board.currentY = board.currentY + y + 1;
                                board.Place();

                            }
                        }

                        keyboardElapsedTime = 50;
                    }

                }

            }
            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();


            board.drawBoard(spriteBatch);

            if (gameState == GameState.Dead)
            {
                spriteBatch.Draw(line, new Rectangle(0, 0, 320, 640), new Color(100, 100, 100, 100));
                spriteBatch.DrawString(font, "DEAD!!!!", new Vector2(330, 100), Color.Gold);
            }

            spriteBatch.DrawString(font, "Score: " + board.score, new Vector2(330, 50), Color.Gold);
            spriteBatch.DrawString(font, "TETORISU", new Vector2(330, 150), Color.Gold);
            spriteBatch.Draw(line, new Rectangle(320, 0, 5, 214), Color.Red);
            spriteBatch.Draw(line, new Rectangle(320, 214, 5, 428), Color.Gold);
            spriteBatch.Draw(line, new Rectangle(320, 428, 5, 640), Color.Blue);
            spriteBatch.Draw(sword, new Vector2(375, 290), Color.White);
            spriteBatch.DrawString(font, string.Format("{0:0.00}", elapsedTime2), new Vector2(330, 600), Color.Gold);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }

    public enum GameState
    {
        Alive,
        Dead
    }
}

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VariousGames
{
    public class Menu : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        const int SCREEN_WIDTH = 1500;
        const int SCREEN_HEIGHT = 600;

        Texture2D _tetris;
        Texture2D _flappyBird;
        Texture2D _pong;
        Texture2D _snake;
        Texture2D _ticTacToe;

        Texture2D _line;

        SpriteFont _font;

        public static GameSelected GameSelected { get; private set; }

        public Menu()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            _graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
            _graphics.ApplyChanges();
            Window.Title = "Menu";
            IsMouseVisible = true;

            base.Initialize();

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _tetris = Content.Load<Texture2D>("tetris");
            _flappyBird = Content.Load<Texture2D>("flappybird");
            _pong = Content.Load<Texture2D>("pong");
            _snake = Content.Load<Texture2D>("snake");
            _ticTacToe = Content.Load<Texture2D>("tictactoe");

            _line = Content.Load<Texture2D>("line");
            
            _font = Content.Load<SpriteFont>("font");
        }

        MouseState _mouse;
        GameSelected _gameSelected;

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _mouse = Mouse.GetState();


            if (_mouse.Position.X <= 299)
            {
                if (_gameSelected != GameSelected.Tetris) _gameSelected = GameSelected.Tetris;

                if (_mouse.LeftButton == ButtonState.Pressed && _mouse.Position.Y >= 0 && _mouse.Position.Y <= 599)
                {
                    GameSelected = GameSelected.Tetris;

                    Exit();
                }
            }
            else if (_mouse.Position.X <= 599)
            {
                if (_gameSelected != GameSelected.FlappyBird) _gameSelected = GameSelected.FlappyBird;

                if (_mouse.LeftButton == ButtonState.Pressed && _mouse.Position.Y >= 0 && _mouse.Position.Y <= 599)
                {
                    GameSelected = GameSelected.FlappyBird;

                    Exit();
                }
            }
            else if (_mouse.Position.X <= 899)
            {
                if (_gameSelected != GameSelected.Pong) _gameSelected = GameSelected.Pong;

                if (_mouse.LeftButton == ButtonState.Pressed && _mouse.Position.Y >= 0 && _mouse.Position.Y <= 599)
                {
                    GameSelected = GameSelected.Pong;

                    Exit();
                }
            }
            else if (_mouse.Position.X <= 1199)
            {
                if (_gameSelected != GameSelected.Snake) _gameSelected = GameSelected.Snake;

                if (_mouse.LeftButton == ButtonState.Pressed && _mouse.Position.Y >= 0 && _mouse.Position.Y <= 599)
                {
                    GameSelected = GameSelected.Snake;

                    Exit();
                }
            }
            else if (_mouse.Position.X <= 1499)
            {
                if (_gameSelected != GameSelected.TicTacToe) _gameSelected = GameSelected.TicTacToe;

                if (_mouse.LeftButton == ButtonState.Pressed && _mouse.Position.Y >= 0 && _mouse.Position.Y <= 599)
                {
                    GameSelected = GameSelected.TicTacToe;

                    Exit();
                }
            }

            else _gameSelected = GameSelected.None;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();

            _spriteBatch.Draw(_tetris, new Rectangle(0, 0, 300, 600), Color.White);
            if (_gameSelected != GameSelected.Tetris) _spriteBatch.Draw(_line, new Rectangle(0, 0, 300, 600), new Color(100, 100, 100, 100));

            _spriteBatch.Draw(_flappyBird, new Rectangle(300, 0, 300, 600), Color.White);
            if (_gameSelected != GameSelected.FlappyBird) _spriteBatch.Draw(_line, new Rectangle(300, 0, 300, 600), new Color(100, 100, 100, 100));

            _spriteBatch.Draw(_pong, new Rectangle(600, 0, 300, 600), Color.White);
            if (_gameSelected != GameSelected.Pong) _spriteBatch.Draw(_line, new Rectangle(600, 0, 300, 600), new Color(100, 100, 100, 100));

            _spriteBatch.Draw(_snake, new Rectangle(900, 0, 300, 600), Color.White);
            if (_gameSelected != GameSelected.Snake) _spriteBatch.Draw(_line, new Rectangle(900, 0, 300, 600), new Color(100, 100, 100, 100));

            _spriteBatch.Draw(_ticTacToe, new Rectangle(1200, 0, 300, 600), Color.White);
            if (_gameSelected != GameSelected.TicTacToe) _spriteBatch.Draw(_line, new Rectangle(1200, 0, 300, 600), new Color(100, 100, 100, 100));


            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }

    public enum GameSelected
    {
        None,
        Tetris,
        FlappyBird,
        Pong,
        Snake,
        TicTacToe
    }
}

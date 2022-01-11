using System;
using VariousGames.Games;

namespace VariousGames
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new Menu())
                game.Run();

            GameSelected gameType = Menu.GameSelected; 

            switch (gameType)
            {
                case GameSelected.Tetris:
                    new Tetris().Run();
                    break;
                case GameSelected.FlappyBird:
                    new FlappyBird().Run();
                    break;
                case GameSelected.Pong:
                    new Pong().Run();
                    break;
                case GameSelected.Snake:
                    new SnakeGame().Run();
                    break;
                case GameSelected.TicTacToe:
                    new TicTacToe().Run();
                    break;

            }
            
        }
    }
}

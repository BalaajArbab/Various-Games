using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VariousGames.Games
{
    class Logic
    {
        string[] board;
        string[] players = new string[3]
        {
            "", "X", "O"
        };

        int boardSize = 3;
        public int turnCounter;
        int startingPosition = 5;
        int currentPosition;
        int previousPosition;        

        KeyboardState keyboardState = new KeyboardState();
        KeyboardState oldState = new KeyboardState();

        Texture2D cross;
        Texture2D circle;
        Texture2D line;
        Texture2D border;

        SpriteBatch spriteBatch;

        int pieceSizeX = 150;
        int pieceSizeY = 150;

        int moveX;
        int moveY;

        int[] centerX = new int[10]
        {
            0, 350, 560, 760, 350, 560, 760, 350, 560, 760
        };

        int[] centerY = new int[10]
        {
            0, 150, 150, 150, 360, 360, 360, 560, 560, 560
        };

        public Logic(string[] Board, Texture2D Cross, Texture2D Circle, Texture2D Line, SpriteBatch SpriteBatch, Texture2D Border)
        {
            board = Board;
            turnCounter = 1;
            currentPosition = startingPosition;

            cross = Cross;
            circle = Circle;
            line = Line;
            spriteBatch = SpriteBatch;
            border = Border;
        }

        public void placePiece(int position)
        {
            board[position] = players[whoseTurn()];
        }

        public int whoseTurn()
        {
            if (turnCounter % 2 == 0)
            {
                return 2;
            }

            return 1;
        }

        public void move()
        {

            previousPosition = currentPosition;

            
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left) && oldState.IsKeyUp(Keys.Left) && !(currentPosition == 1 || currentPosition == 1 + boardSize || currentPosition == 1 + (2 * boardSize)))
            {
                currentPosition -= 1;
            }

            if (keyboardState.IsKeyDown(Keys.Right) && oldState.IsKeyUp(Keys.Right) && !(currentPosition == 3 || currentPosition == 3 + boardSize || currentPosition == 3 + (2 * boardSize)))
            {
                currentPosition += 1;
            }

            if (keyboardState.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up) && !(currentPosition == 1 || currentPosition == 2 || currentPosition == 3))
            {
                currentPosition -= boardSize;
            }

            if (keyboardState.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down) && !(currentPosition == 7 || currentPosition == 8 || currentPosition == 9))
            {
                currentPosition += boardSize;
            }

            if (keyboardState.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space))
            {
                placePiece(currentPosition);
                currentPosition = 5;
                if (!winCheck())
                {
                    turnCounter++;
                }
            }

            oldState = keyboardState;

        }

        public void drawPieces()
        {
            for(int i = 1; i < 10; i++)
            {
                if (board[i] == "O")
                {
                    spriteBatch.Draw(circle, new Rectangle(centerX[i], centerY[i], pieceSizeX, pieceSizeY), null, Color.White, 0, new Vector2(pieceSizeX / 2, pieceSizeY / 2), SpriteEffects.None, 1);
                        
                }

                if (board[i] == "X")
                {
                    spriteBatch.Draw(cross, new Rectangle(centerX[i] + 10, centerY[i] + 10, pieceSizeX, pieceSizeY), null, Color.White, 0, new Vector2(pieceSizeX / 2, pieceSizeY / 2), SpriteEffects.None, 1);

                }
                

            }

        }

        public void drawMove()
        {
            try
            {
                moveX = centerX[currentPosition];
                moveY = centerY[currentPosition];

                if (whoseTurn() == 1)
                {
                    spriteBatch.Draw(cross, new Rectangle(moveX + 10, moveY + 10, pieceSizeX, pieceSizeY), null, new Color(200, 200, 200, 100), 0, new Vector2(pieceSizeX / 2, pieceSizeY / 2), SpriteEffects.None, 1);
                    if (board[currentPosition] != null)
                    {
                        spriteBatch.Draw(border, new Rectangle(moveX + 50, moveY + 50, border.Width, border.Height), null, Color.Red, 0, new Vector2(border.Width / 2, border.Height / 2), SpriteEffects.None, 1);
                    }
                    if (board[currentPosition] == null)
                    {
                        spriteBatch.Draw(border, new Rectangle(moveX + 50, moveY + 50, border.Width, border.Height), null, Color.Green, 0, new Vector2(border.Width / 2, border.Height / 2), SpriteEffects.None, 1);
                    }
                }

                if (whoseTurn() == 2)
                {
                    spriteBatch.Draw(circle, new Rectangle(moveX, moveY, pieceSizeX, pieceSizeY), null, new Color(200, 200, 200, 100), 0, new Vector2(pieceSizeX / 2, pieceSizeY / 2), SpriteEffects.None, 1);
                    if (board[currentPosition] != null)
                    {
                        spriteBatch.Draw(border, new Rectangle(moveX + 50, moveY + 50, border.Width, border.Height), null, Color.Red, 0, new Vector2(border.Width / 2, border.Height / 2), SpriteEffects.None, 1);
                    }
                    if (board[currentPosition] == null)
                    {
                        spriteBatch.Draw(border, new Rectangle(moveX + 50, moveY + 50, border.Width, border.Height), null, Color.Green, 0, new Vector2(border.Width / 2, border.Height / 2), SpriteEffects.None, 1);
                    }
                }
            }
            catch
            {
                currentPosition = previousPosition;
            }
        }

        public bool lineChecker(int start, int step)
        {
            string piece = board[start];
            if (piece == null) return false;

            return (piece == board[start + step] && piece == board[start + (2 * step)]);
        }

        public bool winCheck()
        {
            return (lineChecker(1, 1) || lineChecker (4, 1) || lineChecker(7, 1)     //horizontal
                   || lineChecker(1, 3) || lineChecker(2, 3) || lineChecker(3, 3)    //vertical
                   || lineChecker(1, 4) || lineChecker(3, 2));                       //diagonal
        }

        public void clearBoard()
        {
            board = new string[10];
        }

        public bool playAgain()
        {
            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Enter))
            {
                clearBoard();
                return true;
            }

            return false;
        }


    }
}

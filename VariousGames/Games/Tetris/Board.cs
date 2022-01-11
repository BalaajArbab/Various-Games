using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VariousGames.Games
{

    public enum PlaceStates
    {
        CAN_PLACE,
        BLOCKED,
        OFFSCREEN
    }


    class Board
    {



        private int[,] gameBoard = new int[20, 10]
        {
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}

        };



        Color[] colors = new Color[]
        {
            Color.FromNonPremultiplied(50, 50, 50, 50),
            Color.Cyan,
            Color.Yellow,
            Color.FromNonPremultiplied(190, 0, 190, 255),
            Color.Red,
            Color.Green,
            Color.MonoGameOrange,
            Color.Blue,

        };



        Color[] shadowColors = new Color[]
            {
                Color.FromNonPremultiplied(50, 50, 50, 50),
                Color.FromNonPremultiplied(0, 255, 255, 50),
                Color.FromNonPremultiplied(255, 255, 0, 50),
                Color.FromNonPremultiplied(190, 0, 190, 50),
                Color.FromNonPremultiplied(255, 0, 0, 50),
                Color.FromNonPremultiplied(0, 128, 0, 50),
                Color.FromNonPremultiplied(231, 60, 0, 50),
                Color.FromNonPremultiplied(0, 0, 255, 50),

            };



        Texture2D block;
        Texture2D galaxy;
        const int BLOCK_SIZE = 32;

        public int[,] currentPiece;
        public int currentX;
        public int currentY;

        Random random = new Random();

        List<int[,]> pieces = new List<int[,]>();

        int n;

        public int score = 0;

        public Board(Texture2D Block, Texture2D Galaxy)
        {
            block = Block;
            galaxy = Galaxy;

            pieces.Add(new int[0, 0]);




            pieces.Add(new int[4, 4]
                {
              {1, 0, 0, 0},
              {1, 0, 0, 0},
              {1, 0, 0, 0},
              {1, 0, 0, 0}
                });

            pieces.Add(new int[2, 2]
            {
                {1, 1},
                {1, 1}
            });

            pieces.Add(new int[3, 3]
            {
                {0, 1, 0},
                {1, 1, 1},
                {0, 0, 0}
            });

            pieces.Add(new int[3, 3]
            {
                {1, 1, 0},
                {0, 1, 1},
                {0, 0, 0}
            });

            pieces.Add(new int[3, 3]
            {
                {0, 1, 1},
                {1, 1, 0},
                {0, 0, 0}
            });

            pieces.Add(new int[3, 3]
            {
                {0, 0, 1},
                {1, 1, 1},
                {0, 0, 0}
            });

            pieces.Add(new int[3, 3]
            {
                {1, 0, 0},
                {1, 1, 1},
                {0, 0, 0}
            });
        }

        public void drawBoard(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < gameBoard.GetLength(0); y++)
            {
                for (int x = 0; x < gameBoard.GetLength(1); x++)
                {
                    spriteBatch.Draw(block, new Vector2(x * BLOCK_SIZE, y * BLOCK_SIZE), colors[gameBoard[y, x]]);

                }
            }

            spriteBatch.Draw(galaxy, new Vector2(160, 180), new Rectangle(0, 0, 128, 128), Color.White, 0f, new Vector2(64, 64), 1f, SpriteEffects.None, 1);

            for (int yc = 19; yc > 0; yc--)
            {
                if (canBePlaced(currentX, currentY + yc) == PlaceStates.CAN_PLACE)
                {
                    for (int y = 0; y < currentPiece.GetLength(0); y++)
                    {
                        for (int x = 0; x < currentPiece.GetLength(1); x++)
                        {

                            int coordX = currentX + x;
                            int coordY = currentY + yc + y;

                            if (currentPiece[y, x] != 0)
                            {
                                spriteBatch.Draw(block, new Vector2(coordX * 32, coordY * 32), shadowColors[n]);

                            }
                        }

                    }

                    break;

                }

                for (int y = 0; y < currentPiece.GetLength(0); y++)
                {
                    for (int x = 0; x < currentPiece.GetLength(1); x++)
                    {

                        int coordX = currentX + x;
                        int coordY = currentY + y;

                        if (currentPiece[y, x] != 0)
                        {
                            spriteBatch.Draw(block, new Vector2(coordX * 32, coordY * 32), colors[n]);

                        }
                    }

                }


            }

        }

        public void spawn()
        {
            n = random.Next(1, 8);
            currentPiece = (int[,])pieces[n].Clone();

            for (int y = 0; y < currentPiece.GetLength(0); y++)
            {
                for (int x = 0; x < currentPiece.GetLength(1); x++)
                {
                    currentPiece[y, x] *= n;
                }
            }

            currentX = 4;
            currentY = 0;

        }

        public PlaceStates canBePlaced(int xx, int yy)
        {
            for (int y = 0; y < currentPiece.GetLength(0); y++)
            {
                for (int x = 0; x < currentPiece.GetLength(1); x++)
                {

                    int coordX = xx + x;
                    int coordY = yy + y;

                    if (currentPiece[y, x] != 0)
                    {
                        if (coordX < 0 || coordX > 9)
                        {
                            return PlaceStates.OFFSCREEN;
                        }

                        if (coordY >= 20 || gameBoard[coordY, coordX] != 0)
                        {
                            return PlaceStates.BLOCKED;
                        }
                    }
                }
            }

            return PlaceStates.CAN_PLACE;

        }

        public void RemoveCompletedLines()
        {

            int scoreMultiplier = 0;

            for (int y = 19; y >= 0; y--)
            {

                bool isComplete = true;

                for (int x = 0; x < gameBoard.GetLength(1); x++)
                {
                    if (this.gameBoard[y, x] == 0)
                    {
                        isComplete = false;
                    }
                }

                if (isComplete)
                {
                    for (int yc = y; yc > 0; yc--)
                    {

                        for (int x = 0; x < gameBoard.GetLength(1); x++)
                        {
                            gameBoard[yc, x] = gameBoard[yc - 1, x];

                        }

                    }

                    scoreMultiplier += 1;

                    y++;
                }

            }

            score += 100 * (int)Math.Pow(scoreMultiplier, 2) * 2;

        }

        public void Place()
        {

            currentY--;
            for (int y = 0; y < currentPiece.GetLength(0); y++)
            {
                for (int x = 0; x < currentPiece.GetLength(1); x++)
                {

                    int coordX = currentX + x;
                    int coordY = currentY + y;

                    if (currentPiece[y, x] != 0)
                    {
                        this.gameBoard[coordY, coordX] = currentPiece[y, x];
                    }

                }
            }

            RemoveCompletedLines();
        }

        public int[,] rotate()
        {
            int dimensions = currentPiece.GetLength(0);

            int[,] placeholderPiece = new int[dimensions, dimensions];

            for (int x = 0; x < dimensions; x++)
            {
                for (int y = 0; y < dimensions; y++)
                {
                    placeholderPiece[y, x] = currentPiece[x, dimensions - 1 - y];
                }
            }

            return placeholderPiece;
        }

        public PlaceStates canBePlaced(int[,] block)
        {
            for (int y = 0; y < block.GetLength(0); y++)
            {
                for (int x = 0; x < block.GetLength(1); x++)
                {

                    int coordX = currentX + x;
                    int coordY = currentY + y;

                    if (block[y, x] != 0)
                    {
                        if (coordX < 0 || coordX > 9)
                        {
                            return PlaceStates.OFFSCREEN;
                        }

                        if (coordY >= 20)
                        {
                            return PlaceStates.BLOCKED;
                        }

                        if (gameBoard[coordY, coordX] != 0)
                        {
                            return PlaceStates.BLOCKED;
                        }
                    }
                }
            }

            return PlaceStates.CAN_PLACE;
        }

    }
}

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VariousGames.Games
{
    public class SnakeBlock
    {
        Rectangle position;

        public int posX;
        public int posY;

        public Rectangle Position
        {
            get
            {
                return this.position;
            }

            set
            {
                this.position = new Rectangle(posX, posY, SnakeGame.BLOCK_SIZE, SnakeGame.BLOCK_SIZE);
            }

        }

        public SnakeBlock(int PosX, int PosY)
        {
            position = new Rectangle(PosX, PosY, SnakeGame.BLOCK_SIZE, SnakeGame.BLOCK_SIZE);
            posX = PosX;
            posY = PosY;

        }
    }


}

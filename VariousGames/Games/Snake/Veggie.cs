using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VariousGames.Games
{
    public class Veggie
    {
        Random random = new Random();

        Texture2D line;


        int posX;
        int posY;

        public Rectangle position;

        public Veggie(Texture2D texture)
        {
            line = texture;
        }

        public void placement()
        {
            posX = random.Next(1, 39) * 20;
            posY = random.Next(1, 39) * 20;

            position = new Rectangle(posX, posY, SnakeGame.BLOCK_SIZE, SnakeGame.BLOCK_SIZE);
        }

        public void drawVeggie(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(line, position, Color.Green);
        }
    }
}

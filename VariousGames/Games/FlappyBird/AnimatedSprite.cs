using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace VariousGames.Games
{
    public class AnimatedSprite
    {
        public Texture2D Texture;
        public int Rows;
        public int Columns;
        private int currentFrame;
        private int totalFrames;

        public AnimatedSprite(Texture2D texture, int rows, int columns)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
        }

        public void Update()
        {
            currentFrame++;
            if (currentFrame == totalFrames)
            {
                currentFrame = 0;
                               
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, int scaling = 1, float angle = 0)
        {
            int height = Texture.Height / Rows;
            int width = Texture.Width / Columns;

            int row = (int)((float)currentFrame / (float)Columns);
            int column = (currentFrame % Columns);

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle ((int)position.X, (int)position.Y, width * scaling, height * scaling);


            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White, angle, new Vector2(width / 2, height / 2), SpriteEffects.None, 1);
            

        }

    }
}

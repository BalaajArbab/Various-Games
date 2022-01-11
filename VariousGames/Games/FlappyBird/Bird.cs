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

    enum BirdState
    {
        Alive, 
        Dead
    }

    class Bird : Game
    {
        public Rectangle Position;
        AnimatedSprite animatedSprite;

        public float positionX;
        float positionY;

        float gravity = 2f;
        float jumpPowa = 50f;

        KeyboardState keyboardState;
        KeyboardState oldState;

        int counter = 0;
        float angle = -0.6f;

        public BirdState birdState;

        public Bird(Texture2D texture, int posX, int posY)
        {
            animatedSprite = new AnimatedSprite(texture, 2, 1);
            positionX = posX;
            positionY = posY;

        }

        public void move()
        {
            positionY += gravity;

            keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Space) && oldState.IsKeyUp(Keys.Space))
            {
                positionY -= jumpPowa;
                if (positionY <= 10) positionY = 10;
                angle = -0.6f;
            } else if (!(angle >= 3.05 / 2))
            {
                angle += 0.01f;
            }

            oldState = keyboardState;

            if (positionY >= 460) birdState = BirdState.Dead;
            if (birdState == BirdState.Dead) positionY = 50;
            

            Position = new Rectangle((int)positionX, (int)positionY, 52, 52);
          
        }

        public void draw(SpriteBatch spriteBatch)
        {
            animatedSprite.Draw(spriteBatch, new Vector2(positionX + 26, positionY + 26), scaling:2, angle:angle);
            counter++;

            if (counter == 6)
            {
                counter = 0;
                animatedSprite.Update();
            }
            
        }

    }
}

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VariousGames.Games
{


    class Snake
    {
        Texture2D line;

        const int speed = 20;

        int speedX = 0;
        int speedY = -speed;

        int posX = 400;
        int posY = 400;

        int counter = 0;

        Direction direction = Direction.Up;
        public SnakeState snakeState = SnakeState.Alive;
        

        SnakeBlock temp;
        SnakeBlock temp2;

        public int snakeSize = 0;
        int lastSize = 0;

        SnakeBlock[] snakeBlocks = new SnakeBlock[500];

        KeyboardState keyboardState;

        int number = 1;

        public Snake(Texture2D texture)
        {
            line = texture;
                      
            snakeBlocks[0] = new SnakeBlock(posX, posY);
            

        }

        public void move()
        {

            temp = new SnakeBlock(snakeBlocks[0].posX, snakeBlocks[0].posY);

            if (counter == 10)
            {
                

                switch (direction)
                {
                    case Direction.Up:
                        speedY = -speed;
                        speedX = 0;
                        number = 0;
                        break;

                    case Direction.Down:
                        speedY = speed;
                        speedX = 0;
                        number = 0;
                        break;

                    case Direction.Right:
                        speedX = speed;
                        speedY = 0;
                        number = 0;
                        break;

                    case Direction.Left:
                        speedX = -speed;
                        speedY = 0;
                        number = 0;
                        break;
                }

                              
                posX += speedX;
                posY += speedY;
                snakeBlocks[0] = new SnakeBlock(posX, posY);

                number++;

                for (int i = 1; i <= snakeSize; i++)
                {

                    temp2 = new SnakeBlock(snakeBlocks[i].posX, snakeBlocks[i].posY);

                    snakeBlocks[i] = new SnakeBlock(temp.posX, temp.posY);

                    temp = new SnakeBlock(temp2.posX, temp2.posY);

                }
                
                counter = 0;
            }

            

            counter++;
        }

        public void checkInput()
        {
            keyboardState = Keyboard.GetState();

            
            
            if (keyboardState.IsKeyDown(Keys.W) && direction != Direction.Down)
            {
                direction = Direction.Up;
            }
            else if (keyboardState.IsKeyDown(Keys.S) && direction != Direction.Up)
            {
                direction = Direction.Down;
            }
            else if (keyboardState.IsKeyDown(Keys.A) && direction != Direction.Right)
            {
                direction = Direction.Left;
            }
            else if (keyboardState.IsKeyDown(Keys.D) && direction != Direction.Left)
            {
                direction = Direction.Right;
            }

            
        }

        public void drawSnake(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(line, snakeBlocks[0].Position, Color.Crimson);

            for (int i = 1; i <= snakeSize; i++)
            {
                spriteBatch.Draw(line, snakeBlocks[i].Position, Color.DarkRed);
            }
        }

        public void extendSnake()
        {

            snakeSize++;
            snakeBlocks[snakeSize] = new SnakeBlock(snakeBlocks[lastSize].posX, snakeBlocks[lastSize].posY + SnakeGame.BLOCK_SIZE);
            lastSize = snakeSize;                         
        }

        public void eat(Veggie veggie)
        {
            if (snakeBlocks[0].Position.Intersects(veggie.position))
            {
                
                extendSnake();

                veggie.placement();
            }
        }

        public void checkCollisions()
        {
            if (posX <= 20 || posX >= 780)
            {
                snakeState = SnakeState.Dead;
            }

            if (posY <= 20 || posY >= 780)
            {
                snakeState = SnakeState.Dead;
            }

            for (int i = 1; i <= snakeSize; i++)
            {
                if (snakeBlocks[0].Position.Intersects(snakeBlocks[i].Position))
                {
                    snakeState = SnakeState.Dead;
                }
            }
        }


    }

    public enum Direction
    {
        Up,
        Down,
        Right,
        Left
    }

    public enum SnakeState
    {
        Alive, 
        Dead
    }
}

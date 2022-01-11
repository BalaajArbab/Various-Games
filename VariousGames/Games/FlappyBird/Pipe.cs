using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace VariousGames.Games
{
    class Pipe
    {
        Random random = new Random();

        private static int gap = 140;
        private static int distanceBetweenPipes = 300;

        private int pipeWidth = 60;
        private int pipeHeight = 400;
        private float speed;

        private float pipeX1 = 400;
        private float pipeX2 = 400 + distanceBetweenPipes;
        private float pipeX3 = 400 + distanceBetweenPipes + distanceBetweenPipes;
        private float pipeX4 = 400 + distanceBetweenPipes + distanceBetweenPipes + distanceBetweenPipes;

        private float pipeY1 = -150;
        private float pipeY2;
        private float pipeY3;
        private float pipeY4;

        Rectangle[] pipesTop = new Rectangle[4];
        Rectangle[] pipesBot = new Rectangle[4];

        Texture2D pipeTop;
        Texture2D pipeBot;

        private int count;
        private int topBound = -300;
        private int botBound = -80;

                
        

        public Pipe(float bckSpeed, Texture2D PipeTop, Texture2D PipeBot)
        {
            
            speed = bckSpeed;

            pipeTop = PipeTop;
            pipeBot = PipeBot;
        }

        public void pipePlacer()
        {
            pipesTop[0] = new Rectangle((int)pipeX1, (int)pipeY1, pipeWidth, pipeHeight);
            pipesBot[0] = new Rectangle((int)pipeX1, (int)pipeY1 + pipeHeight + gap, pipeWidth, pipeHeight);
            pipeX1 -= speed;

            pipesTop[1] = new Rectangle((int)pipeX2, (int)pipeY2, pipeWidth, pipeHeight);
            pipesBot[1] = new Rectangle((int)pipeX2, (int)pipeY2 + pipeHeight + gap, pipeWidth, pipeHeight);
            pipeX2 -= speed;

            pipesTop[2] = new Rectangle((int)pipeX3, (int)pipeY3, pipeWidth, pipeHeight);
            pipesBot[2] = new Rectangle((int)pipeX3, (int)pipeY3 + pipeHeight + gap, pipeWidth, pipeHeight);
            pipeX3 -= speed;

            pipesTop[3] = new Rectangle((int)pipeX4, (int)pipeY4, pipeWidth, pipeHeight);
            pipesBot[3] = new Rectangle((int)pipeX4, (int)pipeY4 + pipeHeight + gap, pipeWidth, pipeHeight);
            pipeX4 -= speed;



            if (pipeX1 == -60)
            {
                pipeX1 = pipeX4 + distanceBetweenPipes;
                pipeY1 = random.Next(topBound, botBound);
            }

            if (pipeX2 == -60)
            {
                pipeX2 = pipeX1 + distanceBetweenPipes;
                pipeY2 = random.Next(topBound, botBound);
            }

            if (pipeX3 == -60)
            {
                pipeX3 = pipeX2 + distanceBetweenPipes;
                pipeY3 = random.Next(topBound, botBound);
            }

            if (pipeX4 == -60)
            {
                pipeX4 = pipeX3 + distanceBetweenPipes;
                pipeY4 = random.Next(topBound, botBound);
            }

        }

        public void pipeDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pipeTop, new Rectangle(pipesTop[0].X += (int)speed, pipesTop[0].Y, pipeWidth, pipeHeight), Color.White);
            spriteBatch.Draw(pipeBot, new Rectangle(pipesBot[0].X += (int)speed, pipesTop[0].Y + pipeHeight + gap, pipeWidth, pipeHeight), Color.White);
            spriteBatch.Draw(pipeTop, new Rectangle(pipesTop[1].X += (int)speed, pipesTop[1].Y, pipeWidth, pipeHeight), Color.White);
            spriteBatch.Draw(pipeBot, new Rectangle(pipesBot[1].X += (int)speed, pipesTop[1].Y + pipeHeight + gap, pipeWidth, pipeHeight), Color.White);
            spriteBatch.Draw(pipeTop, new Rectangle(pipesTop[2].X += (int)speed, pipesTop[2].Y, pipeWidth, pipeHeight), Color.White);
            spriteBatch.Draw(pipeBot, new Rectangle(pipesBot[2].X += (int)speed, pipesTop[2].Y + pipeHeight + gap, pipeWidth, pipeHeight), Color.White);
            spriteBatch.Draw(pipeTop, new Rectangle(pipesTop[3].X += (int)speed, pipesTop[3].Y, pipeWidth, pipeHeight), Color.White);
            spriteBatch.Draw(pipeBot, new Rectangle(pipesBot[3].X += (int)speed, pipesTop[3].Y + pipeHeight + gap, pipeWidth, pipeHeight), Color.White);

        }

        public void initializePipeY()
        {
            if (count == 0)
            {
                pipeY2 = random.Next(topBound, botBound);
                pipeY3 = random.Next(topBound, botBound);
                pipeY4 = random.Next(topBound, botBound);
                count++;

            }
        }

        public void scoreCounter(ref int score, Bird bird)
        {
            if ((bird.positionX == pipeX1 + pipeWidth || bird.positionX == pipeX2 + pipeWidth || bird.positionX == pipeX3 + pipeWidth || bird.positionX == pipeX4 + pipeWidth) && bird.birdState != BirdState.Dead)
            {
                score++;
            }
        }

        public void collisions(Bird bird)
        {
            for (int i = 0; i < 4; i++)
            {
                if(pipesTop[i].Intersects(bird.Position) || pipesBot[i].Intersects(bird.Position))
                {
                    bird.birdState = BirdState.Dead;                    
                }
            }
        }


    }
}

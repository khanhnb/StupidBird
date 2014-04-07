using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace StupidBird
{
    class PipeControl
    {
        public float PipeDistance;
        public float PipeMoveSpeed;
        public float SpaceDistance;
        public int NumberOfPipe;
        int MaxSpacePosition;
        int MinSpacePosition;
        public Queue<Pipe> q_pipe1;
        public Queue<Pipe> q_pipe2;
        ContentManager Content;
        SpriteBatch spriteBatch;
        Texture2D TopPipe;
        Texture2D BottomPipe;

        public PipeControl(ContentManager Content, SpriteBatch spriteBatch)
        {
            this.Content = Content;
            this.spriteBatch = spriteBatch;
            PipeDistance = 290f;
            SpaceDistance = 365;
            PipeMoveSpeed = 1.25f;              // Dieu chinh speed
            NumberOfPipe = 5;
            MinSpacePosition = -500;
            MaxSpacePosition = -200;
            q_pipe1 = new Queue<Pipe>();
            q_pipe2 = new Queue<Pipe>();
            LoadContent();
            GeneratePipeFirstTime();
        }

        public void LoadContent()
        {
            TopPipe = Content.Load<Texture2D>("img/pipe1");
            BottomPipe = Content.Load<Texture2D>("img/pipe2");
        }
        public void Update()
        {
            PipeMove();
            DeactivePipe();
        }
        public void Draw()
        {
            for (int i = 0; i < NumberOfPipe; i++)
            {
                spriteBatch.Draw(TopPipe, q_pipe1.ElementAt<Pipe>(i).Position, Color.White);
                spriteBatch.Draw(BottomPipe, q_pipe2.ElementAt<Pipe>(i).Position, Color.White);
            }
        }


        public void GeneratePipeFirstTime()
        {
            int y = RandCustom.randInt(MinSpacePosition, MaxSpacePosition);
            Pipe pipe1 = new Pipe();
            pipe1.Active = true;
            pipe1.Position = new Vector2(960f, y);
            q_pipe1.Enqueue(pipe1);

            Pipe pipe2 = new Pipe();
            pipe2.Active = true;
            pipe2.Position = new Vector2(960f, (q_pipe1.ElementAt<Pipe>(0).Position.Y + 431f) + SpaceDistance);  
            q_pipe2.Enqueue(pipe2);
            

            for (int i = 1; i < NumberOfPipe; i++)
            {
                y = RandCustom.randInt(MinSpacePosition, MaxSpacePosition);
                Pipe pipe3 = new Pipe();
                pipe3.Active = true;
                pipe3.Position = new Vector2(q_pipe1.ElementAt<Pipe>(i - 1).Position.X + PipeDistance, y);
                q_pipe1.Enqueue(pipe3);

                Pipe pipe4 = new Pipe();
                pipe4.Active = true;
                pipe4.Position = new Vector2(q_pipe1.ElementAt<Pipe>(i - 1).Position.X + PipeDistance, pipe3.Position.Y + 431f + SpaceDistance);
                q_pipe2.Enqueue(pipe4);
            }
        }
        public void PipeMove()
        {
            for (int i = 0; i < NumberOfPipe; i++)
            {
                q_pipe1.ElementAt<Pipe>(i).Position = new Vector2(q_pipe1.ElementAt<Pipe>(i).Position.X - PipeMoveSpeed, q_pipe1.ElementAt<Pipe>(i).Position.Y);
                q_pipe2.ElementAt<Pipe>(i).Position = new Vector2(q_pipe2.ElementAt<Pipe>(i).Position.X - PipeMoveSpeed, q_pipe2.ElementAt<Pipe>(i).Position.Y);
            }
        }
        public void DeactivePipe()
        {
            if (q_pipe1.Peek().Position.X <= -90f)
            {
                int y = RandCustom.randInt(MinSpacePosition, MaxSpacePosition);
                
                Pipe item = q_pipe1.Dequeue();
                item.Position = new Vector2(q_pipe1.ElementAt<Pipe>(q_pipe1.Count - 1).Position.X + PipeDistance, y);
                item.CanScore = true;
                q_pipe1.Enqueue(item);

                Pipe item2 = q_pipe2.Dequeue();
                item2.CanScore = true;
                item2.Position = new Vector2(q_pipe2.ElementAt<Pipe>(q_pipe2.Count - 1).Position.X + PipeDistance, q_pipe1.ElementAt<Pipe>(q_pipe1.Count - 1).Position.Y + 431f + SpaceDistance);
                q_pipe2.Enqueue(item2);
            }
        }
        public bool CheckCollideWithBird(Bird StupidBird)
        {
            for (int i = 0; i < NumberOfPipe; i++)
            {
                Rectangle rectangle = new Rectangle((int)StupidBird.Position.X, (int)StupidBird.Position.Y - 5, 60, 42);
                if (rectangle.Intersects(new Rectangle((int)q_pipe1.ElementAt<Pipe>(i).Position.X + 35, (int)q_pipe1.ElementAt<Pipe>(i).Position.Y + 20, 100, 615)))
                {
                    q_pipe2.ElementAt<Pipe>(i).CanScore = false;
                    return true;
                }
                Rectangle rectangle2 = new Rectangle((int)StupidBird.Position.X, (int)StupidBird.Position.Y - 20, 60, 42);
                if (rectangle.Intersects(new Rectangle((int)q_pipe2.ElementAt<Pipe>(i).Position.X + 35, (int)q_pipe2.ElementAt<Pipe>(i).Position.Y, 100, 615)))
                {
                    q_pipe2.ElementAt<Pipe>(i).CanScore = false;
                    return true;
                }
            }
            return false;
        }
    }
    
}

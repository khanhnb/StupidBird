using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace StupidBird
{
    class Bird
    {
        ContentManager Content;
        SpriteBatch spriteBatch;
        Texture2D BirdTexture;
        public float CurrentSpeedDown;
        public float CurrentSpeedUp;
        public bool isDead;
        public bool isUp;
        public float JumpSpeed;
        public float JumpSpeedDown;
        public Vector2 Position;
        public float Rotation;
        public float RotationSpeedDown;
        public float RotationSpeedUp;
        public bool CanMoveUp;
        public bool isTouchGround;


        public Bird(ContentManager Content, SpriteBatch spriteBatch)
        {
            this.Content = Content;
            this.spriteBatch = spriteBatch;
            LoadContent();


            Position = new Vector2(100f, 360f);
            CurrentSpeedDown = 0f;
            CurrentSpeedUp = 5f;
            JumpSpeed = 0.14f;
            JumpSpeedDown = 0.03f;
            isDead = false;
            isUp = true;
            RotationSpeedDown = 0.015f;
            RotationSpeedUp = 0.1f; 
            Rotation = 0;
            CanMoveUp = true;
        }
        public void ResetMove()
        {
            CurrentSpeedDown = 0f;
            CurrentSpeedUp = 5f;
            JumpSpeed = 0.14f;
            JumpSpeedDown = 0.03f;
            RotationSpeedDown = 0.015f;
            isDead = false;
            isUp = true;
            RotationSpeedUp = 0.1f;
        }

        public void LoadContent()
        {
            BirdTexture = Content.Load<Texture2D>("img/bird1");
        }
        
        public void MoveDown()
        {
            Position = new Vector2(Position.X, Position.Y + 2f);
        }
        public void Move()
        {
            if (isUp && CanMoveUp)
            {
                CurrentSpeedUp -= JumpSpeed;
                CurrentSpeedDown = 0f;
                if (Rotation >= -0.5f)
                {
                    Rotation -= RotationSpeedUp;
                }
                Position = new Vector2(Position.X, Position.Y - CurrentSpeedUp);
                if (Position.Y <= 0f)
                {
                    Position = new Vector2(Position.X, 0f);
                }
            }
            if (!isUp)
            {
                CurrentSpeedDown += JumpSpeedDown;
                if (Rotation <= 1.5f)
                {
                    Rotation += RotationSpeedDown;
                }
                Position = new Vector2(Position.X, Position.Y + CurrentSpeedDown);
                
            }
            if (CurrentSpeedUp <= 0)
            {
                isUp = false;
                CurrentSpeedUp = 5f;
            }
            if (Position.Y >= 630f)
            {
                isTouchGround = true;
                Position = new Vector2(Position.X, 630f);
                JumpSpeed = 0.14f;
                CurrentSpeedDown = 0f;
            }
        }
        public void Update()
        {
            Move();
        }
        public void Draw()
        {
            spriteBatch.Draw(BirdTexture, Position, null, Color.White, Rotation, new Vector2(20f,30f), 1.0f, SpriteEffects.None, 0f);
        }
    }
}

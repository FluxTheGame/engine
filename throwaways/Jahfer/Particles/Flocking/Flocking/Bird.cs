using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flocking
{
    public abstract class Bird
    {
        protected static Random rand = new Random();
        protected static Vector2 border = new Vector2(100f, 100f);
        protected static float sight = 75f;
        protected static float separation = 30f;
        protected float speed;
        protected Vector2 boundary;
              
        protected Flock flock;
        protected Texture2D texture;

        protected Vector2 position;
        public Vector2 Position
        {
            get { return position; }
        }

        protected Vector2 offSet;
        public Vector2 Offset
        {
            get { return offSet; }
        }

        public Bird(Vector2 boundary, Texture2D texture)
        {
            position = new Vector2(rand.Next((int)boundary.X), rand.Next((int)boundary.Y));
            this.boundary = boundary;
            this.texture = texture;
         }

        public virtual void Update()
        {
            HandleEdgeCollision();
            NormalizeOffset();
            position += offSet;
        }

        private void HandleEdgeCollision()
        {
            //Left and top
            if (position.X < border.X)
            {
                offSet.X += border.X - position.X;
            }

            if (position.Y < border.Y)
            {
                offSet.Y += border.Y - position.Y;
            }

            //Right and bottom
            Vector2 farEnd = boundary - border;
            
            if (position.X > farEnd.X)
            {
                offSet.X += farEnd.X - position.X;
            }

            if (position.Y > farEnd.Y)
            {
                offSet.Y += farEnd.Y - position.Y;
            }
        }

        protected void NormalizeOffset()
        {
            float offSetLength = offSet.Length();
         
            if (offSetLength > speed)
            {
                offSet = offSet * speed / offSetLength;
            }
        }

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}

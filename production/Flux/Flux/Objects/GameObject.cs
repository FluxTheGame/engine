using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Flux
{
    public class GameObject
    {

        public float scale;
        public int display;
        protected int height = 50;
        public bool disabled = false;

        public Vector2 position;
        public Vector2 velocity;
        public Vector2 acceleration;
        public float maxSpeed = 15f;
        public float dampening = 1f;

        protected DateTime created;
        protected bool wrapY = false;
        protected float originalRadius;


        public GameObject()
        {
            scale = 1f;
            position = ScreenManager.Middle();
            display = 0;
            created = DateTime.Now;
        }

        public virtual void Draw(GameTime gameTime)
        {
            //Override to implement draw behavior
        }

        public virtual void Update()
        {
            if (disabled) return;

            Constrain();

            velocity = Vector2.Add(velocity, acceleration);
            velocity = Vector2.Multiply(velocity, dampening);
            velocity = Vectorizer.Limit(velocity, maxSpeed);
            position = Vector2.Add(position, velocity);
        }

        public virtual Vector3 Location()
        {
            return ScreenManager.Location(position, display);
        }


        protected void Constrain()
        {
            if (!wrapY)
            {
                if (position.Y > ScreenManager.world.Y - height) position.Y = ScreenManager.world.Y - height;
                if (position.Y < height) position.Y = height;
            }
            if (position.X > ScreenManager.window.X)
            {
                display++;
                position.X = 5;
                velocity.X += 1;
            }
            if (position.X < 0)
            {
                display--;
                position.X = ScreenManager.window.X - 5;
                velocity.X -= 1;
            }
            if (display > 3) display = 0;
            if (display < 0) display = 3;
        }

        public static float Distance(GameObject one, GameObject two)
        {
            return Vector2.Distance(
                ScreenManager.AdjustedPosition(one.position, one.display), 
                ScreenManager.AdjustedPosition(two.position, two.display)
            );
        }

    }
}

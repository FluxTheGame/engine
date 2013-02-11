using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flocking
{
    class Sparrow : Bird
    {

        Vector2 target;

        public Sparrow(Vector2 boundary, Flock flock, Texture2D texture)
            : base(boundary, texture)
        {
            this.flock = flock;
            speed = 10f;

            this.target = new Vector2(900, 240);
        }

        public override void Update()
        {
            Flock();

            base.Update();
        }

        private void Flock()
        {
            foreach (Bird bird in flock.Birds.Where(b => b is Sparrow && b != this))
            {
                float distance = Vector2.Distance(position, bird.Position);

                // separation
                if (distance < separation)
                {
                    //offSet += position - bird.Position;
                }
                // cohesion
                else if (distance < sight)
                {
                    offSet += (bird.Position - position) * 0.025f;
                }

                // alignment
                if (distance < sight)
                {
                    offSet += bird.Offset * 0.5f;
                }
            }

            foreach (Bird raven in flock.Birds.Where(b => b is Raven && b != this))
            {
                float distanceRaven = Vector2.Distance(position, raven.Position);
                if (distanceRaven < sight) // Flee
                {
                    offSet += (position - raven.Position) * 1.5f;
                }
            }

            
            offSet += target - position;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}

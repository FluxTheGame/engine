using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flocking
{
    public class Sparrow : Bird
    {

        public Sparrow(Vector2 boundary, Flock flock, Texture2D texture)
            : base(boundary, texture)
        {
            this.flock = flock;
            speed = 10f;
        }

        public void Update(Vector2 target)
        {
            Flock(target);

            base.Update();
        }

        private void Flock(Vector2 target)
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

            /*foreach (Bird raven in flock.Birds.Where(b => b is Raven && b != this))
            {
                float distanceRaven = Vector2.Distance(position, raven.Position);
                if (distanceRaven < sight) // Flee
                {
                    offSet += (position - raven.Position) * 1.5f;
                }
            }*/

            if (target != null)
            {
                // chase target!
                float distToTarget = Vector2.Distance(target, position) * 0.1f;
                offSet += (target - position) * distToTarget;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}

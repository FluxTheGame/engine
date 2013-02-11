using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flocking
{
    class Raven : Bird
    {
        public Raven(Vector2 boundary, Flock flock, Texture2D texture)
            : base(boundary, texture)
        {
            this.flock = flock;
            speed = 6f;
        }

        public override void Update()
        {
            Hunt();

            base.Update();
        }

        private void Hunt()
        {
            var l = flock.Birds.Where(b => b is Sparrow && Vector2.Distance(position, b.Position) < sight);
            var m = l.OrderBy(b => Vector2.Distance(position, b.Position));
            var n = m.FirstOrDefault();
            Bird p = ((Bird)n);

            // and move towards to attack...
            if (p != null)
                offSet += p.Position - position;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.Brown);
        }

    }
}

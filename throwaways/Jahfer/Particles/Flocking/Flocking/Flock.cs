using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flocking
{
    public class Flock
    {
        public List<Bird> Birds = new List<Bird>();

        public Flock(Vector2 boundary, int nofSparrows, int nofRavens, Texture2D texture)
        {
            for (int i = 0; i < nofSparrows; i++)
            {
                Birds.Add(new Sparrow(boundary, this, texture));
            }

            for (int i = 0; i < nofRavens; i++)
            {
                Birds.Add(new Raven(boundary, this, texture));
            }
        }

        public void Update()
        {
            foreach (Bird bird in Birds)
            {
                bird.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bird bird in Birds)
            {
                bird.Draw(spriteBatch);
            }            
        }
    }
}

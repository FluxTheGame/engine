using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Flocking
{
    public class Flock
    {
        public List<Sparrow> Birds = new List<Sparrow>();

        public Flock(Vector2 boundary, int nofSparrows, int nofRavens, Texture2D texture)
        {
            for (int i = 0; i < nofSparrows; i++)
            {
                Birds.Add(new Sparrow(boundary, this, texture));
            }
        }

        public void Update()
        {
            MouseState mouse = Mouse.GetState();

            foreach (Sparrow bird in Birds)
            {
                bird.Update(new Vector2(mouse.X, mouse.Y));
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

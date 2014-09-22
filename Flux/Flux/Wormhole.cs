using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Flux.Environment
{
    public class Wormhole
    {
        private int Screen;
        private Vector2 Position;
        private Texture2D Texture;

        public Wormhole(Vector3 pos)
        {
            this.Position = new Vector2(pos.X, pos.Y);
            this.Screen = (int) pos.Z;
        }

        public void LoadContent(ContentManager content)
        {
            this.Texture = content.Load<Texture2D>("wormhole");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, this.Position, Color.White);
        }
    }
}

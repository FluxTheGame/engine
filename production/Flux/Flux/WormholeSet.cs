using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Flux.Environment
{
    class WormholeSet
    {
        public Wormhole[] wormholes = new Wormhole[2];

        public WormholeSet(Vector3 pos1, Vector3 pos2)
        {
            wormholes[0] = new Wormhole(pos1);
            wormholes[1] = new Wormhole(pos2);
        }

        public void LoadContent(ContentManager content)
        {

        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}

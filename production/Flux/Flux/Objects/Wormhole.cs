using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flux
{
    public class Wormhole : GameObject
    {
        public Wormhole() : base()
        {
            icon = ContentManager.wormhole;
            position = new Vector2(500, 300);
        }
    }
}

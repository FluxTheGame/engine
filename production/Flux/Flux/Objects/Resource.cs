using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Flux
{
    public class Resource : GameObject
    {
        public Resource() : base()
        {
            icon = ContentManager.resource;

            int x = Randomizer.RandomInt(0, 700);
            int y = Randomizer.RandomInt(500, 700);
            position = new Vector2(x, y);
        }

        public override void Update()
        {

            base.Update();
        }

    }
}

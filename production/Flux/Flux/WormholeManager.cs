using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Flux.Environment;

namespace Flux
{
    public class WormholeManager : DrawableGameComponent
    {
        WormholeSet wormholeSet;

        public WormholeManager(Game game) : base(game)
        {
            wormholeSet = new WormholeSet(Vector3.Zero, Vector3.Zero);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            wormholeSet.draw();
            base.Draw(gameTime);
        }
    }
}

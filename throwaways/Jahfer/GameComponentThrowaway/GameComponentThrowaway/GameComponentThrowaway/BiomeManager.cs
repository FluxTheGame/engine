using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace GameComponentThrowaway {
    public class BiomeManager : DrawableGameComponent {

        public BiomeManager(Game g)
            : base(g) {
            g.Components.Add(this);
        }


        public override void Initialize() {
            Console.WriteLine("BiomeManager has been initialized!");
            base.Initialize();
        }

        protected override void LoadContent() {
            Console.WriteLine("BiomeManager has loaded its content!");
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime) {
            Console.WriteLine("BiomeManager has been drawn!");
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime) {
            Console.WriteLine("BiomeManager has been updated!");
            base.Update(gameTime);
        }

    }
}

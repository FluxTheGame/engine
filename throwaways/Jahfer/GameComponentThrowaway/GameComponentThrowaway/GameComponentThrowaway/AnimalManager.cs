using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace GameComponentThrowaway {
    public class AnimalManager : DrawableGameComponent {

        public AnimalManager(Game g)
            : base(g) {
            g.Components.Add(this);
        }

        public override void Initialize() {
            Console.WriteLine("AnimalManager has been initialized!");
            base.Initialize();
        }

        protected override void LoadContent() {
            Console.WriteLine("AnimalManager has loaded its content!");
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime) {
            Console.WriteLine("AnimalManager has been drawn!");
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime) {
            Console.WriteLine("AnimalManager has been updated!");
            base.Update(gameTime);
        }

    }
}

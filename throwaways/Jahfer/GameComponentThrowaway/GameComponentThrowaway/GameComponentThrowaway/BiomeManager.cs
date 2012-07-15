using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace GameComponentThrowaway {
    public class BiomeManager : DrawableGameComponent {

        List<Biome> biomeList = new List<Biome>();

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

        public override void Update(GameTime gameTime) {
            Console.WriteLine("BiomeManager has been updated!");

            foreach (Biome biome in biomeList) {
                biome.update();
            }

            base.Update(gameTime);
        }

        public bool createNewBiome(int pID) {
            this.biomeList.Add( new Biome(pID) );
            Console.WriteLine("AnimalManager has added a new Animal!");
            return true;
        }

        public override void Draw(GameTime gameTime) {
            Console.WriteLine("BiomeManager has been drawn!");

            foreach (Biome biome in biomeList) {
                biome.draw();
            }

            base.Draw(gameTime);
        }

    }
}

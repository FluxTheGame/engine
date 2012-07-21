using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace GameComponentThrowaway {
    public class BiomeManager {

        List<Biome> biomeList = new List<Biome>();
        Game game;

        public BiomeManager(Game g) {
            game = g;
        }


        public void Initialize() {
            //Console.WriteLine("BiomeManager has been initialized!");
        }

        protected void LoadContent() {
            //Console.WriteLine("BiomeManager has loaded its content!");
        }

        public void Update(GameTime gameTime) {
            //Console.WriteLine("BiomeManager has been updated!");
            foreach (Biome biome in biomeList) {
                biome.update();
            }

        }

        public bool createNewBiome(int pID) {
            this.biomeList.Add( new Biome(pID, game.Content) );
            //Console.WriteLine("AnimalManager has added a new Animal!");
            return true;
        }

        public void Draw(GameTime gameTime) {
            //Console.WriteLine("BiomeManager has been drawn!");
            foreach (Biome biome in biomeList) {
                biome.draw();
            }

        }

    }
}

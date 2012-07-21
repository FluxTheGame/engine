using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace GameComponentThrowaway {
    public class PlantManager {

        List<Plant> plantList = new List<Plant>();
        Game game;

        public PlantManager(Game g) {
            game = g;
        }

        public void Initialize() {
            //Console.WriteLine("PlantManager has been initialized!");
        }

        protected void LoadContent() {
            //Console.WriteLine("PlantManager has loaded its content!");
        }

        public void Update(GameTime gameTime) {
            //Console.WriteLine("PlantManager has been updated!");
            foreach (Plant plant in plantList) {
                plant.update();
            }
        }

        public void Draw(GameTime gameTime) {
            //Console.WriteLine("PlantManager has been drawn!");
            foreach (Plant plant in plantList) {
                plant.update();
            }
        }

        public bool createNewPlant() {
            this.plantList.Add( new Plant(game.Content) );
            //Console.WriteLine("PlantManager has added a new Plant!");
            return true;
        }

    }
}

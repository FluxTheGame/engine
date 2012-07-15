using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace GameComponentThrowaway {
    public class PlantManager : DrawableGameComponent {

        List<Plant> plantList = new List<Plant>();

        public PlantManager(Game g)
            : base(g) {
            g.Components.Add(this);
        }

        public override void Initialize() {
            Console.WriteLine("PlantManager has been initialized!");
            base.Initialize();
        }

        protected override void LoadContent() {
            Console.WriteLine("PlantManager has loaded its content!");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime) {
            Console.WriteLine("PlantManager has been updated!");

            foreach (Plant plant in plantList) {
                plant.update();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            Console.WriteLine("PlantManager has been drawn!");

            foreach (Plant plant in plantList) {
                plant.update();
            }

            base.Draw(gameTime);
        }

        public bool createNewPlant() {
            this.plantList.Add( new Plant() );
            Console.WriteLine("PlantManager has added a new Plant!");
            return true;
        }

    }
}

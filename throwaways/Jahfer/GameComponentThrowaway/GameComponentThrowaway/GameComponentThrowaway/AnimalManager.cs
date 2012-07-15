using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace GameComponentThrowaway {
    public class AnimalManager : DrawableGameComponent {

        List<Animal> animalList = new List<Animal>();

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

        public override void Update(GameTime gameTime) {
            Console.WriteLine("AnimalManager has been updated!");

            foreach (Animal animal in animalList) {
                animal.update();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime) {
            Console.WriteLine("AnimalManager has been drawn!");

            foreach (Animal animal in animalList) {
                animal.draw();
            }

            base.Draw(gameTime);
        }

        public bool createNewAnimal() {
            this.animalList.Add( new Animal() );
            Console.WriteLine("AnimalManager has added a new Animal!");
            return true;
        }

    }
}

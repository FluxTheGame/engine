using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace GameComponentThrowaway {
    public class AnimalManager {

        List<Animal> animalList = new List<Animal>();
        Game game;

        public AnimalManager(Game g) {
            game = g;
        }

        public void Update(GameTime gameTime) {
            //Console.WriteLine("AnimalManager has been updated!");
            foreach (Animal animal in animalList) {
                animal.Update();
            }
        }

        public void Draw(GameTime gameTime) {
            //Console.WriteLine("AnimalManager has been drawn!");
            foreach (Animal animal in animalList) {
                animal.Draw();
            }
        }

        public bool createNewAnimal() {
            this.animalList.Add( new Animal(game.Content) );
            //Console.WriteLine("AnimalManager has added a new Animal!");
            return true;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameComponentThrowaway {
    public class AnimalManager {

        List<Animal> animalList = new List<Animal>();
        Game game;
        Effect effect;
        Texture2D modelTexture;

        public AnimalManager(Game g) {
            game = g;
        }

        public void LoadContent() {
            // load the shader
            effect = game.Content.Load<Effect>("shader");
            effect.CurrentTechnique = effect.Techniques["Technique1"];

            effect.Parameters["AmbientColor"].SetValue(Color.DarkBlue.ToVector4());
            effect.Parameters["AmbientIntensity"].SetValue(1.0f);
            effect.Parameters["DiffuseColor"].SetValue(Color.White.ToVector4());
            effect.Parameters["DiffuseIntensity"].SetValue(1.0f);
            effect.Parameters["SpecularColor"].SetValue(Color.White.ToVector4());

            modelTexture = game.Content.Load<Texture2D>("Textures\\wedge_p1_diff_v1");
            effect.Parameters["ColorMap"].SetValue(modelTexture);
        }

        public void Update(GameTime gameTime) {
            //Console.WriteLine("AnimalManager has been updated!");
            foreach (Animal animal in animalList) {
                animal.Update();
            }
        }

        public void Draw(GameTime gameTime, Matrix world, Matrix view, Matrix projection) {
            //Console.WriteLine("AnimalManager has been drawn!");
            foreach (Animal animal in animalList) {
                effect.Parameters["View"].SetValue(view);
                effect.Parameters["Projection"].SetValue(projection);
                animal.Draw(game, effect);
            }
        }

        public bool createNewAnimal() {
            this.animalList.Add( new Animal(game.Content) );
            //Console.WriteLine("AnimalManager has added a new Animal!");
            return true;
        }

    }
}

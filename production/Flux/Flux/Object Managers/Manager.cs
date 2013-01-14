using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Flux
{
    public abstract class Manager : DrawableGameComponent
    {

        protected List<GameObject> objects;


        public Manager(Game game) : base(game)
        {
            objects = new List<GameObject>();
        }


        public override void Initialize()
        {
            base.Initialize();
        }


        public override void Update(GameTime gameTime)
        {
            for (int i = objects.Count - 1; i >= 0; i--)
            {
                objects[i].Update();
                UpdateEach(i);
            }

            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {

            foreach (GameObject obj in objects)
            {
                obj.Draw();
            }

            base.Draw(gameTime);
        }


        public virtual void UpdateEach(int i)
        {
            //Can override to implement custom functionality for child classes
        }

    }
}

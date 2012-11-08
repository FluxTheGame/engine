using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Flux
{

    public class ResourceManager : DrawableGameComponent
    {

        SpriteBatch spriteBatch;
        List<Resource> resources;


        public ResourceManager(Game game) : base(game)
        {
        }


        public override void Initialize()
        {
            resources = new List<Resource>();
            base.Initialize();
        }


        public override void Update(GameTime gameTime)
        {

            foreach (Resource resource in resources)
            {
                resource.Update();
            }

            base.Update(gameTime);
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);

            for (int i = 0; i < 1000; i++)
            {
                resources.Add(new Resource());
            }

            base.LoadContent();
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            foreach (Resource resource in resources)
            {
                resource.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}

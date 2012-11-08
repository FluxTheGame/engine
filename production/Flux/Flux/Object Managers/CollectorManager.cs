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
    
    public class CollectorManager : DrawableGameComponent
    {

        SpriteBatch spriteBatch;
        List<Collector> collectors;

        
        public CollectorManager(Game game) : base(game)
        {
        }


        public override void Initialize()
        {
            collectors = new List<Collector>();
            base.Initialize();
        }

      
        public override void Update(GameTime gameTime)
        {

            for (int i = collectors.Count - 1; i >= 0; i--)
            {
                collectors[i].Update();
                CheckMerged(collectors[i]);
            }

            base.Update(gameTime);
        }


        private void CheckMerged(Collector current)
        {
            for (int i = collectors.Count - 1; i >= 0; i--)
            {
                if (Vector2.Distance(current.position, collectors[i].position) < 10 && current != collectors[i])
                {
                    current.MergeWith(collectors[i]);
                    collectors.RemoveAt(i);
                }
            }
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);

            for (int i = 0; i < 3; i++)
            {
                Collector c = new Collector();
                c.position = new Vector2((i+1)*150, (i+1)*150);
                collectors.Add(c);
            }
            base.LoadContent();
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            foreach (Collector c in collectors)
            {
                c.Draw(spriteBatch);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }


    }
}

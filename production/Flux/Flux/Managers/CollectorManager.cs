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

        Texture2D collectorSm;
        Texture2D collectorM;
        Texture2D collectorLg;

        List<Collector> collectors;

        
        public CollectorManager(Game game)
            : base(game)
        {
        }


        public override void Initialize()
        {
            collectors = new List<Collector>();
            base.Initialize();
        }

      
        public override void Update(GameTime gameTime)
        {

            foreach (Collector c in collectors)
            {
                c.Update();
                checkMerged(c);
            }

            base.Update(gameTime);
        }

        private void checkMerged(Collector current)
        {
            foreach (Collector c in collectors)
            {
                if (Vector2.Distance(current.position, c.position) < 10 && current != c)
                {
                    Console.WriteLine("Merged");
                }
            }
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
            
            collectorSm = Game.Content.Load<Texture2D>("collector_sm");
            collectorM = Game.Content.Load<Texture2D>("collector_m");
            collectorLg = Game.Content.Load<Texture2D>("collector_lg");


            for (int i = 0; i < 3; i++)
            {
                Collector c = new Collector(collectorSm);
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

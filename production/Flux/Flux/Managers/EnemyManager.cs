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
    
    public class EnemyManager : DrawableGameComponent
    {

        SpriteBatch spriteBatch;
        Texture2D enemy;

        List<Enemy> enemies;

        
        public EnemyManager(Game game)
            : base(game)
        {
        }


        public override void Initialize()
        {
            enemies = new List<Enemy>();
            base.Initialize();
        }

      
        public override void Update(GameTime gameTime)
        {

            foreach (Enemy e in enemies)
            {
                e.Update();
            }

            base.Update(gameTime);
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(this.Game.GraphicsDevice);
            enemy = Game.Content.Load<Texture2D>("enemy");
            

            for (int i = 0; i < 3; i++)
            {
                enemies.Add(new Enemy(enemy));
            }

            base.LoadContent();
        }


        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            foreach (Enemy e in enemies)
            {
                e.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

    }
}

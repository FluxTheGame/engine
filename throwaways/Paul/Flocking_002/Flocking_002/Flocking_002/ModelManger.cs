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

namespace Flocking_002
{

    public class ModelManger : DrawableGameComponent
    {

        List<Boid> entities = new List<Boid>();

        public ModelManger(Game game) : base(game)
        {



        }//end ModelManager

        protected override void LoadContent()
        {

            entities.Add(new Boid(Game.Content.Load<Model>(@"Models\Chicken_001")));
            entities.Add(new Boid(Game.Content.Load<Model>(@"Models\Chicken_001")));
            entities.Add(new Boid(Game.Content.Load<Model>(@"Models\Chicken_001")));
            base.LoadContent();

        }//end LoadContent

        public override void Initialize()
        {
            
            base.Initialize();

        }//end Initialize

        public override void Update(GameTime gameTime)
        {

            //run through all entities and run their Update functions
            for (int i = 0; i < entities.Count; i++)
            {

                entities[i].Update(entities);

            }

            base.Update(gameTime);

        }//end Update

        public override void Draw(GameTime gameTime)
        {
            foreach (BasicEntity be in entities)
            {

                be.Draw(((Game1)Game).camera);

            }

            base.Draw(gameTime);

        }//end Draw

    }

}

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
                UpdateEach(i);
                objects[i].Update();
            }

            base.Update(gameTime);
        }


        public int Closest(GameObject target)
        {
            GameObject closest = null;
            foreach (GameObject obj in objects)
            {
                if (closest == null) closest = obj;
                else
                {
                    float distance1 = Vector2.Distance(target.position, obj.position);
                    float distance2 = Vector2.Distance(target.position, closest.position);
                    if (distance1 < distance2) closest = obj;
                }
            }
            return objects.IndexOf(closest);
        }


        public override void Draw(GameTime gameTime)
        {

            foreach (GameObject obj in objects)
            {
                ScreenManager.SetTarget(obj.display);
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

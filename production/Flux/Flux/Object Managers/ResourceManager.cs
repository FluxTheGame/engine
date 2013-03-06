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

    public class ResourceManager : Manager
    {

        public static ResourceManager instance;
        protected List<Resource> resources;


        public ResourceManager(Game game) : base(game)
        {
            ResourceManager.instance = this;
        }


        public override void Initialize()
        {
            resources = new List<Resource>();

            PlaceResources();

            base.Initialize();
        }

        public void PlaceResources()
        {
            //Pass in an instance of "parent" - this loads a mask to position the resources in the right place.
            //The parent object could be a tree, and we should have access to its position in 3d space.. and its bounding box

            Vector3 position = new Vector3(0, 3, 0); //Position of tree
            Vector2 size = new Vector2(3.2f, 5.3f); //Bounding box coordinates of tree
            Texture2D mask = ContentManager.Mask("dot"); //Mask containing the positions to place resources

            Color[] data = new Color[mask.Width * mask.Height];
            mask.GetData<Color>(data);

            for (int i = 0; i < 200; i++)
            {
                float x = Randomizer.RandomFloat(0, size.X);
                float y = Randomizer.RandomFloat(0, size.Y);
                Vector3 pos = new Vector3(x, y, 0);
                Vector3 posN = Vector3.Normalize(pos);

                Point maskIndex = new Point((int)(posN.X * mask.Width), (int)(posN.Y * mask.Height));
                int index = maskIndex.Y * (int)(mask.Width-1) + maskIndex.X;


                if (data[index] == Color.White)
                {
                    resources.Add(new Resource(pos));
                }
                else
                {
                    i--;
                }
            }
        }

        public static void Gather(Collector collector)
        {
            for (int i = instance.resources.Count - 1; i >= 0; i--)
            {
                float dist = Vector3.Distance(instance.resources[i].NonDepthLocation(), collector.Location());

                if (dist < collector.SuckRadius())
                {
                    instance.resources[i].target = collector.Location();

                    if (dist < 0.2f)
                    {
                        collector.Collect(instance.resources[i]);
                        instance.resources.RemoveAt(i);
                    }
                }
            }
        }

        
        public override void Update(GameTime gameTime)
        {
            foreach (Resource r in resources)
            {
                r.Update();
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (Resource r in resources)
            {
                r.Draw();
            }
            base.Draw(gameTime);
        }

    }
}

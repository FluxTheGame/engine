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
            resources = new List<Resource>();
        }

        public void PlaceResources(WorldManager worldManager)
        {
            List<WorldObject> trees = worldManager.Trees();
            foreach (WorldObject tree in trees)
            {

                //Pass in an instance of "parent" - this loads a mask to position the resources in the right place.
                //The parent object could be a tree, and we should have access to its position in 3d space.. and its bounding box

                Vector3 position = tree.Location(); //Position of tree model
                Vector2 size = new Vector2(tree.box.Max.X - tree.box.Min.X, tree.box.Max.Y - tree.box.Min.Y); //Bounding box coordinates of tree model

                //Mask containing the positions to place resources
                //Should be proportional to bounding box size to ensure accurate placement
                Texture2D mask = ContentManager.Mask(tree.objectName);

                for (int i = 0; i < 50; i++)
                {
                    float x = Randomizer.RandomFloat(0, size.X);
                    float y = Randomizer.RandomFloat(0, size.Y);
                    Vector3 pos = new Vector3(position.X + x - size.X * 0.5f, position.Y + size.Y - y, position.Z + Randomizer.RandomFloat(0.2f, 0.8f));
                    Vector2 posN = new Vector2(x / size.X, y / size.Y);

                    Point maskIndex = new Point((int)(posN.X * mask.Width), (int)(posN.Y * mask.Height));

                    Color[] data = new Color[1];
                    mask.GetData<Color>(0, new Rectangle(maskIndex.X, maskIndex.Y, 1, 1), data, 0, 1);
                    int probability = Randomizer.RandomInt(1, 255);

                    if (probability <= data[0].R)
                    {
                        Resource r = new Resource(pos, "env/" + tree.leafName);
                        r.display = tree.display;
                        resources.Add(r);
                    }
                    else i--;
                }
            }
        }

        public static void Gather(Collector collector)
        {
            if (instance == null || instance.resources == null) return;

            foreach (Resource r in instance.resources)
            {
                if (r.active)
                {
                    float dist = Vector3.Distance(r.NonDepthLocation(), collector.Location());

                    if (dist < collector.SuckRadius())
                    {
                        if (!collector.isDying) r.SetCollector(collector);
                        if (dist < 0.2f) collector.Collect(r);
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
            resources = resources.OrderBy(r => r.display).ThenBy(r => r.location.Z).ToList();

            foreach (Resource r in resources)
            {
                r.Draw();
            }
            base.Draw(gameTime);
        }

    }
}

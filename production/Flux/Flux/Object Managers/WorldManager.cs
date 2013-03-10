using System;
using System.IO;
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

    public class WorldManager : DrawableGameComponent
    {

        protected List<WorldObject> worldObjects;

        public WorldManager(Game game) : base(game)
        {
            worldObjects = new List<WorldObject>();

            CSVRead();
        }


        public override void Initialize()
        {
            base.Initialize();
        }


        public override void Update(GameTime gameTime)
        {
            foreach (WorldObject obj in worldObjects)
            {
                obj.Update();
            }

            base.Update(gameTime);
        }


        public override void Draw(GameTime gameTime)
        {
            foreach (WorldObject obj in worldObjects)
            {
                ScreenManager.SetTarget(obj.display);
                obj.Draw();
            }

            base.Draw(gameTime);
        }


        private void CSVRead()
        {
            var args = new Dictionary<string, string>();
            var keys = new List<string>();

            var fileStream = new StreamReader("../../../../FluxContent/csv/March_10.csv");

            while (!fileStream.EndOfStream)
            {
                var line = fileStream.ReadLine();
                var values = line.Split(',');

                // reading in names of attrs
                if (keys.Count == 0)
                {
                    foreach (string key in values) 
                    {
                        keys.Add(key);
                    }
                }

                else
                {
                    args.Clear();

                    bool isComment = false;

                    for (int i = 0; i < keys.Count; i++)
                    {
                        // if first char is '#'
                        if (i == 0 && values[0][0] == '#')
                        {
                            isComment = true;
                            break;
                        }

                        args.Add(keys[i], values[i]);
                    }

                    // read next line
                    if (isComment) continue;

                    Model modelLoad;
                    switch (args["objGroup"])
                    {
                        case "2":
                            modelLoad = ContentManager.Model(@"env/Palm_01");
                            break;
                        case "3":
                            modelLoad = ContentManager.Model(@"env/Flower_Yellow_04");
                            break;
                        case "4":
                            modelLoad = ContentManager.Model(@"env/Flower_Yellow_01");
                            break;
                        case "5":
                            modelLoad = ContentManager.Model(@"env/Flower_Yellow_03");
                            break;
                        case "6":
                            modelLoad = ContentManager.Model(@"env/Flower_Yellow_02");
                            break;
                        case "7":
                            modelLoad = ContentManager.Model(@"env/Flower_Blue_03");
                            break;
                        case "8":
                            modelLoad = ContentManager.Model(@"env/Flower_Blue_02");
                            break;
                        case "9":
                            modelLoad = ContentManager.Model(@"env/Flower_Blue_04");
                            break;
                        case "10":
                            modelLoad = ContentManager.Model(@"env/Clover_05");
                            break;
                        case "11":
                            modelLoad = ContentManager.Model(@"env/Clover_03");
                            break;
                        case "12":
                            modelLoad = ContentManager.Model(@"env/Clover_02");
                            break;
                        case "13":
                            modelLoad = ContentManager.Model(@"env/Clover_01");
                            break;
                        case "14":
                            modelLoad = ContentManager.Model(@"env/Clover_04");
                            break;
                        case "15":
                            modelLoad = ContentManager.Model(@"env/Flower_Red_01");
                            break;
                        case "16":
                            modelLoad = ContentManager.Model(@"env/Flower_Red_02");
                            break;
                        case "17":
                            modelLoad = ContentManager.Model(@"env/Flower_Red_03");
                            break;
                        case "18":
                            modelLoad = ContentManager.Model(@"env/Cattails_01");
                            break;
                        case "19":
                            modelLoad = ContentManager.Model(@"env/Cattails_02");
                            break;
                        case "20":
                            modelLoad = ContentManager.Model(@"env/Cattails_04");
                            break;
                        case "21":
                            modelLoad = ContentManager.Model(@"env/Birch_01");
                            break;
                        case "22":
                            modelLoad = ContentManager.Model(@"env/Birch_02");
                            break;
                        case "23":
                            modelLoad = ContentManager.Model(@"env/Birch_03");
                            break;
                        case "24":
                            modelLoad = ContentManager.Model(@"env/CherryTree_01");
                            break;
                        case "25":
                            modelLoad = ContentManager.Model(@"env/CherryTree_01");
                            break;
                        case "26":
                            modelLoad = ContentManager.Model(@"env/CherryTree_01");
                            break;
                        case "27":
                            modelLoad = ContentManager.Model(@"env/OakTree_01");
                            break;
                        case "28":
                            modelLoad = ContentManager.Model(@"env/OakTree_02");
                            break;
                        case "29":
                            modelLoad = ContentManager.Model(@"env/OakTree_03");
                            break;
                        default:
                            modelLoad = ContentManager.Model("chicken");
                            break;
                    }

                    worldObjects.Add(new WorldObject(args, modelLoad));
                }

            }

        }

    }

}

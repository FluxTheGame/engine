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

        public List<WorldObject> Trees()
        {
            return worldObjects; //Use linq to see if object is a tree
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

            var fileStream = new StreamReader("../../../../FluxContent/csv/March_28.csv");

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
                        case "21":
                            args["objName"] = "Birch_01";
                            args["leafName"] = "Birch_01_Leaf";
                            break;
                        case "22":
                            args["objName"] = "Birch_02";
                            args["leafName"] = "Birch_01_Leaf";
                            break;
                        case "23":
                            args["objName"] = "Birch_03";
                            args["leafName"] = "Birch_01_Leaf";
                            break;
                        case "24":
                            args["objName"] = "CherryTree_01";
                            args["leafName"] = "Cherry_01_Leaf";
                            break;
                        case "25":
                            args["objName"] = "CherryTree_02";
                            args["leafName"] = "Cherry_01_Leaf";
                            break;
                        case "26":
                            args["objName"] = "CherryTree_03";
                            args["leafName"] = "Cherry_01_Leaf";
                            break;
                        case "27":
                            args["objName"] = "OakTree_01";
                            args["leafName"] = "Oak_01_Leaf";
                            break;
                        case "28":
                            args["objName"] = "OakTree_02";
                            args["leafName"] = "Oak_01_Leaf";
                            break;
                        case "29":
                            args["objName"] = "OakTree_03";
                            args["leafName"] = "Oak_01_Leaf";
                            break;
                        default:
                            args["objName"] = "Birch_01";
                            args["leafName"] = "Birch_01_Leaf";
                            break;
                    }

                    modelLoad = ContentManager.Model("env/" + args["objName"]);
                    worldObjects.Add(new WorldObject(args, modelLoad));

                }

            }

        }

    }

}

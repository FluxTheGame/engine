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
                obj.Draw();
            }

            base.Draw(gameTime);
        }


        private void CSVRead()
        {
            var args = new Dictionary<string, string>();
            var keys = new List<string>();

            var fileStream = new StreamReader("../../../../FluxContent/csv/Our_first_csv_woo.csv");

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
                        case "0":
                            modelLoad = ContentManager.Model("Birch_01");
                            break;
                        case "1":
                            modelLoad = ContentManager.Model("Oak_01");
                            break;
                        case "2":
                            modelLoad = ContentManager.Model("Palm_01");
                            break;
                        case "3":
                            modelLoad = ContentManager.Model("Flower_Yellow_04");
                            break;
                        case "4":
                            modelLoad = ContentManager.Model("Flower_Yellow_01");
                            break;
                        case "5":
                            modelLoad = ContentManager.Model("Flower_Yellow_03");
                            break;
                        case "6":
                            modelLoad = ContentManager.Model("Flower_Yellow_02");
                            break;
                        case "7":
                            modelLoad = ContentManager.Model("Flower_Blue_03");
                            break;
                        case "8":
                            modelLoad = ContentManager.Model("Flower_Blue_02");
                            break;
                        case "9":
                            modelLoad = ContentManager.Model("Flower_Blue_04");
                            break;
                        case "10":
                            modelLoad = ContentManager.Model("Clover_05");
                            break;
                        case "11":
                            modelLoad = ContentManager.Model("Clover_03");
                            break;
                        case "12":
                            modelLoad = ContentManager.Model("Clover_02");
                            break;
                        case "13":
                            modelLoad = ContentManager.Model("Clover_01");
                            break;
                        case "14":
                            modelLoad = ContentManager.Model("Clover_04");
                            break;
                        case "15":
                            modelLoad = ContentManager.Model("Flower_Red_01");
                            break;
                        case "16":
                            modelLoad = ContentManager.Model("Flower_Red_02");
                            break;
                        case "17":
                            modelLoad = ContentManager.Model("Flower_Red_03");
                            break;
                        case "18":
                            modelLoad = ContentManager.Model("Ground_Plane");
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

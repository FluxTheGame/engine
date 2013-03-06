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
    class ContentManager
    {

        private static Game g;


        public static void Initialize(Game game)
        {
            g = game;
        }

        public static Model Model(string name)
        {
            return g.Content.Load<Model>(@"models/" + name);
        }

        public static Texture2D Sprite(string name)
        {
            return g.Content.Load<Texture2D>(@"sprites/" + name);
        }

        public static Texture2D Mask(string name)
        {
            return g.Content.Load<Texture2D>(@"masks/" + name);
        }

        public static SpriteFont Font(string name)
        {
            return g.Content.Load<SpriteFont>(@"fonts/" + name);
        }

        public static Effect Shader(string name)
        {
            return g.Content.Load<Effect>(@"shaders/" + name);
        }
    }
}

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
    public class WormholeManager : Manager
    {

        public static WormholeManager instance;
        public static Effect SwirlShader;
        public static EffectParameter ShaderPosition;
        public static EffectParameter ShaderAngle;

        protected List<WormholePair> wormholePairs;


        public WormholeManager(Game game) : base(game)
        {
            WormholeManager.instance = this;
        }

        public override void Initialize()
        {
            wormholePairs = new List<WormholePair>();

            LoadShaders();

            base.Initialize();
        }


        protected override void LoadContent()
        {
            Dictionary<string, string> audioFiles = new Dictionary<string, string>();
            audioFiles.Add("wormhole.opposite", "sfx/interactions/wormhole_opposite.wav");
            audioFiles.Add("wormhole.origin", "sfx/interactions/wormhole_origin.wav");

            Audio.Load(audioFiles);

            base.LoadContent();
        }


        private void LoadShaders()
        {
            SwirlShader = ContentManager.Shader("Swirl");

            SwirlShader.CurrentTechnique = SwirlShader.Techniques["Plain"];
            SwirlShader.Parameters["TextureSize"].SetValue(ScreenManager.window);
            SwirlShader.Parameters["Radius"].SetValue(100.0f);
            ShaderPosition = SwirlShader.Parameters["Center"];
            ShaderAngle = SwirlShader.Parameters["Angle"];
        }

        public static void Add(Vector2 position, bool inward, int display)
        {
            bool make = true;
            foreach (WormholePair w in instance.wormholePairs)
            {
                make = w.Make(position, display);
                if (!make) break;
            }

            if (make) {
                WormholePair wormholePair = new WormholePair(inward, position, display);
                instance.wormholePairs.Add(wormholePair);
            }
        }

        public static void Suck(GameObject passenger)
        {
            if (instance == null) return;

            foreach (WormholePair w in instance.wormholePairs)
            {
                w.Suck(passenger);
            }
        }

        public static void Remove(WormholePair wormholePair)
        {
            instance.wormholePairs.Remove(wormholePair);
        }

        public override void Update(GameTime gameTime)
        {
            //Pushes list of GameObjects to parent for general processing (Update, Draw)
            objects = wormholePairs.Cast<GameObject>().ToList();
            base.Update(gameTime);
        }
    }
}

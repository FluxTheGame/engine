using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Flux
{
    public class Wormhole : GameObject
    {
        Stopwatch stopwatch = new Stopwatch();

        public AnimSet wormholeAnim;
        public bool inward;
        public float twirlAngle = 0;

        enum States { BloatIntro, BloatStatic, BloatOutro, SuckIntro, SuckStatic, SuckOutro };

        private RenderTarget2D rt =
            new RenderTarget2D(ScreenManager.graphics, (int)ScreenManager.window.X, (int)ScreenManager.window.Y, false,
                ScreenManager.graphics.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24, 8, RenderTargetUsage.PreserveContents);

        public Wormhole(bool suck, Vector2 position, int display) : base()
        {
            inward = suck;
            scale = 0.08f;
            this.position = position;
            this.display = display;
            SetupAnimations();
        }

        public override void Update()
        {
            if (inward) 
                GridManager.Pinch(position, 100, 0.005f, display);
            else
                GridManager.Bloat(position, 100, 0.005f, display);

            wormholeAnim.Update(position);

            base.Update();
        }

        public override void Draw(GameTime gameTime)
        {
            /*if (startTime == 0)
                startTime = gameTime.TotalGameTime.TotalSeconds * 0.5;

            double curTime = gameTime.TotalGameTime.TotalSeconds * 0.5;
            double twirlAngle = curTime - startTime;*/

            ScreenManager.SetTarget(display);

            ScreenManager.spriteBatch.Begin();
            wormholeAnim.Draw();
            ScreenManager.spriteBatch.End();

            WormholeManager.ShaderAngle.SetValue(twirlAngle);
            WormholeManager.ShaderPosition.SetValue(position);
            Shaderizer.Draw2D(display, WormholeManager.SwirlShader);
        }

        protected void SetupAnimations()
        {
            Spritesheet[] wormholeAnimations = {
                new Spritesheet("wormhole_blow_intro", new Point(400, 400), (int)States.BloatIntro, 39, false, (int)States.BloatStatic, true),
                new Spritesheet("wormhole_blow_static", new Point(400, 400), (int)States.BloatStatic, 75, true, -1, true),
                new Spritesheet("wormhole_blow_outro", new Point(400, 400), (int)States.BloatOutro, 42, false, -1, true),
                new Spritesheet("wormhole_suck_intro", new Point(400, 400), (int)States.SuckIntro, 39, false, (int)States.SuckStatic, true),
                new Spritesheet("wormhole_suck_static", new Point(400, 400), (int)States.SuckStatic, 75, true, -1, true),
                new Spritesheet("wormhole_suck_outro", new Point(400, 400), (int)States.SuckOutro, 42, false, -1, true),
            };

            wormholeAnim = new AnimSet(wormholeAnimations);
            if (inward) wormholeAnim.Play((int)States.SuckIntro);
            else wormholeAnim.Play((int)States.BloatIntro);
        }
    }
}

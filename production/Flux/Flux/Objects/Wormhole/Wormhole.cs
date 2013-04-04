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
        Color[] bits = new Color[(int)(ScreenManager.window.X * ScreenManager.window.Y)];
        public bool inward;
        private double startTime = 0;
        private RenderTarget2D scene;
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
        }

        public override void Update()
        {
            if (inward) 
                GridManager.Pinch(position, 100, 0.005f, display);
            else
                GridManager.Bloat(position, 100, 0.005f, display);


            base.Update();
        }

        public override void Draw(GameTime gameTime)
        {
            if (startTime == 0)
                startTime = gameTime.TotalGameTime.TotalSeconds * 0.5;

            double curTime = gameTime.TotalGameTime.TotalSeconds * 0.5;
            double twirlAngle = curTime - startTime;

            WormholeManager.ShaderAngle.SetValue((float)twirlAngle);
            WormholeManager.ShaderPosition.SetValue(position);
            scene = ScreenManager.Target(display);

            ScreenManager.graphics.SetRenderTarget(rt);
            ScreenManager.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque, null, null, RasterizerState.CullNone, WormholeManager.SwirlShader);
            ScreenManager.spriteBatch.Draw(scene, ScreenManager.screenRect, Color.White);

            ScreenManager.graphics.SetRenderTarget(scene);
            ScreenManager.spriteBatch.Draw(rt, ScreenManager.screenRect, Color.White);
            ScreenManager.spriteBatch.End();
        }
    }
}

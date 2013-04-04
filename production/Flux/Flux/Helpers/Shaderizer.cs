using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flux
{
    class Shaderizer
    {
        static RenderTarget2D scene;
        static RenderTarget2D tmpTarget = new RenderTarget2D(ScreenManager.graphics, (int)ScreenManager.window.X, (int)ScreenManager.window.Y, false,
                ScreenManager.graphics.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24, 8, RenderTargetUsage.PreserveContents);

        public static void Draw2D(int display, Effect shader)
        {
            scene = ScreenManager.Target(display);

            ScreenManager.graphics.SetRenderTarget(tmpTarget);
            ScreenManager.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque, null, null, RasterizerState.CullNone, shader);
            ScreenManager.spriteBatch.Draw(scene, ScreenManager.screenRect, Color.White);

            ScreenManager.graphics.SetRenderTarget(scene);
            ScreenManager.spriteBatch.Draw(tmpTarget, ScreenManager.screenRect, Color.White);
            ScreenManager.spriteBatch.End();
        }
    }
}

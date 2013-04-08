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
        public static RenderTarget2D tmpTarget = new RenderTarget2D(ScreenManager.graphics, (int)ScreenManager.window.X, (int)ScreenManager.window.Y, false,
                ScreenManager.graphics.PresentationParameters.BackBufferFormat,
                DepthFormat.Depth24, 8, RenderTargetUsage.PreserveContents);

        public static RenderTarget2D _drawShader(RenderTarget2D source, RenderTarget2D rt, Effect shader, bool end = true)
        {
            // set target
            ScreenManager.graphics.SetRenderTarget(rt);
            // draw texture with shader applied
            ScreenManager.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque, null, null, RasterizerState.CullNone, shader);
            ScreenManager.spriteBatch.Draw(source, ScreenManager.screenRect, Color.White);

            if (end)
                ScreenManager.spriteBatch.End();

            return rt;
        }

        public static void _renderTexture(int display, RenderTarget2D tex)
        {
            if (display >= 0) ScreenManager.SetTarget(display);
            else ScreenManager.graphics.SetRenderTarget(null);

            ScreenManager.spriteBatch.Draw(tex, ScreenManager.screenRect, Color.White);
            ScreenManager.spriteBatch.End();
        }

        public static void Draw2D(int display, Effect shader)
        {
            RenderTarget2D tex = _drawShader(ScreenManager.Target(display), tmpTarget, shader, false);
            _renderTexture(display, tex);
        }
    }
}

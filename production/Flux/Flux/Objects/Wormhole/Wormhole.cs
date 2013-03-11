using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flux
{
    public class Wormhole : GameObject
    {

        public bool inward;
        

        public Wormhole(bool suck) : base()
        {
            model = ContentManager.Model("chicken");
            inward = suck;
            scale = 0.08f;
        }

        public override void Update()
        {
            if (inward) 
                GridManager.Pinch(position, 100, 0.005f, display);
            else
                GridManager.Bloat(position, 100, 0.005f, display);

            base.Update();
        }

        public override void Draw()
        {
            RenderTarget2D t = ScreenManager.MakeTarget();

            ScreenManager.graphics.SetRenderTarget(t);
            Texture2D sceneTexture = (Texture2D)ScreenManager.Target(display);

            WormholeManager.SwirlShader.Parameters["Center"].SetValue(position);

            ScreenManager.graphics.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.DarkSlateBlue, 1.0f, 0);
            ScreenManager.spriteBatch.Begin(0, BlendState.Opaque, null, null, RasterizerState.CullNone, WormholeManager.SwirlShader);
            ScreenManager.spriteBatch.Draw(sceneTexture, sceneTexture.Bounds, Color.White);
            ScreenManager.spriteBatch.End();

            ScreenManager.PutTarget(display, t);
        }
    }
}

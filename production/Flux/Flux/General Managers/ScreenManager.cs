using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Flux
{
    class ScreenManager
    {
        private static Camera[] camera = new Camera[4]; //Array of cameras
        private static RenderTarget2D[] target = new RenderTarget2D[4]; //Array of render targets
        private static int spacing = 60;

        public static SpriteBatch spriteBatch;
        public static Vector2 window; //Size of a single window
        public static Vector2 world; //Size of entire world (4 grids combined), not including offset
        public static GraphicsDevice graphics;
        public static int screens = 4;


        public static void Initialize(Game game, GraphicsDevice g)
        {
            window = new Vector2(1280, 800);
            world = new Vector2(window.X * screens, window.Y);
            graphics = g;
            spriteBatch = new SpriteBatch(graphics);


            for (int i = 0; i < target.Length; i++)
            {
                target[i] = MakeTarget();
            }


            for (int i = 0; i < camera.Length; i++)
            {
                float worldOffsetX = 6.255f; //  -1 * Location(Vector2.Zero, 0).X;
                float cameraOffsetX = Location(Vector2.Zero, i).X;
                camera[i] = new Camera(game, new Vector3(worldOffsetX + cameraOffsetX, -0.05f, 9f), new Vector3(worldOffsetX + cameraOffsetX, -0.05f, 0), Vector3.Up);
                camera[i].display = i;
            }
        }

        public static RenderTarget2D MakeTarget()
        {
            return new RenderTarget2D(graphics, (int)window.X, (int)window.Y, false, 
                graphics.PresentationParameters.BackBufferFormat, 
                DepthFormat.Depth24, 8, RenderTargetUsage.PreserveContents);
        }

        public static void Update(GameTime gameTime)
        {
            foreach (Camera c in camera)
            {
                c.Update(gameTime);
            }
        }

        public static Camera Camera(int display)
        {
            graphics.DepthStencilState = DepthStencilState.Default;
            graphics.RasterizerState = RasterizerState.CullCounterClockwise;
            graphics.BlendState = BlendState.Opaque;
            //graphics.SamplerStates[0] = SamplerState.PointWrap;

            return camera[display];
        }

        public static void PutTarget(int display, RenderTarget2D t)
        {
            target[display] = t;
        }

        public static void SetTarget(int display)
        {
            graphics.SetRenderTarget(Target(display));
        }

        public static RenderTarget2D Target(int display)
        {
            return target[display];
        }

        public static Vector3 Location(Vector2 position, int display)
        {
            Vector2 mid = Middle();
            Vector3 location = PixelsToUnits(new Vector2(position.X - mid.X, (position.Y - mid.Y)));
            location.X += (spacing * display);
            return location;
        }

        public static Vector3 PixelsToUnits(Vector2 pixels)
        {
            return new Vector3(pixels.X * 0.00952f, pixels.Y * -0.0099f, 0);
        }

        public static Vector2 Middle()
        {
            return new Vector2(window.X * 0.5f, window.Y * 0.5f);
        }

        public static int Opposite(int display)
        {
            return (display + 2) % 4;
        }

        public static Vector2 AdjustedPosition(Vector2 position, int display)
        {
            return new Vector2(position.X + (window.X * display), position.Y);
        }

    }
}

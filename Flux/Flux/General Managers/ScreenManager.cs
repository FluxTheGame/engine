﻿using System;
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

        public static Viewport tmpViewport;
        public static SpriteBatch spriteBatch;
        public static Vector2 window; //Size of a single window
        public static Rectangle screenRect;
        public static Vector2 world; //Size of entire world (4 grids combined), not including offset
        public static GraphicsDevice graphics;
        public static int screens = 4;


        public static void Initialize(Game game, GraphicsDevice g)
        {
            window = new Vector2(1280, 800);
            screenRect = new Rectangle(0, 0, (int)window.X, (int)window.Y);
            world = new Vector2(window.X * screens, window.Y);
            graphics = g;
            spriteBatch = new SpriteBatch(graphics);

            tmpViewport = new Viewport(screenRect);

            for (int i = 0; i < target.Length; i++)
            {
                target[i] = MakeTarget();
            }

            for (int i = 0; i < camera.Length; i++)
            {
                float worldOffsetX = 6.255f; //  -1 * Location(Vector2.Zero, 0).X;
                float cameraOffsetX = Location(Vector2.Zero, i).X + 0.02f;
                camera[i] = new Camera(game, new Vector3(worldOffsetX + cameraOffsetX, -0.07f, 9f), new Vector3(worldOffsetX + cameraOffsetX, -0.07f, 0), Vector3.Up);
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

            if (display > 3) display = 3;

            return camera[display];
        }

        public static void PutTarget(int display, RenderTarget2D t)
        {
            target[display] = t;
        }

        public static void SetTarget(RenderTarget2D target)
        {
            graphics.SetRenderTarget(target);
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

        public static int DisplayOfLocation(Vector3 location)
        {
            float x = location.X + spacing * 0.5f;
            return (int)Math.Floor(x / 60f);
        }

        public static int Opposite(int display)
        {
            return (display + 2) % 4;
        }

        public static Vector2 AdjustedPosition(Vector2 position, int display)
        {
            return new Vector2(position.X + (window.X * display), position.Y);
        }

        public static Vector2 ProjectedPosition(Vector3 location, int display) 
        {
            Matrix proj = Camera(display).projection;
            Matrix view = Camera(display).view;
            Vector3 screenPos = tmpViewport.Project(location, proj, view, Matrix.Identity);

            Vector2 position = new Vector2(screenPos.X, screenPos.Y);

            /*if (position.X > window.X) position.X = window.X;
            if (position.X < 0) position.X = 0;
            if (position.Y > window.Y) position.Y = window.Y;
            if (position.Y < 0) position.Y = 0;*/

            return position;
        }

    }
}

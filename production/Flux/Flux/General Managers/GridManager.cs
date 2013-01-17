using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Flux
{
    class GridManager
    {
        private static Grid grid;

        public static void Initialize(int windowX, int windowY, int scale)
        {
            int fieldX = (int)ScreenManager.world.X / scale;
            int fieldY = (int)ScreenManager.world.Y / scale;

            grid = new Grid((int)ScreenManager.world.X, (int)ScreenManager.world.Y, fieldX, fieldY);
        }

        public static void Update()
        {
            grid.Update();
        }

        public static void Draw()
        {
            grid.Draw();
        }

        public static void Bloat(Vector2 position, float addForceRadius, float strength)
        {
            grid.AddForceCircle(position, addForceRadius, strength, false);
        }

        public static void Pinch(Vector2 position, float addForceRadius, float strength)
        {
            grid.AddForceCircle(position, addForceRadius, strength, true);
        }

        public static Vector2 GetForce(Vector2 position, float getForceRadius)
        {
            return grid.GetForceAtPosition(position, getForceRadius);
        }


    }
}

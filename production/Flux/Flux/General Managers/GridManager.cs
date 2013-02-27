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
        private static Grid[] grid = new Grid[4];

        public static void Initialize(int windowX, int windowY, int scale)
        {
            int fieldX = (int)ScreenManager.window.X / scale;
            int fieldY = (int)ScreenManager.window.Y / scale;

            for (int i = 0; i < grid.Length; i++) 
            {
                grid[i] = new Grid((int)ScreenManager.window.X, (int)ScreenManager.window.Y, fieldX, fieldY);
                grid[i].display = i;
            }
        }

        public static void Update()
        {
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i].Update();
            }
        }

        public static void Draw()
        {
            for (int i = 0; i < grid.Length; i++)
            {
                grid[i].Draw();
            }
        }

        public static void Bloat(Vector2 position, float addForceRadius, float strength, int display)
        {
            grid[display].AddForceCircle(position, addForceRadius, strength, false);
        }

        public static void Pinch(Vector2 position, float addForceRadius, float strength, int display)
        {
            grid[display].AddForceCircle(position, addForceRadius, strength, true);
        }

        public static Vector2 GetForce(Vector2 position, float getForceRadius, int display)
        {
            return grid[display].GetForceAtPosition(position, getForceRadius);
        }


    }
}

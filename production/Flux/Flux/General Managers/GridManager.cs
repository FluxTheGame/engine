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
        private static Grid[] grid = new Grid[1]; //Array of grids

        public static void Initialize(int windowX, int windowY, int scale)
        {
            int fieldX = windowX / scale;
            int fieldY = windowY / scale;
            grid[0] = new Grid(windowX, windowY, fieldX, fieldY);
            grid[0].display = 0;
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

        public static void Bloat(int display, Vector2 position, float addForceRadius, float strength)
        {
            grid[display].AddForceCircle(position, addForceRadius, strength, false);
        }

        public static void Pinch(int display, Vector2 position, float addForceRadius, float strength)
        {
            grid[display].AddForceCircle(position, addForceRadius, strength, true);
        }

        public static Vector2 GetForce(int display, Vector2 position, float getForceRadius)
        {
            return grid[display].GetForceAtPosition(position, getForceRadius);
        }


    }
}

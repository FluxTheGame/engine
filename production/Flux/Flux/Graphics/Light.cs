using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Flux
{
    class Light
    {
        public static Vector3 SunDirection = new Vector3(1, -1, -1);
        public static Vector3 SunDiffuseColor = Color.SkyBlue.ToVector3(); //new Vector3(0.5f, 0, 0)
        public static Vector3 SunSpecularColor = Color.OrangeRed.ToVector3();

        private static Color SunDiffuseColorNight = Color.Black;
        private static Color SunDiffuseColorMid = Color.Goldenrod;
        private static Color SunDiffuseColorDay = Color.PaleGoldenrod;

        private static Color transitionalColor;


        public static void Update(GameTime gameTime)
        {
            float lerp = (float)(Math.Cos(gameTime.TotalGameTime.TotalSeconds * 0.3f) + 1) * 0.5f; // [0..1]

            SunDirection.Y = (lerp - 1);

            // how much daylight do we want compared to night?
            float amt = lerp * 3; // [0..3] = 2/3 daylight

            if (amt > 1)
            {
                // lerp = [1..0.33]
                SunDiffuseColor = Color.Lerp(SunDiffuseColorMid, SunDiffuseColorDay, lerp).ToVector3();
                transitionalColor = new Color(SunDiffuseColor);
            }
            else
            {
                SunDiffuseColor = Color.Lerp(SunDiffuseColorNight, transitionalColor, amt).ToVector3();
            }
        }
    }
}

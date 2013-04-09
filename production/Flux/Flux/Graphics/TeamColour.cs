using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Flux
{
    public class TeamColour
    {

        private static List<Color> colours;

        public static void Initialize()
        {
            colours = new List<Color>();
            string[] hexes = new string[] {
                "f93424", /*"79ab4c", "388bb7",*/ "6a4d8d", "fa8c41", "d3e172", "6ecdd1", "96407d",
                "f1eb4f", "2b7d57", "a11a16", "e0627a", "b8e3dc", "8a9eb6", "fac251"
            };

            foreach (string hex in hexes)
            {
                colours.Add(ToColor(hex));
            }
        }

        public static Color Get()
        {
            Color c;
            if (colours.Count > 0)
            {
                c = colours[0];
                colours.RemoveAt(0);
            }
            else
            {
                c = Color.White;   
            }

            Console.WriteLine(c);

            return c;
        }

        public static void Put(Color c)
        {
            if (c != Color.White)
            {
                colours.Add(c);
            }
        }

        public static string ToHex(Color color, bool includeHash)
        {
            string[] argb = {
                //color.A.ToString("X2"),
                color.R.ToString("X2"),
                color.G.ToString("X2"),
                color.B.ToString("X2"),
            };
            return (includeHash ? "#" : string.Empty) + string.Join(string.Empty, argb);
        }
       
        public static Color ToColor(string hexString)
        {
            if (hexString.StartsWith("#"))
                hexString = hexString.Substring(1);
            uint hex = uint.Parse(hexString, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            Color color = Color.White;
            if (hexString.Length == 8)
            {
                color.A = (byte)(hex >> 24);
                color.R = (byte)(hex >> 16);
                color.G = (byte)(hex >> 8);
                color.B = (byte)(hex);
            }
            else if (hexString.Length == 6)
            {
                color.R = (byte)(hex >> 16);
                color.G = (byte)(hex >> 8);
                color.B = (byte)(hex);
            }
            else
            {
                throw new InvalidOperationException("Invald hex representation of an ARGB or RGB color value.");
            }
            return color;
        }
    }
}

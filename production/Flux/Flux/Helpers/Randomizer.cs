using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flux
{
    class Randomizer
    {

        private static Random random = new Random();

        public static int RandomInt(int lower, int upper)
        {
            return random.Next(lower, upper);
        }

    }

}

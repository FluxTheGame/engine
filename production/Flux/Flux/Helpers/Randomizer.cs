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

        public static float RandomFloat(float lower, float upper)
        {
            Double r = random.NextDouble();
            r *= (upper - lower);
            r += lower;
            return (float)r;
        }

        public static Vector2 RandomVelocity(float magnitude=1.0f)
        {
            Vector2 velocity = new Vector2(
                Randomizer.RandomFloat(-1.0f, 1.0f), 
                Randomizer.RandomFloat(-0.5f, 0.5f)
            );
            return Vector2.Multiply(velocity, magnitude);
        }

    }

}

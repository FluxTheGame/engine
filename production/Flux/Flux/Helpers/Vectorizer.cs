using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Flux
{
    public class Vectorizer
    {
        public static Vector2 Limit(Vector2 v, float max)
        {
            float length = v.Length();
            if (length > max)
            {
                v = Vector2.Normalize(v);
                v *= max;
            }
            return v;
        }
    }
}

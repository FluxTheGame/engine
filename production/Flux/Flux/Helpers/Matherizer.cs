using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flux
{
    public class Matherizer
    {
        public static float ToRadians(float degrees)
        {
            return (float)(degrees * Math.PI / 180);
        }
    }
}

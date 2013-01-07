using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Flux
{
    public class DefaultCamera : Camera
    {

        public DefaultCamera(Game game) : base(
            game,
            new Vector3(0, 0, 5), 
            Vector3.Zero, 
            Vector3.Up
        )
        {
            
        }
    }
}

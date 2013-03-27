using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Flux
{
    public class Water : Resource
    {


        public Water(Vector3 location, string modelName, float speed = 0.05f) : base(location, modelName, speed)
        {
        }

        protected override void MoveToCollector()
        {
            //Override to shake or something
            //You don't have to call base.Update.. it just makes the resource move towards the collector without restraint
            base.Update();
        }
    }
}

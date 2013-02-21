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
    public class Resource : GameObject
    {

        public Vector2 target;
        private float speed = 0.001f;

        public Resource() : base()
        {
            model = ContentManager.Model("chicken");
            scale = 0.01f;

            int x = Randomizer.RandomInt(0, (int)ScreenManager.window.X);
            int y = Randomizer.RandomInt(0, (int)ScreenManager.window.Y);
            position = new Vector2(x, y);

            target = position;
        }

        public override void Update()
        {

            float distToTarget = Vector2.Distance(target, position) * this.speed;
            Vector2 offset = (target - position) * distToTarget;

            position += offset;

            base.Update();
        }

    }
}

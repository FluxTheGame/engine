using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Flux
{
    public class Projectile : GameObject
    {

        public Enemy target;
        public Collector collector;

        private Texture2D sprite;


        public Projectile(Enemy target, Collector collector) : base()
        {
            this.target = target;
            this.collector = collector;
            this.position = collector.position;
            this.display = collector.display;
            height = 0;

            this.sprite = ContentManager.Sprite("collector_attack");
        }

        public override void Update()
        {
            velocity = Vector2.Normalize(target.position - position) * 8f;

            if (GameObject.Distance(this, target) < 20f)
            {
                target.BeAttacked(1);
                collector.DestroyProjectile(this);
            }

            if (GameObject.Distance(this, target) > 1000)
            {
                collector.DestroyProjectile(this);
            }

            base.Update();
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SetTarget(collector.display);
            ScreenManager.spriteBatch.Begin();
            ScreenManager.spriteBatch.Draw(sprite, position, Color.White);
            ScreenManager.spriteBatch.End();
        }
    }
}

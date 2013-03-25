using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace Flux
{
    public class Enemy : GridObject
    {

        protected bool isDying = false;
        protected int health = 1;
        protected int wrapBuffer = 50;
        protected float rotation;
        protected AnimSprite animation;
        protected AnimSprite explosion;


        public Enemy() : base()
        {
            wrapY = true;
            height = 0;
            velocity = Randomizer.RandomVelocity();

            position = new Vector2(Randomizer.RandomFloat(0, ScreenManager.window.X), -wrapBuffer);
            display = Randomizer.RandomInt(0, 4);
            dampening = 0.985f;

            Animation[] stateAnimations = {
                new Animation(0, 48, false)
            };
            explosion = new AnimSprite("enemy_explosion", new Point(85, 75), stateAnimations);
            
        }

        public void BeAttacked(int damage)
        {
            Console.WriteLine("Enemy got attacked, dying");
            Die();
        }

        public void Kamikaze(Collector collector)
        {
            if (!isDying)
            {
                collector.BeAttacked(25);
                Die();
            }
        }

        public void Die()
        {
            isDying = true;
            explosion.Play(0);
            explosion.WhenFinished(() =>
            {
                EnemyManager.Remove(this);
            });
        }

        public override void Update()
        {
            WrapY();

            rotation = (float)Math.Atan2(velocity.X, velocity.Y) + Matherizer.ToRadians(90);

            if (!isDying) animation.Update(position, rotation);
            else explosion.Update(position);
            base.Update();
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SetTarget(display);
            ScreenManager.spriteBatch.Begin();
            if (!isDying) animation.Draw();
            else explosion.Draw();
            ScreenManager.spriteBatch.End();
        }

        protected void WrapY()
        {
            if (position.Y > ScreenManager.world.Y + wrapBuffer) position.Y = -wrapBuffer;
            if (position.Y < -wrapBuffer) position.Y = ScreenManager.world.Y + wrapBuffer;
        }

    }
}

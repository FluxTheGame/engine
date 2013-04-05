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

        protected int health = 1;
        protected int wrapBuffer = 50;
        protected float rotation;
        protected AnimSprite animation;
        protected AnimSprite explosion;
        public Schedualizer addDelay;
        public bool isDying = false;


        public Enemy() : base()
        {
            wrapY = true;
            height = 0;
            velocity = Randomizer.RandomVelocity();

            position = new Vector2(Randomizer.RandomFloat(0, ScreenManager.window.X), -wrapBuffer);
            display = Randomizer.RandomDisplay();
            dampening = 0.985f;
            addDelay = new Schedualizer(0, 1, 60);

            Animation[] stateAnimations = {
                new Animation(0, 40, false)
            };
            explosion = new AnimSprite("enemy_explosion", new Point(100, 75), stateAnimations);
            
        }

        public virtual void Activate()
        {
            //Override for enemy-specific activation behavior
        }

        public void BeAttacked(int damage)
        {
            if (!isDying)
            {
                Console.WriteLine("Enemy got attacked, dying");
                Die();
            }
        }

        public void Kamikaze(Collector collector)
        {
            if (!isDying)
            {
                collector.BeAttacked(35);
                Die();
            }
        }

        public void Die()
        {
            Audio.Play("enemy.death", display);

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

            rotation = (float)Math.Atan2(velocity.X, velocity.Y) + MathHelper.ToRadians(90);

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

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

        protected int health = 20;
        protected Vector2 velocity;
        protected Vector2 acceleration;
        protected float dampening = 0.999f;
        protected int wrapBuffer = 50;


        public Enemy() : base()
        {
            wrapY = true;
            velocity = Randomizer.RandomVelocity();
            position = new Vector2(Randomizer.RandomInt(100, 700), Randomizer.RandomInt(100, 700));
        }

        public void BeAttacked()
        {
            health --;
            Console.WriteLine("Got attacked, health: " + health);
            if (health <= 0) Die();
        }

        public void Die()
        {
            EnemyManager.Remove(this);
        }

        public override void Update()
        {
            velocity = Vector2.Add(velocity, acceleration);
            velocity = Vector2.Multiply(velocity, dampening);
            position = Vector2.Add(position, velocity);

            WrapY();

            base.Update();
        }

        protected void WrapY()
        {
            if (position.Y > ScreenManager.world.Y + wrapBuffer) position.Y = -wrapBuffer;
            if (position.Y < -wrapBuffer) position.Y = ScreenManager.world.Y + wrapBuffer;
        }

    }
}

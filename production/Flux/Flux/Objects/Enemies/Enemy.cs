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


        public Enemy() : base()
        {
            wrapY = true;
            velocity = Randomizer.RandomVelocity();
            position = new Vector2(Randomizer.RandomFloat(0, ScreenManager.window.X), Randomizer.RandomFloat(0, ScreenManager.window.Y));
            display = Randomizer.RandomInt(0, 4);
            dampening = 0.985f;
        }

        public void BeAttacked(int damage)
        {
            //health -= damage;
            Console.WriteLine("Enemy got attacked, dying");
            Die();
            //if (health <= 0) Die();
        }

        public void Kamikaze(Collector collector)
        {
            collector.BeAttacked(25);
            Die();
        }

        public void Die()
        {
            EnemyManager.Remove(this);
        }

        public override void Update()
        {
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

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


        public Enemy() : base()
        {
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
            position = Vector2.Add(position, velocity);
            base.Update();
        }

    }
}

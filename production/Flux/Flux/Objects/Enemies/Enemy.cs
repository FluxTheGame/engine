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

        public Enemy() : base()
        {
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

    }
}

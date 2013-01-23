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

        protected int health = 100;

        public Enemy() : base()
        {
        }

        public void BeAttacked()
        {
            Console.WriteLine("I got attacked :(");
        }

        public void Die()
        {

        }

    }
}

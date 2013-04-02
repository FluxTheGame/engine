using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Flux
{
    class EnemyCrazy : Enemy
    {

        protected Schedualizer accelerationSchedule;


        public EnemyCrazy() : base()
        {
            velocity = Randomizer.RandomVelocity(2.0f);
            accelerationSchedule = new Schedualizer(0.0f, 0f, 2f);
            drag = 6f;

            Audio.Play("enemy.spawn2", display);

            Animation[] stateAnimations = {
                new Animation(0, 48)
            };
            animation = new AnimSprite("enemy_bomb", new Point(85, 64), stateAnimations);
        }

        public override void Update()
        {
            if (accelerationSchedule.IsOn())
            {
                acceleration = Randomizer.RandomVelocity(0.05f);
            }

            CheckCollision();

            base.Update();
        }

        protected void CheckCollision()
        {
            CollectorManager.CheckEnemyCollide(this);
        }
    }
}
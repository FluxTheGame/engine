using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flux
{
    class EnemyCrazy : Enemy
    {

        protected Schedualizer accelerationSchedule;

        public EnemyCrazy() : base()
        {
            model = ContentManager.enemyCrazy;
            scale = 0.05f;
            velocity = Randomizer.RandomVelocity(2.0f);
            accelerationSchedule = new Schedualizer(0.0f, 0, 2);
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
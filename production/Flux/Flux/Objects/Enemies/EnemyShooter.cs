using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace Flux
{
    public class EnemyShooter : Enemy
    {

        protected Schedualizer shootSchedule;
        protected Schedualizer accelerationSchedule;
        protected float shootDistance = 300.0f;


        public EnemyShooter() : base()
        {
            shootSchedule = new Schedualizer(1.0f, 5, 10);
            accelerationSchedule = new Schedualizer(0.0f, 3, 6);
            model = ContentManager.enemyShooter;
            scale = 0.07f;
            drag = 6.0f;
        }

        public override void Update()
        {

            if (shootSchedule.IsOn()) {
                float phase = shootSchedule.Phase();
                GridManager.Bloat(position + Vector2.Normalize(velocity) * (shootDistance * phase), 80.0f*phase, 0.3f);
            }

            if (accelerationSchedule.IsOn())
            {
                acceleration = Randomizer.RandomVelocity(0.01f);
            }

            base.Update();
        }

        

    }
}

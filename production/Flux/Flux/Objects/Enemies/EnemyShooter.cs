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
        protected bool wasShooting = false;


        public EnemyShooter() : base()
        {    
            drag = 16.0f;

            Animation[] stateAnimations = {
                new Animation(0, 48),
                new Animation(1, 48, false, 0),
            };
            animation = new AnimSprite("enemy_shooter", new Point(85, 64), stateAnimations);
        }

        public override void Activate()
        {
            Audio.Play("enemy.spawn3", display);
            shootSchedule = new Schedualizer(1.0f, 5f, 10f);
            accelerationSchedule = new Schedualizer(0.0f, 3f, 6f);
            base.Activate();
        }

        public override void Update()
        {
            if (disabled) return;

            if (shootSchedule.IsOn())
            {
                float phase = shootSchedule.Phase();
                GridManager.Bloat(position + Vector2.Normalize(velocity) * (shootDistance * phase), 80.0f * phase, 0.07f, display);
                if (!wasShooting) animation.Play(1);
                wasShooting = true;
            }
            else
            {
                wasShooting = false;
            }

            if (accelerationSchedule.IsOn())
            {
                acceleration = Randomizer.RandomVelocity(0.02f);
            }

            base.Update();
        }

        

    }
}

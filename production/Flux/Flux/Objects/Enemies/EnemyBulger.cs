using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace Flux
{
    public class EnemyBulger : Enemy
    {

        protected Schedualizer bulgeSchedule;
        protected Schedualizer accelerationSchedule;
        protected bool wasBloating = false;


        public EnemyBulger() : base()
        {
            drag = 10.0f;
            Animation[] stateAnimations = {
                new Animation(0, 48),
                new Animation(1, 48, false, 0),
            };
            animation = new AnimSprite("enemy_bloat", new Point(85, 62), stateAnimations);
        }

        public override void Activate()
        {
            bulgeSchedule = new Schedualizer(0.5f, 5f, 10f);
            accelerationSchedule = new Schedualizer(0.0f, 1f, 3f);
            Audio.Play("enemy.spawn1", display);
            base.Activate();
        }

        public override void Update()
        {

            if (bulgeSchedule.IsOn()) {
                GridManager.Bloat(position, 100.0f, 0.1f, display);
                if (!wasBloating) animation.Play(1);
                wasBloating = true;
            }
            else
            {
                wasBloating = false;
            }

            if (accelerationSchedule.IsOn())
            {
                acceleration = Randomizer.RandomVelocity(0.01f);
            }

            base.Update();
        }

    }
}

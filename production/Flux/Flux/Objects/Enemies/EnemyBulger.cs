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


        public EnemyBulger() : base()
        {
            bulgeSchedule = new Schedualizer(0.5f, 5f, 10f);
            accelerationSchedule = new Schedualizer(0.0f, 1f, 3f);
            model = ContentManager.Model("enemy_bulger");
            scale = 0.025f;
            drag = 10.0f;
        }

        public override void Update()
        {

            if (bulgeSchedule.IsOn()) {
                GridManager.Bloat(position, 100.0f, 0.1f);
            }

            if (accelerationSchedule.IsOn())
            {
                acceleration = Randomizer.RandomVelocity(0.01f);
            }

            base.Update();
        }

    }
}

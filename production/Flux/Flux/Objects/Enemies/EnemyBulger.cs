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

        public EnemyBulger() : base()
        {
            bulgeSchedule = new Schedualizer(0.5f, 5, 10);
            model = ContentManager.enemyBulger;
            scale = 0.025f;
            drag = 3.0f;
        }

        public override void Update()
        {

            if (bulgeSchedule.IsOn()) {
                Console.WriteLine(bulgeSchedule.Phase());
                GridManager.Bloat(position, 100.0f, 0.4f);
            }

            base.Update();
        }

    }
}

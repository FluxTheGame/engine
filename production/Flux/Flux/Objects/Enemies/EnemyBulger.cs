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

        private DateTime nextBloat;
        private Double bloatDuration = 0.5f;
        private Vector2 velocity;


        public EnemyBulger() : base()
        {
            model = ContentManager.enemy;
            scale = 0.03f;
            drag = 3.0f;
            position = new Vector2(Randomizer.RandomInt(100, 700), Randomizer.RandomInt(100, 700));
            nextBloat = created;
            AddTime();
            velocity = new Vector2(Randomizer.RandomFloat(-1.0f, 1.0f), Randomizer.RandomFloat(-0.2f, 0.2f));
        }

        public override void Update()
        {

            position = Vector2.Add(position, velocity);

            if (DateTime.Now.CompareTo(nextBloat) > 0) {
                GridManager.Bloat(position, 100.0f, 0.4f);
                if (DateTime.Now.CompareTo(nextBloat.AddSeconds(bloatDuration)) > 0) {
                    AddTime();
                }
            }

            base.Update();
        }

        private void AddTime() {
            nextBloat = nextBloat.AddSeconds(Randomizer.RandomInt(5, 10));
        }

    }
}

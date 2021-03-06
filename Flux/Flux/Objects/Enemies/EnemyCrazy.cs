﻿using System;
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
            drag = 6f;

            Animation[] stateAnimations = {
                new Animation(0, 48)
            };
            animation = new AnimSprite("enemy_bomb", new Point(85, 64), stateAnimations);
        }

        public override void Activate()
        {
            accelerationSchedule = new Schedualizer(0.0f, 0f, 2f);
            Audio.Play("enemy.spawn2", display);
            base.Activate();
        }

        public override void Update()
        {
            if (disabled) return;

            if (accelerationSchedule.IsOn())
            {
                acceleration = Randomizer.RandomVelocity(0.05f);
            }

            base.Update();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flocking_002
{

    public class Boid_old : BasicEntity
    {
        //logic
        private Vector3 acceleration;
        private Vector3 velocity;
        private float r;
        private float maxSpeed;
        private float maxForce;
        private Game game;

        Random random;

        public Boid_old(Model mod, Game game) : base(mod)
        {
            random = new Random();

            this.game = game;

            acceleration = Vector3.Zero;
            velocity = new Vector3(random.Next(-1, 1), random.Next(-1, 1), 0);
            location = new Vector3(game.Window.ClientBounds.Width / 2, game.Window.ClientBounds.Height / 2, 0);
            r = 100.0f;
            maxSpeed = 2;
            maxForce = 0.03f;
        }

        public void Run(List<Boid> boids)
        {
            flock(boids);
            update();
            borders();

            degRot = Math.Max(Math.Min(heading2D(velocity), 30.0f), -30.0f);

            base.Update();
        }
        
        private void flock(List<Boid> list)
        {
            //get the values for each boid behaviour
            Vector3 sep = separation(list);
            Vector3 ali = alignment(list);
            Vector3 coh = cohesion(list);

            //weight the forces
            sep *= 7.5f;
            ali *= 6;
            coh *= 6;

            //apply the forces to the boid's heading
            acceleration += sep;
            acceleration += ali;
            acceleration += coh;
        }

        private void update()
        {
            velocity += acceleration;
            if (velocity.Length() > maxSpeed)
            {
                velocity.Normalize();
                velocity *= maxSpeed;
            }
            // limit speed..
            location += velocity;
            acceleration *= 0;
        }

        private void seek(Vector3 tar)
        {
            acceleration += steer(tar, false);
        }

        private void arrive(Vector3 tar)
        {
            acceleration += steer(tar, true);
        }

        private Vector3 steer(Vector3 tar, bool slowdown)
        {
            Vector3 steer;
            Vector3 desired = tar - location;
            float d = desired.Length();

            if (d > 0)
            {
                desired.Normalize();
                if (slowdown && d < 100)
                    desired *= maxSpeed * (d / 100);
                else
                    desired *= maxSpeed;

                steer = (desired - velocity); /* strange... */
                if (steer.Length() > maxForce)
                {
                    steer.Normalize();
                    steer *= maxForce;
                } 
            } else {
                steer = Vector3.Zero;
            }
            return steer;
        }

        private void borders()
        {
            int width = game.Window.ClientBounds.Width;
            int height = game.Window.ClientBounds.Height;

            if (location.X < -r) location.X = width + r;
            if (location.Y < -r) location.Y = height + r;
            if (location.X > width + r) location.X = -r;
            if (location.Y > height + r) location.Y = -r;
        }

        //Draw may need to be updated and put here but for now left basic
        //wraparound code my be required but can be added later

        private Vector3 separation(List<Boid> list)
        {
            float separateThreshold = r;
            Vector3 steer = Vector3.Zero;
            int count = 0;

            //check other boids to see if they are in range
            foreach(Boid other in list)
            {
                float dist = Vector3.Distance(location, other.location);

                if ((dist > 0) && (dist < separateThreshold))
                {
                    Vector3 difference = location - other.location;
                    difference.Normalize();
                    difference /= dist;
                    steer += difference;
                    count++;
                }

            }

            //average the steer vector by the number of neighbors
            if (count > 0)
            {
                steer /= count;
            }

            if (steer.Length() > 0)
            {
                steer.Normalize();
                steer *= maxSpeed;
                steer -= velocity;
                if (steer.Length() > maxForce)
                {
                    steer.Normalize();
                    steer *= maxForce;
                }
            }

            return steer;

        }//end separation

        private Vector3 alignment(List<Boid> list)
        {
            float alignThreshold = r;
            Vector3 steer = Vector3.Zero;
            int count = 0;

            foreach (Boid other in list)
            {
                float dist = Vector3.Distance(location, other.location);

                if ((dist > 0) && (dist < alignThreshold))
                {
                    steer += other.velocity;
                    count++;
                }
            }

            if (count > 0)
            {
                steer /= count;
            }

            if (steer.Length() > 0)
            {
                steer.Normalize();
                steer *= maxForce;
                steer -= velocity;
                if (steer.Length() > maxForce)
                {
                    steer.Normalize();
                    steer *= maxForce;
                }
            }

            return steer;

        }//end alignment

        private Vector3 cohesion(List<Boid> list)
        {
            float cohesionThreshold = r;
            Vector3 sum = Vector3.Zero;
            int count = 0;

            foreach (Boid other in list)
            {
                float dist = Vector3.Distance(location, other.location);

                if ((dist > 0) && (dist < cohesionThreshold))
                {
                    sum += other.location;
                    count++;
                }
            }

            if (count > 0)
            {
                sum /= count;
                return steer(sum, false);
            }

            return sum;

        }

        float heading2D(Vector3 v)
        {
            float angle = (float)Math.Atan2(-v.Y, v.X);
            return -angle;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flocking_002
{

    class Boid : BasicEntity
    {

        //logic
        public Vector3 position;
        private Vector3 acceleration;
        private Vector3 velocity;
        private float maxSpeed;
        private float maxForce;

        public Boid(Model mod) : base(mod)
        {

            position = Vector3.Zero;
            acceleration = Vector3.Zero;
            velocity = new Vector3(0 , 0 , 1);

            maxSpeed = 2;
            maxForce = 0.03f;

        }//end Boid

        public void Update(List<Boid> boids)
        {

            flock(boids);
            relocate();

            base.Update();

        }//end Update

        private void addAcceleration(Vector3 force)
        {

            acceleration = Vector3.Add(acceleration, force);

        }//addAcceleration

        private void flock(List<Boid> list)
        {

            //get the values for each boid behaviour
            Vector3 sep = separation(list);
            Vector3 ali = alignment(list);
            Vector3 coh = cohesion(list);

            //weight the forces
            sep = Vector3.Multiply(sep, 1);
            ali = Vector3.Multiply(ali, 1);
            coh = Vector3.Multiply(coh, 1);

            //apply the forces to the boid's heading
            addAcceleration(sep);
            addAcceleration(ali);
            addAcceleration(coh);

        }//end flock

        private void relocate()
        {

            velocity = Vector3.Add(velocity, acceleration);

            if (velocity.Length() > maxSpeed)
            {

                velocity.Normalize();
                velocity = Vector3.Multiply(velocity, maxSpeed);

            }

            location = Vector3.Add(location, velocity);

            acceleration = Vector3.Zero;

        }//relocate

        private Vector3 steering(Vector3 tar)
        {

            Vector3 endPoint = Vector3.Zero;
            endPoint = Vector3.Subtract(endPoint, velocity);
            endPoint.Normalize();
            endPoint = Vector3.Multiply(endPoint , maxSpeed);

            Vector3 steer = Vector3.Subtract(endPoint, velocity);
            if (steer.Length() > maxSpeed)
            {

                steer.Normalize();
                steer = Vector3.Multiply(steer, maxSpeed);

            }

            return steer;

        }//end steering

        //Draw may need to be updated and put here but for now left basic

        //wraparound code my be required but can be added later

        private Vector3 separation(List<Boid> list)
        {

            Vector3 sum = Vector3.Zero;
            float separateThreshold = 25f;
            int count = 0;

            //check other boids to see if they are in range
            foreach(Boid boid in list)
            {

                float dist = Vector3.Distance(location, boid.location);

                if ((dist > 0) && (dist < separateThreshold))
                {

                    Vector3 difference = Vector3.Subtract(location, boid.location);
                    difference.Normalize();
                    difference = Vector3.Divide(difference, dist);
                    sum = Vector3.Add(sum, difference);
                    count++;

                }

            }

            //average the steer vector by the number of neighbors
            if (count > 1)
            {

                sum = Vector3.Divide(sum, count);

            }

            if (sum.Length() > 0)
            {

                sum.Normalize();
                sum = Vector3.Multiply(sum, maxSpeed);
                sum = Vector3.Subtract(sum, velocity);

                if (sum.Length() > maxForce)
                {

                    sum.Normalize();
                    sum = Vector3.Multiply(sum, maxForce);

                }

            }

            return sum;

        }//end separation

        private Vector3 alignment(List<Boid> list)
        {

            Vector3 sum = Vector3.Zero;
            float alignThreshold = 50f;
            int count = 0;

            foreach (Boid boid in list)
            {

                float dist = Vector3.Distance(location, boid.location);

                if ((dist > 0) && (dist < alignThreshold))
                {

                    sum = Vector3.Add(sum, boid.velocity);
                    count++;

                }

            }

            if (count > 0)
            {

                sum = Vector3.Divide(sum, (float)count);
                sum.Normalize();
                sum = Vector3.Multiply(sum, maxSpeed);

                Vector3 steer;
                steer = Vector3.Subtract(sum, velocity);

                if (steer.Length() > maxForce)
                {

                    steer.Normalize();
                    steer = Vector3.Multiply(steer, maxForce);

                }

                return steer;

            }
            else
            {

                return Vector3.Zero;

            }

        }//end alignment

        private Vector3 cohesion(List<Boid> list)
        {

            Vector3 sum = Vector3.Zero;
            float cohesionThreshold = 50;
            int count = 0;

            foreach (Boid boid in list)
            {

                float dist = Vector3.Distance(location, boid.location);

                if ((dist > 0) && (dist < cohesionThreshold))
                {

                    sum = Vector3.Add(sum, boid.location);
                    count++;

                }

            }

            if (count > 0)
            {

                sum = Vector3.Divide(sum, count);
                return steering(sum);

            }
            else
            {

                return Vector3.Zero;

            }

        }//end cohesion

    }

}

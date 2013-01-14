using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Flocking_002
{
    public class Boid
    {
        public Vector3 pos;
        Vector3 vel, acc, ali, coh, sep;
        float neighborRadius;
        float maxSpeed = 0.2f;
        float maxForce = 0.1f;
        float rot = 0f;

        Random random = new Random();

        Model model;

        public Boid(Vector3 start)
        {
            pos = start;
            vel = new Vector3(random.Next(-1, 1), random.Next(-1, 1), random.Next(-1, 1));
            acc = Vector3.Zero;
            neighborRadius = 3;
        }

        public void LoadContent(ContentManager content)
        {
            model = content.Load<Model>(@"Models\Chicken_001");
        }

        public void run(List<Boid> list)
        {
            flock(list);
            move();
            checkBounds();
            rot = Math.Max(Math.Min(heading2D(vel), 30.0f), -30.0f);
        }

        private void flock(List<Boid> list)
        {
            ali = alignment(list);
            coh = cohesion(list);
            sep = seperation(list);

            acc += ali * 2.5f;
            acc += coh * 1;
            acc += sep * 1;
        }

        private void move()
        {
            vel += acc;
            vel = limit(vel, maxSpeed);
            pos += vel;
            acc *= 0;
        }

        private void checkBounds()
        {
        }

        public void render(Camera camera)
        {
            

            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            Matrix RotationMatrix = Matrix.CreateLookAt(Vector3.Zero, vel, Vector3.Up);
            RotationMatrix = Matrix.Transpose(RotationMatrix) * Matrix.CreateRotationY(MathHelper.ToRadians(90.0f));

            //draw the model
            //loop through the meshes in the model
            foreach (ModelMesh mesh in model.Meshes)
            {

                //loop through the mesh layers
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.Projection = camera.projection;
                    effect.View = camera.view;
                    effect.World = transforms[mesh.ParentBone.Index] * RotationMatrix * Matrix.CreateScale(0.3f) * Matrix.CreateTranslation(pos);
                }
                //draw the mesh after setting the effects on the mesh layers
                mesh.Draw();
            }
        }

        private Vector3 steer(Vector3 target, bool arrival)
        {
            Vector3 steer = Vector3.Zero;
            if (!arrival)
            {
                steer = target - pos;
                steer = limit(steer, maxForce);
            }
            else
            {
                Vector3 targetOffset = target - pos;
                float distance = targetOffset.Length();
                float rampedSpeed = maxSpeed * (distance / 100);
                float clippedSpeed = Math.Min(rampedSpeed, maxSpeed);
                Vector3 desiredVelocity = targetOffset * (clippedSpeed / distance);
                steer = desiredVelocity - vel;
            }
            return steer;
        }

        private Vector3 avoid(Vector3 target, bool weight)
        {
            Vector3 steer = pos - target;
            return steer;
        }

        private Vector3 alignment(List<Boid> list)
        {
            Vector3 steer = Vector3.Zero;
            int count = 0;

            foreach (Boid other in list)
            {
                float d = Vector3.Distance(pos, other.pos);
                if (d > 0 && d <= neighborRadius)
                {
                    steer += other.vel;
                    count++;
                }
            }

            if (count > 0)
            {
                steer /= count;
                steer = limit(steer, maxForce);
            }

            return steer;
        }

        private Vector3 cohesion(List<Boid> list)
        {
            Vector3 steer = Vector3.Zero;
            Vector3 sum = Vector3.Zero;
            int count = 0;

            foreach (Boid other in list)
            {
                float d = Vector3.Distance(pos, other.pos);
                if (d > 0 && d <= neighborRadius)
                {
                    sum += other.pos;
                    count++;
                }
            }

            if (count > 0)
            {
                sum /= count;
            }

            steer = sum - pos;
            steer = limit(steer, maxForce);
            return steer;
        }

        private Vector3 seperation(List<Boid> list)
        {
            Vector3 steer = Vector3.Zero;
            int count = 0;

            foreach (Boid other in list)
            {
                float d = Vector3.Distance(pos, other.pos);
                if (d > 0 && d <= neighborRadius)
                {
                    steer = pos - other.pos;
                    steer.Normalize();
                    steer /= d;
                    count++;
                }
            }
            return steer;
        }

        private Vector3 limit(Vector3 inVec, float max) 
        {
            if (inVec.Length() > max)
            {
                inVec.Normalize();
                inVec *= max;
            }
            return inVec;
        }

        float heading2D(Vector3 v)
        {
            float angle = (float)Math.Atan2(-v.Y, v.X);
            return -angle;
        }
    }
}

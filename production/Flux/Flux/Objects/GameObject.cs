using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Flux
{
    public class GameObject
    {

        public float scale;
        public int display;
        public Model model;
        public BoundingSphere sphere;

        public Vector2 position;
        public Vector2 velocity;
        public Vector2 acceleration;
        public float maxSpeed = 15f;
        public float dampening = 1f;

        protected DateTime created;
        protected bool wrapY = false;


        public GameObject()
        {
            scale = 0.1f;
            position = ScreenManager.Middle();
            display = 0;
            created = DateTime.Now;
        }

        public virtual void Update()
        {
            CalculateBoundingSphere();
            Constrain();

            velocity = Vector2.Add(velocity, acceleration);
            velocity = Vector2.Multiply(velocity, dampening);
            velocity = Vectorizer.Limit(velocity, maxSpeed);
            position = Vector2.Add(position, velocity);
        }

        public virtual Vector3 Location()
        {
            return ScreenManager.Location(position, display);
        }

        public virtual void Draw()
        {
            Camera camera = ScreenManager.Camera(display);
            Drawer3D.Draw(model, Location(), scale, 1f, camera);
        }


        protected void Constrain()
        {
            if (!wrapY)
            {
                if (position.Y > ScreenManager.world.Y) position.Y = ScreenManager.world.Y;
                if (position.Y < 0) position.Y = 0;
            }
            if (position.X > ScreenManager.window.X)
            {
                display++;
                position.X = 0;
            }
            if (position.X < 0)
            {
                display--;
                position.X = ScreenManager.window.X;
            }
            if (display > 3) display = 0;
            if (display < 0) display = 3;
        }

        protected void CalculateBoundingSphere()
        {
            if (model != null)
            {
                foreach (ModelMesh mesh in model.Meshes)
                {
                    if (sphere.Radius == 0) sphere = mesh.BoundingSphere;
                    else sphere = BoundingSphere.CreateMerged(sphere, mesh.BoundingSphere);
                }
                sphere.Center = Location();
                sphere.Radius *= scale;
            }
        }

        public static float Distance(GameObject one, GameObject two)
        {
            return Vector2.Distance(
                ScreenManager.AdjustedPosition(one.position, one.display), 
                ScreenManager.AdjustedPosition(two.position, two.display)
            );
        }

        public static bool Collides(GameObject one, GameObject two)
        {
            return one.sphere.Intersects(two.sphere);
        }

    }
}

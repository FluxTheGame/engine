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
        public Vector2 position;
        public int display;
        public Model model;
        public Texture2D sprite;
        protected DateTime created;


        public GameObject()
        {
            scale = 0.1f;
            position = ScreenManager.Middle();
            display = 0;
            created = DateTime.Now;
        }

        public virtual void Update()
        {
            if (position.X > ScreenManager.world.X) position.X = 0;
            if (position.X < 0) position.X = ScreenManager.world.X;
            if (position.Y > ScreenManager.world.Y) position.Y = ScreenManager.world.Y;
            if (position.Y < 0) position.Y = 0;
        }

        public virtual Vector3 Location()
        {
            return ScreenManager.Location(position, display);
        }

        public virtual void Draw()
        {
            if (model != null)
                DrawModel();

            else if (sprite != null) 
                DrawSprite();
        }

        protected void DrawModel()
        {
            Camera camera = ScreenManager.Camera(display);
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);

            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.EnableDefaultLighting();
                    effect.Projection = camera.projection;
                    effect.View = camera.view;
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateScale(scale) * Matrix.CreateTranslation(Location());
                }
                mesh.Draw();
            }
        }

        protected void DrawSprite()
        {
            Vector2 offset = new Vector2(sprite.Bounds.Width * 0.5f, sprite.Bounds.Height * 0.5f);
            ScreenManager.spriteBatch.Begin();
            ScreenManager.spriteBatch.Draw(sprite, Vector2.Subtract(position, offset), Color.White);
            ScreenManager.spriteBatch.End();
        }


    }
}

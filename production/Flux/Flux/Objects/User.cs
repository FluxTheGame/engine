using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Flux
{
    public class User : GameObject
    {
        public Collector collector;
        public string username;
        public int id;
        public int points = 0;

        private Vector2 delta;
        private Texture2D sprite;


        public User(string user, int idNumber) : base ()
        {
            delta = Vector2.Zero;
            username = user;
            id = idNumber;
            sprite = ContentManager.user;
            collector = CollectorManager.First();
        }

        public void SetDelta(int x, int y)
        {
            delta = new Vector2((float)x, (float)y);
            delta = Vector2.Divide(delta, 20);
        }

        public override void Update()
        {
            position = Vector2.Add(position, delta);
            base.Update();
        }

        public override void Draw()
        {
            Vector2 offset = new Vector2(sprite.Bounds.Width * 0.5f, sprite.Bounds.Height * 0.5f);
            ScreenManager.spriteBatch.Begin();
            ScreenManager.spriteBatch.Draw(sprite, position, null, Color.White, CollectorAngle() + Matherizer.ToRadians(135f), offset, 1.0f, SpriteEffects.None, 0f);
            DrawUsername();
            ScreenManager.spriteBatch.End();
        }

        protected void DrawUsername()
        {
            Vector2 offset = new Vector2(-90, -50);
            Vector2 fontSize = ContentManager.userfont.MeasureString(username);
            Vector2 boxSize = new Vector2(ContentManager.userBox.Width, ContentManager.userBox.Height);
            Vector2 pos = new Vector2(position.X, position.Y);
            pos += offset;

            ScreenManager.spriteBatch.DrawString(ContentManager.userfont, username, pos - fontSize * 0.5f, Color.White);
            ScreenManager.spriteBatch.Draw(ContentManager.userBox, pos - boxSize * 0.5f, Color.White);
        }

        protected float CollectorAngle() 
        {
            double radians = Math.Atan2((position.Y - collector.position.Y), (position.X - collector.position.X));
            return (float)radians;
        }

    }
}

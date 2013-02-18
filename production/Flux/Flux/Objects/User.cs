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
        public int points = 0; //Points the user has total

        private string gotPoints; //Points notification string
        private Vector2 delta; //Amount to move the user
        private SpriteBatch spriteBatch;

        private Durationizer gotPointsDuration;
        private Durationizer gotBadgeDuration;

        private Texture2D boxSprite;
        private Texture2D badgeSprite;

        private AnimSprite pointerAnim;

        private SpriteFont usernameFont;
        private SpriteFont userpointsFont;

        private Vector2 usernameOffset;
        private Vector2 usernameFontOffset;
        private Vector2 boxOffset;
        private Vector2 badgeOffset;
        private Vector2 pointsOffset;


        public User(string user, int idNumber) : base ()
        {
            delta = Vector2.Zero;
            username = user;
            id = idNumber;
            scale = 1.0f;
            dampening = 0.8f;
            maxSpeed = 20f;

            spriteBatch = ScreenManager.spriteBatch;
            collector = CollectorManager.First(); //For testing cursor pointing

            gotPointsDuration = new Durationizer(3.0f);
            gotBadgeDuration = new Durationizer(4.0f);

            usernameFont = ContentManager.Font("user_name");
            userpointsFont = ContentManager.Font("user_points");
            boxSprite = ContentManager.Sprite("user_box1");

            //Animations
            Animation[] animations = {
                new Animation(0, 10, 1),
                new Animation(1, 10)
            };
            pointerAnim = new AnimSprite("test_spritesheet", new Point(45, 36), animations);

            SetupOffsets();
        }

        public void SetDelta(int x, int y)
        {
            delta = new Vector2((float)x, (float)y);
            delta = Vector2.Divide(delta, 20);
        }

        public override void Update()
        {
            velocity = Vector2.Add(velocity, Vector2.Multiply(delta, 0.3f));
            base.Update();
            pointerAnim.Update(position, CollectorAngle());
        }

        public void GetPoints(int value) 
        {
            points += value;
            gotPoints = "+" + value.ToString();
            gotPointsDuration.Fire();
        }

        public void GetBadge(string type)
        {
            badgeSprite = ContentManager.Sprite("user_badge_"+type);
            gotBadgeDuration.Fire();
            GetPoints(674); //TEMPORARY
        }

        public override void Draw()
        {
            spriteBatch.Begin();
                //spriteBatch.Draw(pointerSprite, position, null, Color.White, CollectorAngle() + Matherizer.ToRadians(135f), pointerOffset, scale, SpriteEffects.None, 0f);
                DrawUsername();
                DrawNotifications();
                DrawPointsRing();
            spriteBatch.End();
        }

        protected void DrawUsername()
        {
            spriteBatch.DrawString(usernameFont, username, position + usernameOffset - usernameFontOffset, Color.White);
            spriteBatch.Draw(boxSprite, position + usernameOffset - boxOffset, Color.White);
        }

        protected void DrawNotifications()
        {
            if (gotPointsDuration.IsOn())
                spriteBatch.DrawString(userpointsFont, gotPoints, position + pointsOffset, Color.White);

            if (gotBadgeDuration.IsOn())
                spriteBatch.Draw(badgeSprite, position + badgeOffset, Color.White);
        }

        protected void DrawPointsRing()
        {
            pointerAnim.Draw();
        }

        protected void SetupOffsets()
        {
            usernameOffset = new Vector2(-90, -50) * scale;
            badgeOffset = new Vector2(65, -40) * scale;
            pointsOffset = new Vector2(55, 10) * scale;

            boxOffset = new Vector2(boxSprite.Width, boxSprite.Height) * 0.5f;
            usernameFontOffset = usernameFont.MeasureString(username) * 0.5f;
        }

        protected float CollectorAngle() 
        {
            double radians = Math.Atan2((position.Y - collector.position.Y), (position.X - collector.position.X));
            return (float)radians;
        }

    }
}

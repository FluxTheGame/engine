﻿using System;
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
        private AnimSprite stateAnim;

        private SpriteFont usernameFont;
        private SpriteFont userpointsFont;

        private Vector2 usernameOffset;
        private Vector2 usernameFontOffset;
        private Vector2 boxOffset;
        private Vector2 badgeOffset;
        private Vector2 pointsOffset;

        enum Pointers { Enter, Alert, Exit };
        enum States { BloatStart, Bloat, BloatEnd, PinchStart, Pinch, PinchEnd, Idle };
        enum Actions { Idling, Bloating, Pinching }

        private int action;


        public User(string user, int idNumber) : base ()
        {
            delta = Vector2.Zero;
            username = user;
            id = idNumber;
            scale = 1.0f;
            dampening = 0.8f;
            maxSpeed = 20f;
            action = (int)Actions.Idling;

            spriteBatch = ScreenManager.spriteBatch;
            collector = CollectorManager.First(); //For testing cursor pointing

            gotPointsDuration = new Durationizer(3.0f);
            gotBadgeDuration = new Durationizer(4.0f);

            usernameFont = ContentManager.Font("user_name");
            userpointsFont = ContentManager.Font("user_points");
            boxSprite = ContentManager.Sprite("user_box1");

            SetupOffsets();


            //Animations
            Animation[] pointerAnimations = {
                new Animation((int)Pointers.Enter, 45, (int)Pointers.Alert),
                new Animation((int)Pointers.Alert, 12),
                new Animation((int)Pointers.Exit, 14),
            };
            pointerAnim = new AnimSprite("user_pointer", new Point(87, 87), pointerAnimations);

            Animation[] stateAnimations = {
                new Animation((int)States.BloatStart, 6, (int)States.Bloat),
                new Animation((int)States.Bloat, 9),
                new Animation((int)States.BloatEnd, 6, (int)States.Idle),
                new Animation((int)States.PinchStart, 6, (int)States.Pinch),
                new Animation((int)States.Pinch, 10),
                new Animation((int)States.PinchEnd, 6, (int)States.Idle),
                new Animation((int)States.Idle, 1),
            };
            stateAnim = new AnimSprite("user_bloat_pinch", new Point(75, 75), stateAnimations);
            stateAnim.Play((int)States.Idle);
        }

        public void SetDelta(int x, int y)
        {
            delta = new Vector2((float)x, (float)y);
            delta = Vector2.Divide(delta, 20);
        }

        public override void Update()
        {
            velocity = Vector2.Add(velocity, Vector2.Multiply(delta, 0.3f));
            ApplyAction();
            base.Update();

            pointerAnim.Update(position, CollectorAngle() + Matherizer.ToRadians(135f));
            stateAnim.Update(position);
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
        }

        public void BloatStart()
        {
            action = (int)Actions.Bloating;
            stateAnim.Play((int)States.BloatStart);
        }

        public void PinchStart()
        {
            action = (int)Actions.Pinching;
            stateAnim.Play((int)States.PinchStart);
        }

        public void BloatEnd()
        {
            action = (int)Actions.Idling;
            stateAnim.Play((int)States.BloatEnd);
        }

        public void PinchEnd()
        {
            action = (int)Actions.Idling;
            stateAnim.Play((int)States.PinchEnd);
        }

        public override void Draw()
        {
            spriteBatch.Begin();
                pointerAnim.Draw();
                stateAnim.Draw();
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
            
        }

        protected void ApplyAction()
        {
            if (action == (int)Actions.Bloating) {
                GridManager.Bloat(position, 80.0f, 0.025f);

            } else if (action == (int)Actions.Pinching) {
                GridManager.Pinch(position, 80.0f, 0.025f);
            }     
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

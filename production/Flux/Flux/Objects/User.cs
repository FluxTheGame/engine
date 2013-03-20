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

        private Vector2 delta; //Amount to move the user
        private SpriteBatch spriteBatch;

        private Texture2D badgeSprite;
        private Texture2D teamRing;

        private AnimSprite pointerAnim;
        private AnimSprite stateAnim;
        private AnimSprite pointsAnim;

        private SpriteFont usernameFont;
        private SpriteFont userpointsFont;

        private Vector2 usernameOffset;
        private Vector2 usernameFontOffset;
        private Vector2 teamRingOffset;

        private Notification pointsNotification;
        private Notification badgeNotification;

        private Durationizer pointsAbsorbDelay;

        private string gotPoints;
        private int pointsBuffer = 0;
        private int pointsAbsorbRate = 1;

        enum Pointers { Alert, Exit, Enter, Idle };
        enum States { Pinch, BloatStart, BloatEnd, Bloat, PinchStart, PinchEnd, Idle };
        enum Actions { Idling, Bloating, Pinching }

        private int action;


        public User(string user, int userId, int teamId) : base ()
        {
            delta = Vector2.Zero;
            username = user;
            id = userId;
            scale = 1.0f;
            dampening = 0.8f;
            maxSpeed = 20f;
            action = (int)Actions.Idling;

            spriteBatch = ScreenManager.spriteBatch;
            collector = CollectorManager.CollectorByID(teamId);
            collector.AddUser(this);

            pointsNotification = new Notification(2.5f);
            badgeNotification = new Notification(3f);
            pointsAbsorbDelay = new Durationizer(0.5f);

            usernameFont = ContentManager.Font("user_name");
            userpointsFont = ContentManager.Font("user_points");
            teamRing = ContentManager.Sprite("user_team");

            SetupOffsets();
            SetupAnimations();
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
            AbsorbPoints();

            base.Update();

            pointerAnim.Update(position, CollectorAngle() + Matherizer.ToRadians(45f));
            pointsAnim.Update(position);
            stateAnim.Update(position);
            pointsNotification.Update(position);
            badgeNotification.Update(position);
        }

        public void GetPoints(int value) 
        {
            pointsNotification.Fire();
            pointsAbsorbDelay.Fire();
            pointsBuffer += value;
        }

        public void GetBadge(string type)
        {
            badgeSprite = ContentManager.Sprite("user_badge_"+type);
            badgeNotification.Fire();
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

        public void Alert()
        {
            pointerAnim.Play((int)Pointers.Alert);
        }

        public void Disconnect()
        {
            pointerAnim.WhenFinished(() =>
            {
                UserManager.Remove(this);
            });
            pointerAnim.Play((int)Pointers.Exit);
            collector = null;
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SetTarget(display);
            spriteBatch.Begin();
                pointerAnim.Draw();
                stateAnim.Draw();
                DrawTeamRing();
                DrawUsername();
                DrawNotifications();
                DrawPointsRing();
            spriteBatch.End();
        }

        protected void DrawTeamRing()
        {
            if (collector != null) spriteBatch.Draw(teamRing, position - teamRingOffset, collector.teamColour);
            else spriteBatch.Draw(teamRing, position - teamRingOffset, Color.White);
        }

        protected void DrawUsername()
        {
            Vector2 fontLocation = position + usernameOffset - usernameFontOffset;
            spriteBatch.DrawString(usernameFont, username, fontLocation, Color.White);
        }

        protected void DrawNotifications()
        {
            pointsNotification.DrawPoints(gotPoints, userpointsFont);
            badgeNotification.DrawSprite(badgeSprite);
        }

        protected void DrawPointsRing()
        {
            float ratio = ((float)points / UserManager.highestPoints);
            int frame = (int)(100 * ratio);

            Console.WriteLine(frame);

            if (frame >= 99) frame = 99;
            if (frame >= 50)
            {
                frame -= 50;
                pointsAnim.SetClip(1);
            }
            else pointsAnim.SetClip(0);

            pointsAnim.SetFrame(frame);
            if (collector != null) pointsAnim.Draw(collector.teamColour);
            else pointsAnim.Draw();
        }

        protected void ApplyAction()
        {
            if (action == (int)Actions.Bloating) {
                GridManager.Bloat(position, 80.0f, 0.025f, display);

            } else if (action == (int)Actions.Pinching) {
                GridManager.Pinch(position, 80.0f, 0.025f, display);
            }
        }

        protected void AbsorbPoints()
        {
            gotPoints = "+" + pointsBuffer.ToString();
            if (pointsBuffer > 0 && !pointsAbsorbDelay.IsOn())
            {
                if (pointsBuffer >= pointsAbsorbRate)
                {
                    points += pointsAbsorbRate;
                    pointsBuffer -= pointsAbsorbRate;
                }
                else
                {
                    points += pointsBuffer;
                    pointsBuffer = 0;
                }
            }
        }

        protected void SetupAnimations()
        {
            Animation[] pointerAnimations = {
                new Animation((int)Pointers.Alert, 6, false, (int)Pointers.Idle),
                new Animation((int)Pointers.Exit, 14, false),
                new Animation((int)Pointers.Enter, 43, false, (int)Pointers.Idle),
                new Animation((int)Pointers.Idle, 1),
            };
            pointerAnim = new AnimSprite("user_pointer", new Point(70, 70), pointerAnimations);
            pointerAnim.Play((int)Pointers.Enter);

            Animation[] pointsAnimations = { 
                new Animation(0, 50),
                new Animation(1, 50)
            };
            pointsAnim = new AnimSprite("user_points", new Point(70, 70), pointsAnimations);
            pointsAnim.SetFrame(26);

            Animation[] stateAnimations = {
                new Animation((int)States.Pinch, 8),
                new Animation((int)States.BloatStart, 4, false, (int)States.Bloat),
                new Animation((int)States.BloatEnd, 4, false, (int)States.Idle),
                new Animation((int)States.Bloat, 8),
                new Animation((int)States.PinchStart, 4, false, (int)States.Pinch),
                new Animation((int)States.PinchEnd, 4, false, (int)States.Idle),
                new Animation((int)States.Idle, 1),
            };
            stateAnim = new AnimSprite("user_bloat_pinch", new Point(70, 70), stateAnimations);
            stateAnim.Play((int)States.Idle);
        }

        protected void SetupOffsets()
        {
            usernameOffset = new Vector2(-40, -50) * scale;
            badgeNotification.offset = new Vector2(47, -38) * scale;
            pointsNotification.offset = new Vector2(42, 10) * scale;
            usernameFontOffset = usernameFont.MeasureString(username) * 0.5f;
            teamRingOffset = new Vector2(teamRing.Width * 0.5f, teamRing.Height * 0.5f);
        }

        protected float CollectorAngle() 
        {
            if (collector != null)
            {
                Vector2 adjustedUserPos = ScreenManager.AdjustedPosition(position, display);
                Vector2 adjustedCollPos = ScreenManager.AdjustedPosition(collector.position, collector.display);

                Vector2 difference = adjustedUserPos - adjustedCollPos;

                if (Math.Abs(difference.X) > ScreenManager.window.X * 2)
                {
                    if (difference.X < 0) 
                        difference.X += ScreenManager.world.X;

                    else if (difference.X >= 0) 
                        difference.X -= ScreenManager.world.X;
                }

                double radians = Math.Atan2((difference.Y), (difference.X));
                return (float)radians;
            }
            return 0f;
        }
    }
}

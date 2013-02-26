using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Flux
{
    public class Notification
    {
        public Vector2 offset;
        private float opacity;
        private Vector2 position;
        private Durationizer duration;

        private float opacityStep;
        private Vector2 offsetStep;

        public Notification(float duration)
        {
            this.duration = new Durationizer(duration);
            this.opacityStep = 0.08f;
            this.offsetStep = new Vector2(0.75f, 0);
        }

        public void Fire() 
        {
            duration.Fire();
        }

        public void Update(Vector2 position)
        {
            this.position = position;

            if (duration.IsOn())
            {
                if (opacity < 1)
                {
                    offset += offsetStep;
                    opacity += opacityStep;
                }
            }
            else
            {
                if (opacity > 0)
                {
                    offset -= offsetStep;
                    opacity -= opacityStep;
                }
            }
        }

        public void DrawPoints(string value, SpriteFont font)
        {
            if (value != null)
            {
                ScreenManager.spriteBatch.DrawString(font, value, position + offset, Color.White * opacity);
            }
        }

        public void DrawSprite(Texture2D sprite)
        {
            if (sprite != null)
            {
                ScreenManager.spriteBatch.Draw(sprite, position + offset, Color.White * opacity);
            }
        }

    }
}

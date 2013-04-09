using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Flux
{
    public class Wormhole : GameObject
    {

        public AnimSet wormholeAnim;
        public bool inward;
        public bool dying = false;

        enum States { BloatIntro, BloatStatic, BloatOutro, SuckIntro, SuckStatic, SuckOutro };

        public Wormhole(bool suck, Vector2 position, int display) : base()
        {
            inward = suck;
            scale = 0.08f;
            this.position = position;
            this.display = display;
            SetupAnimations();
        }

        public void Collapse()
        {
            if (!dying)
            {
                dying = true;
                if (inward) wormholeAnim.Play((int)States.SuckOutro);
                else wormholeAnim.Play((int)States.BloatOutro);
            }
        }

        public override void Update()
        {
            if (inward) 
                GridManager.Pinch(position, 100, 0.005f, display);
            else
                GridManager.Bloat(position, 100, 0.005f, display);

            wormholeAnim.Update(position);

            base.Update();
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SetTarget(display);
            ScreenManager.spriteBatch.Begin();
            wormholeAnim.Draw();
            ScreenManager.spriteBatch.End();
        }

        protected void SetupAnimations()
        {
            wormholeAnim = new AnimSet(WormholeAnimations());
            if (inward) wormholeAnim.Play((int)States.SuckIntro);
            else wormholeAnim.Play((int)States.BloatIntro);
        }

        public static Spritesheet[] WormholeAnimations()
        {
            Spritesheet[] wormholeAnimations = {
                new Spritesheet("wormhole_blow_intro", new Point(400, 400), (int)States.BloatIntro, 39, false, (int)States.BloatStatic, true),
                new Spritesheet("wormhole_blow_static", new Point(400, 400), (int)States.BloatStatic, 75, true, -1, true),
                new Spritesheet("wormhole_blow_outro", new Point(400, 400), (int)States.BloatOutro, 42, false, -1, true),
                new Spritesheet("wormhole_suck_intro", new Point(400, 400), (int)States.SuckIntro, 39, false, (int)States.SuckStatic, true),
                new Spritesheet("wormhole_suck_static", new Point(400, 400), (int)States.SuckStatic, 75, true, -1, true),
                new Spritesheet("wormhole_suck_outro", new Point(400, 400), (int)States.SuckOutro, 42, false, -1, true),
            };
            return wormholeAnimations;
        }
    }
}

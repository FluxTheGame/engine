using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using System.Diagnostics;

namespace SmoothGrid
{
 
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;

        Camera camera;

        List<Vector3> points = new List<Vector3>();
        Vector3 pt1, pt2;
        VLine line;

       

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

      
        protected override void Initialize()
        {
            camera = new Camera(this, new Vector3(0, 0, 5), Vector3.Zero, Vector3.Up);
            Components.Add(camera); 

            VLine.Effect = new BasicEffect(GraphicsDevice);

            pt1 = new Vector3(0,  0.66f, 0);
            pt2 = new Vector3(0, -0.66f, 0);

            points.Add(new Vector3(0, 1.8f, 0));
            points.Add(pt1);
            points.Add(pt2);
            points.Add(new Vector3(0, -1.8f, 0));

            base.Initialize();
        }

    
    
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            pt1.X += 0.01f;
            pt2.X -= 0.01f;
            points[1] = pt1;
            points[2] = pt2;

            line = new VLine(points, graphics.GraphicsDevice, 0.1f, 5000);
            line.curve();
            line.stroke();

            base.Update(gameTime);
        }

     
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            line.draw(camera);

            base.Draw(gameTime);
        }
    }
}

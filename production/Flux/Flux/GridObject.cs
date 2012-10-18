using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace Flux
{
    class GridObject
    {

        Vector2 position;
        Texture2D icon;
        VectorField vf;


        public GridObject(Texture2D ico, Vector2 pos, VectorField vectorField)
        {
            position = pos;
            icon = ico;
            vf = vectorField;
        }

        public void Update()
        {
            Vector2 force = vf.getForceAtPosition(position, 100.0f);
            position = Vector2.Add(position, Vector2.Divide(force, 5.0f));
            vf.addForceCircle(position, 40.0f, 0.025f, false);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(icon, position, Color.White);
        }

    }
}

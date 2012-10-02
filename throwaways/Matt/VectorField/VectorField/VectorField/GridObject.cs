using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;



namespace VectorField
{
    class GridObject
    {

        Vector2 position;
        Texture2D icon;


        public GridObject(Texture2D ico, Vector2 pos)
        {
            position = pos;
            icon = ico;

           /* Vector2 force = vf.getForceAtPosition(objectPos, 100.0f);
            objectPos = Vector2.Add(objectPos, Vector2.Divide(force, 5.0f));
            vf.addForceCircle(objectPos, 40.0f, 0.1f, false);*/
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(icon, position, Color.White);
        }

    }
}

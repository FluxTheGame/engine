using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SmoothGrid
{
    class VLine
    {
        VertexPositionColor[] verts;
        BasicEffect effect;

        public float Stroke;
        private Vector3 Start, End;
        private GraphicsDevice Graphics;

        public VLine(Vector3 start, Vector3 end, GraphicsDevice graphics, float stroke = 1f)
        {
            Stroke = stroke;
            Start = start;
            End = end;
            Graphics = graphics;

            Vector3 lineVec = End - Start;
            Vector3 perp = Vector3.Cross(lineVec, new Vector3(0, 0, 1));
            perp.Normalize();
            perp *= stroke;

            verts = new VertexPositionColor[4];

            // TODO: Offset initial draw by stroke/2 to center line
            verts[0] = new VertexPositionColor(Start, Color.Red);
            verts[1] = new VertexPositionColor(Start - perp, Color.Red);
            verts[2] = new VertexPositionColor(End, Color.Yellow);
            verts[3] = new VertexPositionColor(End - perp, Color.Yellow);

            effect = new BasicEffect(graphics);
        }

        public void draw(Camera camera)
        {
            effect.World = Matrix.Identity;
            effect.View = camera.view;
            effect.Projection = camera.projection;
            effect.VertexColorEnabled = true;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                Graphics.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, verts, 0, 2);
            }
        }


    }
}

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
        public List<Vector3> points = new List<Vector3>();
        public static BasicEffect Effect;

        public float Stroke;
        public int Resolution;
        private GraphicsDevice Graphics;
        Color color;

        /*******
         * Example:
         * 
         * List<Vector3> points = {new Vector3(0, 2, 0), new Vector3(0, 0, 0), new Vector3(0, -2, 0)};
         * line = new VLine(points, graphics.GraphicsDevice, 0.1f, 5000);
         * line.curve();
         * line.stroke();
         * 
         * line.draw(camera);
         * 
         * */


        public VLine(List<Vector3> points, GraphicsDevice graphics, float stroke = 1f, int resolution = 1000)
        {
            Stroke = stroke;
            Resolution = resolution;
            Graphics = graphics;
            color = Color.Red;

            this.points.Clear();
            this.points = points;

            // TODO: Offset initial draw by stroke/2 to center line
        }

        public void stroke()
        {
            List<Vector3> stroked = new List<Vector3>();

            for (int i = 0; i < points.Count; i++)
            {
                stroked.Add(points[i]);

                int nextIndex = (i + 1) % points.Count;

                Vector3 perp = Vector3.Cross(points[nextIndex] - points[i], new Vector3(0, 0, 1));
                perp.Normalize();
                perp *= Stroke;

                stroked.Add(points[i] - perp);
            }

            points = stroked;
        }

        public void curve()
        {
            List<Vector3> curvePts = new List<Vector3>();

            for (int i=0; i<points.Count; i++) {
                Vector3 p1 = points[i - 1 < 0 ? points.Count - 1 : i - 1];
                Vector3 p2 = points[i];
                Vector3 p3 = points[(i + 1) % points.Count];
                Vector3 p4 = points[(i + 2) % points.Count];

                float distance = Vector3.Distance(p2, p3);
                int numOfIters = (int)(Resolution * distance / 100.0f);
                if (numOfIters <= 0)
                    numOfIters = 1;

                for (int iter = 0; iter < numOfIters; iter++)
                {
                    Vector3 newVertex = Vector3.CatmullRom(p1, p2, p3, p4, iter / (float)numOfIters);
                    curvePts.Add(newVertex);
                }
            }

            points = curvePts;
        }

        public void draw(Camera camera)
        {
            VLine.Effect.World = Matrix.Identity;
            VLine.Effect.View = camera.view;
            VLine.Effect.Projection = camera.projection;
            VLine.Effect.VertexColorEnabled = true;

            List<VertexPositionColor> vpc = new List<VertexPositionColor>();

            int count = 0;

            foreach(Vector3 point in points) {
                if (count % 3 == 0) color = Color.Red;
                else color = Color.Purple;

                vpc.Add(new VertexPositionColor(point, color));
                count++;
            }

            foreach (EffectPass pass in VLine.Effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                Graphics.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, vpc.ToArray(), 0, points.Count-2);
            }
        }


    }
}

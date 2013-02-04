using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Flux
{
    class VLine
    {
        public List<Vector3> points = new List<Vector3>();
        public static BasicEffect Effect;

        public float stroke;
        public int resolution;
        public Color color;


        /*******
         * Example:
         * 
         * List<Vector3> points = {new Vector3(0, 2, 0), new Vector3(0, 0, 0), new Vector3(0, -2, 0)};
         * line = new VLine(points, 0.1f, 5000);
         * line.curve();
         * line.stroke();
         * 
         * line.draw([int] display);
         * 
         * */


        public VLine(List<Vector3> points, float stroke = 1f, int resolution = 1000)
        {
            this.stroke = stroke;
            this.resolution = resolution;
            color = Color.White;

            this.points.Clear();
            this.points = points;

            // TODO: Offset initial draw by stroke/2 to center line
        }

        public void Stroke()
        {
            List<Vector3> stroked = new List<Vector3>();

            for (int i = 0; i < points.Count; i++)
            {
                stroked.Add(points[i]);

                int nextIndex = (i + 1) % points.Count;

                Vector3 perp = Vector3.Cross(points[nextIndex] - points[i], Vector3.UnitZ);
                perp.Normalize();
                perp *= stroke;

                stroked.Add(points[i] - perp);
            }

            points = stroked;
        }

        public void Curve()
        {
            List<Vector3> curvePts = new List<Vector3>();

            for (int i = 0; i < points.Count; i++)
            {
                int minIndex = i - 1 < 0 ? i : i - 1;
                int midIndex = i + 1 >= points.Count ? i : i + 1;
                int maxIndex = i + 2 >= points.Count ? midIndex : i + 2;

                Vector3 p1 = points[minIndex];
                Vector3 p2 = points[i];
                Vector3 p3 = points[midIndex];
                Vector3 p4 = points[maxIndex];

                float distance = Vector3.Distance(p2, p3);
                int numOfIters = (int)(resolution * distance / 100.0f);
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

        public void Draw(int display)
        {
            Camera camera = ScreenManager.Camera(display);

            GraphicsDevice graphics = ScreenManager.Graphics(display);
            RasterizerState rs = new RasterizerState();
            rs.CullMode = CullMode.None;
            graphics.RasterizerState = rs;
            graphics.BlendState = BlendState.AlphaBlend;

            VLine.Effect.World = Matrix.Identity;
            VLine.Effect.View = camera.view;
            VLine.Effect.Projection = camera.projection;
            VLine.Effect.VertexColorEnabled = true;
            VLine.Effect.Alpha = 0.6f;

            List<VertexPositionColor> vpc = new List<VertexPositionColor>();

            int count = 0;

            foreach (Vector3 point in points)
            {
                vpc.Add(new VertexPositionColor(point, color));
                count++;
            }

            if (count > 0)
            {
                foreach (EffectPass pass in VLine.Effect.CurrentTechnique.Passes)
                {
                    pass.Apply();
                    graphics.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, vpc.ToArray(), 0, points.Count - 2);
                }
            }
        }
    }
}

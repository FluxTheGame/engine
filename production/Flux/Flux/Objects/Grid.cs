using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Flux
{
    public class Grid
    {

        //Wormhole creation threshold
        float wormholeThreshold = 2.2f;

        //Regular grid
        List<Vector2> field = new List<Vector2>();
        Vector2 fieldSize;
        Vector2 externalSize;
        Vector2 scaleFieldToScreen;
        int fieldLength;
        int resolution = 500;
        float thickness = 0.01f;

        //Low res copy
        Vector2 lowResFieldSize;
        int lowResScale = 5;
        int lowResResolution = 1000;
        float lowResThickness = 0.025f;
        float lowResMaxForce = 20f;

        float drawDampening = 2f;

        public int display;


        public Grid(int windowX, int windowY, int fieldX, int fieldY)
        {
            externalSize.X = windowX;
            externalSize.Y = windowY;
            fieldSize.X = fieldX;
            fieldSize.Y = fieldY;
            fieldLength = fieldX * fieldY;

            scaleFieldToScreen = Vector2.Divide(externalSize, fieldSize);

            //Low res
            lowResFieldSize = new Vector2(
                (float)Math.Ceiling(fieldSize.X / lowResScale),
                (float)Math.Ceiling(fieldSize.Y / lowResScale));


            for (int i = 0; i < fieldLength; i++)
            {
                Vector2 item = Vector2.Zero;
                field.Add(item);
            }
        }


        /*
         * Adds forces in the matrix within a circle
         */
        public void AddForceCircle(Vector2 pos, float radius, float strength, bool inward)
        {
            float fieldRadius = ConvertScreenToFieldRadius(radius);
            Vector2 fieldPos = ConvertScreenToFieldPos(pos);
            Vector2[] range = GetRangeFromRadius(fieldPos, radius);

            for (int i = (int)range[0].X; i < range[1].X; i++) {
                for (int j = (int)range[0].Y; j < range[1].Y; j++) {

                    int index = ConvertCoordinatesToIndex(i, j);
                    float distance = CalculateFieldDistance(fieldPos, i, j);

                    if (distance < fieldRadius)
                    {
                        float percent = 1.0f - (distance / fieldRadius);
                        float strongness = strength * percent;
                        Vector2 unit = new Vector2(
                            (fieldPos.X-i) / distance,
                            (fieldPos.Y-j) / distance
                        );

                        Vector2 force = new Vector2(unit.X * strongness, unit.Y * strongness);
                        if (inward) field[index] = Vector2.Add(field[index], force);
                        else        field[index] = Vector2.Subtract(field[index], force);

                        CheckForces(i, j);
                    }

                }
            }

        }


        public Vector2 GetForceAtPosition(Vector2 pos, float sampleRadius)
        {
            Vector2 force = Vector2.Zero;
            float fieldRadius = ConvertScreenToFieldRadius(sampleRadius);
            Vector2 fieldPos = ConvertScreenToFieldPos(pos);
            Vector2[] range = GetRangeFromRadius(fieldPos, fieldRadius);

            for (int i = (int)range[0].X; i < range[1].X; i++) {
                for (int j = (int)range[0].Y; j < range[1].Y; j++) {

                    int index = ConvertCoordinatesToIndex(i, j);

                    Vector2 screenPos = ConvertFieldToScreenPos(i, j);
                    float distance = CalculateScreenDistance(screenPos, pos);

                    if (distance < sampleRadius)
                    {
                        float strength = 1.0f - (distance / sampleRadius);
                        Vector2 influence = Vector2.Multiply(field[index], strength);
                        force = Vector2.Add(force, influence);
                    }
                }
            }

            return force;
        }



        public void Update()
        {
            for (int i = 0; i < fieldSize.X; i++) {
                for (int j = 0; j < fieldSize.Y; j++) {

                    int index = ConvertCoordinatesToIndex(i, j); //Position in array
                    field[index] = Vector2.Multiply(field[index], 0.995f);
                }
            }
        }


        public void Draw()
        {
            //DrawHighRes();
            DrawLowRes();
        }

        public void DrawHighRes()
        {
            List<Vector3> points = new List<Vector3>();
            List<VLine> lines = new List<VLine>();

            //High res Columns
            for (int i = 0; i < fieldSize.X; i++) {
                for (int j = 0; j < fieldSize.Y; j++) {

                    int index = ConvertCoordinatesToIndex(i, j);
                    Vector2 position = ConvertFieldToScreenPos(i, j);
                    position = Vector2.Add(position, field[index]);
                    points.Add(Location(position));

                    if (points.Count == (int)fieldSize.Y) {
                        lines.Add(new VLine(points, thickness, resolution));
                        points = new List<Vector3>();
                    }
                }
            }

            //High res Rows
            for (int j = 0; j < fieldSize.Y; j++) {
                for (int i = 0; i < fieldSize.X; i++) {

                    int index = ConvertCoordinatesToIndex(i, j);
                    Vector2 position = ConvertFieldToScreenPos(i, j);
                    position = Vector2.Add(position, field[index]);
                    points.Add(Location(position));

                    if (points.Count == (int)fieldSize.X)
                    {
                        lines.Add(new VLine(points, thickness, resolution));
                        points = new List<Vector3>();
                    }
                }
            }

            //Draw all lines
            foreach (VLine line in lines) {
                line.Curve();
                line.Stroke();
                line.Draw(display);
            }
        }


        /*
         * Draw the grid in low resolution using VLine
         */
        public void DrawLowRes()
        {
            List<Vector2> lowRes = CalculateLowResolution();
            List<Vector3> points = new List<Vector3>();
            List<VLine> lines = new List<VLine>();

            //Low res Columns
            for (int i = 0; i < lowResFieldSize.X; i++) {
                for (int j = 0; j < lowResFieldSize.Y; j++) {

                    int index = ConvertLowResCoordinatesToIndex(i, j);
                    points.Add(Location(lowRes[index]));

                    if (points.Count == (int)lowResFieldSize.Y) {
                        lines.Add(new VLine(points, lowResThickness, lowResResolution));
                        points = new List<Vector3>();
                    }
                }
            }

            //Low res Rows
            for (int j = 0; j < lowResFieldSize.Y; j++) {
                for (int i = 0; i < lowResFieldSize.X; i++) {

                    int index = ConvertLowResCoordinatesToIndex(i, j);
                    points.Add(Location(lowRes[index]));

                    if (points.Count == (int)lowResFieldSize.X) {
                        lines.Add(new VLine(points, lowResThickness, lowResResolution));
                        points = new List<Vector3>();
                    }
                }
            }
            
            //Draw all lines
            foreach (VLine line in lines) {
                line.Curve();
                line.Stroke();
                line.Draw(display);
            }
        }



        /* 
         * Get a low resolution version of this grid for displaying
         */
        private List<Vector2> CalculateLowResolution()
        {
            List<Vector2> positions = new List<Vector2>();

            //Loop through low resolution version
            for (int i = 0; i < lowResFieldSize.X; i++) {
                for (int j = 0; j < lowResFieldSize.Y; j++) {

                    //Loop through the high resolution version, but only within the current low-res range
                    Vector2 start = new Vector2(i * lowResScale, j * lowResScale);
                    Vector2 end = new Vector2(
                        Math.Min(start.X + lowResScale, fieldSize.X),
                        Math.Min(start.Y + lowResScale, fieldSize.Y));

                    Vector2 sum = Vector2.Zero;

                    for (int w = (int)start.X; w < end.X; w++) {
                        for (int h = (int)start.Y; h < end.Y; h++) {
                            int index = ConvertCoordinatesToIndex(w, h);
                            sum = Vector2.Add(sum, field[index]);
                        }
                    }

                    Vector2 pos = ConvertLowResFieldToScreenPos(i, j);
                    sum = Vector2.Multiply(sum, drawDampening);
                    sum = Vectorizer.Limit(sum, lowResMaxForce);

                    positions.Add(Vector2.Add(pos, sum));
                }
            }

            return positions;
        }


        private Vector3 Location(Vector2 position) {
            return ScreenManager.Location(position, display);
        }


        /*
        * Checks for wormhole potential at grid position
        */
        private void CheckForces(int i, int j) 
        {

            int index = ConvertCoordinatesToIndex(i, j);

            if (i > 0 && i < fieldSize.X - 1)
            {
                Vector2 forceLeft = field[index - 1];
                Vector2 forceRight = field[index + 1];

                if (forceLeft.Length() > wormholeThreshold && forceRight.Length() > wormholeThreshold)
                {
                    Vector2 position = ConvertFieldToScreenPos(i, j);

                    if (forceLeft.X < 0 && forceRight.X > 0)
                    {
                        //Add outward wormhole
                        WormholeManager.Add(position, false, display);

                    } else if (forceLeft.X > 0 && forceRight.X < 0)
                    {
                        //Add inward wormhole
                        WormholeManager.Add(position, true, display);
                    }
                }
            }
        }


        private int ConvertCoordinatesToIndex(int i, int j)
        {
            return j * (int)fieldSize.X + i;
        }

        private int ConvertLowResCoordinatesToIndex(int i, int j)
        {
            return i * (int)lowResFieldSize.Y + j;
        }

        /*
        * Converts screen coordinates to fieldPos coordinates
        */
        private Vector2 ConvertScreenToFieldPos(Vector2 screenPos)
        {
            Vector2 scale = Vector2.Divide(screenPos, externalSize);
            Vector2 fieldPos = Vector2.Multiply(scale, fieldSize);

            fieldPos.X = Math.Max(0, Math.Min(fieldPos.X, fieldSize.X - 1));
            fieldPos.Y = Math.Max(0, Math.Min(fieldPos.Y, fieldSize.Y - 1));

            return fieldPos;
        }


        private Vector2 ConvertLowResFieldToScreenPos(int i, int j)
        {
            Vector2 pos = ConvertFieldToScreenPos(i, j);
            pos = Vector2.Multiply(pos, lowResScale);
            Vector2 displayOffset = new Vector2(scaleFieldToScreen.X * (lowResScale / 2 + 1), scaleFieldToScreen.Y * (lowResScale / 2 + 1));
            pos = Vector2.Add(pos, displayOffset);
            return pos;
        }

        private Vector2 ConvertFieldToScreenPos(int i, int j)
        {
            Vector2 pos = new Vector2(scaleFieldToScreen.X * i, scaleFieldToScreen.Y * j);
            return pos;
        } 


        /*
         * Converts screen radius to fieldRadius float
         */
        private float ConvertScreenToFieldRadius(float radius)
        {
            float radiusScale = radius / externalSize.X;
            float fieldRadius = (float)(radiusScale * fieldSize.X);
            return fieldRadius;
        } 

        /*
         * Calculate the distance in a loop away from a given fieldPos
         */
        private float CalculateFieldDistance(Vector2 fieldPos, int i, int j)
        {
            float distance = (float)Math.Sqrt(
                (fieldPos.X - i) * (fieldPos.X - i) +
                (fieldPos.Y - j) * (fieldPos.Y - j)
            );

            if (distance < 0.0001) distance = 0.0001f;
            return distance;
        } 

        private float CalculateScreenDistance(Vector2 pos1, Vector2 pos2)
        {
            float distance = Vector2.Distance(pos1, pos2);
            if (distance < 0.0001) distance = 0.0001f;
            return distance;
        } 

        /*
         * Gets the start and end field positions given a fieldPos and radius
         */
        private Vector2[] GetRangeFromRadius(Vector2 fieldPos, float fieldRadius)
        {

            Vector2[] range = new Vector2[2];

            range[0] = new Vector2(
                Math.Max(fieldPos.X - fieldRadius, 0),
                Math.Max(fieldPos.Y - fieldRadius, 0)
            );

            range[1] = new Vector2(
                Math.Min(fieldPos.X + fieldRadius, fieldSize.X),
                Math.Min(fieldPos.Y + fieldRadius, fieldSize.Y)
            );

            return range;
        }


    }
}

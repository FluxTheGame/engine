using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Flux
{
    public class Grid
    {

        //Regular grid
        List<Vector2> field = new List<Vector2>();
        Vector2 fieldSize;
        Vector2 externalSize;
        Vector2 scaleFieldToScreen;
        int fieldLength;

        //Low res copy
        Vector2 lowResFieldSize;
        int lowResScale;

        
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
            lowResScale = 10;
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


        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {

            for (int i=0; i<fieldSize.X; i++) {
                for (int j=0; j<fieldSize.Y; j++) {

                    int index = ConvertCoordinatesToIndex(i, j); //Position in array
                    Vector2 pos = new Vector2(scaleFieldToScreen.X * i + field[index].X, scaleFieldToScreen.Y * j + field[index].Y);
                    spriteBatch.DrawString(font, Math.Round(field[index].X).ToString(), pos, Color.CornflowerBlue);
                }
            }


            DrawLowResolution(spriteBatch);
        }


        /* 
         * Get a low resolution version of this grid for displaying
         */
        public void DrawLowResolution(SpriteBatch spriteBatch)
        {
           
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

                    Vector2 pos = new Vector2(scaleFieldToScreen.X * i * lowResScale + sum.X, scaleFieldToScreen.Y * j * lowResScale + sum.Y);
                    Vector2 displayOffset = new Vector2(scaleFieldToScreen.X * (lowResScale / 2 + 1), scaleFieldToScreen.Y * (lowResScale / 2 + 1));
                    spriteBatch.Draw(ContentManager.enemy, Vector2.Add(pos, displayOffset), Color.White);
                }
            }
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

                if (forceLeft.Length() > 4 && forceRight.Length() > 4)
                {
                    Vector2 position = ConvertFieldToScreenPos(i, j);

                    if (forceLeft.X < 0 && forceRight.X > 0)
                    {
                        //Add outward wormhole
                        WormholeManager.Add(position, false);

                    } else if (forceLeft.X > 0 && forceRight.X < 0)
                    {
                        //Add inward wormhole
                        WormholeManager.Add(position, true);
                    }
                }
            }
        }


        private int ConvertCoordinatesToIndex(int i, int j)
        {
            return j * (int)fieldSize.X + i;
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

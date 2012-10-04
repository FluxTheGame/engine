using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Flux
{
    public class VectorField
    {

        List<Vector2> field = new List<Vector2>();

        Vector2 fieldSize;
        Vector2 externalSize;
        Vector2 scaleFieldToScreen;

        int fieldLength;


        public VectorField(int windowX, int windowY, int fieldX, int fieldY)
        {
            externalSize.X = windowX;
            externalSize.Y = windowY;
            fieldSize.X = fieldX;
            fieldSize.Y = fieldY;
            fieldLength = fieldX * fieldY;

            scaleFieldToScreen = Vector2.Divide(externalSize, fieldSize);

            for (int i = 0; i < fieldLength; i++)
            {
                Vector2 item = Vector2.Zero;
                field.Add(item);
            }

            
        } //end VectorField()


        /*
         * Adds forces in the matrix within a circle
         */
        public void addForceCircle(Vector2 pos, float radius, float strength, bool inward)
        {
            float fieldRadius = convertScreenToFieldRadius(radius);
            Vector2 fieldPos = convertScreenToFieldPos(pos);
            Vector2[] range = getRangeFromRadius(fieldPos, radius);

            for (int i = (int)range[0].X; i < range[1].X; i++) {
                for (int j = (int)range[0].Y; j < range[1].Y; j++) {

                    int index = j * (int)fieldSize.X + i;
                    float distance = calculateFieldDistance(fieldPos, i, j);

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
                    }

                }
            }

        } //end addForceCircle()


        public Vector2 getForceAtPosition(Vector2 pos, float sampleRadius)
        {
            Vector2 force = Vector2.Zero;
            float fieldRadius = convertScreenToFieldRadius(sampleRadius);
            Vector2 fieldPos = convertScreenToFieldPos(pos);
            Vector2[] range = getRangeFromRadius(fieldPos, fieldRadius);

            for (int i = (int)range[0].X; i < range[1].X; i++) {
                for (int j = (int)range[0].Y; j < range[1].Y; j++) {

                    int index = j * (int)fieldSize.X + i;

                    Vector2 screenPos = convertFieldToScreenPos(i, j);
                    float distance = calculateScreenDistance(screenPos, pos);

                    if (distance < sampleRadius)
                    {
                        float strength = 1.0f - (distance / sampleRadius);
                        Vector2 influence = Vector2.Multiply(field[index], strength);
                        force = Vector2.Add(force, influence);
                    }
                }
            }

            return force;

           // int index = (int)fieldPos.Y * (int)fieldSize.X + (int)fieldPos.X;

           // return field[index];
        } //end getForceAtPosition()





        public void Update()
        {
            for (int i = 0; i < fieldSize.X; i++) {
                for (int j = 0; j < fieldSize.Y; j++) {

                    int index = j * (int)fieldSize.X + i; //Position in array
                    field[index] = Vector2.Multiply(field[index], 0.995f);
                }
            }
        } //end Update()


        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            
            for (int i=0; i<fieldSize.X; i++) {
                for (int j=0; j<fieldSize.Y; j++) {

                    int index = j * (int)fieldSize.X + i; //Position in array
                    Vector2 pos = new Vector2(scaleFieldToScreen.X * i + field[index].X, scaleFieldToScreen.Y * j + field[index].Y);
                    
                    spriteBatch.DrawString(font, Math.Round(field[index].X).ToString(), pos, Color.CornflowerBlue);
                    
                }
            }
        } //end Draw()





        /*
        * Converts screen coordinates to fieldPos coordinates
        */
        private Vector2 convertScreenToFieldPos(Vector2 screenPos)
        {
            Vector2 scale = Vector2.Divide(screenPos, externalSize);
            Vector2 fieldPos = Vector2.Multiply(scale, fieldSize);

            fieldPos.X = Math.Max(0, Math.Min(fieldPos.X, fieldSize.X - 1));
            fieldPos.Y = Math.Max(0, Math.Min(fieldPos.Y, fieldSize.Y - 1));

            return fieldPos;
        } //end convertScreenToFieldPos()


        private Vector2 convertFieldToScreenPos(int i, int j)
        {
            Vector2 pos = new Vector2(scaleFieldToScreen.X * i, scaleFieldToScreen.Y * j);
            return pos;
        } //end convertFieldToScreenPos()


        /*
         * Converts screen radius to fieldRadius float
         */
        private float convertScreenToFieldRadius(float radius)
        {
            float radiusScale = radius / externalSize.X;
            float fieldRadius = (float)(radiusScale * fieldSize.X);
            return fieldRadius;
        } //end convertScreenToFieldRadius(

        /*
         * Calculate the distance in a loop away from a given fieldPos
         */
        private float calculateFieldDistance(Vector2 fieldPos, int i, int j)
        {
            float distance = (float)Math.Sqrt(
                (fieldPos.X - i) * (fieldPos.X - i) +
                (fieldPos.Y - j) * (fieldPos.Y - j)
            );

            if (distance < 0.0001) distance = 0.0001f;
            return distance;
        } //end calculateFieldDistance()

        private float calculateScreenDistance(Vector2 pos1, Vector2 pos2)
        {
            float distance = Vector2.Distance(pos1, pos2);
            if (distance < 0.0001) distance = 0.0001f;
            return distance;
        } //end calculateScreenDistance()

        /*
         * Gets the start and end field positions given a fieldPos and radius
         */
        private Vector2[] getRangeFromRadius(Vector2 fieldPos, float fieldRadius)
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
        } //end getRangeFromRadius


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace VectorField
{
    class VectorField
    {

        List<Vector2> field = new List<Vector2>();

        Vector2 fieldSize;
        Vector2 externalSize; 

        int fieldLength;


        public VectorField(int windowX, int windowY, int fieldX, int fieldY)
        {
            externalSize.X = windowX;
            externalSize.Y = windowY;
            fieldSize.X = fieldX;
            fieldSize.Y = fieldY;
            fieldLength = fieldX * fieldY;

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

                    float distance = (float)Math.Sqrt(
                        (fieldPos.X-i) * (fieldPos.X-i) +
                        (fieldPos.Y-j) * (fieldPos.Y-j)
                    );

                    if (distance < 0.0001) distance = 0.0001f;

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


        /*
         * Converts screen coordinates to fieldPos coordinates
         */
        private Vector2 convertScreenToFieldPos(Vector2 screenPos) 
        {
            Vector2 scale = Vector2.Divide(screenPos, externalSize);
            Vector2 fieldPos = Vector2.Multiply(scale, fieldSize);
            return fieldPos;
        } //end convertScreenToFieldPos()


        /*
         * Converts screen radius to fieldRadius float
         */
        private float convertScreenToFieldRadius(float radius)
        {
            float radiusScale = radius / externalSize.X;
            float fieldRadius = (float)(radiusScale * fieldSize.X);
            return fieldRadius;
        } //end convertScreenToFieldRadius()


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


        public Vector2 getForceAtPosition(Vector2 pos)
        {
            Vector2 scale = Vector2.Divide(pos, externalSize);
            Vector2 force = Vector2.Zero;

            if (scale.X < 0 || scale.X > 1 || scale.Y < 0 || scale.Y > 1)
            {
                return force;
            }

            Vector2 fieldPos = Vector2.Multiply(scale, fieldSize);

            fieldPos.X = Math.Max(0, Math.Min(fieldPos.X, fieldSize.X - 1));
            fieldPos.Y = Math.Max(0, Math.Min(fieldPos.Y, fieldSize.Y - 1));

            int index = (int)fieldPos.Y * (int)fieldSize.X + (int)fieldPos.X;

            return field[index];
        } //end getForceAtPosition()


        public void Update()
        {
            for (int i = 0; i < fieldSize.X; i++) {
                for (int j = 0; j < fieldSize.Y; j++) {

                    int index = j * (int)fieldSize.X + i; //Position in array
                    field[index] = Vector2.Multiply(field[index], 0.999f);
                }
            }
        } //end Update()


        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            Vector2 scale = Vector2.Divide(externalSize, fieldSize);
            
            for (int i=0; i<fieldSize.X; i++) {
                for (int j=0; j<fieldSize.Y; j++) {

                    int index = j * (int)fieldSize.X + i; //Position in array
                    //Vector2 pos = new Vector2(scale.X * i + field[index].X, scale.Y * j + field[index].Y);
                    Vector2 pos = new Vector2(scale.X * i, scale.Y * j);

                    //spriteBatch.Draw(icon, pos, Color.White);
                    spriteBatch.DrawString(font, Math.Round(field[index].X).ToString(), pos, Color.CornflowerBlue);
                    //spriteBatch.DrawString(font, index.ToString()+" "+Math.Round(field[index].X+field[index].Y).ToString(), pos, Color.CornflowerBlue);
                    //spriteBatch.DrawString(font, index.ToString(), pos, Color.CornflowerBlue);
                }
            }
        } //end Draw()


    }
}

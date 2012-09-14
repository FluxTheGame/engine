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



        public void addForceCircle(Vector2 pos, float radius, float strength, bool inward)
        {
            Vector2 scale = Vector2.Divide(pos, externalSize);
            Vector2 fieldPos = Vector2.Multiply(scale, fieldSize);

            float radiusScale = radius / externalSize.X;
            float fieldRadius = (float)(radiusScale * fieldSize.X);

            Vector2 start = new Vector2(
                Math.Max(fieldPos.X - fieldRadius, 0),
                Math.Max(fieldPos.Y - fieldRadius, 0)
            );

            Vector2 end = new Vector2(
                Math.Min(fieldPos.X + fieldRadius, fieldSize.X),
                Math.Min(fieldPos.Y + fieldRadius, fieldSize.Y)
            );


            for (int i = (int)start.X; i < end.X; i++) {
                for (int j = (int)start.Y; j < end.Y; j++) {

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



        public void Update()
        {
            for (int i = 0; i < fieldSize.X; i++) {
                for (int j = 0; j < fieldSize.Y; j++) {

                    int index = j * (int)fieldSize.X + i; //Position in array
                    field[index] = Vector2.Multiply(field[index], 0.98f);
                }
            }
        } //end Update()


        public void Draw(SpriteBatch spriteBatch, Texture2D icon)
        {
            Vector2 scale = Vector2.Divide(externalSize, fieldSize);
            
            for (int i=0; i<fieldSize.X; i++) {
                for (int j=0; j<fieldSize.Y; j++) {

                    int index = j * (int)fieldSize.X + i; //Position in array
                    Vector2 pos = new Vector2(scale.X * i + field[index].X, scale.Y * j + field[index].Y);

                    spriteBatch.Draw(icon, pos, Color.White);
                 
                }
            }
        } //end Draw()


    }
}

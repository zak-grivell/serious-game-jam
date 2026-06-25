using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Godot;

namespace NewGameProject
{
    public static class MathUtils
    {
        public static float VectorToAngle(Vector2 vector)
        {
            float output = MathF.Atan2(vector.Y, vector.X) + MathF.PI / 2; //ANGLES ARE FROM THE vertical

            if (output > 0)
            {
                return output;
            }
            else return output + Mathf.Pi * 2;
        }

        public static float CubicEasing(float x)
        {
            return x * x * x;
        }

        public static Vector2 RotateVectorClockwise(Vector2 input, float angle) //angle counterclockwise
        {
            return new Vector2(input.X * MathF.Cos(angle) - input.Y * MathF.Sin(angle),
                            input.X * MathF.Sin(angle) + input.Y * MathF.Cos(angle)); //perform rotation
        }

        public static Vector2 Lerp(Vector2 a, Vector2 b, float interp)
        {
            float x = Mathf.Lerp(a.X, b.X, interp);
            float y = Mathf.Lerp(a.Y, b.Y, interp);

            return new Vector2(x, y);
        }
    }
}

using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewGameProject.main
{
    public static class Maths
    {
        public static Vector2 Lerp(Vector2 start, Vector2 end, float t)
        {
            float retX = Mathf.Lerp(start.X, end.Y, t);
            float retY = Mathf.Lerp(start.Y, end.Y, t);
            return new Vector2(retX, retY);
        }
    }
}

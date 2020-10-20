using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace OneButtonGame
{
    public class Rectangle
    {
        public Vector2 Origin { get { return new Vector2(X, Y); } set { X = value.X; Y = value.Y; } }
        public float X;
        public float Y;
        public float Width;
        public float Height;

        public Rectangle()
        {
            X = 0;
            Y = 0;
            Width = 10;
            Height = 10;
        }
        public Rectangle(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace OneButtonGame
{
    public class Transform
    {
        public Vector2 Position;
        public Vector2 Rotation;
        public Vector2 Scale;

        public Transform()
        {
            Position = Vector2.Zero;
            Rotation = Vector2.Zero;
            Scale = Vector2.Zero;
        }
        public Transform(Vector2 position, Vector2 rotation, Vector2 scale)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }
        public Transform(Vector2 position, Vector2 rotation)
        {
            Position = position;
            Rotation = rotation;
            Scale = Vector2.Zero;
        }
        public Transform(Vector2 position)
        {
            Position = position;
            Rotation = Vector2.Zero;
            Scale = Vector2.Zero;
        }
    }
}

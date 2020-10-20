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
    public class HitBox
    {
        private int _trackingID;
        private Rectangle _rect;
        private Transform _transform;
        public Rectangle Rect { get { return offsetRect(); } }
        public int TrackingID
        {
            get { return _trackingID; }
            set { if (_trackingID == -1) _trackingID = value; }
        }
        public Vector2 Origin { get { return new Vector2(_transform.Position.X + _rect.X, _transform.Position.Y + _rect.Y); }}
        public ECollision Type;
        

        public HitBox(Rectangle rect, Transform transform) {
            _trackingID = -1;
            _rect = rect;
            _transform = transform;
            Type = ECollision.obstacle;
        }
        public HitBox(Rectangle rect, Transform transform, ECollision type)
        {
            _trackingID = -1;
            _rect = rect;
            _transform = transform;
            Type = type;
        }

        public bool CheckCollision(HitBox other)
        {
            return CheckCollision(this, other);
        }

        private Rectangle offsetRect()
        {
            return new Rectangle(_transform.Position.X + _rect.X, _transform.Position.Y + _rect.Y, _rect.Width, _rect.Height);
        }

        public static bool CheckCollision(HitBox A, HitBox B)
        {
            Rectangle a = A.Rect;
            Rectangle b = B.Rect;
            return CheckCollision(a, b);
        }
        public static bool CheckCollision(Rectangle A, Rectangle B)
        {
            return 
                (A.X<B.X + B.Width &&
                A.X + A.Width> B.X &&
                A.Y<B.Y + B.Height &&
                A.Y + A.Height> B.Y);
        }
        public static bool CheckCollision(HitBox A, Vector2 Offset, HitBox B)
        {
            Rectangle a = new Rectangle(Offset.X + A.Rect.X, Offset.Y + A.Rect.Y, A.Rect.Width, A.Rect.Height);
            Rectangle b = B.Rect;
            return CheckCollision(a, b);
        }
    }
}

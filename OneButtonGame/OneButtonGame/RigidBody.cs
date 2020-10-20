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
    class RigidBody
    {
        public Vector2 Speed;
        public Vector2 Acceleration;
        public float Mass
        {
            get { return _mass; }
            set { if (value == 0) _mass = 1; else _mass = value; }
        }
        private List<HitBox> HitBoxes;
        public float Friction
        {
            get { return _friction; }
            set { if (value < 0) _friction = 0; else if (value > 1) _friction = 1; else _friction = value; }
        }

        private float _mass;
        private float _friction;



        #region Constructors
        private void init(Vector2 speed, float mass, Vector2 acceleration, List<HitBox> boxes, float friction)
        {
            Speed = speed;
            Mass = mass;
            Acceleration = acceleration;
            HitBoxes = boxes;
            registerHitboxes();
            Friction = friction;
        }
        public RigidBody()
        {
            init(Vector2.Zero, 1, Vector2.Zero, new List<HitBox>(), 0.15f);
        }
        public RigidBody(float mass, float friction)
        {
            init(Vector2.Zero, mass, Vector2.Zero, new List<HitBox>(), friction);
        }
        public RigidBody(float mass, float friction, List<HitBox> hitBoxes)
        {
            init(Vector2.Zero, mass, Vector2.Zero, hitBoxes, friction);
        }
        #endregion

      

        public void Update(GameTime gameTime)
        {
            updateSpeed(gameTime);
            checkColliders();
        }



        public void AddForce(Vector2 force)
        {
            Acceleration += force / Mass;
        }
        private void updateSpeed(GameTime gameTime)
        {
            Speed += Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        private void checkColliders()
        {

        }
        private void registerHitboxes()
        {

        }
    }
}

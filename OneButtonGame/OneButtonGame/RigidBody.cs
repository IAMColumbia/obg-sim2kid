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
    public class RigidBody
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

        private float _gravity;
        private float _mass;
        private float _friction;
        private Game _game;
        private Transform _transform;

        private HitBoxTrackerService _hitTracker;



        #region Constructors
        private void init(Game game, Vector2 speed, float mass, Vector2 acceleration, List<HitBox> boxes, float friction, float gravity, Transform transform)
        {
            _game = game;
            Speed = speed;
            Mass = mass;
            Acceleration = acceleration;
            HitBoxes = boxes;
            Friction = friction;
            _gravity = gravity;
            _transform = transform;

            _hitTracker = (HitBoxTrackerService) _game.Services.GetService(typeof(IHitBoxTrackerService));
            if (_hitTracker == null)
            {
                _hitTracker = new HitBoxTrackerService(_game);
                _game.Services.AddService(_hitTracker);
            }

            registerHitboxes();
            foreach (HitBox box in HitBoxes)
            {
                Vector2 origin = box.Origin;
                origin += _transform.Position;
                box.Origin = origin;
            }
        }
        public RigidBody(Game game, Transform transform)
        {
            init(game, Vector2.Zero, 1, Vector2.Zero, new List<HitBox>(), 0.15f, 9.8f, transform);
        }
        public RigidBody(Game game, Transform transform, float mass, float friction, float gravity)
        {
            init(game, Vector2.Zero, mass, Vector2.Zero, new List<HitBox>(), friction, gravity, transform);
        }
        public RigidBody(Game game, Transform transform, float mass, float friction, float gravity, List<HitBox> hitBoxes)
        {
            init(game, Vector2.Zero, mass, Vector2.Zero, hitBoxes, friction, gravity, transform);
        }
        #endregion

      

        public void Update(GameTime gameTime)
        {
            updateSpeed(gameTime);
            short reflection = checkColliders();
            if ((reflection & 0b01) == 0b01)
            {
                //reflect X
                Speed *= new Vector2(-1, 1);
            }
            if ((reflection & 0b10) == 0b10)
            {
                //reflect Y
                Speed *= new Vector2(1, -1);
            }
            foreach (HitBox box in HitBoxes)
            {
                Vector2 origin = box.Origin;
                origin += Speed;
                box.Origin = origin;
            }
            _transform.Position += Speed;
        }



        public void AddForce(Vector2 force)
        {
            Acceleration += force / Mass;
        }
        private void updateSpeed(GameTime gameTime)
        {
            Speed += Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
            Speed += new Vector2(0, 1) * _gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        private short checkColliders()
        {
            short reflect = 0b00;
            HitBox collider = null;
            HitBox collided = null;
            foreach (HitBox other in _hitTracker.WorldHitBoxes)
            {
                foreach (HitBox box in HitBoxes)
                {
                    if (box.TrackingID == other.TrackingID) { break; }
                    if (HitBox.CheckCollision(box, Speed, other)) 
                    {
                        collider = box;
                        collided = other;
                        break;
                    }
                }
                if (collided != null) { break; }
            }

            

            if (collider != null && collided != null)
            {
                if (HitBox.CheckCollision(collider, new Vector2(Speed.X, 0), collided)) 
                {
                    reflect += 0b01;
                }
                if (HitBox.CheckCollision(collider, new Vector2(0, Speed.Y), collided))
                {
                    reflect += 0b10;
                }
            }
            return reflect;
        }
        private void registerHitboxes()
        {
            _hitTracker.RegisterHitBoxes(HitBoxes);
        }
    }
}

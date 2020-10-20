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
        public List<HitBox> HitBoxes;
        public float Friction
        {
            get { return _friction; }
            set { if (value < 0) _friction = 0; else if (value > 1) _friction = 1; else _friction = value; }
        }
        public float Gravity;
        public bool collisions;

        private float _mass;
        private float _friction;
        private Game _game;
        private Transform _transform;

        private HitBoxTrackerService _hitTracker;



        #region Constructors
        private void init(Game game, Vector2 speed, float mass, Vector2 acceleration, List<HitBox> boxes, float friction, float gravity)
        {
            _game = game;
            Speed = speed;
            Mass = mass;
            Acceleration = acceleration;
            HitBoxes = boxes;
            Friction = friction;
            Gravity = gravity;
            collisions = false;

            _hitTracker = (HitBoxTrackerService)_game.Services.GetService(typeof(IHitBoxTrackerService));
            Console.WriteLine(_hitTracker == null);
            if (_hitTracker == null)
            {
                _hitTracker = new HitBoxTrackerService(_game);
                this._game.Services.AddService((IHitBoxTrackerService)_hitTracker);
            }
        }
        public RigidBody(Game game)
        {
            init(game, Vector2.Zero, 1, Vector2.Zero, new List<HitBox>(), 0.15f, 9.8f);
        }
        public RigidBody(Game game, float mass, float friction, float gravity)
        {
            init(game, Vector2.Zero, mass, Vector2.Zero, new List<HitBox>(), friction, gravity);
        }
        public RigidBody(Game game, float mass, float friction, float gravity, List<HitBox> hitBoxes)
        {
            init(game, Vector2.Zero, mass, Vector2.Zero, hitBoxes, friction, gravity);
        }
        #endregion
        
        public void LoadContent(Transform transform)
        {
            _transform = transform;
            registerHitboxes();
        }
        public void Update(GameTime gameTime)
        {
            updateSpeed(gameTime);
            if (collisions)
            {
                short reflection = checkColliders();
                if (reflection > 0b00)
                {
                    Speed *= 1 - Friction;
                }
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
                if (Speed.LengthSquared() < 0.000001f)
                {
                    Speed = Vector2.Zero;
                }
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
            Speed += new Vector2(0, 1) * Gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
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
                Console.WriteLine(reflect);
            }
            return reflect;
        }
        private void registerHitboxes()
        {
            _hitTracker.RegisterHitBoxes(HitBoxes);
        }
    }
}

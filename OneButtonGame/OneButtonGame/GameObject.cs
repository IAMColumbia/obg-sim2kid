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
    public class GameObject : Sprite
    {
        private Game _game;
        public RigidBody Rigidbody;
        private ECollision _type;
        private List<HitBox> _boxes;

        #region Constructors
        public GameObject(Game game, string textureName, List<HitBox> hitBoxes) : base(game, textureName)
        {
            _game = game;
            _boxes = hitBoxes;
        }

        public GameObject(Game game, string textureName, ECollision type) : base(game, textureName)
        {
            _game = game;
            _type = type;
            _boxes = new List<HitBox>();
        }
        public GameObject(Game game, string textureName) : base(game, textureName)
        {
            _game = game;
            _boxes = new List<HitBox>();
            _type = ECollision.obstacle;
        }
        public GameObject(Game game) : base(game)
        {
            _game = game;
            _boxes = new List<HitBox>();
            _type = ECollision.obstacle;
        }
        #endregion

        public override void Initialize()
        {
            Rigidbody = new RigidBody(_game);
            base.Initialize();
        }
        protected override void LoadContent()
        {
            base.LoadContent();
            if (_boxes.Count < 1)
                _boxes.Add(new HitBox(new Rectangle(transform.Position.X, transform.Position.Y, spriteTexture.Bounds.Width, spriteTexture.Bounds.Height), transform, _type));
            Rigidbody.HitBoxes = _boxes;
            Rigidbody.LoadContent(transform);
        }
        public override void Update(GameTime gameTime)
        {
            Rigidbody.Update(gameTime);
            base.Update(gameTime);
        }
    }
}

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

        private List<HitBox> _boxes;
        
        public GameObject(Game game, string textureName, List<HitBox> hitBoxes) : base(game, textureName)
        {
            _game = game;
            _boxes = hitBoxes;
        }
        public GameObject(Game game, string textureName) : base(game, textureName)
        {
            _game = game;
            _boxes = new List<HitBox>();
        }
        public GameObject(Game game) : base(game)
        {
            _game = game;
            _boxes = new List<HitBox>();
        }
        public override void Initialize()
        {
            Rigidbody = new RigidBody(_game);
            base.Initialize();
        }
        protected override void LoadContent()
        {
            base.LoadContent();
            if (_boxes.Count < 1)
                _boxes.Add(new HitBox(new Rectangle(transform.Position.X, transform.Position.Y, spriteTexture.Bounds.Width, spriteTexture.Bounds.Height), transform));
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

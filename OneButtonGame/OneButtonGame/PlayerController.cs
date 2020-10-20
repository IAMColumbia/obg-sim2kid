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
    public class PlayerController : Sprite
    {
        public EPlayerState PlayerState;

        private JumpHandler _jumper;
        private RigidBody _rigidBody;

        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;

        private Game _game;

        public PlayerController(Game game) : base(game, "slime")
        {
            _game = game;
            _jumper = new JumpHandler(this, game);
        }



        public override void Initialize()
        {
            graphics = (GraphicsDeviceManager)Game.Services.GetService(typeof(IGraphicsDeviceManager));
            base.Initialize();
            List<HitBox> boxes = new List<HitBox>();
            boxes.Add(new HitBox(new Rectangle(transform.Position.X, transform.Position.Y, spriteTexture.Bounds.Width, spriteTexture.Bounds.Height)));
            _rigidBody = new RigidBody(_game, transform, 10f, 0.5f, 20f, boxes);
        }
        public override void Update(GameTime gameTime)
        {
            _rigidBody.Update(gameTime);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }



        public void PrepareJump()
        {

        }
        public void Jump()
        {

        }
        public void Land()
        {

        }
        public void Die()
        {

        }
    }

    public enum EPlayerState
    {
        idle,
        jumping,
        falling,
        landing,
        dying
    }
}

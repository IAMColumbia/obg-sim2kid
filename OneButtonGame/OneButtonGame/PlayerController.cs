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
        public EPlayerState State;

        private JumpHandler _jumper;
        private RigidBody _rigidBody; //TODO: Reduce player down to a GameObject

        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;

        private Game _game;

        public PlayerController(Game game) : base(game, "slime")
        {
            _game = game;
            _jumper = new JumpHandler(this, game);
            State = EPlayerState.falling;
        }



        public override void Initialize()
        {
            graphics = (GraphicsDeviceManager)Game.Services.GetService(typeof(IGraphicsDeviceManager));
            _rigidBody = new RigidBody(_game, 10f, 0.5f, 20f);
            _rigidBody.collisions = true;
            base.Initialize();
        }
        protected override void LoadContent()
        {
            List<HitBox> boxes = new List<HitBox>();
            base.LoadContent();
            boxes.Add(new HitBox(new Rectangle(transform.Position.X, transform.Position.Y, spriteTexture.Bounds.Width, spriteTexture.Bounds.Height), transform));
            _rigidBody.HitBoxes = boxes;
            _rigidBody.LoadContent(transform);
            
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
            State = EPlayerState.jumping;
        }
        public void Jump()
        {
            State = EPlayerState.falling;
        }
        public void Land()
        {
            State = EPlayerState.landing;
            State = EPlayerState.idle;
        }
        public void Die()
        {
            State = EPlayerState.dying;
            State = EPlayerState.dead;
        }
    }

    public enum EPlayerState
    {
        idle,
        jumping,
        falling,
        landing,
        dying,
        dead
    }
}

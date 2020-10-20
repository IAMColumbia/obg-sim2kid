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
    class PlayerController : Sprite
    {
        public EPlayerState PlayerState;

        private JumpHandler _jumper;
        private RigidBody _rigidBody;

        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;



        public PlayerController(Game game) : base(game)
        {
            _jumper = new JumpHandler(this, game);
        }



        public override void Initialize()
        {
            graphics = (GraphicsDeviceManager)Game.Services.GetService(typeof(IGraphicsDeviceManager));
            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
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

    enum EPlayerState
    {
        idle,
        jumping,
        falling,
        landing,
        dying
    }
}

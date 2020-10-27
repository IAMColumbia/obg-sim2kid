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
        private KeyboardHandler _keyboard;
        public RigidBody _rigidBody; //TODO: Reduce player down to a GameObject
        private SpriteFont text;

        private Game _game;

        public PlayerController(Game game) : base(game, "slime")
        {
            _game = game;
            State = EPlayerState.falling;
        }

        public void Reset(Vector2 position) 
        {
            transform.Position = position;
            _rigidBody.Reset();
        }

        public override void Initialize()
        {
            _jumper = new JumpHandler(this.Game, this);
            _jumper.Initialize();
            this.Game.Components.Add(_jumper);
            _rigidBody = new RigidBody(_game, 1f, 0.5f, 20f);
            _rigidBody.collisions = true;
            _keyboard = new KeyboardHandler();
            base.Initialize();
        }
        protected override void LoadContent()
        {
            List<HitBox> boxes = new List<HitBox>();
            base.LoadContent();
            boxes.Add(new HitBox(new Rectangle(transform.Position.X, transform.Position.Y, spriteTexture.Bounds.Width, spriteTexture.Bounds.Height), transform));
            _rigidBody.HitBoxes = boxes;
            _rigidBody.LoadContent(transform);
            _jumper.playerOrgin = new Vector2(this.spriteTexture.Bounds.Width / 2, this.spriteTexture.Bounds.Height / 2);
            transform.Position = new Vector2(25, 200);
            text = this.Game.Content.Load<SpriteFont>("font");
        }
        public override void Update(GameTime gameTime)
        {
            _keyboard.Update();

            switch (State)
            {
                case EPlayerState.falling:
                    _rigidBody.Update(gameTime);
                    if (_rigidBody.SmoothedSpeed < 0.1f)
                    {
                        Land();
                    }
                    if ((_rigidBody.CollisionFlags & (int)ECollision.spikes) == (int)ECollision.spikes)
                    {
                        Die();
                    }
                    if ((_rigidBody.CollisionFlags & (int)ECollision.goal) == (int)ECollision.goal)
                    {
                        Win();
                    }
                    break;
                case EPlayerState.idle:
                    if (_keyboard.onKeyDown(Keys.Space))
                    {
                        PrepareJump();
                    }
                    break;
                case EPlayerState.jumping:
                    if (_keyboard.onKeyUp(Keys.Space))
                    {
                        Jump();
                    }
                    break;
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            switch (State)
            {
                case EPlayerState.dead:
                    spriteBatch.Begin();
                    spriteBatch.DrawString(text, "YOU LOSE!!!", new Vector2(50, 50), Color.DarkRed, 0, Vector2.Zero, 10, SpriteEffects.None, 0);
                    spriteBatch.End();
                    break;
                case EPlayerState.win:
                    spriteBatch.Begin();
                    spriteBatch.DrawString(text, "YOU WIN!!!", new Vector2(50, 50), Color.DarkGreen, 0, Vector2.Zero, 10, SpriteEffects.None, 0);
                    spriteBatch.End();
                    break;
            }
        }


        public void PrepareJump()
        {
            State = EPlayerState.jumping;
        }
        public void Jump()
        {
            _jumper.Hide();
            _rigidBody.AddForce(_jumper.Launch());
            State = EPlayerState.falling;
        }
        public void Land()
        {
            State = EPlayerState.landing;
            State = EPlayerState.idle;
            _jumper.Show();
        }
        public void Die()
        {
            State = EPlayerState.dying;
            State = EPlayerState.dead;
        }
        public void Win()
        {
            State = EPlayerState.win;
        }
    }

    public enum EPlayerState
    {
        idle,
        jumping,
        falling,
        landing,
        dying,
        dead,
        win
    }
}

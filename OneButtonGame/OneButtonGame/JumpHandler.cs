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
    public class JumpHandler : DrawableGameComponent
    {
        private PlayerController _player;
        private Texture2D _dash;
        private Texture2D _arrow;
        private Vector2 _force;
        private Vector4 _forceLimits;

        private float[] previousGameTimes;

        public Vector2 playerOrgin;
        public EJumpHandler State;

        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;

        public JumpHandler(Game game, PlayerController pc) : base(game)
        {
            _player = pc;
        }
        public override void Initialize()
        {
            _forceLimits = new Vector4(-5, -25, 1, 10);
            _force = new Vector2(5, 0);
            State = EJumpHandler.hidden;
            graphics = (GraphicsDeviceManager)Game.Services.GetService(typeof(IGraphicsDeviceManager));
            previousGameTimes = new float[50];
            for (int i = 0; i < previousGameTimes.Length; i++)
            {
                previousGameTimes[i] = 0;
            }
            base.Initialize();
        }
        protected override void LoadContent()
        {
            _dash = this.Game.Content.Load<Texture2D>("dash");
            _arrow = this.Game.Content.Load<Texture2D>("arrow");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            switch (_player.State)
            {
                case EPlayerState.jumping:
                    _force.Y -= 4f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    _force.X += 1.8f * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    if (_force.Y < _forceLimits.Y)
                    {
                        _force.Y = _forceLimits.Y;
                    }
                    if (_force.X > _forceLimits.W)
                    {
                        _force.X = _forceLimits.W;
                    }
                    break;
            }

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            switch (State)
            {
                case EJumpHandler.shown:
                    float gametimeTotal = 0;
                    for (int i = 0; i < previousGameTimes.Length - 1; i++)
                    {
                        previousGameTimes[i] = previousGameTimes[i + 1];
                        gametimeTotal += previousGameTimes[i];
                    }
                    gametimeTotal +=
                        previousGameTimes[previousGameTimes.Length - 1] = (float) gameTime.ElapsedGameTime.TotalSeconds;
                    

                    spriteBatch.Begin();
                    drawArc(_player.transform.Position + _player.Origin, (_force / _player._rigidBody.Mass), _player._rigidBody.Gravity, gametimeTotal / (float)previousGameTimes.Length);
                    spriteBatch.End();
                    break;
            }
            
            base.Draw(gameTime);
        }

        private void drawArc(Vector2 start, Vector2 launchForce, float gravity, float gameTime)
        {
            float distance = 5;
            int count = 0;
            Vector2 pos = start + playerOrgin;
            Vector2 speed = launchForce;
            while (pos.Y <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height &&
                pos.X <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width &&
                pos.X >= 0 && count < 200 * distance)
            {
                speed += (new Vector2(0, 1) * gravity) * gameTime;

                pos += speed;

                if(count % distance == 0)
                    spriteBatch.Draw(_dash, pos, Color.White);

                count++;
            }
        }

        public Vector2 Launch()
        {
            Hide();
            return _force;
        }

        public void Show()
        {
            _force.Y = _forceLimits.X;
            _force.X = _forceLimits.Z;
            State = EJumpHandler.showing;
            State = EJumpHandler.shown;
        }
        public void Hide()
        {
            State = EJumpHandler.hiding;
            State = EJumpHandler.hidden;
        }
    }

    public enum EJumpHandler
    {
        hidden,
        showing,
        shown,
        hiding
    }
}

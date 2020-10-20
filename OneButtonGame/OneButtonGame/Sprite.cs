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
    public class Sprite : DrawableGameComponent
    {
        public Transform transform;
        public Vector2 Origin;

        public Color color;

        public Texture2D spriteTexture;
        public string textureName;

        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;



        public Sprite(Game game) : base(game)
        {
            textureName = "";
        }
        public Sprite(Game game, string texture) : base(game)
        {
            textureName = texture;
        }



        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            transform = new Transform();
            Origin = Vector2.Zero;
            graphics = (GraphicsDeviceManager)Game.Services.GetService(typeof(IGraphicsDeviceManager));
            color = Color.White;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteTexture = this.Game.Content.Load<Texture2D>(textureName);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(this.spriteTexture, this.transform.Position - this.Origin, color);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

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
    public class LevelManager : DrawableGameComponent
    {
        //private int _level;
        public GameObject ground, ceiling, lWall, rWall, piller1, piller2, platform1, platform2, dspike1, dspike2, dspike3, uspike1, uspike2, goal;
        public SpriteFont font;
        private Game _game;

        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;

        public LevelManager(Game game) : base(game)
        {
            _game = game;
            ground = new GameObject(_game, "floor"); // tons of technical debt
            ceiling = new GameObject(_game, "floor");
            lWall = new GameObject(_game, "wall");
            rWall = new GameObject(_game, "wall");
            piller1 = new GameObject(_game, "piller");
            piller2 = new GameObject(_game, "piller");
            platform1 = new GameObject(_game, "platform");
            platform2 = new GameObject(_game, "platform");
            dspike1 = new GameObject(_game, "spikesdown", ECollision.spikes);
            dspike2 = new GameObject(_game, "spikesdown", ECollision.spikes);
            dspike3 = new GameObject(_game, "spikesdown", ECollision.spikes);
            uspike1 = new GameObject(_game, "spikesup", ECollision.spikes);
            uspike2 = new GameObject(_game, "spikesup", ECollision.spikes);
            goal = new GameObject(_game, "goal", ECollision.goal);

            _game.Components.Add(ground);
            _game.Components.Add(ceiling);
            _game.Components.Add(lWall);
            _game.Components.Add(rWall);
            _game.Components.Add(piller1);
            _game.Components.Add(piller2);
            _game.Components.Add(platform1);
            _game.Components.Add(platform2);
            _game.Components.Add(dspike1);
            _game.Components.Add(dspike2);
            _game.Components.Add(dspike3);
            _game.Components.Add(uspike1);
            _game.Components.Add(uspike2);
            _game.Components.Add(goal);
        }

        public override void Initialize()
        {
            base.Initialize();
            graphics = (GraphicsDeviceManager)Game.Services.GetService(typeof(IGraphicsDeviceManager));
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = this.Game.Content.Load<SpriteFont>("font");
            ground.transform.Position = new Vector2(0,700);
            ceiling.transform.Position = new Vector2(0, 0);
            lWall.transform.Position = new Vector2(0, -1440);
            rWall.transform.Position = new Vector2(1260, -1440);
            
            piller1.transform.Position = new Vector2(256, 508);
            piller2.transform.Position = new Vector2(800, 444);
            platform1.transform.Position = new Vector2(928, 168);
            platform2.transform.Position = new Vector2(448, 332);

            dspike1.transform.Position = new Vector2(192, 20);
            dspike2.transform.Position = new Vector2(320, 20);
            dspike3.transform.Position = new Vector2(928, 232);
            uspike1.transform.Position = new Vector2(384, 668);
            uspike2.transform.Position = new Vector2(926, 668);

            goal.transform.Position = new Vector2(1196, 572);
            
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Press Space to Jump!", new Vector2(512, 40), Color.Black, 0, Vector2.Zero, 4, SpriteEffects.None, 0);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}

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
        private int _level;
        Dictionary<string, GameObject> _objects;
        public SpriteFont font;
        private Game _game;

        protected GraphicsDeviceManager graphics;
        protected SpriteBatch spriteBatch;

        public LevelManager(Game game) : base(game)
        {
            _game = game;
            _objects = new Dictionary<string, GameObject>();
        }

        public override void Initialize()
        {
            _level = 1;
            defineLevel();
            
            foreach (KeyValuePair<string, GameObject> entry in _objects)
            {
                _game.Components.Add(entry.Value);
            }

            base.Initialize();
            graphics = (GraphicsDeviceManager)Game.Services.GetService(typeof(IGraphicsDeviceManager));
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = this.Game.Content.Load<SpriteFont>("font");

            loadLevel();

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Press Space to Jump!", new Vector2(512, 40), Color.Black, 0, Vector2.Zero, 4, SpriteEffects.None, 0);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void defineLevel() 
        {
            switch (_level) 
            {
                case 1:
                    _objects.Add("ground", new GameObject(_game, "floor"));
                    _objects.Add("ceiling", new GameObject(_game, "floor"));
                    _objects.Add("lWall", new GameObject(_game, "wall"));
                    _objects.Add("rWall", new GameObject(_game, "wall"));
                    _objects.Add("piller1", new GameObject(_game, "piller"));
                    _objects.Add("piller2", new GameObject(_game, "piller"));
                    _objects.Add("platform1", new GameObject(_game, "platform"));
                    _objects.Add("platform2", new GameObject(_game, "platform"));

                    _objects.Add("dspike1", new GameObject(_game, "spikesdown", ECollision.spikes));
                    _objects.Add("dspike2", new GameObject(_game, "spikesdown", ECollision.spikes));
                    _objects.Add("dspike3", new GameObject(_game, "spikesdown", ECollision.spikes));
                    _objects.Add("uspike1", new GameObject(_game, "spikesup", ECollision.spikes));
                    _objects.Add("uspike2", new GameObject(_game, "spikesup", ECollision.spikes));

                    _objects.Add("goal", new GameObject(_game, "goal", ECollision.goal));
                    break;
            }
        }

        private void loadLevel()
        {
            switch (_level)
            {
                case 1:
                    _objects["ground"].transform.Position = new Vector2(0, 700);
                    _objects["ceiling"].transform.Position = new Vector2(0, 0);
                    _objects["lWall"].transform.Position = new Vector2(0, -1440);
                    _objects["rWall"].transform.Position = new Vector2(1260, -1440);

                    _objects["piller1"].transform.Position = new Vector2(256, 508);
                    _objects["piller2"].transform.Position = new Vector2(800, 444);
                    _objects["platform1"].transform.Position = new Vector2(928, 168);
                    _objects["platform2"].transform.Position = new Vector2(448, 332);

                    _objects["dspike1"].transform.Position = new Vector2(192, 20);
                    _objects["dspike2"].transform.Position = new Vector2(320, 20);
                    _objects["dspike3"].transform.Position = new Vector2(928, 232);
                    _objects["uspike1"].transform.Position = new Vector2(384, 668);
                    _objects["uspike2"].transform.Position = new Vector2(926, 668);

                    _objects["goal"].transform.Position = new Vector2(1196, 572);
                    break;
            }
        }

        private void unloadLevel() 
        {
            foreach (KeyValuePair<string, GameObject> entry in _objects)
            {
                _game.Components.Remove(entry.Value);
            }
            _objects = new Dictionary<string, GameObject>();
        }
    }
}

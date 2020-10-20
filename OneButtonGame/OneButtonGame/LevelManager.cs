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
        public GameObject ground;
        private Game _game;

        public LevelManager(Game game) : base(game)
        {
            _game = game;
            ground = new GameObject(_game, "floor");
            _game.Components.Add(ground);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            ground.Rigidbody.Gravity = 0;
            ground.transform.Position = new Vector2(0,600);

            base.LoadContent();
        }
    }
}

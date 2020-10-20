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
    public class JumpHandler
    {
        private PlayerController _player;
        private Sprite _dash;
        private Sprite _arrow;
        private Vector2 force;

        public EJumpHandler State;

        public JumpHandler(PlayerController pc, Game game)
        {
            _player = pc;
            _dash = new Sprite(game);
            _arrow = new Sprite(game);
            force = Vector2.Zero;
            State = EJumpHandler.hidden;
        }

        public void Update()
        {

        }

        public Vector2 Launch()
        {
            Hide();
            return force;
        }

        public void Show()
        {
            State = EJumpHandler.shown;
        }
        public void Hide()
        {
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

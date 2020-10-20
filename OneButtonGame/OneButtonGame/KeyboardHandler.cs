using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneButtonGame
{
    class KeyboardHandler
    {
        KeyboardState lastState;
        KeyboardState keyboardState;

        public KeyboardHandler()
        {
            lastState = Keyboard.GetState();
            Update();
        }

        public void Update()
        {
            lastState = keyboardState;
            keyboardState = Keyboard.GetState();
        }

        public bool onKeyDown(Keys key)
        {
            return !lastState.IsKeyDown(key) && keyboardState.IsKeyDown(key);
        }
        public bool onKeyUp(Keys key)
        {
            return lastState.IsKeyDown(key) && !keyboardState.IsKeyDown(key);
        }
        public bool onKeyHeldDown(Keys key)
        {
            return lastState.IsKeyDown(key) && keyboardState.IsKeyDown(key);
        }

        public bool onKeyHeldUp(Keys key)
        {
            return !lastState.IsKeyDown(key) && !keyboardState.IsKeyDown(key);
        }
    }
}

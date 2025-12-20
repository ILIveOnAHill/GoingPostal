using Microsoft.Xna.Framework.Input;

namespace GoingPostal.Core.Input
{
    public class KeyboardInput
    {
        private KeyboardState _current;
        private KeyboardState _previous;

        public void Update()
        {
            _previous = _current;
            _current = Keyboard.GetState();
        }

        public bool IsDown(Keys key) =>
            _current.IsKeyDown(key);

        public bool IsUp(Keys key) =>
            _current.IsKeyUp(key);

        public bool WasPressed(Keys key) =>
            _current.IsKeyDown(key) && _previous.IsKeyUp(key);

        public bool WasReleased(Keys key) =>
            _current.IsKeyUp(key) && _previous.IsKeyDown(key);
    }
}

using Microsoft.Xna.Framework.Input;

namespace GoingPostal.Core.Input
{
    public class InputManager
    {
        public KeyboardInput Keyboard { get; private set; }
        public MouseState Mouse { get; private set; }

        public InputManager()
        {
            Keyboard = new KeyboardInput();
        }

        public void Update()
        {
            Keyboard.Update();
        }

        public static MouseState GetMouseState()
        {
            return Microsoft.Xna.Framework.Input.Mouse.GetState();
        }
    }
}

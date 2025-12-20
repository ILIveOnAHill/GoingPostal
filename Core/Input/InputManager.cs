namespace GoingPostal.Core.Input
{
    public class InputManager
    {
        public KeyboardInput Keyboard { get; private set; }

        public InputManager()
        {
            Keyboard = new KeyboardInput();
        }

        public void Update()
        {
            Keyboard.Update();
        }
    }
}

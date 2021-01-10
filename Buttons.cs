using Microsoft.Xna.Framework.Input;

namespace EMReallyifs
{
    public static class Buttons
    {
        public static MouseState MouseState
        {
            get;
            private set;
        }

        public static MouseState OldMouseState
        {
            get;
            private set;
        }

        internal static void Update()
        {
            if (MouseState == null)
                MouseState = Mouse.GetState();
            OldMouseState = MouseState;
            MouseState = Mouse.GetState();
        }

        public static bool LeftPressed() => OldMouseState.LeftButton == ButtonState.Released && MouseState.LeftButton == ButtonState.Pressed;

        public static bool LeftReleased() => OldMouseState.LeftButton == ButtonState.Pressed && MouseState.LeftButton == ButtonState.Released;

        public static bool MiddlePressed()
        {
            return OldMouseState.MiddleButton == ButtonState.Released && MouseState.MiddleButton == ButtonState.Pressed;
        }

        public static bool MiddleReleased()
        {
            return OldMouseState.MiddleButton == ButtonState.Pressed && MouseState.MiddleButton == ButtonState.Released;
        }

        public static bool RightPressed() => OldMouseState.RightButton == ButtonState.Released && MouseState.RightButton == ButtonState.Pressed;

        public static bool RightReleased() => OldMouseState.RightButton == ButtonState.Pressed && MouseState.RightButton == ButtonState.Released;

        public static bool LeftPressing() => MouseState.LeftButton == ButtonState.Pressed;

        public static bool MiddlePressing() => MouseState.MiddleButton == ButtonState.Pressed;

        public static bool RightPressing() => MouseState.RightButton == ButtonState.Pressed;

        public static bool LeftReleasing() => MouseState.LeftButton == ButtonState.Released;

        public static bool MiddleReleasing() => MouseState.MiddleButton == ButtonState.Released;

        public static bool RightReleasing() => MouseState.RightButton == ButtonState.Released;
    }
}
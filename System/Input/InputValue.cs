using SFML.Window;

namespace Andromeda.System
{
    /// <summary>
    /// An input binding type
    /// </summary>
    public struct InputValue
    {

        /// <summary>
        /// All the number keys (0-9)
        /// </summary>
        public static InputValue[] NumKeys => new InputValue[] {
            Keyboard.Key.Num0,
            Keyboard.Key.Num1,
            Keyboard.Key.Num2,
            Keyboard.Key.Num3,
            Keyboard.Key.Num4,
            Keyboard.Key.Num5,
            Keyboard.Key.Num6,
            Keyboard.Key.Num7,
            Keyboard.Key.Num8,
            Keyboard.Key.Num9
        };

        public static InputValue[] FunctionKeys => new InputValue[] {
            Keyboard.Key.F1,
            Keyboard.Key.F2,
            Keyboard.Key.F3,
            Keyboard.Key.F4,
            Keyboard.Key.F5,
            Keyboard.Key.F6,
            Keyboard.Key.F7,
            Keyboard.Key.F8,
            Keyboard.Key.F9,
            Keyboard.Key.F10,
            Keyboard.Key.F11,
            Keyboard.Key.F12,
        };

        public static InputValue[] NumpadKeys => new InputValue[] {
            Keyboard.Key.Numpad0,
            Keyboard.Key.Numpad1,
            Keyboard.Key.Numpad2,
            Keyboard.Key.Numpad3,
            Keyboard.Key.Numpad4,
            Keyboard.Key.Numpad5,
            Keyboard.Key.Numpad6,
            Keyboard.Key.Numpad7,
            Keyboard.Key.Numpad8,
            Keyboard.Key.Numpad9,
            Keyboard.Key.Numpad0,
        };

        public enum InputType
        {
            Keyboard,
            KeyboardCombination,
            Mouse
        }

        public InputType Type { get; internal set; }

        public Keyboard.Key[] KeyCodes { get; internal set; }
        public Keyboard.Key KeyCode { get; internal set; }
        public Mouse.Button Button { get; internal set; }

        public static implicit operator InputValue(KeyCombination keyCombination)
        {
            return new InputValue() { Type = InputType.KeyboardCombination, KeyCodes = keyCombination.Keys };
        }

        public static implicit operator InputValue(Mouse.Button button)
        {
            return new InputValue() { Type = InputType.Mouse, Button = button };
        }

        public static implicit operator InputValue(Keyboard.Key key)
        {
            return new InputValue() { Type = InputType.Keyboard, KeyCode = key };
        }
    }
}

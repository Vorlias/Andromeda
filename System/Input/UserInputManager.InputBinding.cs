using System.Collections.Generic;
using System.Linq;
using SFML.Window;

namespace Andromeda.System
{

    public partial class UserInputManager
    {
        public class InputBinding
        {
            internal System.InputValue[] inputs;
            protected string actionName;
            //protected InputBindingBehaviour inputBehaviour = InputBindingBehaviour.Fallthrough;

            /// <summary>
            /// The name of the action
            /// </summary>
            public string ActionName
            {
                get
                {
                    return actionName;
                }
            }

            /// <summary>
            /// The mouse buttons that are used in this action
            /// </summary>
            public IEnumerable<Mouse.Button> MouseButtons
            {
                get
                {
                    return inputs
                        .Where(input => input.Type == System.InputValue.InputType.Mouse)
                        .Select(input => input.Button);
                }
            }

            /// <summary>
            /// The named inputs that are used in this action
            /// </summary>
            public string[] Strings
            {
                get
                {
                    return inputs.OfType<string>().ToArray();
                }
            }

            /// <summary>
            /// The keys that are used in this action
            /// </summary>
            public IEnumerable<Keyboard.Key> KeyCodes
            {
                get
                {
                    return inputs
                        .Where(input => input.Type == System.InputValue.InputType.Keyboard)
                        .Select(input => input.KeyCode);
                }
            }

            /// <summary>
            /// The keyboard combinations associated with this action
            /// </summary>
            public IEnumerable<KeyCombination> KeyCombinations
            {
                get => inputs
                    .Where(input => input.Type == System.InputValue.InputType.KeyboardCombination)
                    .Select(input => new KeyCombination(input.KeyCodes));
            }

            /// <summary>
            /// Method used to check if any of the mouse buttons are down
            /// </summary>
            /// <returns>True if one of the mouse buttons are down</returns>
            public bool HasMouseButtonPressed()
            {
                foreach (Mouse.Button key in MouseButtons)
                    if (Mouse.IsButtonPressed(key))
                        return true;

                return false;
            }

            /// <summary>
            /// Method used to check if any of the keys are down
            /// </summary>
            /// <returns>True if one of the keys are pressed</returns>
            public bool HasKeyDown()
            {
                foreach (Keyboard.Key key in KeyCodes)
                    if (Keyboard.IsKeyPressed(key))
                        return true;

                return false;
            }

            public InputBinding(string actionName, System.InputValue[] inputs)
            {
                this.actionName = actionName;
                this.inputs = inputs;
            }
        }
    }
}

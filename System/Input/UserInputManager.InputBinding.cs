using System.Collections.Generic;
using System.Linq;
using SFML.Window;

namespace Andromeda.System
{

    public partial class UserInputManager
    {
        public class InputBinding
        {
            internal object[] inputs;
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
            public Mouse.Button[] MouseButtons
            {
                get
                {
                    return inputs.OfType<Mouse.Button>().ToArray();
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
            public Keyboard.Key[] KeyCodes
            {
                get
                {
                    return inputs.OfType<Keyboard.Key>().ToArray();
                }
            }

            /// <summary>
            /// The keyboard combinations associated with this action
            /// </summary>
            public IEnumerable<KeyCombination> KeyCombinations
            {
                get => inputs.OfType<KeyCombination>();
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

            public InputBinding(string actionName, object[] inputs)
            {
                this.actionName = actionName;
                this.inputs = inputs;
            }
        }
    }
}

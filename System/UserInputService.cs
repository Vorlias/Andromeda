using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.System;

namespace VorliasEngine2D.System
{
    public class UserInputManager
    {
        Dictionary<string, Mouse.Button> buttonBindings = new Dictionary<string, Mouse.Button>();
        Dictionary<string, Keyboard.Key> keyBindings = new Dictionary<string, Keyboard.Key>();
        List<InputAction> actions = new List<InputAction>();

        public class InputAction
        {
            Action<UserInputAction> action;
            object[] inputs;
            string actionName;

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
            /// The function that is called
            /// </summary>
            public Action<UserInputAction> Action
            {
                get
                {
                    return action;
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
            /// Creates a new input action
            /// </summary>
            /// <param name="name">The name of the action</param>
            /// <param name="action">The action that is taken</param>
            /// <param name="inputs">The inputs that invoke this action</param>
            public InputAction(string name, Action<UserInputAction> action, object[] inputs)
            {
                this.actionName = name;
                this.action = action;
                this.inputs = inputs;
            }
        }

        /// <summary>
        /// Bind an action to a method with the specified inputs
        /// </summary>
        /// <param name="name">The name of this binding</param>
        /// <param name="actionMethod">The method that is called when these inputs are invoked</param>
        /// <param name="inputs">The inputs (Key, MouseButton, etc.)</param>
        public void BindAction(string name, Action<UserInputAction> actionMethod, params object[] inputs)
        {
            InputAction action = new InputAction(name, actionMethod, inputs);
            actions.Add(action);
        }

        public void InvokeInput(Application application, Mouse.Wheel input, Vector2f delta, InputState state)
        {
            
        }

        /// <summary>
        /// Invokes the input for the mouse
        /// </summary>
        /// <param name="application">The application for the input</param>
        /// <param name="input">The input mouse button</param>
        /// <param name="state">The state of the mouse button</param>
        public void InvokeInput(Application application, Mouse.Button input, InputState state)
        {
            foreach (InputAction action in actions)
            {
                foreach (Mouse.Button button in action.MouseButtons)
                    if (button == input)
                        action.Action.Invoke(new MouseInputAction(button, state, Mouse.GetPosition(application.Window)));
            }
        }

        /// <summary>
        /// Invokes input for the keyboard
        /// </summary>
        /// <param name="application">The application for the input</param>
        /// <param name="input">The input key</param>
        /// <param name="state">The state of the key</param>
        public void InvokeInput(Application application, Keyboard.Key input, InputState state)
        {
            foreach (InputAction action in actions)
            {
                foreach (Keyboard.Key key in action.KeyCodes)
                    if (key == input)
                        action.Action.Invoke(new KeyboardInputAction(key, state));

                foreach (string stringKey in action.Strings)
                    if (stringKey == input.ToString())
                        action.Action.Invoke(new KeyboardInputAction(input, state));
            }
        }
    }
}

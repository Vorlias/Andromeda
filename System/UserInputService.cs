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
        List<InputBindingAction> actions = new List<InputBindingAction>();
        List<InputBinding> bindings = new List<InputBinding>();

        public class InputBinding
        {
            protected object[] inputs;
            protected string actionName;

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

            public InputBinding(string actionName, object[] inputs)
            {
                this.actionName = actionName;
                this.inputs = inputs;
            }
        }

        public class InputBindingAction : InputBinding
        {
            Action<string, UserInputAction> action;

            /// <summary>
            /// The function that is called
            /// </summary>
            public Action<string, UserInputAction> Action
            {
                get
                {
                    return action;
                }
            }

            /// <summary>
            /// Creates a new input action
            /// </summary>
            /// <param name="name">The name of the action</param>
            /// <param name="action">The action that is taken</param>
            /// <param name="inputs">The inputs that invoke this action</param>
            public InputBindingAction(string name, Action<string, UserInputAction> action, object[] inputs) : base(name, inputs)
            {
                this.action = action;   
            }
        }

        /// <summary>
        /// Bind an action to a method with the specified inputs
        /// </summary>
        /// <param name="name">The name of this binding</param>
        /// <param name="actionMethod">The method that is called when these inputs are invoked</param>
        /// <param name="inputs">The inputs (Key, MouseButton, etc.)</param>
        public void BindAction(string name, Action<string, UserInputAction> actionMethod, params object[] inputs)
        {
            InputBindingAction action = new InputBindingAction(name, actionMethod, inputs);
            actions.Add(action);
        }
        
        /// <summary>
        /// Binds the inputs to an action name
        /// </summary>
        /// <param name="name">The name of the action</param>
        /// <param name="inputs">The inputs for this action</param>
        public void Bind(string name, params object[] inputs)
        {
            InputBinding binding = new InputBinding(name, inputs);
            bindings.Add(binding);
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
            foreach (InputBindingAction action in actions)
            {
                foreach (Mouse.Button button in action.MouseButtons)
                    if (button == input)
                        action.Action.Invoke(action.ActionName, new MouseInputAction(button, state, Mouse.GetPosition(application.Window)));
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
            foreach (InputBindingAction action in actions)
            {
                foreach (Keyboard.Key key in action.KeyCodes)
                    if (key == input)
                        action.Action.Invoke(action.ActionName, new KeyboardInputAction(key, state));

                foreach (string stringKey in action.Strings)
                    if (stringKey == input.ToString())
                        action.Action.Invoke(action.ActionName, new KeyboardInputAction(input, state));
            }
        }
    }
}

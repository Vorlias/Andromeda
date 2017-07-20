using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.System;
using VorliasEngine2D.System.Utility;

namespace VorliasEngine2D.System
{

    /// <summary>
    /// The input manager
    /// </summary>
    public class UserInputManager
    {
        Dictionary<string, Mouse.Button> buttonBindings = new Dictionary<string, Mouse.Button>();
        Dictionary<string, Keyboard.Key> keyBindings = new Dictionary<string, Keyboard.Key>();
        List<InputBindingAction> actions = new List<InputBindingAction>();
        List<InputBinding> bindings = new List<InputBinding>();

        /// <summary>
        /// Reset the input manager
        /// </summary>
        internal void ClearBindings()
        {
            bindings.Clear();
            actions.Clear();
            keyBindings.Clear();
            buttonBindings.Clear();
        }

        public class InputBinding
        {
            protected object[] inputs;
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

        public class InputBindingAction : InputBinding
        {
            Action<string, UserInputAction> action;
            InputBindingPriority priority = InputBindingPriority.Normal;

            /// <summary>
            /// The binding priority
            /// </summary>
            public InputBindingPriority BindingPriority
            {
                get
                {
                    return priority;
                }
            }

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
            internal InputBindingAction(string name, Action<string, UserInputAction> action, object[] inputs, InputBindingPriority priority = InputBindingPriority.Normal) : base(name, inputs)
            {
                this.action = action;
                this.priority = priority;
            }
        }


        internal IOrderedEnumerable<InputBindingAction> ActionsByPriority
        {
            get => actions.OrderBy(e => e.BindingPriority);
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
            bindings.Add(action);
        }

        /// <summary>
        /// Bind an action to a method with the specified inputs
        /// </summary>
        /// <param name="value">The enum value of this binding</param>
        /// <param name="actionMethod">The method that is called when these inputs are invoked</param>
        /// <param name="inputs">The inputs (Key, MouseButton, etc.)</param>
        public void BindAction(Enum value, Action<string, UserInputAction> actionMethod, params object[] inputs)
        {
            InputBindingAction action = new InputBindingAction(value.ToString(), actionMethod, inputs);
            actions.Add(action);
            bindings.Add(action);
        }

        /// <summary>
        /// Binds the inputs to an action name
        /// </summary>
        /// <param name="value">The enum value of the action</param>
        /// <param name="inputs">The inputs for this action</param>
        public void Bind(Enum value, params object[] inputs)
        {
            InputBinding binding = new InputBinding(value.ToString(), inputs);
            bindings.Add(binding);
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

        /// <summary>
        /// Returns whether or not the key is down
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsActive(string name)
        {
            var binding = bindings.Find(b => b.ActionName == name);
            if (binding != null)
            {
                return binding.HasKeyDown() || binding.HasMouseButtonPressed();
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns whether or not the key is down using an enum's name
        /// </summary>
        /// <param name="enumValue">The enum value</param>
        /// <returns></returns>
        public bool IsActive(Enum enumValue)
        {
            return IsActive(enumValue.ToString());
        }

        /// <summary>
        /// Invoke the input for the mouse wheel
        /// </summary>
        /// <param name="application">The application for the input</param>
        /// <param name="input">The input mouse button</param>
        /// <param name="delta">The delta movement of the scroll wheel</param>
        /// <param name="state">The state of the mouse button</param>
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
            foreach (InputBindingAction action in ActionsByPriority)
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
            foreach (InputBindingAction action in ActionsByPriority)
            {
                foreach (KeyCombination combo in action.KeyCombinations)
                {
                    if (combo.AllKeysPressed)
                    {
                        var keyInputAction = new KeyboardInputAction(state, combo.Keys);
                        action.Action.Invoke(action.ActionName, keyInputAction);
                    }
                }

                foreach (Keyboard.Key key in action.KeyCodes)
                    if (key == input)
                    {
                        var keyInputAction = new KeyboardInputAction(state, key);
                        action.Action.Invoke(action.ActionName, keyInputAction);
                    }


                foreach (string stringKey in action.Strings)
                    if (stringKey == input.ToString())
                    {
                        var keyInputAction = new KeyboardInputAction(state, input);
                        action.Action.Invoke(action.ActionName, keyInputAction);
                    }
            }
        }
    }
}

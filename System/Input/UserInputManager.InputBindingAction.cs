using System;

namespace Andromeda.System
{

    public partial class UserInputManager
    {
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
    }
}

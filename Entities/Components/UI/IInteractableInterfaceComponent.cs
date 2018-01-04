using Andromeda.Entities.Components.Internal;
using Andromeda.System;
using SFML.System;

namespace Andromeda.Entities.Components.UI
{
    interface IInteractableInterfaceComponent
    {
        /// <summary>
        /// Whether or not the component should prevent fall through of mouse input
        /// </summary>
        bool ShouldPreventFallthrough { get; }

        /// <summary>
        /// Whether or not this component ignores fall through restrictions of mouse input
        /// </summary>
        bool IsIgnoringFallthroughState { get; }

        /// <summary>
        /// Whether or not this component has the fallthrough priority of the mouse input
        /// </summary>
        bool HasFallthroughPriority { get; }

        void MouseButtonClicked(MouseInputAction inputAction, bool inside);
        void MouseButtonReleased(MouseInputAction inputAction);

        /// <summary>
        /// Called when the input event is triggered
        /// </summary>
        /// <param name="inputAction">The input action</param>
        void InputRecieved(UserInputAction inputAction);
    }
}

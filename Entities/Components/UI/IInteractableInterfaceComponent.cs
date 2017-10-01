using Andromeda2D.Entities.Components.Internal;
using Andromeda2D.System;
using SFML.System;

namespace Andromeda2D.Entities.Components.UI
{
    interface IInteractableInterfaceComponent : IEventListenerComponent
    {
        bool PreventsFallthrough { get; }
        bool IsPreventingFallthrough { get; }

        void MouseButtonClicked(MouseInputAction inputAction, bool inside);
        void MouseButtonReleased(MouseInputAction inputAction);
    }
}

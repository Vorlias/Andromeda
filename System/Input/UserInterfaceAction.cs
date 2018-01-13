using Andromeda.Entities.Components.Internal;

namespace Andromeda.System
{
    public class UserInterfaceAction : UserInputAction
    {

        public override InputType InputType => InputType.UserInterface;

        public UIComponent UIComponent
        {
            get;
        }

        public UIActionType Action
        {
            get;
        }

        public UserInterfaceAction(UIActionType action, UIComponent component)
        {
            Action = action;
            UIComponent = component;
        }
    }
}

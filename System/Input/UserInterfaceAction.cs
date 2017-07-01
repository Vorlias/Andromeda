using VorliasEngine2D.Entities.Components.Internal;

namespace VorliasEngine2D.System
{
    public class UserInterfaceAction : UserInputAction
    {
        public enum Type
        {
            MouseEnter,
            MouseLeave,
        }

        public override InputType InputType => InputType.UserInterface;

        public UIComponent UIComponent
        {
            get;
        }

        public Type Action
        {
            get;
        }

        public UserInterfaceAction(Type action, UIComponent component)
        {
            Action = action;
            UIComponent = component;
        }
    }
}

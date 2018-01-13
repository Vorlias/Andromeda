using SFML.Window;

namespace Andromeda2D.System
{
    public class JoystickInputAction : UserInputAction
    {
        Joystick.Axis axis;

        /// <summary>
        /// The input type of this object
        /// </summary>
        public override InputType InputType
        {
            get
            {
                return InputType.Joystick;
            }
        }
    }
}

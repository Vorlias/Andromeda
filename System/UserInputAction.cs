using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.System;

namespace VorliasEngine2D.System
{
    public enum InputType
    {
        Unknown,
        Mouse,
        Keyboard,
        Joystick
    }

    public enum MouseInputType
    {
        Moved,
        Button,
        Wheel,
    }

    public enum InputState
    {
        Active,
        Update,
        Inactive,
    }

    public abstract class UserInputAction
    {
        protected InputState state;

        /// <summary>
        /// The input state of this object
        /// </summary>
        public InputState InputState
        {
            get
            {
                return state;
            }
        }

        /// <summary>
        /// The input type of this object
        /// </summary>
        public abstract InputType InputType
        {
            get;
        }

        public MouseInputAction Mouse()
        {
            return this as MouseInputAction;
        }

        public KeyboardInputAction Keyboard()
        {
            return this as KeyboardInputAction;
        }
    }

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

    public class KeyboardInputAction : UserInputAction
    {
        Keyboard.Key key;

        /// <summary>
        /// The key associated with this input object
        /// </summary>
        public Keyboard.Key Key
        {
            get
            {
                return key;
            }
        }

        /// <summary>
        /// The input type of this object
        /// </summary>
        public override InputType InputType
        {
            get
            {
                return InputType.Keyboard;
            }
        }

        public KeyboardInputAction(Keyboard.Key key, InputState state)
        {
            this.key = key;
            this.state = state;
        }
    }

    public class MouseInputAction : UserInputAction
    {
        

        Mouse.Button button;
        Mouse.Wheel wheel;
        MouseInputType mouseInputType;
        Vector2f delta;
        Vector2i position;

        public Vector2i Position
        {
            get
            {
                return position;
            }
        }

        public Vector2f Delta
        {
            get
            {
                return delta;
            }
        }

        public Mouse.Wheel Wheel
        {
            get
            {
                return wheel;
            }
        }

        public Mouse.Button Button
        {
            get
            {
                return button;
            }
        }

        public MouseInputType MouseInputType
        {
            get
            {
                return mouseInputType;
            }
        }

        /// <summary>
        /// The input type of this object
        /// </summary>
        public override InputType InputType
        {
            get
            {
                return InputType.Mouse;
            }
        }

        public MouseInputAction(Mouse.Wheel wheel, InputState state, Vector2f delta = new Vector2f())
        {
            this.mouseInputType = MouseInputType.Wheel;
        }

        public MouseInputAction(Mouse.Button button, InputState state, Vector2i position = new Vector2i())
        {
            this.button = button;
            this.state = state;
            this.mouseInputType = MouseInputType.Button;
            this.position = position;        
        }
    }
}

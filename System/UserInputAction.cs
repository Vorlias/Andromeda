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

        public MouseInputAction Mouse
        {
            get => this as MouseInputAction;
        }

        public KeyboardInputAction Keyboard
        {
            get => this as KeyboardInputAction;
        }

        public override string ToString()
        {
            if (this is KeyboardInputAction)
            {
                return "KeyboardInputAction: " + Keyboard.Key.ToString();
            }
            else if (this is MouseInputAction)
            {
                return "MouseInputAction: " + Mouse.Button.ToString();
            }
            else
                return "UserInputAction";
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
        List<Keyboard.Key> keys;

        /// <summary>
        /// The key associated with this input object
        /// </summary>
        public Keyboard.Key Key
        {
            get
            {
                return keys.FirstOrDefault();
            }
        }

        public IEnumerable<Keyboard.Key> Keys
        {
            get => keys;
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

        public KeyboardInputAction(InputState state, params Keyboard.Key[] keys)
        {
            this.keys = new List<Keyboard.Key>();
            this.keys.AddRange(keys);
        }

        public KeyboardInputAction(InputState state, Keyboard.Key key)
        {
            keys = new List<Keyboard.Key>();
            keys.Add(key);
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

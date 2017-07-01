using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VorliasEngine2D.System
{

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
}

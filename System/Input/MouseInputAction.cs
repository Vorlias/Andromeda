using SFML.Window;
using SFML.System;

namespace Vorlias2D.System
{
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

using System.Collections.Generic;
using SFML.Window;

namespace VorliasEngine2D.System
{
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
}

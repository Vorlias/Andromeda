﻿using SFML.Window;

namespace Andromeda.System
{
    /// <summary>
    /// A combination of keys
    /// </summary>
    public struct KeyCombination
    {
        public Keyboard.Key[] Keys
        {
            get;
        }

        public bool AllKeysPressed
        {
            get
            {
                bool isActive = true;
                foreach (Keyboard.Key key in Keys)
                {
                    if (!Keyboard.IsKeyPressed(key))
                        isActive = false;
                }

                return isActive;
            }
        }

        public KeyCombination(params Keyboard.Key[] keys)
        {
            Keys = keys;
        }
    }
}

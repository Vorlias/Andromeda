using Andromeda.System;

namespace Andromeda.System
{
    public class GameViewConfig
    {
        public string Name { get; }
        public GameViewPriority Priority = GameViewPriority.Normal;
        public bool Active = true;

        public GameViewConfig(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Create a default GameViewConfig using just the name
        /// </summary>
        /// <param name="name">The name</param>
        public static implicit operator GameViewConfig(string name)
        {
            return new GameViewConfig(name);
        }
    }
}

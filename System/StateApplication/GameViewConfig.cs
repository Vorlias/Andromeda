using Andromeda2D.System;

namespace Andromeda.System.StateApplication
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

        void Test()
        {
            new GameViewConfig("Test") {  };
        }
    }
}

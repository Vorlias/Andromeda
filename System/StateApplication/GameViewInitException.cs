using System;

namespace Andromeda.System
{
    public class GameViewInitException : Exception
    {
        public GameViewInitException() : base("GameView already initialized!")
        {

        }
    }
}

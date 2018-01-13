using System;

namespace Andromeda2D.System
{
    public class GameViewInitException : Exception
    {
        public GameViewInitException() : base("GameView already initialized!")
        {

        }
    }
}

using System;

namespace Vorlias2D.System
{
    public class GameViewInitException : Exception
    {
        public GameViewInitException() : base("GameView already initialized!")
        {

        }
    }
}

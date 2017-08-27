using System;

namespace VorliasEngine2D.System
{
    public class GameViewInitException : Exception
    {
        public GameViewInitException() : base("GameView already initialized!")
        {

        }
    }
}

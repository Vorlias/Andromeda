using System;

namespace VorliasEngine2D.System
{
    internal class PrefabSyntaxErrorException : Exception
    {
        public PrefabSyntaxErrorException(string message) : base ("Syntax Error: " + message)
        {

        }
    }
}

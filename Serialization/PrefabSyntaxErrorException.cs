using System;

namespace Andromeda.System
{
    internal class PrefabSyntaxErrorException : Exception
    {
        public PrefabSyntaxErrorException(string message) : base ("Syntax Error: " + message)
        {

        }
    }
}

using System;

namespace Vorlias2D.Entities.Components
{
    public class SetEntityInvalidException : Exception
    {
        public SetEntityInvalidException() : base("Attempted to set parent of a Component")
        {

        }
    }
}

using System;

namespace VorliasEngine2D.Entities.Components
{
    /// <summary>
    /// Exception thrown if two components 'clash'
    /// </summary>
    public class InvalidComponentPairException : Exception
    {
        public InvalidComponentPairException(IComponent a, IComponent b) : base ("Cannot have a " + a.Name + " component with a " + b.Name + " component!")
        {

        }
    }
}

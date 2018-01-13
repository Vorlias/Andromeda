using System;

namespace Andromeda2D.Entities
{
    public sealed class ComponentNotFoundException<T> : Exception
    {
        public ComponentNotFoundException() : base("Could not find component: " + typeof(T).Name)
        {

        }
    }
}

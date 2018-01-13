using System;

namespace Andromeda.Entities
{
    public sealed class ComponentNotFoundException<T> : Exception
    {
        public ComponentNotFoundException() : base("Could not find component: " + typeof(T).Name)
        {

        }
    }
}

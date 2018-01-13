using System.Collections.Generic;

namespace Andromeda2D.System.Internal
{
    class ResourceNotFoundException : KeyNotFoundException
    {
        public ResourceNotFoundException(string typeName, string resourceId) : base("Invalid " + typeName + " resource specified: " + resourceId)
        {
        }
    }
}

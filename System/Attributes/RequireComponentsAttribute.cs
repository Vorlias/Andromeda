using Andromeda2D.Entities;
using Andromeda2D.Entities.Components.Internal;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda.System
{
    /// <summary>
    /// Attribute that is used for required components to other components
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class RequireComponentsAttribute : Attribute
    {
        Type[] _types;

        public RequireComponentsAttribute(params Type[] requiredTypes)
        {
            _types = requiredTypes;
        }

        internal void AddRequiredComponents(Entity entity)
        {
            foreach (var type in _types)
            {
                entity.FindOrCreateComponent(type, out var created, true);
            }
        }
    }
}

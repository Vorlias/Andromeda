using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda.Entities.Components
{
    public interface ICollisionComponent : IComponent
    {
        bool CollidesWith(ICollisionComponent other);
        void IgnoreCollisionsWith(ICollisionComponent collider);

        /// <summary>
        /// Used for determining if this will prevent movement through the entity
        /// </summary>
        bool IsTrigger
        {
            get;
            set;
        }
    }
}

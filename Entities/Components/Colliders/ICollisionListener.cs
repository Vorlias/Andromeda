using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda.Entities.Components.Colliders
{
    public interface ICollisionListener : IComponent
    {
        void Collided(Entity collidedWith);
    }
}

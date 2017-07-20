using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.Entities.Components.Internal;

namespace VorliasEngine2D.Entities.Components.Colliders
{
    public abstract class CollisionComponent : Component, ICollisionComponent
    {
        List<ICollisionComponent> ignoreList = new List<ICollisionComponent>();

        public override bool AllowsMultipleInstances => false;

        bool trigger = false;
        public bool IsTrigger
        {
            get => trigger;
            set => trigger = value;
        }

        /// <summary>
        /// Check if the object collides with this object
        /// </summary>
        /// <param name="other">The other object</param>
        /// <returns>True if they've collided</returns>
        public bool CollidesWith(ICollisionComponent other)
        {
            if (ignoreList.Contains(other))
                return false;
            else
                return this.CheckCollision(other);
        }

        public override abstract void OnComponentCopy(Entity source, Entity copy);

        /// <summary>
        /// Ignore collisions with the specified object
        /// </summary>
        /// <param name="collider">The collider to ignore collisions with</param>
        public void IgnoreCollisionsWith(ICollisionComponent collider)
        {
            if (!ignoreList.Contains(collider))
            {
                ignoreList.Add(collider);
                collider.IgnoreCollisionsWith(this);
            }
        }

        /// <summary>
        /// Ignore collisions with the specified entity
        /// </summary>
        /// <param name="other">The entity to ignore collisions with</param>
        public void IgnoreCollisionsWith(Entity other)
        {
            other.Collider?.IgnoreCollisionsWith(this);
        }
    }
}

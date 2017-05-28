using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.System.Utility;

namespace VorliasEngine2D.Entities.Components
{
    /// <summary>
    /// Rectangle Collider component
    /// </summary>
    public class RectCollider : ICollisionComponent
    {
        private Entity entity;

        public bool AllowsMultipleInstances
        {
            get
            {
                return false;
            }
        }

        public Entity Entity
        {
            get
            {
                return entity;
            }
        }

        public string Name
        {
            get
            {
                return "BoxCollider";
            }
        }

        private FloatRect collisionRect;
        public FloatRect LocalCollisionRect
        {
            get
            {
                return collisionRect;
            }
            set
            {
                collisionRect = value;
            }
        }

        /// <summary>
        /// Gets the rectangle to world coordinates
        /// </summary>
        public FloatRect WorldCollisionRect
        {
            get
            {
                Vector2f startPosition = Entity.Position;
                return LocalCollisionRect.Combine(startPosition);
            }
        }

        private bool trigger;

        /// <summary>
        /// Whether or not this collider is a trigger
        /// </summary>
        public bool IsTrigger
        {
            get
            {
                return trigger;
            }

            set
            {
                trigger = value;
            }
        }

        public bool CollidesWith(ICollisionComponent other)
        {
            if (other is RectCollider)
            {
                var otherCollider = other as RectCollider;
                return otherCollider.LocalCollisionRect.Intersects(LocalCollisionRect);
            }

            return false;
        }

        public void OnComponentCopy(Entity source, Entity copy)
        {
            
        }

        public void OnComponentInit(Entity entity)
        {
            if (this.entity == null)
                this.entity = entity;
            else
                throw new SetEntityInvalidException();

            // If we have a sprite renderer, we can default to the sprite ;)
            if (entity.HasComponent<SpriteRenderer>())
            {
                var spriteRenderer = entity.GetComponent<SpriteRenderer>();
                var textureSize = spriteRenderer.Texture.Size.ToFloatVector();
                LocalCollisionRect = new FloatRect(-textureSize.X / 2, -textureSize.Y / 2, textureSize.X, textureSize.Y);
            }
        }
    }
}

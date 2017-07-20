using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.Serialization;
using VorliasEngine2D.System.Utility;

namespace VorliasEngine2D.Entities.Components
{
    /// <summary>
    /// Rectangle Collider component (Polygon rectangle)
    /// </summary>
    public class PolygonRectCollider : IPolygonColliderComponent
    {
        private Entity entity;

        internal Polygon polygon;

        [SerializableProperty("Polygon", PropertyType = SerializedPropertyType.Polygon)]
        public Polygon Polygon
        {
            get
            {
                return polygon;
            }
            set
            {
                polygon = value;
            }
        }

        HashSet<ICollisionComponent> ignoreList = new HashSet<ICollisionComponent>();

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

        /// <summary>
        /// Creates a collider with the specified size
        /// </summary>
        /// <param name="size">The size of the collider</param>
        public void CreateRectCollider(Vector2f size)
        {
            // Using polygons because they're easier to work with than IntRect/FloatRect when coming to rotation
            polygon = Polygon.CreateRectangle(size.X, size.Y);
        }

        /*private FloatRect collisionRect;
        public FloatRect LocalCollisionRect
        {
            get
            {
                return collisionRect;
            }
            set
            {
                collisionRect = value;
                polygon = Polygon.CreateRectangle(value.Width, value.Height);
            }
        }*/

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

        private Vector2f origin;
        public Vector2f Origin
        {
            get
            {
                return origin;
            }

            set
            {
                origin = value;
            }
        }

        public void IgnoreCollisionsWith(ICollisionComponent collider)
        {
            ignoreList.Add(collider);
        }

        public bool CollidesWith(ICollisionComponent other)
        {
            if (!ignoreList.Contains(other))
                return this.CheckCollision(other);
            else
                return false;
        }

        public void OnComponentCopy(Entity source, Entity copy)
        {
            var com = copy.AddComponent<PolygonRectCollider>();
            com.polygon = polygon;
        }

        public void ComponentInit(Entity entity)
        {
            if (this.entity == null)
                this.entity = entity;
            else
                throw new SetEntityInvalidException();

            // If we have a sprite renderer, we can default to the sprite ;)
            if (entity.HasComponent<SpriteRenderer>())
            {
                var spriteRenderer = entity.GetComponent<SpriteRenderer>();
                var textureSize = spriteRenderer.Texture.Size.ToFloat();
                CreateRectCollider(new Vector2f(textureSize.X, textureSize.Y)); // Create a collider with the size of the sprite (ish)
                origin = -(textureSize / 2);
            }
        }
    }
}

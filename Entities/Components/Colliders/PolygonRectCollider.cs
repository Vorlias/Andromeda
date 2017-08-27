using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Andromeda2D.Entities.Components.Colliders;
using Andromeda2D.Serialization;
using Andromeda2D.System.Types;
using Andromeda2D.System.Utility;

namespace Andromeda2D.Entities.Components
{
    /// <summary>
    /// Rectangle Collider component (Polygon rectangle)
    /// </summary>
    public class PolygonRectCollider : CollisionComponent, IPolygonColliderComponent
    {
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

        public override string Name
        {
            get
            {
                return "PolygonRectCollider";
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

        public override void OnComponentCopy(Entity source, Entity copy)
        {
            var com = copy.AddComponent<PolygonRectCollider>();
            com.polygon = polygon;
        }

        public override void OnComponentInit(Entity entity)
        {
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

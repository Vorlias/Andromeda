using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Andromeda.Entities.Components.Colliders;
using Andromeda.Serialization;
using Andromeda.System.Types;
using Andromeda.System.Utility;

namespace Andromeda.Entities.Components
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

        /// <summary>
        /// Creates a collider with the specified size
        /// </summary>
        /// <param name="size">The size of the collider</param>
        public void CreateRectCollider(Vector2f size)
        {
            // Using polygons because they're easier to work with than IntRect/FloatRect when coming to rotation
            polygon = Polygon.CreateRectangle(size.X, size.Y);
        }

        /// <summary>
        /// Create a rect collider based off a sprite renderer
        /// </summary>
        /// <param name="spriteRenderer">The sprite renderer</param>
        public void CreateRectCollider(SpriteRenderer spriteRenderer)
        {
            var textureSize = spriteRenderer.Texture.Size.ToFloat();
            CreateRectCollider(new Vector2f(textureSize.X, textureSize.Y)); // Create a collider with the size of the sprite (ish)
            origin = -(textureSize / 2);
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

        protected override void OnComponentInit(Entity entity)
        {

        }
    }
}

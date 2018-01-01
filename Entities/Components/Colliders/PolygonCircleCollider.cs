using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using Andromeda2D.System.Utility;
using Andromeda2D.Entities.Components.Colliders;
using Andromeda2D.System.Types;

namespace Andromeda2D.Entities.Components
{
    public class PolygonCircleCollider : CollisionComponent, IPolygonColliderComponent
    {
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

        internal Polygon polygon = new Polygon();
        public Polygon Polygon
        {
            get
            {
                return polygon;
            }
        }

        internal Vector2f CreatePolygonPoint(int vertex, float radius, float resolution)
        {
            float angle = 360.0f / resolution * vertex;
            float pX = (float)Math.Cos(angle * MathUtil.DEGREES_TO_RADIANS) * radius;
            float pY = (float)Math.Sin(angle * MathUtil.DEGREES_TO_RADIANS) * -radius;
            return new Vector2f(pX + radius, pY + radius);
        }

        public void CreateCircleCollider(float radius, int resolution = 8)
        {
            var polygon = new Polygon();
            for (int i = 0; i <= resolution; i++)
            {
                polygon.Add(CreatePolygonPoint(i, radius, resolution));
            }

            this.polygon = polygon;
        }

        public override void OnComponentCopy(Entity source, Entity copy)
        {

        }

        protected override void OnComponentInit(Entity entity)
        {
            // If we have a sprite renderer, we can default to the sprite ;)
            if (entity.HasComponent<SpriteRenderer>())
            {
                var spriteRenderer = entity.GetComponent<SpriteRenderer>();
                var textureSize = spriteRenderer.Texture.Size.ToFloat();
                var diagonal = (float)Math.Sqrt(Math.Pow(textureSize.Y, 2) + Math.Pow(textureSize.X, 2));

                CreateCircleCollider(textureSize.X / 2);
                origin = -(textureSize / 2);
            }
        }
    }
}

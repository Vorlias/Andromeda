using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using VorliasEngine2D.System.Utility;

namespace VorliasEngine2D.Entities.Components
{
    public class PolygonCircleCollider : IPolygonColliderComponent
    {
        public bool AllowsMultipleInstances
        {
            get
            {
                return false;
            }
        }

        private Entity entity;
        public Entity Entity
        {
            get
            {
                return entity;
            }
        }

        public bool IsTrigger
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                return "CircleCollider";
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

        internal Polygon polygon = new Polygon();
        public Polygon Polygon
        {
            get
            {
                return polygon;
            }
        }

        public bool CollidesWith(ICollisionComponent other)
        {
            return this.CheckCollision(other);
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

        public void OnComponentCopy(Entity source, Entity copy)
        {
            

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
                var textureSize = spriteRenderer.Texture.Size.ToFloatVector();
                var diagonal = (float)Math.Sqrt(Math.Pow(textureSize.Y, 2) + Math.Pow(textureSize.X, 2));

                CreateCircleCollider(textureSize.X / 2);
                origin = -(textureSize / 2);
            }
        }
    }
}

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
    /// A collider based on polygon points
    /// </summary>
    public class PolygonCollider : IPolygonColliderComponent
    {
        public bool AllowsMultipleInstances
        {
            get
            {
                return false;
            }
        }

        private Texture collisionTexture;
        private Image collisionImage;
        internal VertexArray vertices;

        public Texture CollisionTexture
        {
            get
            {
                return collisionTexture;
            }
            set
            {
                collisionImage = value.CopyToImage();
                collisionTexture = value;
            }
        }

        internal Polygon polygon;

        public Polygon Polygon
        {
            get
            {
                return polygon;
            }
        }

        private Vector2f origin = new Vector2f(0, 0);
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

        /// <summary>
        /// Creates the collider from a polygon
        /// </summary>
        /// <param name="polygon">The polygon</param>
        public void CreateFromPolygon(Polygon polygon)
        {
            this.polygon = polygon;
        }

        /// <summary>
        /// Creates a polygon collider from a Polypoint Map Image
        /// </summary>
        /// <param name="target">The target texture</param>
        public void CreateFromPolyMap(Texture target)
        {
            Image i = target.CopyToImage();
            polygon = new Polygon();

            int idx = 0;
            bool foundPxl = false;

            do
            {
                foundPxl = false;
                for (uint y = 0; y < i.Size.Y; y++)
                {
                    for (uint x = 0; x < i.Size.X; x++)
                    {

                        Color pixel = i.GetPixel(x, y);
                        byte gIdx = pixel.G;

                        if (pixel.R == 0xFF && pixel.B == 0xFF && pixel.G == idx)
                        {
                            foundPxl = true;
                            polygon.Add(new Vector2f(x, y));
                            idx++;
                            break;
                        }
                    }
                    if (foundPxl)
                        break;
                }
            }
            while (foundPxl);

            var first = polygon.FirstOrDefault();
            if (first != null)
            {
                polygon.Add(first);
            }

            Origin = -(i.Size / 2).ToFloatVector();
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
                return "PolygonCollider";
            }
        }

        public bool CollidesWith(ICollisionComponent other)
        {
            return this.CheckCollision(other);
        }

        public void OnComponentCopy(Entity source, Entity copy)
        {
            //throw new NotImplementedException();
        }

        public void OnComponentInit(Entity entity)
        {
            if (this.entity == null)
                this.entity = entity;
            else
                throw new SetEntityInvalidException();

            SpriteRenderer renderer;
            if (entity.FindComponent(out renderer))
            {
                origin = renderer.Origin;
            }
        }
    }
}

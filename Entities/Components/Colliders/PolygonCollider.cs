using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Andromeda2D.Entities.Components.Colliders;
using Andromeda2D.System.Types;
using Andromeda2D.System.Utility;
using Andromeda.Colliders;

namespace Andromeda2D.Entities.Components
{
    /// <summary>
    /// A collider based on polygon points
    /// </summary>
    public class PolygonCollider : CollisionComponent, IPolygonColliderComponent
    {

        private Texture collisionTexture;
        private Image collisionImage;

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

            Origin = -(new Vector2f(polygon.Width, polygon.Height) / 2);

            var first = polygon.FirstOrDefault();
            if (first != null)
            {
                polygon.Add(first);
            }
        }

        /// <summary>
        /// Creates a collider from the StarMapColliderInfo
        /// </summary>
        /// <param name="starMapColliderInfo"></param>
        public void CreateFromCollider(StarMapColliderInfo starMapColliderInfo)
        {
            polygon = starMapColliderInfo.Polygon;
            Origin = -(starMapColliderInfo.Size.ToFloat() / 2);

            var first = polygon.FirstOrDefault();
            if (first != null)
            {
                polygon.Add(first);
            }
        }

        /// <summary>
        /// Creates a polygon collider from a Polypoint Map Image
        /// </summary>
        /// <param name="target">The target texture</param>
        [Obsolete("Use 'CreateFromPolygon' from a polygon map instead.", true)]
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

            Origin = -(i.Size / 2).ToFloat();
        }

        public override void OnComponentCopy(Entity source, Entity copy)
        {
            //throw new NotImplementedException();
        }

        protected override void OnComponentInit(Entity entity)
        {
            SpriteRenderer renderer;
            if (entity.FindComponent(out renderer))
            {
                origin = renderer.Origin;
            }
        }
    }
}

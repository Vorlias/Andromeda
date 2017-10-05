using Andromeda2D.System.Types;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda.Colliders
{
    /// <summary>
    /// A collider that works with the Star Map editor for Andromeda
    /// </summary>
    public class StarMapColliderInfo
    {
        public Vector2u Size
        {
            get;
        }

        public Polygon Polygon
        {
            get;
        }

        public StarMapColliderInfo(Polygon polygon, Vector2u size)
        {
            Size = size;
            Polygon = polygon;
        }
    }
}

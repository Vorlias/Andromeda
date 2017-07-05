using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.System.Utility;

namespace VorliasEngine2D.Entities.Components
{

    public interface IPolygonColliderComponent : ICollisionComponent
    {
        Polygon Polygon
        {
            get;
        }

        Vector2f Origin
        {
            get;
            set;
        }
    }
}

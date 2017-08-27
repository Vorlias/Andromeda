using SFML.System;
using Vorlias2D.System.Types;

namespace Vorlias2D.Entities.Components
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

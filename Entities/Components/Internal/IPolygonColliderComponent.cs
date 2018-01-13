using SFML.System;
using Andromeda2D.System.Types;

namespace Andromeda2D.Entities.Components
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

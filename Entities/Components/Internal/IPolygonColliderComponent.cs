using SFML.System;
using Andromeda.System.Types;

namespace Andromeda.Entities.Components
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

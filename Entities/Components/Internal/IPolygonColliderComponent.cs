using SFML.System;
using VorliasEngine2D.System.Types;

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

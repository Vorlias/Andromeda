namespace Andromeda2D.Entities.Components
{
    internal static class ColliderDetection
    {
        /// <summary>
        /// Checks if this PolygonCollider collides with the other PolygonCollider
        /// </summary>
        /// <param name="first">The first polygon collider</param>
        /// <param name="second">The second polygon collider</param>
        /// <returns></returns>
        internal static bool CheckPolygonCollision(this IPolygonColliderComponent first, IPolygonColliderComponent second)
        {
            var poly1 = first.Polygon?.Transform(first.Entity.Position, first.Origin, first.Entity.Transform.Rotation); //new Polygon(pThisPoly);
            var poly2 = second.Polygon?.Transform(second.Entity.Position, second.Origin, second.Entity.Transform.Rotation);//new Polygon(pOtherPoly);

            return (poly1 != null && poly2 != null) && poly1.Overlaps(poly2);
        }
    }
}

using Andromeda.Colliders;

namespace Andromeda.Resources
{
    public class SMColliderResource : IResource
    {
        public ResourceType Type => ResourceType.SMCollider;

        public StarMapColliderInfo Collider { get; }

        internal SMColliderResource(StarMapColliderInfo tx)
        {
            Collider = tx;
        }
    }
}

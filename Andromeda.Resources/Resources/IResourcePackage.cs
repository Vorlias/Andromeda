using Andromeda.Colliders;
using SFML.Graphics;
using System.Collections.Generic;

namespace Andromeda.Resources
{
    public interface IResourcePackage
    {
        IEnumerable<IResource> Resources { get; }
        IEnumerable<ILoadableResource> LoadableResources { get; }

        Texture GetTexture(string relative, IntRect area = new IntRect());

        Font GetFont(string relative);

        StarMapColliderInfo GetSMCollider(string relative);
    }
}

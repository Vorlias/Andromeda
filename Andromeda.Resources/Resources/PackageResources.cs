using Andromeda.Colliders;
using Andromeda.Resources.Archive;
using SFML.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Andromeda.Resources
{
    public class PackageResources : IResourcePackage
    {
        public IEnumerable<IResource> Resources
        {
            get
            {
                List<IResource> resources = new List<IResource>();
                foreach (var file in Archive.Entries)
                {
                    if (file.FullName.EndsWith(".png") || file.FullName.EndsWith(".jpg"))
                        resources.Add(new TextureResouce(file.Name, Archive.GetTexture(file.Name.ToIdFormat(), new IntRect())));
                    else if (file.FullName.EndsWith(".smc"))
                        resources.Add(new SMColliderResource(new StarMapColliderInfo(file.Open())));
                    else if (file.FullName.EndsWith(".ttf"))
                        resources.Add(new FontResource(file.Name.ToIdFormat(), new Font(file.Open())));
                    else if (file.FullName.EndsWith(".pfb"))
                        resources.Add(new PrefabResource(file.Open()));
                    else if (file.FullName.EndsWith(".as"))
                        resources.Add(new SpritePackResource(Archive.ReadAllBytes(file.FullName)));
                    else if (file.FullName.EndsWith(".txt") || file.FullName.EndsWith(".json"))
                        resources.Add(new TextFileResource(file.Open()));
                }

                return resources;
            }
        }

        public IEnumerable<ILoadableResource> LoadableResources
        {
            get
            {
                return Resources.OfType<ILoadableResource>();
            }
        }

        internal AssetArchive Archive { get; }

        public Font GetFont(string relative)
        {
            return null;
        }

        public Texture GetTexture(string relative, IntRect area = new IntRect())
        {
            return Archive.GetTexture(relative, area);
        }

        public StarMapColliderInfo GetSMCollider(string relative)
        {
            return Archive.ReadSMCColliderBinary(relative);
        }

        internal PackageResources(string archive)
        {
            Archive = AssetArchive.ReadArchive(archive);
        }
    }
}

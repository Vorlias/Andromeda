using Andromeda.Colliders;
using SFML.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Andromeda.Resources
{
    public class FolderResources : IResourcePackage
    {
        DirectoryInfo di;

        public IEnumerable<IResource> Resources
        {
            get
            {
                List<IResource> resources = new List<IResource>();
                foreach (var file in di.GetFiles())
                {
                    if (file.Extension == ".png" || file.Extension == ".jpg")
                        resources.Add(new TextureResouce(file.Name.ToIdFormat(), new Texture(file.FullName)));
                    else if (file.Extension == ".ttf")
                        resources.Add(new FontResource(file.Name.ToIdFormat(), new Font(file.FullName)));
                    else if (file.Extension == ".smc")
                        resources.Add(new SMColliderResource(new StarMapColliderInfo(new FileStream(file.FullName, FileMode.Open))));
                    else if (file.Extension == ".pfb")
                        resources.Add(new PrefabResource(new FileStream(file.FullName, FileMode.Open)));
                    else if (file.Extension == ".as")
                        resources.Add(new SpritePackResource(File.ReadAllBytes(file.FullName)));
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

        public Font GetFont(string relative)
        {
            return new Font(relative);
        }

        public FileInfo[] GetFilesMatchingExtension(string searchPattern)
        {
            return di.GetFiles(searchPattern);
        }

        public Texture GetTexture(string relative, IntRect area = new IntRect())
        {
            return new Texture(relative);
        }

        public StarMapColliderInfo GetSMCollider(string relative)
        {
            return new StarMapColliderInfo(File.OpenRead(relative));
        }

        internal FolderResources(string folder)
        {
            di = new DirectoryInfo(folder);
        }
    }
}

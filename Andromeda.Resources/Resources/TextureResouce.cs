using Andromeda.System;
using SFML.Graphics;

namespace Andromeda.Resources
{
    public class TextureResouce : ILoadableResource
    {
        public string Id { get; }
        public ResourceType Type => ResourceType.Texture;

        internal Texture Texture { get; }

        public void Load()
        {
            TextureManager.Instance.Add(Id, Texture);
        }

        internal TextureResouce(string id, Texture tx)
        {
            Texture = tx;
            Id = id;
        }
    }
}

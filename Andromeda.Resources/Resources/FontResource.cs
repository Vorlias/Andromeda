using Andromeda.System;
using SFML.Graphics;

namespace Andromeda.Resources
{
    public class FontResource : ILoadableResource
    {
        public ResourceType Type => ResourceType.Font;

        internal Font Font { get; }

        public string Id { get; }

        public void Load()
        {
            FontManager.Instance.Add(Id, Font);
        }

        internal FontResource(string id, Font tx)
        {
            Font = tx;
            Id = id;
        }
    }
}

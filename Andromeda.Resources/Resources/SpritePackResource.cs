using Andromeda.Resources.FileTypes;
using Andromeda.Resources.Types;
using Andromeda.System;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda.Resources
{

    public class SpritePackResource : ILoadableResource
    {
        public string Id { get; }

        public ResourceType Type => ResourceType.Sprite;

        Dictionary<string, Texture> sprites;

        public IEnumerable<KeyValuePair<string, Texture>> Sprites => sprites;

        public void Load()
        {
            foreach (var sprite in sprites)
            {
                TextureManager.Instance.Add(sprite.Key, sprite.Value);
            }
        }

        internal SpritePackResource(byte[] spritePackData)
        {
            sprites = new Dictionary<string, Texture>();

            if (SpriteFile.Decode(spritePackData, out byte[] spritesheetData, out byte[] imageData))
            {
                TextureMap textureMap = TextureMap.FromJSON(Encoding.ASCII.GetString(spritesheetData));
                foreach (var info in textureMap.Metadata)
                {
                    Texture texture = new Texture(new MemoryStream(imageData), info.TextureRect)
                    {
                        Smooth = true
                    };
                    sprites.Add(info.Id, texture);
                }
            }
        }
    }
}

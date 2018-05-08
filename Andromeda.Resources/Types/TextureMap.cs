using Newtonsoft.Json.Linq;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda.Resources.Types
{
    internal class TextureMap
    {
        HashSet<TextureMapMetadata> metadata = new HashSet<TextureMapMetadata>();

        private TextureMap()
        {

        }

        public TextureMapMetadata[] Metadata
        {
            get
            {
                return metadata.ToArray();
            }
        }

        public static TextureMap FromJSON(string jsonString)
        {
            TextureMap map = new TextureMap();
            var root = JArray.Parse(jsonString);
            var spriteRoot = root[0];
            var sprites = spriteRoot["Sprites"];

            foreach (var value in sprites)
            {
                //var test = value.ToObject<Dictionary<string, int[]>>();
                JProperty property = value as JProperty;
                var array = value.Values().Select(v => v.Value<int>()).ToArray();

                TextureMapMetadata coords = new TextureMapMetadata
                {
                    Id = property.Name,
                    TextureRect = new IntRect(array[0], array[1], array[2], array[3])
                };

                map.metadata.Add(coords);

            }

            return map;
        }
    }
}

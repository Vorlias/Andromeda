using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using VorliasEngine2D.System.Utility;
using VorliasEngine2D.System.Internal;

namespace VorliasEngine2D.System
{
    public class InvalidTextureIdException : Exception
    {
        public InvalidTextureIdException(string textureId) : base ("Invalid texture id given: " + textureId)
        {

        }
    }

    public class TextureManager : ResourceManager<Texture>
    {
        static TextureManager textureManager = new TextureManager();
        public static TextureManager Instance
        {
            get => textureManager;
        }

        public void LoadToId(string id, string file, IntRect area = new IntRect())
        {
            Texture newTexture = new Texture(new Image(file), area);
            Add(id, newTexture);
        }
    }
}

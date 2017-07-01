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
        public static TextureManager Instance
        {
            get => ManagerInstance as TextureManager;
        }

        public void LoadToId(string id, string file, IntRect area = new IntRect())
        {
            Texture newTexture = new Texture(new Image(file), area);
            Add(id, newTexture);
        }
    }
}

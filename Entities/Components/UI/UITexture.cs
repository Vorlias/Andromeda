using SFML.Graphics;
using Andromeda.System;

namespace Andromeda.Entities.Components.UI
{
    /// <summary>
    /// Information about a texture
    /// </summary>
    public class UITexture
    {
        Texture texture;
        
        public Texture Texture
        {
            get => texture;
            set => texture = value;
        }

        public static implicit operator Texture(UITexture textureInfo)
        {
            return textureInfo.Texture;
        }

        public string Id
        {
            get => TextureManager.Instance.FindId(Texture);
            set => texture = TextureManager.Instance.Get(value);
        }

        public UITexture(Texture texture)
        {
            this.texture = texture;
        }

        public UITexture(string textureId)
        {
            texture = TextureManager.Instance.Get(textureId);
        }
    }
}

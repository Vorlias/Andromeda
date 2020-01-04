using SFML.Graphics;

namespace Andromeda.Entities.Components
{
    public interface ITextureComponent : IRenderableComponent
    {
        /// <summary>
        /// The TextureId of the sprite
        /// </summary>
        string TextureId
        {
            set;
            get;
        }

        /// <summary>
        /// The texture of this sprite
        /// </summary>
        Texture Texture
        {
            get;
            set;
        }
    }
}

using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VorliasEngine2D.Entities.Components
{
    public interface IRenderableComponent : IComponent, Drawable
    {

    }

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

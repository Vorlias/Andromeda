using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VorliasEngine2D.Entities.Components
{
    public enum RenderOrder
    {
        Background = 0,
        Normal = 10,
        Character = 100,
        Foreground = 1000
    }

    public interface IRenderableComponent : IComponent, Drawable
    {
        RenderOrder RenderOrder
        {
            get;
            set;
        }
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

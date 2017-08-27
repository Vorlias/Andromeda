using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vorlias2D.Entities.Components.Internal;
using Vorlias2D.Serialization;
using Vorlias2D.System;

namespace Vorlias2D.Entities.Components
{
    public abstract class TextureComponent : Component, ITextureComponent
    {
        Texture texture;
        string textureId;
        Color color = Color.White;

        public Color Color
        {
            get => color;
            set => color = value;
        }

        [SerializableProperty("TextureId")]
        /// <summary>
        /// The TextureId of the sprite
        /// </summary>
        public string TextureId
        {
            set
            {
                textureId = value;
                texture = TextureManager.Instance.Get(value);
            }
            get
            {
                if (textureId == null)
                    return "";
                else
                    return textureId;
            }
        }

        /// <summary>
        /// The texture of this sprite
        /// </summary>
        public Texture Texture
        {
            get
            {
                return texture;
            }
            set
            {
                texture = value;
                textureId = null;
            }
        }

        RenderOrder renderOrder = RenderOrder.Normal;

        public RenderOrder RenderOrder
        {
            get => renderOrder;
            set => renderOrder = value;
        }

        public abstract void Draw(RenderTarget target, RenderStates states);
    }
}

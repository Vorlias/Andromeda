using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.System;

namespace VorliasEngine2D.Entities.Components
{
    public class SpriteRenderer : Drawable, IComponent
    {
        public string Name
        {
            get
            {
                return "SpriteRenderer";
            }
        }

        Texture texture;

        /// <summary>
        /// The TextureId of the sprite (Settable only atm)
        /// </summary>
        public string TextureId
        {
            set
            {
                texture = TextureManager.Instance.GetTexture(value);
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
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (texture == null)
            {
                RectangleShape rs = new RectangleShape(new SFML.System.Vector2f(10, 10));
                target.Draw(rs);
            }
        }
    }
}

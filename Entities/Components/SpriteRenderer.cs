using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.System;

namespace VorliasEngine2D.Entities.Components
{
    public enum SpriteRenderOrder
    {
        Background = 0,
        Normal = 10,
        Character = 100,
        Foreground = 1000
    }

    public class SpriteRenderer : Drawable, IComponent
    {
        private SpriteRenderOrder renderOrder = SpriteRenderOrder.Normal;

        /// <summary>
        /// The render order of this sprite
        /// </summary>
        public SpriteRenderOrder RenderOrder
        {
            get
            {
                return renderOrder;
            }
            set
            {
                renderOrder = value;
            }
        }

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

        private Entity entity;
        public Entity Entity
        {
            get
            {
                return entity;
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            Transform transform = entity.Transform;
            if (texture == null)
            {
                RectangleShape rs = new RectangleShape(new SFML.System.Vector2f(100, 100));
                rs.Origin = transform.Origin;
                rs.Position = transform.Position;
                target.Draw(rs);
            }
            else
            {
                Sprite sprite = new Sprite(texture);
                sprite.Position = transform.Position;
                target.Draw(sprite);
            }
        }

        /// <summary>
        /// Called when the component is added to an entity
        /// </summary>
        /// <param name="entity">The entity it is added to</param>
        /// <exception cref="SetEntityInvalidException">Called if the user tries to set it</exception>
        public void OnComponentInit(Entity entity)
        {
            if (this.entity == null)
                this.entity = entity;
            else
                throw new SetEntityInvalidException();
        }
    }
}

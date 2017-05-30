using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.System;
using VorliasEngine2D.System.Utility;

namespace VorliasEngine2D.Entities.Components
{


    public sealed class AnchorPoint
    {
        public float X
        {
            get;
            set;
        }

        public float Y
        {
            get;
            set;
        }

        public AnchorPoint(float x, float y)
        {
            X = Math.Min(Math.Max(0.0f, x), 1.0f);
            Y = Math.Min(Math.Max(0.0f, y), 1.0f);
        }
        
        internal Vector2f AppliedTo(SpriteRenderer renderer)
        {
            Vector2f textureSize = renderer.Texture.Size.ToFloatVector();
            textureSize.X *= X;
            textureSize.Y *= Y;

            return textureSize;
        }
    }

    public sealed class SpriteRenderer : ITextureComponent
    {
        private RenderOrder renderOrder = RenderOrder.Normal;

        /// <summary>
        /// The render order of this sprite
        /// </summary>
        public RenderOrder RenderOrder
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

        private AnchorPoint renderAnchor = new AnchorPoint(0.5f, 0.5f);

        /// <summary>
        /// The origin for rendering
        /// </summary>
        public AnchorPoint RenderOriginAnchor
        {
            get
            {
                return renderAnchor;
            }
            set
            {
                renderAnchor = value;
            }
        }

        public Vector2f Origin
        {
            get
            {
                return renderAnchor.AppliedTo(this);
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
        string textureId;

        /// <summary>
        /// The TextureId of the sprite
        /// </summary>
        public string TextureId
        {
            set
            {
                textureId = value;
                texture = TextureManager.Instance.GetTexture(value);
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

        private Entity entity;
        public Entity Entity
        {
            get
            {
                return entity;
            }
        }

        public bool AllowsMultipleInstances
        {
            get
            {
                return false;
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            Transform transform = entity.Transform;
            if (texture == null)
            {
                RectangleShape rs = new RectangleShape(new SFML.System.Vector2f(100, 100));
                rs.Origin = new Vector2f( 100 * renderAnchor.X, 100 * renderAnchor.Y);
                rs.Position = transform.Position;
                target.Draw(rs);
            }
            else
            {
                Sprite sprite = new Sprite(texture);
                sprite.Origin = new Vector2f(texture.Size.X * renderAnchor.X, texture.Size.Y * renderAnchor.Y);
                sprite.Position = transform.Position;
                sprite.Rotation = transform.Rotation;
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

        public void OnComponentCopy(Entity source, Entity copy)
        {
            SpriteRenderer renderer = copy.AddComponent<SpriteRenderer>();

            // Set the texture to be the same
            if (texture != null)
                renderer.texture = texture;
        }
    }
}

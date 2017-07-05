using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.Entities.Components.Internal;
using VorliasEngine2D.Serialization;
using VorliasEngine2D.System;

namespace VorliasEngine2D.Entities.Components
{

    public sealed class SpriteRenderer : Component, ITextureComponent
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

        public override string Name
        {
            get
            {
                return "SpriteRenderer";
            }
        }

        Texture texture;
        string textureId;

        [PersistentProperty("TextureId")]
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

        public override bool AllowsMultipleInstances
        {
            get
            {
                return false;
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            //target.SetView(StateApplication.Application.WorldView);

            Transform transform = entity.Transform;
            Vector2f position = transform.Position;


            if (transform.Axis == Transform.AxisType.World)
            {
                // TODO: Position based on world offset to window corner
                //Vector2f middle = target.GetView().Size / 2;
                //position = position - middle;
            }

            if (texture == null)
            {
                RectangleShape rs = new RectangleShape(new Vector2f(100, 100));
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

        public override void OnComponentCopy(Entity source, Entity copy)
        {
            SpriteRenderer renderer = copy.AddComponent<SpriteRenderer>();

            // Set the texture to be the same
            if (texture != null)
                renderer.texture = texture;
        }
    }
}

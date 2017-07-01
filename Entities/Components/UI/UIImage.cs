using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using VorliasEngine2D.Entities.Components.Internal;
using VorliasEngine2D.System;
using VorliasEngine2D.Serialization;

namespace VorliasEngine2D.Entities.Components
{
    /// <summary>
    /// An image in a User Interface
    /// </summary>
    public class UIImage : UIComponent, ITextureComponent
    {
        public override string Name => "UIImage";

        private Texture texture;
        private string textureId;

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

        public override void Draw(RenderTarget target, RenderStates states)
        {
            //target.SetView(StateApplication.Application.InterfaceView);

            if (texture != null)
            {
                Sprite sprite = new Sprite(texture)
                {
                    Position = Transform.PositionRelative.GlobalAbsolute,
                    Color = Color
                };

                target.Draw(sprite);
            }
        }

        [PersistentProperty("TextureId", PropertyType = SerializedPropertyType.String)]
        public string TextureId
        {
            get
            {
                return textureId;
            }

            set
            {
                texture = TextureManager.Instance.Get(value);
                Transform.Size = new UICoordinates(0, texture.Size.X, 0, texture.Size.Y);
                textureId = value;
                RenderOrder = RenderOrder.Interface;
            }
        }

        public override void OnComponentCopy(Entity source, Entity copy)
        {
            var sourceImage = source.GetComponent<UIImage>();
            var copyImage = copy.AddComponent<UIImage>();

            if (sourceImage.TextureId != null && sourceImage.TextureId != string.Empty)
                copyImage.TextureId = sourceImage.TextureId;
        }

        public override void AfterUpdate()
        {
            
        }

        public override void BeforeUpdate()
        {
            
        }

        public override void Update()
        {

        }

        public override string ToString()
        {
            return "UI Image: " + TextureId;
        }
    }
}

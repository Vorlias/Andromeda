using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using VorliasEngine2D.System;
using SFML.System;
using VorliasEngine2D.Entities.Components.Internal;
using VorliasEngine2D.System.Debug;
using SFML.Window;
using VorliasEngine2D.Entities.Components.UI;

namespace VorliasEngine2D.Entities.Components
{
    public class UIImageButton : UIButton, ITextureComponent, IComponentEventListener
    {
        public event MouseEvent OnButtonPressed;

        public override string Name
        {
            get
            {
                return "ImageButton";
            }
        }


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
                Transform.Size = new UICoordinates(0, texture.Size.X, 0, texture.Size.Y);
                RenderOrder = RenderOrder.Interface;
                ButtonCollider.CreateRectCollider(Transform.Size.GlobalAbsolute);
            }
        }

        public string TextureId
        {
            get
            {
                return textureId;
            }

            set
            {
                texture = TextureManager.Instance.GetTexture(value);
                Transform.Size = new UICoordinates(0, texture.Size.X, 0, texture.Size.Y);
                textureId = value;
                RenderOrder = RenderOrder.Interface;
                ButtonCollider.CreateRectCollider(Transform.Size.GlobalAbsolute);
            }
        }

        public override void OnComponentCopy(Entity source, Entity copy)
        {
            var copyComponent = copy.AddComponent<UIImageButton>();

            if (TextureId != null)
                copyComponent.TextureId = TextureId;
            else if (texture != null)
                copyComponent.Texture = texture;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            UICoordinates size = Transform.Size;
            Vector2f totalSize = size.Absolute(target);

            if (texture != null)
            {
                Sprite sprite = new Sprite(texture)
                {
                    Position = Entity.Transform.Position,
                    Scale = new Vector2f(totalSize.X / texture.Size.X, totalSize.Y / texture.Size.Y)
                };

                target.Draw(sprite);
            }
        }

        public override void OnButtonInit(Entity entity)
        {
            var rectCollider = Entity.AddComponent<PolygonRectCollider>();
            rectCollider.CreateRectCollider(new Vector2f(100, 20));
        }

        public override void ButtonClick(MouseInputAction inputAction)
        {
            OnButtonPressed?.Invoke(inputAction);
        }

        public override void Update()
        {
            
        }

        public override void AfterUpdate()
        {
            
        }

        public override void BeforeUpdate()
        {
            
        }
    }
}

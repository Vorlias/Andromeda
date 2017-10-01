using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using Andromeda2D.System;
using SFML.System;
using Andromeda2D.Entities.Components.Internal;
using Andromeda2D.System.Debug;
using SFML.Window;
using Andromeda2D.Entities.Components.UI;
using Andromeda2D.Serialization;
using Andromeda2D.System.Utility;

namespace Andromeda2D.Entities.Components
{
    public class UIImageButton : UIInteractable, ITextureComponent, IEventListenerComponent
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
                Transform.LocalSize = new UICoordinates(0, texture.Size.X, 0, texture.Size.Y);
                RenderOrder = RenderOrder.Interface;
                ButtonCollider.CreateRectCollider(Transform.LocalSize.GlobalAbsolute);
            }
        }

        [SerializableProperty("TextureId", PropertyType = SerializedPropertyType.String)]
        public string TextureId
        {
            get
            {
                return textureId;
            }

            set
            {
                texture = TextureManager.Instance.Get(value);
                Transform.LocalSize = new UICoordinates(0, texture.Size.X, 0, texture.Size.Y);
                textureId = value;
                RenderOrder = RenderOrder.Interface;
                ButtonCollider.CreateRectCollider(Transform.LocalSize.GlobalAbsolute);
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
            UICoordinates size = Transform.LocalSize;
            Vector2f totalSize = size.Absolute(target);

            if (texture != null)
            {
                Sprite sprite = new Sprite(texture)
                {
                    Position = Entity.Transform.Position,
                    Scale = new Vector2f(totalSize.X / texture.Size.X, totalSize.Y / texture.Size.Y),
                    Color = Color
                };

                target.Draw(sprite);
            }
        }

        public override void OnButtonInit(Entity entity)
        {
            var rectCollider = Entity.AddComponent<PolygonRectCollider>();
            rectCollider.CreateRectCollider(new Vector2f(100, 20));
        }

        public override void MouseButtonClicked(MouseInputAction inputAction, Vector2f mouseRelativePosition)
        {
            OnButtonPressed?.Invoke(inputAction);
        }

        public override void AfterUpdate()
        {

        }

        public override void BeforeUpdate()
        {
            
        }
    }
}

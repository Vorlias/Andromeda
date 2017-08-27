using SFML.Graphics;
using SFML.System;
using System;
using Vorlias2D.Entities.Components.Internal;
using Vorlias2D.Serialization;
using Vorlias2D.System;

namespace Vorlias2D.Entities.Components.UI
{

    public class UIImageCheckButton : UIInteractable, IComponentEventListener
    {
        public delegate void CheckStateChange(bool state);
        public event MouseEvent OnCheckboxPressed;
        public event CheckStateChange OnCheckboxStateChanged;


        public override string Name
        {
            get
            {
                return "ImageButton";
            }
        }

        bool checkedState = false;

        /// <summary>
        /// Whether or not the check button is checked
        /// </summary>
        public bool Checked
        {
            get => checkedState;
            set => checkedState = value;
        }

        UICheckButtonTextures textures = new UICheckButtonTextures();
        public UICheckButtonTextures Textures
        {
            get => textures;
        }

        public override void OnComponentCopy(Entity source, Entity copy)
        {
            //var copyComponent = copy.AddComponent<UIImageButton>();
            
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            UICoordinates size = Transform.Size;
            Vector2f totalSize = size.Absolute(target);

            if (Textures.Unchecked != null)
            {
                Texture texture;

                if (checkedState)
                    texture = Textures.Checked;
                else
                    texture = Textures.Unchecked;

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
            //var rectCollider = Entity.AddComponent<PolygonRectCollider>();
            //rectCollider.CreateRectCollider(new Vector2f(100, 20));
        }

        public override void ButtonClick(MouseInputAction inputAction)
        {
            checkedState = !checkedState;
            OnCheckboxPressed?.Invoke(inputAction);
            OnCheckboxStateChanged?.Invoke(checkedState);
        }

        public override void AfterUpdate()
        {

        }

        public override void BeforeUpdate()
        {
            /*Texture texture = Textures.Unchecked;

            if (texture != null && ButtonCollider.Polygon == null)
            {
                
                ButtonCollider.CreateRectCollider(new Vector2f(texture.Size.X, texture.Size.Y));
               
            }

            Transform.Size = new Vector2f(texture.Size.X, texture.Size.Y);*/
        }
    }
}

using SFML.Graphics;
using SFML.System;
using System;
using Andromeda.Entities.Components.Internal;
using Andromeda.Serialization;
using Andromeda.System;

namespace Andromeda.Entities.Components.UI
{

    public class UIImageCheckButton : UIInteractable, IEventListenerComponent
    {
        public delegate void CheckStateChange(bool state);
        public event MouseEvent OnCheckboxPressed;
        public event CheckStateChange OnCheckboxStateChanged;

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
            UICoordinates size = Transform.LocalSize;
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

        }

        public override void AfterUpdate()
        {

        }

        public override void BeforeUpdate()
        {

        }

        public override void MouseButtonClicked(MouseInputAction inputAction, bool inside)
        {
            if (inside)
            {
                checkedState = !checkedState;
                OnCheckboxPressed?.Invoke(inputAction);
                OnCheckboxStateChanged?.Invoke(checkedState);
            }
        }
    }
}

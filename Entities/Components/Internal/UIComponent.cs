using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using Andromeda2D.System;
using SFML.Window;
using Andromeda2D.System.Utility;
using Andromeda2D.Serialization;

namespace Andromeda2D.Entities.Components.Internal
{
    public abstract class UIComponent : Component, IRenderableComponent, IUpdatableComponent
    {
        public delegate void MouseEvent(MouseInputAction action);
        public delegate void KeyboardEvent(KeyboardInputAction action);
        public delegate void InterfaceEvent(UserInterfaceAction action);

        public UIComponent()
        {
            input = new UserInputManager();
        }

        private UserInputManager input;
        internal UserInputManager Input
        {
            get
            {
                return input;
            }
        }

        public UITransform Transform
        {
            get
            {
                if (!entity.HasComponent<UITransform>())
                    entity.AddComponent<UITransform>();

                return entity.GetComponent<UITransform>();
            }
        }

        Color color = Color.White;
        /// <summary>
        /// The colour of this UIComponent
        /// </summary>
        public Color Color
        {
            get => color;
            set => color = value;
        }

        /// <summary>
        /// The mouse position
        /// </summary>
        internal Vector2i MousePosition
        {
            get
            {
                return Mouse.GetPosition(StateApplication.Application.Window);
            }
        }

        /// <summary>
        /// Whether or not the mouse is over
        /// </summary>
        internal bool IsMouseOver
        {
            get
            {
                FloatRect mouseRect = new FloatRect(MousePosition.ToFloat(), new Vector2f(2, 2));
                FloatRect uiRect = new FloatRect(Transform.LocalPosition, Transform.Size);

                return mouseRect.Intersects(uiRect);
            }
        }

        private RenderOrder renderOrder;

        [SerializableProperty("RenderOrder", PropertyType = SerializedPropertyType.Enum)]
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

        public override bool AllowsMultipleInstances
        {
            get
            {
                return false;
            }
        }

        public override string Name
        {
            get
            {
                return "UIRenderer";
            }
        }

        public UpdatePriority UpdatePriority => UpdatePriority.Interface;

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
        }

        public abstract void Update();
        public abstract void AfterUpdate();
        public abstract void BeforeUpdate();
    }
}

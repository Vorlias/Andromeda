using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using Andromeda.System;
using SFML.Window;
using Andromeda.System.Utility;
using Andromeda.Serialization;
using Andromeda.System.Types;

namespace Andromeda.Entities.Components.Internal
{
    [RequireComponents(typeof(UITransform)), DisallowMultiple]
    public abstract class UIComponent : Component, IInterfaceComponent
    {
        public delegate void MouseEvent(MouseInputAction action);
        public delegate void KeyboardEvent(KeyboardInputAction action);
        public delegate void InterfaceEvent(UserInterfaceAction action);

        public const int ZINDEX_MAX = 1000;
        public const int ZINDEX_MIN = 0;

        static IntNumberRange zIndexRange = new IntNumberRange(ZINDEX_MIN, ZINDEX_MAX);
        public static IntNumberRange ZIndexRange
        {
            get => zIndexRange;
        }

        /// <summary>
        /// The opacity of this UI component
        /// </summary>
        public int Transparency { get; set; }

        int _zIndex = 0;

        /// <summary>
        /// The ZIndex of this UIComponent
        /// </summary>
        public int ZIndex
        {
            get
            {
                if (Entity.Parent != null && Entity.Parent.HasComponent<IInterfaceComponent>())
                {
                    var interfaceComponent = Entity.Parent.GetComponent<IInterfaceComponent>();
                    return interfaceComponent.ZIndex + _zIndex;
                }
                else
                    return _zIndex;
            }
            set => _zIndex = zIndexRange.Clamped(value);
        }

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

        /// <summary>
        /// Whether or not this component is visible
        /// </summary>
        public bool Visible
        {
            get => Entity.Visible;
            set => Entity.Visible = value;
        }

        /// <summary>
        /// The resulting size of this UIComponent relative to the window
        /// </summary>
        public Vector2f AbsoluteSize => Transform.LocalSize.GlobalAbsolute;

        /// <summary>
        /// The resulting window position of this UIComponent
        /// </summary>
        public Vector2f AbsolutePosition => Transform.LocalPosition.GlobalAbsolute;

        /// <summary>
        /// The position of this UIComponent
        /// </summary>
        public UICoordinates Position
        {
            get => Transform.LocalPosition;
            set => Transform.LocalPosition = value;
        }

        /// <summary>
        /// The size of this UIComponent
        /// </summary>
        public UICoordinates Size
        {
            get => Transform.LocalSize;
            set => Transform.LocalSize = value;
        }

        /// <summary>
        /// The transform of this UIComponent
        /// </summary>
        public UITransform Transform
        {
            get
            {
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
                FloatRect uiRect = new FloatRect(Transform.GlobalPosition, Transform.LocalSize);

                return mouseRect.Intersects(uiRect);
            }
        }

        private RenderOrder renderOrder;

        /// <summary>
        /// The render order of this UIComponent
        /// </summary>
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

        public virtual UpdatePriority UpdatePriority => UpdatePriority.Interface;


        public virtual void Draw(RenderTarget target, RenderStates states)
        {
        }

        public abstract void Update();
        public abstract void AfterUpdate();
        public abstract void BeforeUpdate();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using VorliasEngine2D.System;
using SFML.Window;
using VorliasEngine2D.System.Utility;
using VorliasEngine2D.Serialization;

namespace VorliasEngine2D.Entities.Components.Internal
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
                PolygonRectCollider rec;
                if (Entity.FindComponent(out rec))
                {
                    return rec.Polygon.Transform(Entity.Transform.Position, Entity.Transform.Origin, Entity.Transform.Rotation).ContainsPoint(MousePosition.ToFloat());
                }

                return false;
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

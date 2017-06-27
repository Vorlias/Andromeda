using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.Entities.Components.Internal;
using VorliasEngine2D.System;
using VorliasEngine2D.System.Debug;

namespace VorliasEngine2D.Entities.Components.UI
{
    /// <summary>
    /// Abstract Button UI class for creating custom buttons
    /// </summary>
    public abstract class UIButton : UIComponent, IComponentEventListener
    {

        public override abstract string Name
        {
            get;
        }

        private bool mouseDown = false;

        /// <summary>
        /// Whether or not the mouse is down
        /// </summary>
        public bool IsMouseDown
        {
            get
            {
                return mouseDown;
            }
        }

        /// <summary>
        /// Used to determine collision between button and mouse (For hover detection)
        /// </summary>
        public PolygonRectCollider ButtonCollider
        {
            get
            {
                return Entity.GetComponent<PolygonRectCollider>();
            }
        }

        public abstract override void OnComponentCopy(Entity source, Entity copy);

        public abstract void OnButtonInit(Entity entity);

        public override void OnComponentInit(Entity entity)
        {
            var rectCollider = Entity.AddComponent<PolygonRectCollider>();
            rectCollider.CreateRectCollider(new Vector2f(100, 20));

            OnButtonInit(entity);
        }

        public abstract override void Draw(RenderTarget target, RenderStates states);

        /// <summary>
        /// Called when the mouse is clicked on the button
        /// </summary>
        /// <param name="inputAction">The mouse input action</param>
        public abstract void ButtonClick(MouseInputAction inputAction);

        public void InputRecieved(UserInputAction inputAction)
        {
            var mouseAction = inputAction.Mouse;
            if (mouseAction?.InputState == InputState.Active && mouseAction?.Button == Mouse.Button.Left)
            {
                // if left click
                if (IsMouseOver && !IsMouseDown)
                {
                    mouseDown = true;
                    ButtonClick(mouseAction);
                }
            }
            else if (mouseAction?.InputState == InputState.Inactive && mouseAction.Button == Mouse.Button.Left)
            {
                mouseDown = false;
            }
        }
    }
}

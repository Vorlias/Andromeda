using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using VorliasEngine2D.Entities.Components.Internal;
using SFML.System;
using VorliasEngine2D.System;
using VorliasEngine2D.System.Utility;

namespace VorliasEngine2D.Entities.Components
{
    public class Camera : Component, IViewComponent, IUpdatableComponent
    {
        public override bool AllowsMultipleInstances => false;
 
        public override string Name => "Camera";

        View view;

        public View View => view;
        /// <summary>
        /// The size of the camera's view
        /// </summary>
        public Vector2f ViewSize
        {
            get => view.Size;
            set => view.Size = value;
        }

        public FloatRect Viewport
        {
            get => view.Viewport;
            set => view.Viewport = value;
        }

        /// <summary>
        /// A constraint based on the camera
        /// </summary>
        public PositionConstraint ViewportConstraint
        {
            get => new PositionConstraint(
                new Vector2f(
                    view.Center.X - view.Size.X / 2, 
                    view.Center.Y - view.Size.Y / 2), 
                new Vector2f(
                    view.Center.X + view.Size.X / 2,
                    view.Center.Y + view.Size.Y / 2)
                );
        }

        public Vector2f Position
        {
            get => view.Center;
        }

        public void SetView(Vector2f size)
        {
            SetView(new View(Position, new Vector2f(size.X, size.Y)));
        }

        public void SetView(View view)
        {
            this.view = view;
            StateApplication app = StateApplication.Application;
            app.Window.SetView(view);
        }

        public override void OnComponentInit(Entity entity)
        {
            StateApplication app = StateApplication.Application;
            view = app.Window.DefaultView;

            entity.AddComponent<Transform>();
        }

        public void AfterUpdate()
        {
            
        }

        public void BeforeUpdate()
        {
            
        }

        public void Update()
        {
            view.Center = Entity.GetComponent<Transform>().Position;
        }
    }
}

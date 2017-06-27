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
    public enum CameraType
    {
        World,
        Interface
    }

    public class Camera : Component, IUpdatableComponent, IRenderableComponent
    {

        public override bool AllowsMultipleInstances => false;
 
        public override string Name => "Camera";

        CameraType cameraType = CameraType.World;
        public CameraType CameraType
        {
            get => cameraType;
        }

        //private Vector2f position = new Vector2f(0, 0);

        /// <summary>
        /// The position of the camera relative to the world's center
        /// </summary>
        public Vector2f WorldPosition
        {
            get => Entity.Transform.Position;
            set => Entity.Transform.Position = value;
        }

        /// <summary>
        /// The position of the camera relative to the view of the window. This _should_ always be the center. (Used for testing)
        /// </summary>
        [Obsolete("Not yet implemented.")]
        internal Vector2f WindowPosition
        {
            get => new Vector2f(0, 0);
        }

        public UpdatePriority UpdatePriority => UpdatePriority.Camera;

        public RenderOrder RenderOrder
        {
            get => RenderOrder.Camera;
            set => throw new NotImplementedException();
        }

        public override void OnComponentInit(Entity entity)
        {
            StateApplication app = StateApplication.Application;
            //view = app.Window.DefaultView;

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
            var application = StateApplication.Application;
            View currentView = application.WorldView;
            currentView.Center = WorldPosition;
            currentView.Rotation = Entity.Transform.LocalRotation;
            application.Window.SetView(currentView);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            RectangleShape rs = new RectangleShape();
            rs.Size = new Vector2f(10, 10);
            rs.Origin = new Vector2f(-5, -5);
            rs.Position = Entity.Position;

            target.Draw(rs);
        }
    }
}

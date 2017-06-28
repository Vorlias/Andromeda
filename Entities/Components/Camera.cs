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

    public class Camera : Component, IUpdatableComponent
    {

        public override bool AllowsMultipleInstances => false;
 
        public override string Name => "Camera";

        View view;
        public View View
        {
            get => view;
        }

        CameraType cameraType = CameraType.World;
        public CameraType CameraType
        {
            get => cameraType;
            set
            {
                if (value == CameraType.Interface)
                {
                    updatePriority = UpdatePriority.Interface - 1;
                    cameraType = value;
                }
                else
                {
                    updatePriority = UpdatePriority.Camera;
                    cameraType = value;
                }
            }
        }

        /// <summary>
        /// The position of the camera relative to the world's center
        /// </summary>
        public Vector2f WorldPosition
        {
            get => Entity.Transform.Position;
            set => Entity.Transform.Position = value;
        }

        /// <summary>
        /// The position on the camera where the world's center (0, 0) is
        /// </summary>
        internal Vector2f WorldZeroPosition
        {
            get => Entity.Transform.Position - View.Center;
        }

        private UpdatePriority updatePriority = UpdatePriority.Camera;
        public UpdatePriority UpdatePriority => updatePriority;

        public RenderOrder RenderOrder
        {
            get => RenderOrder.Camera;
            set => throw new NotImplementedException();
        }

        public override void OnComponentInit(Entity entity)
        {
            StateApplication app = StateApplication.Application;

            entity.AddComponent<Transform>();
        }

        public void AfterUpdate()
        {
            
        }

        public void BeforeUpdate()
        {
            
        }

        /// <summary>
        /// Resets the camera position and rotation
        /// </summary>
        public void Reset()
        {
            WorldPosition = new Vector2f(0, 0);
            Entity.Transform.Rotation = 0;
        }

        public void Update()
        {
            var application = StateApplication.Application;

            if (cameraType == CameraType.Interface)
            {
                if (view == null)
                    view = new View(application.InterfaceView);

                view.Rotation = 0;
                view.Center = application.Window.Size.ToFloatVector() / 2;
            }
            else
            {
                if (view == null)
                    view = new View(application.WorldView);

                view.Center = WorldPosition;
                view.Rotation = Entity.Transform.LocalRotation;
            }
                
            application.Window.SetView(view);
        }
    }
}

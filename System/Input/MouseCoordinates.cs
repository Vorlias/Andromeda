using SFML.System;
using SFML.Window;
using VorliasEngine2D.System;
using VorliasEngine2D.System.Utility;
using SFML.Graphics;
using VorliasEngine2D.Entities.Components;

namespace VorliasEngine2D.System
{
    /// <summary>
    /// The coordinates of the mouse
    /// </summary>
    public struct MouseCoordinates
    {
        public GameView View
        {
            get;
        }

        /// <summary>
        /// The position of the mouse relative to the world
        /// </summary>
        public Vector2f WorldPosition
        {
            get;
        }

        /// <summary>
        /// The position of the mouse relative to the window
        /// </summary>
        public Vector2f WindowPosition
        {
            get;
        }

        internal MouseCoordinates(Application application, GameView view)
        {
            Vector2i mousePosition = Mouse.GetPosition(application.Window);
            View = view;

            if (view.Camera != null && view.Camera.View != null)
            {
                View cameraView = view.Camera.View;
                Vector2f viewCenter = cameraView.Center;
                Vector2u windowSize = application.Window.Size;
                Camera defaultCamera = view.Camera;
                Vector2f offset = new Vector2f(
                    viewCenter.X - ( windowSize.X / 2 ) + mousePosition.X,
                    viewCenter.Y - ( windowSize.Y / 2 ) + mousePosition.Y
                );

                WorldPosition = offset.Rotate(cameraView.Rotation);
                WindowPosition = new Vector2f(mousePosition.X, mousePosition.Y);
            }
            else
            {
                WindowPosition = new Vector2f(mousePosition.X, mousePosition.Y);
                WorldPosition = WindowPosition;
            }
        }
    }
}

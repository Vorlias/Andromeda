using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Andromeda2D.System.Utility;

namespace Andromeda2D.System
{
    /// <summary>
    /// A vector that represents a UI coordinate
    /// (scale, offset) for (x, y)
    /// </summary>
    public struct UICoordinates
    {
        UIAxis x;
        UIAxis y;

        public UIAxis X => x;

        public UIAxis Y => y;

        /// <summary>
        /// Same as X
        /// </summary>
        public UIAxis Width => x;

        /// <summary>
        /// Same as Y
        /// </summary>
        public UIAxis Height => y;

        

        public static UICoordinates operator +(UICoordinates left, UICoordinates right)
        {
            return new UICoordinates(left.X + right.X, left.Y + right.Y);
        }

        public static UICoordinates operator +(UICoordinates left, Vector2f right)
        {
            return new UICoordinates(left.X + right.X, left.Y + right.Y);
        }

        public static UICoordinates operator *(UICoordinates child, UICoordinates parent)
        {
            return new UICoordinates(
                new UIAxis((child.X.Scale * parent.X.Scale), child.X.Offset),
                new UIAxis((child.Y.Scale * parent.Y.Scale), child.Y.Offset)
            );
        }

        public static implicit operator UICoordinates(Vector2f vector)
        {
            return new UICoordinates(0, vector.X, 0, vector.Y);
        }

        public static implicit operator Vector2f(UICoordinates coordinates)
        {
            return coordinates.GlobalAbsolute;
        }

        public static UICoordinates operator *(UICoordinates left, Vector2f right)
        {
            // Used for size relative conversion
            return new UICoordinates(new UIAxis(right.X / left.X.Scale, left.X.Offset), new UIAxis(right.Y * left.Y.Scale, left.Y.Offset));
        }

        public UICoordinates(float scaleX, float offsetX, float scaleY, float offsetY)
        {
            x = new UIAxis(scaleX, offsetX);
            y = new UIAxis(scaleY, offsetY);
        }

        public UICoordinates(UIAxis left, UIAxis right)
        {
            x = left;
            y = right;
        }

        /// <summary>
        /// Gets the absolute value relative to the window
        /// </summary>
        public Vector2f GlobalAbsolute
        {
            get
            {
                return Absolute(StateApplication.Application.Window);
            }
        }

        /// <summary>
        /// Returns the absolute Vector2 based on the RenderTarget size
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        internal Vector2f Absolute(RenderTarget window)
        {
            return new Vector2f(x.Offset + window.Size.X * x.Scale, y.Offset + window.Size.Y * y.Scale);
        }

        internal Vector2f Absolute(View view)
        {
            return new Vector2f(x.Offset + view.Size.X * x.Scale, y.Offset + view.Size.Y * y.Scale);
        }

        public override string ToString()
        {
            return string.Format("{0}; {1}", X, Y);
        }
    }
}

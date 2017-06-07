using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.System.Utility;

namespace VorliasEngine2D.System
{
    public class UIAxis
    {
        public float Scale
        {
            get;
            set;
        }

        public float Offset
        {
            get;
            set;
        }

        internal UIAxis(float scale, float offset)
        {
            Scale = scale;
            Offset = offset;
        }

        public override string ToString()
        {
            return Scale + ", " + Offset;
        }
    }

    /// <summary>
    /// A vector that represents a UI coordinate
    /// (scale, offset) for (x, y)
    /// </summary>
    public class UICoordinates // TODO: Think of better name
    {
        UIAxis x;
        UIAxis y;

        public UIAxis X
        {
            get
            {
                return x;
            }
        }

        /// <summary>
        /// Same as X
        /// </summary>
        public UIAxis Width
        {
            get
            {
                return x;
            }
        }

        /// <summary>
        /// Same as Y
        /// </summary>
        public UIAxis Height
        {
            get
            {
                return y;
            }
        }

        public UIAxis Y
        {
            get
            {
                return y;
            }
        }

        public UICoordinates(float scaleX, float offsetX, float scaleY, float offsetY)
        {
            x = new UIAxis(scaleX, offsetX);
            y = new UIAxis(scaleY, offsetY);
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

        public override string ToString()
        {
            return string.Format("{0}; {1}", X, Y);
        }
    }
}

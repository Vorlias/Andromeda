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
    public class UIVector2
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

        internal UIVector2(float scale, float offset)
        {
            Scale = scale;
            Offset = offset;
        }
    }

    /// <summary>
    /// A vector that represents a UI coordinate
    /// (scale, offset) for (x, y)
    /// </summary>
    public class UIVector4
    {
        UIVector2 x;
        UIVector2 y;

        public UIVector2 X
        {
            get
            {
                return x;
            }
        }

        public UIVector2 Y
        {
            get
            {
                return y;
            }
        }

        public UIVector4(float scaleX, float offsetX, float scaleY, float offsetY)
        {
            x = new UIVector2(scaleX, offsetX);
            y = new UIVector2(scaleY, offsetY);
        }

        /// <summary>
        /// Returns the absolute Vector2 based on the RenderTarget size
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        internal Vector2f Absolute(RenderTarget window)
        {
            Vector2f windowSize = window.Size.ToFloatVector();
            return new Vector2f(windowSize.X * x.Scale + x.Offset, windowSize.Y * y.Scale + y.Offset);
        }
    }
}

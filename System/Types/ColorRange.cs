using Andromeda.System.Utility;
using SFML.Graphics;

namespace Andromeda.System.Types
{
    /// <summary>
    /// A range of colours between two colours
    /// </summary>
    public struct ColorRange
    {
        /// <summary>
        /// The start of the range
        /// </summary>
        public Color Start { get; set; }
        /// <summary>
        /// The end of the range
        /// </summary>
        public Color End { get; set; }

        /// <summary>
        /// Return a color based on a delta (0-1)
        /// </summary>
        /// <param name="delta"></param>
        /// <returns></returns>
        public Color Lerp(float delta)
        {
            if (Start == End)
            {
                return Start;
            }
            else
                return Start.Lerp(End, delta);
        }

        public static implicit operator ColorRange(Color color)
        {
            return new ColorRange() { Start = color, End = color };
        }
    }
}

using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda.System.Utility
{
    /// <summary>
    /// Screen-based utilities
    /// </summary>
    public class ScreenUtil
    {
        /// <summary>
        /// Get the aspect ratio of the specified video mode
        /// </summary>
        /// <param name="mode">The video mode</param>
        /// <returns>The aspect ratio (Height, Width)</returns>
        public static Vector2u GetAspectRatio(VideoMode mode)
        {
            uint nGCD = GetGreatestCommonDivisor(mode.Height, mode.Width);
            return new Vector2u(mode.Width / nGCD, mode.Height / nGCD);
        }

        /// <summary>
        /// Get the aspect ratio of the specified size
        /// </summary>
        /// <param name="size">The size</param>
        /// <returns>The aspect ratio (Height, Width)</returns>
        public static Vector2u GetAspectRatio(Vector2u size)
        {
            uint nGCD = GetGreatestCommonDivisor(size.Y, size.X);
            return new Vector2u(size.X / nGCD, size.Y / nGCD);
        }

        private static uint GetGreatestCommonDivisor(uint a, uint b)
        {
            return b == 0 ? a : GetGreatestCommonDivisor(b, a % b);
        }
    }
}

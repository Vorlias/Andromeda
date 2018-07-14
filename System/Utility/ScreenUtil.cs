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
        public Vector2u GetAspectRatio(VideoMode mode)
        {
            uint nGCD = GetGreatestCommonDivisor(mode.Height, mode.Width);
            return new Vector2u(mode.Height / nGCD, mode.Width / nGCD);
        }

        private static uint GetGreatestCommonDivisor(uint a, uint b)
        {
            return b == 0 ? a : GetGreatestCommonDivisor(b, a % b);
        }
    }
}

using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VorliasEngine2D.System.Utility
{
    internal class VectorUtil
    {
        const double DEGREES_TO_RADIANS = Math.PI / 180;
        const double RADIANS_TO_DEGREES = 180 / Math.PI;
        
        /// <summary>
        /// Returns a Vector2f rotated by the specified angle
        /// </summary>
        /// <param name="vector">The vector to rotate</param>
        /// <param name="angle">The angle</param>
        /// <returns>The vector rotated by the angle</returns>
        public static Vector2f GetRotatedVector(Vector2f vector, float angle)
        {
            float targetCos = (float)Math.Cos(angle * DEGREES_TO_RADIANS);
            float targetSin = (float)Math.Sin(DEGREES_TO_RADIANS * angle);

            return new Vector2f(
                targetCos * vector.X - targetSin * vector.Y,
                targetSin * vector.X - targetCos * vector.Y
            );
        }
    }
}

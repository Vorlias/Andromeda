using SFML.System;
using System;

namespace VorliasEngine2D.System.Utility
{

    internal class VectorUtil
    {

        
        /// <summary>
        /// Returns a Vector2f rotated by the specified angle
        /// </summary>
        /// <param name="vector">The vector to rotate</param>
        /// <param name="angle">The angle</param>
        /// <returns>The vector rotated by the angle</returns>
        public static Vector2f GetRotatedVector(Vector2f vector, float angle)
        {
            float targetCos = (float)Math.Cos(angle * MathUtil.DEGREES_TO_RADIANS);
            float targetSin = (float)Math.Sin(MathUtil.DEGREES_TO_RADIANS * angle);

            return new Vector2f(
                targetCos * vector.X - targetSin * vector.Y,
                targetSin * vector.X + targetCos * vector.Y
            );
        }

        public static double GetAngle(Vector2f end, Vector2f start)
        {
            Vector2f dir = (end - start);
            return MathUtil.RADIANS_TO_DEGREES * Math.Atan2(-dir.X, dir.Y);
        }
    }
}

using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VorliasEngine2D.System.Utility
{
    public static class VectorExtension
    {
        /// <summary>
        /// Gets the angle between this vector and another vector
        /// </summary>
        /// <param name="left">The source vector</param>
        /// <param name="right">The target vector</param>
        /// <returns>The angle between the two vectors</returns>
        public static float GetAngle(this Vector2f left, Vector2f right)
        {
            return (float) VectorUtil.GetAngle(left, right);
        }

        /// <summary>
        /// Returns the normal of the vector
        /// </summary>
        /// <param name="self"></param>
        /// <returns>The normal of this vector</returns>
        public static Vector2f Normalize(this Vector2f self)
        {
            float length = (float) Math.Sqrt(self.X * self.X + self.Y * self.Y);
            return new Vector2f(self.X / length, self.Y / length);
        }

        public static Vector2f Rotate(this Vector2f self, float angle)
        {
            return VectorUtil.GetRotatedVector(self, angle);
        }
    }

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

        public static double GetAngle(Vector2f end, Vector2f start)
        {
            Vector2f dir = (end - start);
            return RADIANS_TO_DEGREES * Math.Atan2(-dir.X, dir.Y);
        }
    }
}

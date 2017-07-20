using SFML.Graphics;
using SFML.System;
using System;
using VorliasEngine2D.Entities.Components.Internal;

namespace VorliasEngine2D.System.Utility
{
    public static class Vectors
    {
        public static FloatRect Combine(this FloatRect lhs, Vector2f target)
        {
            return new FloatRect(new Vector2f(lhs.Left, lhs.Top) + target, new Vector2f(lhs.Width, lhs.Height));
        }

        public static Vector2i ToInt(this Vector2f old)
        {
            return new Vector2i((int) old.X, (int) old.Y);
        }

        public static Vector2u ToUInt(this Vector2i old)
        {
            return new Vector2u((uint) old.X, (uint) old.Y);
        }

        public static Vector2f ToFloat(this Vector2u old)
        {
            return new Vector2f(old.X, old.Y);
        }

        public static Vector2f ToFloat(this Vector2i old)
        {
            return new Vector2f(old.X, old.Y);
        }

        /// <summary>
        /// Transforms the point to the object's space
        /// </summary>
        /// <param name="transform">The transform</param>
        /// <param name="other">The point</param>
        /// <returns></returns>
        public static Vector2f ToObjectSpace(this Entities.Components.Transform transform, Vector2f other)
        {

            return transform.Transform.TransformPoint(other);
        }

        public static UICoordinates ToUICoordinates(this Vector2f old)
        {
            return new UICoordinates(0, old.X, 0, old.Y);
        }

        /// <summary>
        /// Gets the angle between this vector and another vector
        /// </summary>
        /// <param name="left">The source vector</param>
        /// <param name="right">The target vector</param>
        /// <returns>The angle between the two vectors</returns>
        public static float GetAngle(this Vector2f left, Vector2f right)
        {
            return (float)VectorUtil.GetAngle(left, right);
        }

        /// <summary>
        /// The dot product of two vectors
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static float Dot(this Vector2f left, Vector2f right)
        {
            return (left.X * right.X) + (left.Y * right.Y);
        }

        /// <summary>
        /// The length of this vector
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static float GetLength(this Vector2f self)
        {
            float length = (float) Math.Sqrt(self.X * self.X + self.Y * self.Y);
            return length;
        }

        /// <summary>
        /// Gets the distance between two vectors, equivalent to (start - end).Length()
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static float GetDistance(this Vector2f start, Vector2f end)
        {
            return ( end - start ).GetLength();
        }

        /// <summary>
        /// Returns the normal of the vector
        /// </summary>
        /// <param name="self"></param>
        /// <returns>The normal of this vector</returns>
        public static Vector2f Normalize(this Vector2f self)
        {
            float length = self.GetLength();
            return new Vector2f(self.X / length, self.Y / length);
        }

        public static Vector2f Rotate(this Vector2f self, float angle)
        {
            return VectorUtil.GetRotatedVector(self, angle);
        }

        /// <summary>
        /// Clamps a vector between two vector values
        /// </summary>
        /// <param name="value">The vector value</param>
        /// <param name="min">The minimum vector value</param>
        /// <param name="max">The maximum vector value</param>
        /// <returns></returns>
        internal static Vector2f Clamp(this Vector2f value, Vector2f min, Vector2f max)
        {
            return new Vector2f(Math.Min(max.X, Math.Max(min.X, value.X)), Math.Min(max.Y, Math.Max(min.Y, value.Y)));
        }
    }
}

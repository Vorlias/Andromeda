using System;

namespace Andromeda.System.Utility
{
    public class MathUtil
    {
        public const double DEGREES_TO_RADIANS = Math.PI / 180;
        public const double RADIANS_TO_DEGREES = 180 / Math.PI;

        public static float Lerp(float firstFloat, float secondFloat, float by)
        {
            return firstFloat * by + secondFloat * (1 - by);
        }
    }
}

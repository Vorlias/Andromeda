using System;

namespace VorliasEngine2D.System.Utility
{
    /// <summary>
    /// A number range
    /// </summary>
    public struct NumberRange
    {
        static Random numberRangeRandom = new Random();

        /// <summary>
        /// The minimum value
        /// </summary>
        public float Min
        {
            get; private set;
        }

        /// <summary>
        /// The maximum value
        /// </summary>
        public float Max
        {
            get; private set;
        }

        /// <summary>
        /// A random value between the Min and Max value
        /// </summary>
        public float Random
        {
            get
            {
                return (float) numberRangeRandom.NextDouble() * (Max - Min) + Min;
            }
        }

        /// <summary>
        /// Whether or not the target NumberRange overlaps this number range
        /// </summary>
        /// <param name="other">The other number range</param>
        /// <returns>True if the NumberRanges overlap</returns>
        public bool Overlaps(NumberRange other)
        {
            return !(other.Max < Min || other.Min > Max);
        }

        /// <summary>
        /// Whether or not the specified number is in the range
        /// </summary>
        /// <param name="value">The number to check</param>
        /// <returns>True if the number is in range</returns>
        public bool InRange(float value)
        {
            return (value >= Min) && (value <= Max);
        }

        /// <summary>
        /// Gets the number clamped to this NumberRange
        /// </summary>
        /// <param name="value">The value to clamp</param>
        /// <returns>The number, clamped</returns>
        public float Clamped(float value)
        {
            if (value > Max)
                return Max;
            else if (value < Min)
                return Min;
            else
                return value;
        }

        /// <summary>
        /// Creates a NumberRange with a single value
        /// </summary>
        /// <param name="value">The value</param>
        public NumberRange(float value)
        {
            Min = value;
            Max = value;
        }

        /// <summary>
        /// Creates a number range with a minimum and maximum
        /// </summary>
        /// <param name="min">The minimum value</param>
        /// <param name="max">The maximum value</param>
        public NumberRange(float min, float max)
        {
            Min = min;
            Max = max;
        }
    }
}

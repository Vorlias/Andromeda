using System;

namespace Andromeda2D.System.Types
{
    public struct IntNumberRange : INumberRange<int>
    {
        static Random numberRangeRandom = new Random();

        /// <summary>
        /// Creates a NumberRange with a single value
        /// </summary>
        /// <param name="value">The value</param>
        public IntNumberRange(int value)
        {
            Min = value;
            Max = value;
        }

        /// <summary>
        /// Creates a number range with a minimum and maximum
        /// </summary>
        /// <param name="min">The minimum value</param>
        /// <param name="max">The maximum value</param>
        public IntNumberRange(int min, int max)
        {
            Min = min;
            Max = max;
        }

        /// <summary>
        /// The range this NumberRange covers 
        /// </summary>
        public int Range => Max - Min;

        /// <summary>
        /// Gets the percentage of this value
        /// </summary>
        /// <param name="value">The value</param>
        /// <returns></returns>
        public float GetPercentage(float value)
        {
            float min = Min;
            float max = Max;
            float intervalMinMax = max - min;
            float intervalValue = value - min;
            

            return intervalValue / intervalMinMax;
        }

        /// <summary>
        /// The minimum value
        /// </summary>
        public int Min { get; set; }

        /// <summary>
        /// The maximum value
        /// </summary>
        public int Max { get; set; }

        /// <summary>
        /// A random value between the Min and Max value
        /// </summary>
        public int Random => numberRangeRandom.Next(Min, Max);

        /// <summary>
        /// Gets the number clamped to this NumberRange
        /// </summary>
        /// <param name="value">The value to clamp</param>
        /// <returns>The number, clamped</returns>
        public int Clamped(int value)
        {
            if (value > Max)
                return Max;
            else if (value < Min)
                return Min;
            else
                return value;
        }

        /// <summary>
        /// Whether or not the specified number is in the range
        /// </summary>
        /// <param name="value">The number to check</param>
        /// <returns>True if the number is in range</returns>
        public bool InRange(int value)
        {
            return (value >= Min) && (value <= Max);
        }

        /// <summary>
        /// Allows assigning of float values implicitly to a number range
        /// </summary>
        /// <param name="value">The float value</param>
        public static implicit operator IntNumberRange(int value)
        {
            return new IntNumberRange(value);
        }

        /// <summary>
        /// Whether or not the target NumberRange overlaps this number range
        /// </summary>
        /// <param name="other">The other number range</param>
        /// <returns>True if the NumberRanges overlap</returns>
        public bool Overlaps(INumberRange<int> other)
        {
            return !(other.Max < Min || other.Min > Max);
        }
    }
}

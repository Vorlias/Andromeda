using System;

namespace Andromeda2D.System.Types
{
    public struct IntNumberRange : INumberRange<int>
    {
        static Random numberRangeRandom = new Random();

        public IntNumberRange(int value)
        {
            Min = value;
            Max = value;
        }

        public IntNumberRange(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public int Min { get; set; }
        public int Max { get; set; }

        public int Random => numberRangeRandom.Next(Min, Max);

        public int Clamped(int value)
        {
            if (value > Max)
                return Max;
            else if (value < Min)
                return Min;
            else
                return value;
        }

        public bool InRange(int value)
        {
            return (value >= Min) && (value <= Max);
        }

        public bool Overlaps(INumberRange<int> other)
        {
            return !(other.Max < Min || other.Min > Max);
        }
    }
}

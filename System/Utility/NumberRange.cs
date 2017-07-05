namespace VorliasEngine2D.System.Utility
{
    public class NumberRange
    {
        public float Min
        {
            get; private set;
        }
        public float Max
        {
            get; private set;
        }

        public bool Overlaps(NumberRange other)
        {
            return !(other.Max < Min || other.Min > Max);
        }

        public NumberRange(float min, float max)
        {
            Min = min;
            Max = max;
        }
    }
}

namespace Andromeda2D.System.Types
{
    public interface INumberRange<NumberType> where NumberType : struct
    {
        NumberType Min { get; set; }
        NumberType Max { get; set; }
        NumberType Random { get; }
        bool Overlaps(INumberRange<NumberType> other);
        bool InRange(NumberType value);
        NumberType Clamped(NumberType value);
    }
}

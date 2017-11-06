using System;

namespace Andromeda2D.Events
{
    public struct AndromedaEvent
    {
        public event Action Event;

        public void Invoke() => Event?.Invoke();

        public static AndromedaEvent operator+(AndromedaEvent lhs, Action rhs)
        {
            lhs.Event += rhs;
            return lhs;
        }
    }

    public struct AndromedaEvent<T>
    {
        public event Action<T> Event;

        public void Invoke(T value) => Event?.Invoke(value);

        public static AndromedaEvent<T> operator +(AndromedaEvent<T> lhs, Action<T> rhs)
        {
            lhs.Event += rhs;
            return lhs;
        }
    }
}

namespace Andromeda.System
{
    /// <summary>
    /// Helper interface for casting a value to another derived value
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICastable<T>
    {
        bool TryCast<U>(out U var) where U : T;
    }
}

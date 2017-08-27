using Vorlias2D.Entities.Components;

namespace Vorlias2D.Entities
{
    /// <summary>
    /// The result of FindComponent
    /// </summary>
    /// <typeparam name="T">The component type</typeparam>
    public struct FindComponentResult<T> where T : IComponent
    {
        /// <summary>
        /// Whether or not the component exists
        /// </summary>
        public bool IsExisting
        {
            get;
        }

        /// <summary>
        /// The component instance
        /// </summary>
        public T Instance
        {
            get;
        }

        /// <summary>
        /// Whether or not this component was created with FindComponent
        /// </summary>
        public bool IsNew
        {
            get;
        }

        public static implicit operator T(FindComponentResult<T> result)
        {
            return result.Instance;
        }

        public static implicit operator bool(FindComponentResult<T> result)
        {
            return result.IsExisting;
        }

        internal FindComponentResult(T component, bool exists, bool created = false)
        {
            Instance = component;
            IsExisting = exists;
            IsNew = created;
        }
    }
}

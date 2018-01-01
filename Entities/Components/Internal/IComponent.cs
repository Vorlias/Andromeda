using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda2D.Entities.Components
{
    public static class ComponentExtension
    {
        /// <summary>
        /// Used to turn abstract components into derived types
        /// </summary>
        /// <typeparam name="T">The type of the component</typeparam>
        /// <param name="self"></param>
        /// <param name="component">The destination component object</param>
        /// <returns>True if derived type is valid for the component</returns>
        public static bool TryCast<T>(this IComponent self, out T component) where T : IComponent
        {
            // The purpose of this function is to save me doing
            // if (component is DerivedComponent) 
            //      (component as DerivedComponent).DoAction();
            //
            // Instead, can do this - which is more readable
            // if (component.TryCast(out DerivedComponent derived))
            //      derived.DoAction();

            if (self is T)
            {
                component = (T)self;
                return true;
            }
            else
            {
                component = default(T);
                return false;
            }
        }
    }

    public interface IComponent
    {
        Entity Entity
        {
            get;
        }

        /// <summary>
        /// Called when the component is added to an entity
        /// </summary>
        /// <param name="entity">The entity it is added to</param>
        /// <exception cref="SetEntityInvalidException">Called if the user tries to set it</exception>
        void ComponentInit(Entity entity);
        void OnComponentCopy(Entity source, Entity copy);
    }
}

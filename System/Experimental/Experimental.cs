using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.Entities;
using VorliasEngine2D.Entities.Components;

namespace VorliasEngine2D.System.Experimental
{
    /// <summary>
    /// Class with experimental extension methods - use with caution!
    /// </summary>
    public static class Experimental
    {
        /// <summary>
        /// Finds all the components of the specified type in the children of this entity
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <param name="entity">The entity</param>
        public static T[] GetComponentsInChildren<T>(this Internal.IInstanceTree entity) where T : IComponent
        {
            var result = entity.Children.Where(child => child.HasComponent<T>()).Select(child => child.GetComponent<T>()).ToArray();
            return result;
        }
    }
}

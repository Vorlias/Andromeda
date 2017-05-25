using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VorliasEngine2D.System.Utility
{
    public static class Extensions
    {
        /// <summary>
        /// Performs an action for each value in the Enumerable object
        /// </summary>
        /// <typeparam name="T">The enumerable value type</typeparam>
        /// <param name="self">The IEnumerable object</param>
        /// <param name="action">The action that gets performed</param>
        public static void ForEach<T>(this IEnumerable<T> self, Action<T> action)
        {
            foreach (var value in self)
            {
                action.Invoke(value);
            }
        }
    }
}

using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.Entities;
using VorliasEngine2D.Entities.Components;

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
            foreach (var value in self.ToArray())
            {
                action.Invoke(value);
            }
        }

        public static bool InArray(this string item, IEnumerable<string> array)
        {
            return array.Contains(item);
        }

        internal static void For<T>(this List<T> self, int count, Func<int, T> predicate)
        {
            for (int i = 0; i < count; i++)
                self.Add(predicate(i));
        }
    }
}

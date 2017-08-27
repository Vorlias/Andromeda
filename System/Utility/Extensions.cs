using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vorlias2D.Entities;
using Vorlias2D.Entities.Components;

namespace Vorlias2D.System.Utility
{
    public static class Extensions
    {
        /// <summary>
        /// Lerps between this colour and the end colour based on the alpha
        /// </summary>
        /// <param name="start">The start color</param>
        /// <param name="end">The target color</param>
        /// <param name="alpha">The alpha between this color and the end color</param>
        /// <returns></returns>
        public static Color Lerp(this Color start, Color end, float alpha)
        {
            return new Color(
                (byte)(end.R + (end.R - start.R) * (alpha - 1)),
                (byte)(end.G + (end.G - start.G) * (alpha - 1)),
                (byte)(end.B + (end.B - start.B) * (alpha - 1)),
                (byte)(end.A + (end.A - start.A) * (alpha - 1))
            );
        }

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

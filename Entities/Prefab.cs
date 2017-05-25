using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VorliasEngine2D.Entities
{
    /// <summary>
    /// A class that handles preset entities
    /// </summary>
    public sealed class Prefab
    {
        Entity original;
        Entity[] entities;

        private Prefab()
        {

        }

        /// <summary>
        /// Creates a copy of the prefab entity
        /// </summary>
        /// <returns>A copy of the prefab entity</returns>
        public Entity Clone()
        {
            return original.Clone();
        }

        public static Prefab Create(Entity original)
        {
            Prefab prefab = new Prefab();
            prefab.original = original;

            return prefab;
        }
    }
}

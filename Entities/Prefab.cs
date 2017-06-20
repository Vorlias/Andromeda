using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.System;

namespace VorliasEngine2D.Entities
{
    /// <summary>
    /// A class that handles preset entities
    /// </summary>
    public sealed class Prefab
    {
        Entity original;

        private Prefab()
        {

        }

        /// <summary>
        /// Creates a copy of the prefab entity
        /// </summary>
        /// <returns>A copy of the prefab entity</returns>
        public Entity Clone(GameView parent)
        {
            Entity child = original.Clone();
            child.SetParentState(parent);
            child.Name = original.Name + " (Instance)";
            parent.AddEntity(child);

            return child;
        }

        /// <summary>
        /// Creates a copy of the prefab entity
        /// </summary>
        /// <returns>A copy of the prefab entity</returns>
        public Entity Clone(Entity parent)
        {
            Entity child = original.Clone(parent);
            child.SetParentState(parent.ParentState);
            child.Name = original.Name + " (Instance)";
            parent.AddEntity(child);

            return child;
        }

        public static Prefab Create()
        {
            Entity original = Entity.Spawn();
            original.SetIsPrefab(true);

            Prefab prefab = new Prefab();
            prefab.original = original;

            return prefab;
        }

        public static Prefab Create(Entity original)
        {
            original.SetIsPrefab(true);

            Prefab prefab = new Prefab();
            prefab.original = original;

            return prefab;
        }
    }
}

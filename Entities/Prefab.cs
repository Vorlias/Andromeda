﻿using System;
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
        Entity[] entities;

        private Prefab()
        {

        }

        /// <summary>
        /// Creates a copy of the prefab entity
        /// </summary>
        /// <returns>A copy of the prefab entity</returns>
        public Entity Clone(GameState parent)
        {
            Entity child = original.Clone();
            child.SetInputManager(parent.Input);
            parent.AddEntity(child);

            return child;
        }

        /// <summary>
        /// Creates a copy of the prefab entity
        /// </summary>
        /// <returns>A copy of the prefab entity</returns>
        public Entity Clone(Entity parent)
        {
            Entity child = original.Clone();
            parent.AddEntity(child);
            child.SetInputManager(parent.Input);

            return child;
        }

        public static Prefab Create(Entity original)
        {
            Prefab prefab = new Prefab();
            prefab.original = original;

            return prefab;
        }
    }
}
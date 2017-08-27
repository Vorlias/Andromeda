using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Andromeda2D.Entities.Components;
using Andromeda2D.System;

namespace Andromeda2D.Entities
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
        public Entity Instantiate(GameView parent)
        {
            Entity child = original.Clone();
            child.SetParentView(parent);
            child.Name = original.Name;
            parent.AddEntity(child);

            return child;
        }

        /// <summary>
        /// Creates a copy of the prefab entity
        /// </summary>
        /// <returns>A copy of the prefab entity</returns>
        public Entity Instantiate(Entity parent)
        {
            Entity child = original.Clone(parent);
            child.SetParentView(parent.GameView);
            child.Name = original.Name;
            parent.AddEntity(child);

            return child;
        }

        public static Prefab Create()
        {
            Entity original = new Entity();
            original.SetIsPrefab(true);

            Prefab prefab = new Prefab();
            prefab.original = original;

            return prefab;
        }

        /// <summary>
        /// Creates a prefab with the specified component
        /// </summary>
        /// <typeparam name="TComponent">The component</typeparam>
        /// <returns>A prefab with the specified component</returns>
        public static Prefab Create<TComponent>() where TComponent : IComponent, new()
        {
            Prefab pfb = Create();
            TComponent tc = pfb.original.AddComponent<TComponent>();
            return pfb;
        }

        public TComponent AddComponent<TComponent>() where TComponent : IComponent, new()
        {
            return original.AddComponent<TComponent>();
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

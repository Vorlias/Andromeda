using Andromeda.Entities;
using Andromeda.Entities.Components;
using Andromeda.System.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda.Linq
{
    public static class EntityCollectionLinq
    {
        public static bool FirstComponent<C>(this IEnumerable<Entity> entities, out C component)
    where C : IComponent
        {
            var coms = entities.ComponentsOfType<C>();
            if (coms.Count() > 0)
            {
                component = coms.First();
                return true;
            }
            else
            {
                component = default(C);
                return false;
            }
        }

        public static IEnumerable<C> ComponentsOfType<C>(this IEnumerable<Entity> entities)
            where C : IComponent
        {
            List<C> coms = new List<C>();
            foreach (var entity in entities)
            {
                if (entity.HasComponent<C>())
                    coms.Add(entity.GetComponent<C>());
            }

            return coms;
        }

        public static void AddComponents<C>(this EntityContainer ec)
            where C : IComponent, new()
        {
            ec.Descendants.ForEach(descendant => descendant.AddComponent<C>());
        }

        public static IEnumerable<Entity> ChildrenWhere(this EntityContainer ec, Func<Entity, bool> entity)
        {
            List<Entity> entityCollection = new List<Entity>();
            foreach (var descendant in ec.Children)
            {
                if (entity(descendant))
                    entityCollection.Add(descendant);
            }

            return entityCollection;
        }

        public static void WithComponents<TComponent>(this Entity entity, Action<TComponent> action)
            where TComponent : IComponent
        {
            var matching = entity.GetComponents<TComponent>();
            foreach (var match in matching)
            {
                action(match);
            }
        }

        public static IEnumerable<Entity> DescendantsWhere(this EntityContainer ec, Func<Entity, bool> entity)
        {
            List<Entity> entityCollection = new List<Entity>();
            foreach (var descendant in ec.Descendants)
            {
                if (entity(descendant))
                    entityCollection.Add(descendant);
            }

            return entityCollection;
        }

        public static IEnumerable<C> FindAllComponentsOfType<C>(this EntityContainer ec) where C : IComponent
        {
            return ec.Descendants.Where(e => e.HasComponent<C>()).Select(e => e.GetComponent<C>());
        }
    }
}

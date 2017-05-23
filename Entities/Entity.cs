using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.Entities.Components;

namespace VorliasEngine2D.Entities
{
    public class Entity
    {
        HashSet<IComponent> components;
        Transform transform;

        /// <summary>
        /// The Transform of this entity
        /// </summary>
        public Transform Transform
        {
            get
            {
                if (transform == null)
                    transform = AddComponent<Transform>();

                return transform;
            }
        }

        public T GetComponent<T>() where T : IComponent
        {
            return components.OfType<T>().First();
        }

        public T AddComponent<T>() where T : IComponent, new()
        {
            T component = new T();
            T existing = (T) components.First(c => component.Name == c.Name);

            if (existing != null)
            {
                return existing;
            }
            else
            {
                components.Add(component);
                return component;
            }
        }

        public Entity()
        {
            
        }
    }
}

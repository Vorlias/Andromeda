using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.Entities.Components;

namespace VorliasEngine2D.Entities
{
    /// <summary>
    /// An entity
    /// </summary>
    public class Entity
    {
        HashSet<IComponent> components = new HashSet<IComponent>();
        Transform transform;

        /// <summary>
        /// The Transform of this entity
        /// </summary>
        public Transform Transform
        {
            get
            {
                if (transform == null)
                {
                    transform = AddComponent<Transform>();
                    transform.Origin = new Vector2f(50, 50);
                }
                   

                return transform;
            }
        }

        /// <summary>
        /// The sprite renderer (if there's one attached to this entity)
        /// </summary>
        public SpriteRenderer SpriteRenderer
        {
            get
            {
                return GetComponent<SpriteRenderer>();
            }
        }

        public T GetComponent<T>() where T : IComponent
        {
            return components.OfType<T>().First();
        }

        public T AddComponent<T>() where T : IComponent, new()
        {
            T component = new T();

            var elements = components.OfType<T>();
            var arr = elements.ToArray();
            T existing = component;

            if (arr.Length > 0)
            {
                existing = elements.First();
                return existing;
            }
            else
            {
                component.Entity = this;
                components.Add(component);
                return component;
            }
        }

        public void Update()
        {
            var scriptedComponents = components.OfType<EntityBehaviour>();
            foreach (EntityBehaviour behaviour in scriptedComponents)
            {
                behaviour.BeforeUpdate();

                behaviour.Update();

                foreach (Entity child in children)
                {
                    child.Update();
                }

                behaviour.AfterUpdate();
            }
        }

        public void Init()
        {
            var scriptedComponents = components.OfType<EntityBehaviour>();
            foreach (EntityBehaviour behaviour in scriptedComponents)
            {
                behaviour.Init();
            }
        }

        public Entity()
        {
            
        }
    }
}

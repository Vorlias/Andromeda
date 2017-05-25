using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.Entities.Components;
using VorliasEngine2D.System;

namespace VorliasEngine2D.Entities
{
    /// <summary>
    /// An entity
    /// </summary>
    public class Entity : IInstanceTree
    {
        HashSet<IComponent> components = new HashSet<IComponent>();
        Transform transform;
        HashSet<Entity> children;
        UserInputManager input;
        string name = "Entity";

        /// <summary>
        /// Gets the InputManager for this entity
        /// </summary>
        public UserInputManager Input
        {
            get
            {
                return input;
            }
        }

        /// <summary>
        /// The name of this entity
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

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

        /// <summary>
        /// Gets the component of the specified type
        /// </summary>
        /// <typeparam name="T">The component type</typeparam>
        /// <returns>The component if it exists, otherwise null</returns>
        public T GetComponent<T>() where T : IComponent
        {
            return components.OfType<T>().First();
        }

        /// <summary>
        /// Adds the component of the specified type if it does not exist and returns the component
        /// </summary>
        /// <typeparam name="T">The component type</typeparam>
        /// <returns>The component</returns>
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
                component.OnComponentInit(this);
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

        /// <summary>
        /// Method called when the entity is initialized
        /// </summary>
        public void Init()
        {
            var scriptedComponents = components.OfType<EntityBehaviour>();
            foreach (EntityBehaviour behaviour in scriptedComponents)
            {
                behaviour.Init();
            }
        }

        /// <summary>
        /// Gets the child entities of this entity
        /// </summary>
        /// <returns></returns>
        public Entity[] GetChildren()
        {
            return children.ToArray();
        }

        /// <summary>
        /// Gets the first child entity with the specified name
        /// </summary>
        /// <param name="name">The name of the entity to find</param>
        /// <returns>An entity if it's found, otherwise null</returns>
        public Entity FindFirstChild(string name)
        {
            return children.First(entity => entity.Name == name);
        }

        public void SetInputManager(UserInputManager inputManager)
        {
            input = inputManager;
        }

        /// <summary>
        /// Add a child entity to this entity
        /// </summary>
        /// <param name="child">The entity</param>
        public void AddEntity(Entity child)
        {
            child.input = this.input;
            children.Add(child);
        }

        /// <summary>
        /// Spawn an entity as a child of this entity
        /// </summary>
        /// <returns>The spawned entity</returns>
        public Entity SpawnEntity()
        {
            Entity entity = new Entity();
            entity.input = this.input;
            children.Add(entity);
            return entity;
        }

        /// <summary>
        /// Spawn an entity under another entity
        /// </summary>
        /// <param name="parent">The parent entity</param>
        /// <returns>The spawned entity</returns>
        public static Entity Spawn(Entity parent)
        {
            Entity entity = new Entity();
            entity.input = parent.input;
            return entity;
        }

        /// <summary>
        /// Spawns the entity under the specified GameState
        /// </summary>
        /// <param name="state">The state to spawn the entity under</param>
        /// <returns></returns>
        public static Entity Spawn(GameState state)
        {
            Entity entity = new Entity();
            entity.input = state.Input;
            state.AddEntity(entity);
            return entity;
        }

        private Entity()
        {
            children = new HashSet<Entity>();
        }
    }
}

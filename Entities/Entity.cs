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
    public sealed class Entity : IInstanceTree
    {
        HashSet<IComponent> components = new HashSet<IComponent>();
        Transform transform;
        HashSet<Entity> children;
        Entity parentEntity;
        GameState parentState;
        bool prefab = false;
        bool useLocalSpace = true;
        UserInputManager input;
        string name = "Entity";

        /// <summary>
        /// The parent state of this entity (If applicable)
        /// </summary>
        public GameState ParentState
        {
            get
            {
                return parentState;
            }
        }

        /// <summary>
        /// The parent entity of this entity (If applicable)
        /// </summary>
        public Entity Parent
        {
            get
            {
                return parentEntity;
            }
        }

        internal void SetParentState(GameState state)
        {
            parentState = state;
        }

        internal void SetParent(Entity parent)
        {
            parentEntity = parent;
        }

        /// <summary>
        /// Whether or not this entity is positioned in local space if parented to another entity
        /// </summary>
        public bool UseLocalSpace
        {
            get
            {
                return useLocalSpace;
            }
            set
            {
                useLocalSpace = value;
            }
        }

        /// <summary>
        /// Whether or not this entity has been set as a prefab entity
        /// </summary>
        public bool IsPrefab
        {
            get
            {
                return prefab;
            }
        }

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

        internal void SetIsPrefab(bool value)
        {
            prefab = value;
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

        internal void Update()
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
        /// Awaken the script components
        /// </summary>
        internal void StartBehaviours()
        {
            var scriptedComponents = components.OfType<EntityBehaviour>();
            foreach (EntityBehaviour behaviour in scriptedComponents)
            {
                behaviour.Start();
            }
        }

        /// <summary>
        /// Method called when the entity is initialized
        /// </summary>
        internal void Init()
        {
            var scriptedComponents = components.OfType<EntityBehaviour>();
            foreach (EntityBehaviour behaviour in scriptedComponents)
            {
                if (!behaviour.Initialized)
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

        internal void SetInputManager(UserInputManager inputManager)
        {
            input = inputManager;
        }

        /// <summary>
        /// Add a child entity to this entity
        /// </summary>
        /// <param name="child">The entity</param>
        public void AddEntity(Entity child)
        {
            children.Add(child);
        }

        /// <summary>
        /// Spawn an entity as a child of this entity
        /// </summary>
        /// <returns>The spawned entity</returns>
        public Entity SpawnEntity()
        {
            Entity entity = new Entity();
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
            entity.SetParent(parent);
            entity.SetParentState(parent.ParentState);
            return entity;
        }

        public static Entity Spawn()
        {
            Entity entity = new Entity();
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
            entity.SetParentState(state);
            state.AddEntity(entity);
            return entity;
        }

        /// <summary>
        /// Creates a component with the specified type
        /// </summary>
        /// <param name="type">The type of the component</param>
        /// <param name="created">The component returned, null if it could not be created</param>
        /// <returns>If the component was successfully created</returns>
        internal bool AddComponent(Type type, out IComponent created)
        {
            // This is a bit of a messy function, mainly for the cloning... :3

            IComponent component = (IComponent)Activator.CreateInstance(type);

            if (component.MultipleAllowed)
            {
                created = component;
                components.Add(component);

                return true;
            }
            else
            {
                var existing = components.Where(c => c.GetType() == type).First();
                if (existing == null)
                {
                    components.Add(component);
                    created = component;
                    return true;
                }
                else
                {
                    created = null;
                    return false;
                }
            }
           
        }

        /// <summary>
        /// Create a copy entity of this entity
        /// </summary>
        /// <returns>A copy of this entity</returns>
        internal Entity Clone()
        {
            Entity copy = new Entity();
            foreach (IComponent component in components)
            {
                if (component is EntityBehaviour)
                {
                   
                    IComponent componentCopy;
                    if (copy.AddComponent(component.GetType(), out componentCopy))
                    {
                        componentCopy.OnComponentInit(copy);
                    }
                }
                else
                    component.OnComponentCopy(this, copy);
            }

            return copy;
        }

        private Entity()
        {
            children = new HashSet<Entity>();
            input = new UserInputManager();
        }
    }
}

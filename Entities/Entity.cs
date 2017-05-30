using SFML.Graphics;
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
    public sealed class ComponentNotFoundException<T> : Exception
    {
        public ComponentNotFoundException() : base("Could not find component: " + typeof(T).Name)
        {

        }
    }

    /// <summary>
    /// An entity
    /// </summary>
    public sealed class Entity : IInstanceTree
    {
        HashSet<IComponent> components = new HashSet<IComponent>();
        Components.Transform transform;
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

        internal IEnumerable<IRenderableComponent> DrawableComponents
        {
            get
            {
                return components.Where(component => component is IRenderableComponent).Select(component => component as IRenderableComponent).OrderBy(component => component.RenderOrder);
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
        /// The position of this entity (alias of Transform.Position)
        /// </summary>
        public Vector2f Position
        {
            get
            {
                return Transform.Position;
            }
            set
            {
                Transform.Position = value;
            }
        }

        /// <summary>
        /// The Transform of this entity
        /// </summary>
        public Components.Transform Transform
        {
            get
            {
                if (transform == null)
                {
                    transform = AddComponent<Components.Transform>();
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

        internal IEnumerable<IComponent> Components
        {
            get
            {
                return components;
            }
        }

        /// <summary>
        /// The behaviours attached to this entity
        /// </summary>
        internal IEnumerable<EntityBehaviour> Behaviours
        {
            get
            {
                return components.OfType<EntityBehaviour>();
            }
        }

        public bool HasComponent<T>() where T : IComponent
        {
            return components.OfType<T>().Count() > 0;
        }

        /// <summary>
        /// Gets the component of the specified type
        /// </summary>
        /// <typeparam name="T">The component type</typeparam>
        /// <returns>The component if it exists, otherwise null</returns>
        public T GetComponent<T>() where T : IComponent
        {
            return components.OfType<T>().FirstOrDefault();
        }

        /// <summary>
        /// Finds the component  of the specified type, and returns whether or not it was found and assigns the component parameter
        /// to be used if it's found.
        /// </summary>
        /// <typeparam name="T">The type of the component</typeparam>
        /// <param name="component">The component variable to set</param>
        /// <param name="create">Whether or not to create it if it doesn't exist</param>
        /// <returns>True if the component is found</returns>
        public bool FindComponent<T>(out T component, bool create = false) where T : IComponent, new()
        {
            if (HasComponent<T>())
            {
                component = GetComponent<T>();
                return true;
            }
            else
            {
                if (create)
                {
                    component = AddComponent<T>();
                    return true;
                }
                else
                {
                    component = default(T); // unfortunately. :c
                    return false;
                }
            }
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

            if (arr.Length > 0 && !component.AllowsMultipleInstances)
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

        internal void Render()
        {
            var scriptedComponents = components.OfType<EntityBehaviour>();
            foreach (EntityBehaviour behaviour in scriptedComponents)
            {
                //behaviour.BeforeUpdate();

                behaviour.Render();

                foreach (Entity child in children)
                {
                    child.Render();
                }

               // behaviour.AfterUpdate();
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
        /// Gets the child entities of this entity via a predicate
        /// </summary>
        /// <param name="condition">The predicate</param>
        /// <returns>The children, filtered by the predicate</returns>
        public Entity[] GetChildren(Func<Entity, bool> predicate)
        {
            return children.Where(predicate).ToArray();
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

            if (component.AllowsMultipleInstances)
            {
                created = component;
                components.Add(component);

                return true;
            }
            else
            {
                var existing = components.Where(c => c.GetType() == type).FirstOrDefault();
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

using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.Entities.Components;
using VorliasEngine2D.Entities.Components.Colliders;
using VorliasEngine2D.Entities.Components.Internal;
using VorliasEngine2D.Serialization;
using VorliasEngine2D.System;
using VorliasEngine2D.System.Internal;
using VorliasEngine2D.System.Utility;

namespace VorliasEngine2D.Entities
{

    /// <summary>
    /// An entity
    /// </summary>
    public sealed class Entity : EntityContainer
    {
        HashSet<IComponent> components = new HashSet<IComponent>();
        Components.Transform transform;
        Entity parentEntity;
        GameView parentState;
        bool prefab = false;
        bool useLocalSpace = true;
        UserInputManager input;
        string name = "Entity";
        List<string> tags = new List<string>();

        /// <summary>
        /// The tags this entity has
        /// </summary>
        public List<string> Tags
        {
            get
            {
                return tags;
            }
        }

        /// <summary>
        /// The collision component of this entity
        /// </summary>
        public CollisionComponent Collider
        {
            get => GetComponent<CollisionComponent>();
        }

        /// <summary>
        /// The parent view of this entity (If applicable)
        /// </summary>
        public GameView GameView
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
                return components.OfType<IRenderableComponent>().OrderByDescending(component => component.RenderOrder);
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

        internal void SetParentState(GameView state)
        {
            parentState = state;
        }

        internal void SetParent(Entity parent)
        {
            parentEntity = parent;
            parent.AddChild(this);
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

        [SerializableProperty("Name")]
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
                return components.OfType<EntityBehaviour>().Where(component => component.IsEnabled);
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

        public IEnumerable<T> GetComponents<T>() where T : IComponent
        {
            return components.OfType<T>();
        }

        /// <summary>
        /// Gets the components of the specified type in descendants
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetComponentsInDescendants<T>() where T: IComponent
        {
            List<T> components = new List<T>();
            Entity[] descendants = Children;

            components.AddRange(GetComponents<T>());

            foreach (var child in descendants)
            {
                components.AddRange(child.GetComponentsInDescendants<T>());
            }

            return components;
        }

        /// <summary>
        /// Renders the entity and children of the entity
        /// </summary>
        /// <param name="window"></param>
        internal void Render(RenderTarget target)
        {
            var scriptedComponents = components.OfType<EntityBehaviour>();
            foreach (EntityBehaviour behaviour in scriptedComponents)
            {
                behaviour.Render();
            }

            foreach (Entity child in Children)
            {
                child.Render(target);
            }

            DrawableComponents.ForEach(component => target.Draw(component));
        }

        /// <summary>
        /// Find the component of the specified type
        /// </summary>
        /// <typeparam name="T">The component type</typeparam>
        /// <param name="create">Whether or not the type should be created if not found</param>
        /// <returns></returns>
        public FindComponentResult<T> FindComponent<T>(bool create = false) where T: IComponent, new()
        {
            if (HasComponent<T>())
                return new FindComponentResult<T>(GetComponent<T>(), true);
            else
                return new FindComponentResult<T>(create ? AddComponent<T>() : default(T), create, create);
        }

        /// <summary>
        /// Finds the component  of the specified type, and returns whether or not it was found and assigns the component parameter
        /// to be used if it's found.
        /// </summary>
        /// <typeparam name="T">The type of the component</typeparam>
        /// <param name="component">The component variable to set</param>
        /// <param name="create">Whether or not to create it if it doesn't exist</param>
        /// <returns>True if the component is found</returns>
        [Obsolete("Use 'FindComponent<T>()' instead")]
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
                component.ComponentInit(this);
                components.Add(component);
                return component;
            }
        }

        internal void Update()
        {
            foreach (Entity child in Children)
            {
                child.Update();
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
        /// Returns the full path of this entity
        /// </summary>
        public string FullName
        {
            get
            {
                if (Parent != null)
                    return Parent.FullName + "[\"" + Name + "\"]";
                else
                    return Name;
            }
        }

        public override string ToString()
        {
            return FullName;
        }

        /// <summary>
        /// Spawn an entity as a child of this entity
        /// </summary>
        /// <returns>The spawned entity</returns>
        public override Entity CreateChild()
        {
            Entity entity = new Entity();
            entity.SetParent(this);
            //children.Add(entity);
            AddChild(entity);
            

            components.Where(component => component is IContainerComponent).Select(component => component as IContainerComponent).ForEach(component => component.ChildAdded(entity));

            return entity;
        }

        /// <summary>
        /// Spawn an entity as a child of this entity with the specified component
        /// </summary>
        /// <returns>The spawned entity</returns>
        public ComponentType CreateChild<ComponentType>() where ComponentType : IComponent, new()
        {
            return CreateChild().AddComponent<ComponentType>();
        }

        /// <summary>
        /// Spawn an entity under another entity
        /// </summary>
        /// <param name="parent">The parent entity</param>
        /// <returns>The spawned entity</returns>
        public static Entity Create(Entity parent)
        {
            Entity entity = Create();
            entity.SetParent(parent);
            entity.SetParentState(parent.GameView);

            parent.components.Where(component => component is IContainerComponent).Select(component => component as IContainerComponent).ForEach(component => component.ChildAdded(entity));

            return entity;
        }

        public static Entity Create(Components.Transform transform = null)
        {
            Entity entity = new Entity();

            if (transform != null)
            {
                entity.Position = transform.Position;
                entity.Transform.Rotation = transform.Rotation;
                entity.Transform.Scale = transform.Scale;
            }

            return entity;
        }

        /// <summary>
        /// Spawns the entity under the specified GameState
        /// </summary>
        /// <param name="state">The state to spawn the entity under</param>
        /// <returns></returns>
        public static Entity Create(GameView state, Components.Transform transform = null)
        {
            Entity entity = Create(transform);
            entity.SetParentState(state);
            state.AddEntity(entity);
            return entity;
        }

        /// <summary>
        /// Tries to find the component of the specified type, otherwise creates it
        /// </summary>
        /// <param name="type">The type of the component</param>
        /// <param name="created">The component returned, null if it could not be created</param>
        /// <returns>If the component was successfully created</returns>
        internal bool FindOrCreateComponent(Type type, out IComponent created, bool returnExisting = true)
        {
            // This is a bit of a messy function, mainly for the cloning... :3

            IComponent component = (IComponent)Activator.CreateInstance(type);

            if (component.AllowsMultipleInstances)
            {
                created = component;
                component.ComponentInit(this);
                components.Add(component);

                return true;
            }
            else
            {
                var existing = components.Where(c => c.GetType() == type).FirstOrDefault();
                if (existing == null)
                {
                    components.Add(component);
                    component.ComponentInit(this);
                    created = component;
                    return true;
                }
                else
                {
                    if (returnExisting)
                    {
                        created = existing;
                        return true;
                    }
                    else
                    {
                        created = null;
                        return false;
                    }
                }
            }
           
        }

        /// <summary>
        /// Create a copy entity of this entity
        /// </summary>
        /// <returns>A copy of this entity</returns>
        internal Entity Clone(Entity parent = null)
        {
            Entity copy = new Entity();
            copy.SetParentState(GameView);
            if (parent != null)
                copy.SetParent(parent);
            

            foreach (IComponent component in components)
            {
                if (component is EntityBehaviour)
                {
                   
                    IComponent componentCopy;
                    if (copy.FindOrCreateComponent(component.GetType(), out componentCopy))
                    {
                        //componentCopy.ComponentInit(copy);
                    }
                }
                else
                    component.OnComponentCopy(this, copy);
            }

            foreach (Entity child in Children)
            {
                Entity childCopy = child.Clone(copy);
                childCopy.Name = child.Name;
                //childCopy.SetParent(copy);
                childCopy.SetParentState(copy.parentState);
                copy.AddChild(childCopy);
            }

            return copy;
        }


        private bool isDestroying = false;
        private float lifeTime;

        /// <summary>
        /// Whether or not this object is being destroyed
        /// </summary>
        internal bool IsBeingDestroyed
        {
            get => isDestroying;
        }

        /// <summary>
        /// The lifetime timer of this object
        /// </summary>
        internal float DestroyTick
        {
            get;
            set;
        }

        /// <summary>
        /// The lifetime of this object
        /// </summary>
        internal float LifeTime
        {
            get => lifeTime;
        }

        /// <summary>
        /// Destroys the object after a set amount of seconds
        /// </summary>
        /// <param name="seconds">The seconds before the object is destroyed</param>
        public void Destroy(float seconds)
        {
            lifeTime = seconds;
            DestroyTick = 0;
            isDestroying = true;
        }

        /// <summary>
        /// Destroys the object
        /// </summary>
        public void Destroy()
        {
            Behaviours.ForEach(behaviour => behaviour.OnDestroy());

            if (Parent != null)
            {
                Parent.RemoveChild(this);
            }
            else if (GameView != null)
            {
                GameView.RemoveChild(this);
            }
        }

        private Entity()
        {
            input = new UserInputManager();
        }
    }
}

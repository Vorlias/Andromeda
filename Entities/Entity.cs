using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Andromeda.Entities.Components;
using Andromeda.Entities.Components.Colliders;
using Andromeda.Entities.Components.Internal;
using Andromeda.Serialization;
using Andromeda.System;
using Andromeda.System.Internal;
using Andromeda.System.Utility;
using Andromeda.System;

namespace Andromeda.Entities
{

    public delegate void EntityComponentAddedEvent(IComponent component);

    /// <summary>
    /// An entity
    /// </summary>
    public class Entity : EntityContainer
    {

        #region Internal
        HashSet<IComponent> components = new HashSet<IComponent>();

        Components.Transform transform;
        EntityContainer parentEntity;
        EntityGameView parentState;
        bool prefab = false;
        bool useLocalSpace = true;
        bool visible = true;
        UserInputManager input;
        string name = "Entity";
        List<object> tags = new List<object>();

        #endregion

        #region Properties
        /// <summary>
        /// The tags this entity has
        /// </summary>
        public List<object> Tags
        {
            get
            {
                return tags;
            }
        }

        /// <summary>
        /// Whether or not this entity is enabled
        /// </summary>
        public bool Enabled
        {
            get;
            set;
        }

        /// <summary>
        /// Whether or not this entity is visible
        /// </summary>
        public bool Visible
        {
            get => Parent != null ? Parent.Visible : visible;
            set => visible = value;
        }

        /// <summary>
        /// All the ancestors of this entity
        /// </summary>
        internal IEnumerable<Entity> Ancestors
        {
            get
            {
                List<Entity> ancestors = new List<Entity>();
                if (Parent != null)
                {
                    ancestors.Add(Parent);
                    ancestors.AddRange(Parent.Ancestors);
                }

                return ancestors.ToArray();
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
        /// The GameState that contains this Entity
        /// </summary>
        public IGameState GameState
        {
            get => GameView.ParentState;
        }

        /// <summary>
        /// The parent view of this entity (If applicable)
        /// </summary>
        public EntityGameView GameView
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
                return components.OfType<IRenderableComponent>().Where(com => com.Entity.Visible).OrderByDescending(component => component.RenderOrder);
            }
        }


        /// <summary>
        /// The parent entity
        /// </summary>
        public Entity Parent
        {
            get => ParentContainer as Entity;
        }

        /// <summary>
        /// The parent container of this entity (If applicable)
        /// </summary>
        public EntityContainer ParentContainer
        {
            get
            {
                return parentEntity;
            }
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
            internal set => prefab = value;
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
        #endregion

        public event EntityComponentAddedEvent ComponentAdded;

        /// <summary>
        /// Returns whether not the entity has the specified tag
        /// </summary>
        /// <param name="tag">The tag</param>
        /// <returns>True if the entity has the specified tag</returns>
        public bool HasTag(object tag)
        {
            return tags.Contains(tag);
        }

    
        internal void SetParentView(EntityGameView state)
        {
            parentState = state;
        }

        internal void SetParent<C>(C parent) where C : EntityContainer
        {
            parentEntity = parent;
            parent.AddChild(this);
            OnParentChanged(parent);
        }



        internal void SetIsPrefab(bool value)
        {
            prefab = value;
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
        /// Renders the entity and children of the entity
        /// </summary>
        /// <param name="window"></param>
        internal void Render(RenderTarget target)
        {
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
            bool isMultipleAllowed = true;

            var attrs = component.GetType().GetCustomAttributes(typeof(RequireComponentsAttribute), true);
            foreach (RequireComponentsAttribute attr in attrs)
            {
                attr.AddRequiredComponents(this);
            }

            var allowMultiple = component.GetType().GetCustomAttributes(typeof(DisallowMultipleAttribute), false);
            if (allowMultiple.Count() > 0)
                isMultipleAllowed = false;

            var elements = components.ToArray().OfType<T>();
            T existing = component;

            if (elements.Count() > 0 && !isMultipleAllowed)
            {
                existing = elements.First();
                return existing;
            }
            else
            {
                component.ComponentInit(this);
                components.Add(component);
                ComponentAdded?.Invoke(component);
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

        }

        /// <summary>
        /// Method called when the entity is spawned
        /// </summary>
        public virtual void OnCreate()
        {

        }

        /// <summary>
        /// Method called when entity parent is changed
        /// </summary>
        /// <param name="newParent"></param>
        public virtual void OnParentChanged(EntityContainer newParent)
        {

        }

        /// <summary>
        /// Method called when the entity is initialized
        /// </summary>
        internal void Init()
        {

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
            return new Entity(this);
        }

        /// <summary>
        /// Gets the components of the specified type in descendants
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetComponentsInDescendants<T>(bool includeParent = false) where T : IComponent
        {
            List<T> components = new List<T>();
            Entity[] descendants = Children;

            if (includeParent)
                components.AddRange(GetComponents<T>());

            foreach (var child in descendants)
            {
                components.AddRange(child.GetComponentsInDescendants<T>(true));
            }

            return components;
        }

        /// <summary>
        /// Creates a blank entity
        /// </summary>
        public Entity()
        {
            input = new UserInputManager();
            OnCreate();
            Enabled = true;
        }

        /// <summary>
        /// Creates a new Entity with the specified parent
        /// </summary>
        /// <param name="parent">The parent of this entity</param>
        public Entity(EntityContainer parent) : this()
        {
            if (parent is EntityGameView)
            {
                SetParentView(parent as EntityGameView);
                parent.AddEntity(this);
            }
            else if (parent is Entity)
            {
                var parentEntity = parent as Entity;
                SetParent(parent);
                SetParentView(parentEntity.GameView);
                parentEntity.components.Where(component => component is IContainerComponent).Select(component => component as IContainerComponent).ForEach(component => component.ChildAdded(this));
            }
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
        /// Tries to find the component of the specified type, otherwise creates it
        /// </summary>
        /// <param name="type">The type of the component</param>
        /// <param name="created">The component returned, null if it could not be created</param>
        /// <returns>If the component was successfully created</returns>
        internal bool FindOrCreateComponent(Type type, out IComponent created, bool returnExisting = true)
        {
            // This is a bit of a messy function, mainly for the cloning... :3

            IComponent component = (IComponent)Activator.CreateInstance(type);
            var disallowMultiple = type.GetCustomAttributes(typeof(DisallowMultipleAttribute), false).Count() > 0;

            if (!disallowMultiple)
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

                    if (!IsPrefab)
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
            copy.SetParentView(GameView);
            if (parent != null)
                copy.SetParent(parent);
            

            foreach (IComponent component in components)
            {
                component.OnComponentCopy(this, copy);
            }

            foreach (Entity child in Children)
            {
                Entity childCopy = child.Clone(copy);
                childCopy.Name = child.Name;
                //childCopy.SetParent(copy);
                childCopy.SetParentView(copy.parentState);
                copy.AddChild(childCopy);
            }

            return copy;
        }


        private bool isDestroying = false;
        private float lifeTime;



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
            Components.OfType<IDestroyedListener>().ForEach(listener => listener.OnDestroy());

            if (ParentContainer != null)
            {
                ParentContainer.RemoveChild(this);
            }
            else if (GameView != null)
            {
                GameView.RemoveChild(this);
            }

            //components.Clear();
        }



        bool initialized = false;

        /// <summary>
        /// Called when the entity is active
        /// </summary>
        internal void Activated()
        {
            if (!initialized)
            {
                initialized = true;
            }
        }

        /// <summary>
        /// Called when the entity is inactive
        /// </summary>
        internal void Deactivated()
        {

        }
    }
}

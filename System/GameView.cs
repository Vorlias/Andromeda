using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
using VorliasEngine2D.Entities;
using VorliasEngine2D.Entities.Components;
using SFML.System;
using VorliasEngine2D.System.Utility;
using VorliasEngine2D.System.Internal;
using VorliasEngine2D.Entities.Components.Internal;

namespace VorliasEngine2D.System
{
    /// <summary>
    /// The GameState's priority
    /// </summary>
    public enum GameViewPriority
    {
        First = 0,
        Background = 10,
        Normal = 100,
        Interface = 500,
        Last = 1000,
    }

    public class GameViewInitException : Exception
    {
        public GameViewInitException() : base("GameView already initialized!")
        {

        }
    }

    public abstract class GameView : IInstanceTree
    {
        bool active = true;
        string id;
        StateGameManager manager;
        GameViewPriority priority;
        HashSet<Entity> entities;

        Camera camera;
        public Camera Camera
        {
            get
            {
                if (camera == null)
                {
                    var existing = UpdatableComponents.OfType<Camera>();
                    if (existing.Count() > 0)
                    {
                        camera = existing.First();
                    }
                    else
                    {
                        camera = CreateChild().AddComponent<Camera>();
                    }
                }

                return camera;
            }
        }

        private UserInputManager inputService;
        protected UserInputManager Input
        {
            get
            {
                return inputService;
            }
        }

        /// <summary>
        /// The TextureManager instance
        /// </summary>
        protected TextureManager TextureManager => TextureManager.Instance;

        /// <summary>
        /// The FontManager instance
        /// </summary>
        protected FontManager FontManager => FontManager.Instance;

        /// <summary>
        /// The SoundManager instance
        /// </summary>
        protected SoundManager SoundManager => SoundManager.Instance;

        protected StateApplication Application
        {
            get
            {
                return manager.Application;
            }
        }

        /// <summary>
        /// The game's states
        /// </summary>
        protected StateManager States
        {
            get => manager.Application.States;
        }

        public Entity[] Descendants
        {
            get
            {
                List<Entity> entityList = new List<Entity>();
                entityList.AddRange(Children);
                foreach (var child in Children)
                {
                    entityList.AddRange(child.Descendants);
                }
                return entityList.ToArray();
            }
        }

        /// <summary>
        /// The collision components
        /// </summary>
        protected IEnumerable<ICollisionComponent> EntityColliders
        {
            get
            {
                return CollidableEntities.Select(e => e.GetComponent<ICollisionComponent>());
            }
        }

        /// <summary>
        /// Entities that have an event thing
        /// </summary>
        protected IEnumerable<IComponentEventListener> EventComponents
        {
            get
            {
                return CollidableEntities.Where(e => e.HasComponent<IComponentEventListener>()).Select(e => e.GetComponent<IComponentEventListener>());
            }
        }

        /// <summary>
        /// Entities that have a collision component
        /// </summary>
        internal Entity[] CollidableEntities
        {
            get
            {
                return Descendants.Where(entity => entity.HasComponent<ICollisionComponent>()).ToArray();
            }
        }


        /// <summary>
        /// Returns all the drawable entities
        /// </summary>
        internal IEnumerable<IRenderableComponent> Renderable
        {
            get
            {
                List<IRenderableComponent> renderable = new List<IRenderableComponent>();
                Descendants.ForEach(v => renderable.AddRange(v.GetComponents<IRenderableComponent>()));
                return renderable.OrderBy(e => e.RenderOrder);
            }
        }

        /// <summary>
        /// Returns all the drawable entities
        /// </summary>
        internal Entity[] DrawableEntities
        {
            get
            {
                return Descendants.Where(entity => entity.DrawableComponents.Count() > 0).ToArray();
            }
        }

        /// <summary>
        /// Returns all the entities with sprites, ordered by the render order
        /// </summary>
        [Obsolete("Use 'DrawableEntities' instead.")]
        internal Entity[] SpriteEntities
        {
            get
            {
                var sprites = entities.Where(entity => entity.HasComponent<SpriteRenderer>());
                return sprites.OrderByDescending(entity => entity.SpriteRenderer.RenderOrder).ToArray();
            }
        }

        /// <summary>
        /// Entities that have a UIComponent
        /// </summary>
        internal Entity[] UIEntities
        {
            get
            {
                var entities = this.entities.Where(entity => entity.HasComponent<UIComponent>());
                return entities.ToArray();
            }
        }

        /// <summary>
        /// The entities in this GameState
        /// </summary>
        public HashSet<Entity> Entities
        {
            get
            {
                return entities;
            }
        }

        /// <summary>
        /// The game state's priority
        /// </summary>
        public GameViewPriority Priority
        {
            get
            {
                return priority;
            }
            set
            {
                priority = value;
            }
        }

        /// <summary>
        /// The state's Id assigned by the StateManager
        /// </summary>
        public string Id
        {
            get
            {
                return id;
            }
        }

        /// <summary>
        /// The active state of the state
        /// </summary>
        public bool IsActive
        {
            get
            {
                return active;
            }
            set
            {
                if (value)
                    OnActivated();
                else
                    OnDeactivated();

                active = value;
            }
        }

        /// <summary>
        /// Method called when the state is added to the state manager
        /// </summary>
        public virtual void OnAdded()
        {

        }

        /// <summary>
        /// Method called when the application starts
        /// </summary>
        public virtual void OnStart()
        {

        }

        /// <summary>
        /// Method called when the state is activated
        /// </summary>
        public virtual void OnActivated()
        {

        }

        /// <summary>
        /// Method called when the state is deactivated
        /// </summary>
        public virtual void OnDeactivated()
        {

        }

        /// <summary>
        /// Method called when the state is updated
        /// </summary>
        /// <param name="application">The application</param>
        public virtual void OnUpdate(Application application)
        {

        }

        /// <summary>
        /// Method called when the state is rendered
        /// </summary>
        /// <param name="window"></param>
        public virtual void OnRender(RenderWindow window)
        {

        }

        private void UpdateCollisions()
        {
            var colldiers = EntityColliders;
            foreach (var collider in colldiers)
            {
                foreach (var collider2 in colldiers.Where(c => c != collider))
                {
                    if (collider.CollidesWith(collider2))
                    {
                        collider.Entity.Behaviours.ForEach(behaviour => behaviour.Collision(collider2.Entity));
                        collider2.Entity.Behaviours.ForEach(behaviour => behaviour.Collision(collider.Entity));
                    }
                }
            }
        }

        /// <summary>
        /// All the updatatable components in this view
        /// </summary>
        internal IEnumerable<IUpdatableComponent> UpdatableComponents
        {
            get
            {
                List<IUpdatableComponent> components = new List<IUpdatableComponent>();
                Entities.Select(entity => entity.GetComponentsInDescendants<IUpdatableComponent>()).ForEach(list => components.AddRange(list));
                return components.OrderByDescending(component => component.UpdatePriority);
            }
        }

        internal void UpdateEntities()
        {
            var updatableComponents = UpdatableComponents;

            updatableComponents.ForEach(com => com.BeforeUpdate());
            updatableComponents.ForEach(com => com.Update());
            updatableComponents.ForEach(com => com.AfterUpdate());

            UpdateCollisions();
        }

        internal void RenderEntities(RenderWindow window)
        {
            //foreach (Entity entity in DrawableEntities)
            //{
            //    entity.Render(window);
            //}

            if (camera != null)
            {
                if (Camera.CameraType == CameraType.Interface)
                    window.SetView(Camera.View);
                else if (Camera.CameraType == CameraType.World)
                    window.SetView(Camera.View);
            }
            else
            {
                window.SetView(Application.InterfaceView);
            }

            foreach (var component in Renderable)
            {
                component.Draw(window, RenderStates.Default);
            }
        }

        internal void Start()
        {
            OnStart();

            if (active)
                OnActivated();
        }

        /// <summary>
        /// Internal function that sets up the state
        /// </summary>
        /// <exception cref="GameViewInitException">Will be thrown if the state's already initialized</exception>
        internal void Added(StateGameManager manager, string id)
        {
            if (this.manager == null)
            {
                this.manager = manager;
                this.id = id;
                inputService = new UserInputManager();
                entities = new HashSet<Entity>();
                OnAdded();
            }
            else
            {
                throw new GameViewInitException();
            }
        }

        public override string ToString()
        {
            return "[GameState " + GetType().Name + "@" + GetType().GUID + "]";
        }

        public Entity FindFirstChild(string name)
        {
            return entities.First(entity => entity.Name == name);
        }

        /// <summary>
        /// The child entities of this GameState
        /// </summary>
        public Entity[] Children
        {
            get
            {
                return entities.ToArray();
            }
        }

        internal void RemoveEntity(Entity child)
        {
            entities.Remove(child);
        }

        /// <summary>
        /// Add an entity to this GameState
        /// </summary>
        /// <param name="child">The entity to add</param>
        public void AddEntity(Entity child)
        {   
            child.Init();
            entities.Add(child);
            child.StartBehaviours();
        }

        /// <summary>
        /// Spawn an entity under this GameState
        /// </summary>
        /// <returns>The spawned entity</returns>
        public Entity CreateChild()
        {
            Entity entity = Entity.Create(this);
            return entity;
        }

        internal Entity[] AllDescendants
        {
            get
            {
                return null;
            }
        }

        internal void InvokeInput(Application application, Mouse.Button input, InputState state)
        {
            Input.InvokeInput(application, input, state);
            foreach (Entity entity in entities)
            {
                entity.Input.InvokeInput(application, input, state);
            }

            foreach (IComponentEventListener com in EventComponents)
            {
                com.InputRecieved(new MouseInputAction(input, state, Mouse.GetPosition(Application.Window)));
            }
        }

        internal void InvokeInput(Application application, Keyboard.Key input, InputState state)
        {
            Input.InvokeInput(application, input, state);
            foreach (Entity entity in entities)
            {
                entity.Input.InvokeInput(application, input, state);
            }

            foreach (IComponentEventListener com in EventComponents)
            {
                com.InputRecieved(new KeyboardInputAction(state, input));
            }
        }
    }
}

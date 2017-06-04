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

namespace VorliasEngine2D.System
{
    /// <summary>
    /// The GameState's priority
    /// </summary>
    public enum GameStatePriority
    {
        First = 0,
        Background = 10,
        Normal = 100,
        Interface = 500,
        Last = 1000,
    }

    public class GameStateInitException : Exception
    {
        public GameStateInitException() : base("GameState already initialized!")
        {

        }
    }

    public class GameState : IInstanceTree
    {
        bool active;
        string id;
        StateManager manager;
        GameStatePriority priority;
        HashSet<Entity> entities;

        private UserInputManager inputService;
        public UserInputManager Input
        {
            get
            {
                return inputService;
            }
        }

        public StateApplication Application
        {
            get
            {
                return manager.Application;
            }
        }
    
        protected IEnumerable<ICollisionComponent> EntityColliders
        {
            get
            {
                return entities.Where(entity => entity.HasComponent<ICollisionComponent>()).Select(e => e.GetComponent<ICollisionComponent>());
            }
        }

        public Entity[] CollidableEntities
        {
            get
            {
                return entities.Where(entity => entity.HasComponent<ICollisionComponent>()).ToArray();
            }
        }

        /// <summary>
        /// Returns all the drawable entities
        /// </summary>
        public Entity[] DrawableEntities
        {
            get
            {
                return entities.Where(entity => entity.DrawableComponents.Count() > 0).ToArray();
            }
        }

        /// <summary>
        /// Returns all the entities with sprites, ordered by the render order
        /// </summary>
        [Obsolete("Use 'DrawableEntities' instead.")]
        public Entity[] SpriteEntities
        {
            get
            {
                var sprites = entities.Where(entity => entity.HasComponent<SpriteRenderer>());
                return sprites.OrderBy(entity => entity.SpriteRenderer.RenderOrder).ToArray();
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
        public GameStatePriority Priority
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

        internal void UpdateEntities()
        {
            foreach (Entity entity in Entities)
            {
                entity.Update();
            }
            UpdateCollisions();
        }

        internal void RenderEntities(RenderWindow window)
        {
            foreach (Entity entity in DrawableEntities)
            {
                entity.Render(window);
            }
        }

        internal void Start()
        {
            OnStart();
        }

        /// <summary>
        /// Internal function that sets up the state
        /// </summary>
        /// <exception cref="GameStateInitException">Will be thrown if the state's already initialized</exception>
        internal void Added(StateManager manager, string id)
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
                throw new GameStateInitException();
            }
        }

        public override string ToString()
        {
            return "[GameState " + GetType().Name + "@"+GetType().GUID+"]";
        }

        public Entity FindFirstChild(string name)
        {
            return entities.First(entity => entity.Name == name);
        }

        public Entity[] GetChildren()
        {
            return entities.ToArray();
        }

        /// <summary>
        /// Add an entity to this GameState
        /// </summary>
        /// <param name="child">The entity to add</param>
        public void AddEntity(Entity child)
        {
            //child.SetInputManager(inputService);
            child.Init();
            entities.Add(child);
            child.StartBehaviours();
        }

        /// <summary>
        /// Spawn an entity under this GameState
        /// </summary>
        /// <returns>The spawned entity</returns>
        public Entity SpawnEntity()
        {
            Entity entity = Entity.Spawn(this);
            return entity;
        }

        internal void InvokeInput(Application application, Mouse.Button input, InputState state)
        {
            Input.InvokeInput(application, input, state);
            foreach (Entity entity in entities)
            {
                entity.Input.InvokeInput(application, input, state);
            }
        }

        internal void InvokeInput(Application application, Keyboard.Key input, InputState state)
        {
            Input.InvokeInput(application, input, state);
            foreach (Entity entity in entities)
            {
                entity.Input.InvokeInput(application, input, state);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
using VorliasEngine2D.Entities;
using VorliasEngine2D.Entities.Components;

namespace VorliasEngine2D.System
{
    public enum GameStatePriority
    {
        Background,
        Normal,
        Interface,
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

        /// <summary>
        /// Returns all the entities with sprites, ordered by the render order
        /// </summary>
        public Entity[] SpriteEntities
        {
            get
            {
                var sprites = entities.Where(entity => entity.GetComponent<SpriteRenderer>() != null);
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
        public virtual void OnInit()
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

        public void UpdateEntities()
        {
            foreach (Entity entity in Entities)
            {
                entity.Update();
            }
        }

        public void RenderSprites(RenderWindow window)
        {
            foreach (Entity entity in SpriteEntities)
            {
                window.Draw(entity.SpriteRenderer);
            }
        }

        /// <summary>
        /// Internal function that sets up the state
        /// </summary>
        /// <exception cref="GameStateInitException">Will be thrown if the state's already initialized</exception>
        public void Init(StateManager manager, string id)
        {
            if (this.manager == null)
            {
                this.manager = manager;
                this.id = id;
                inputService = new UserInputManager();
                entities = new HashSet<Entity>();
                OnInit();
            }
            else
            {
                throw new GameStateInitException();
            }
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
            child.SetInputManager(this.inputService);
            child.Init();
            entities.Add(child);
        }

        /// <summary>
        /// Spawn an entity under this GameState
        /// </summary>
        /// <returns>The spawned entity</returns>
        public Entity SpawnEntity()
        {
            Entity entity = new Entity();
            
            entity.SetInputManager(this.inputService);
            entities.Add(entity);

            return entity;
        }
    }
}

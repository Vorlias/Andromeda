using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
using Andromeda2D.Entities;
using Andromeda2D.Entities.Components;
using SFML.System;
using Andromeda2D.System.Utility;
using Andromeda2D.System.Internal;
using Andromeda2D.Entities.Components.Internal;
using System;

namespace Andromeda2D.System
{

    public abstract class GameView : EntityContainer
    {
        bool active = true;
        string id;
        StateGameManager manager;
        GameViewPriority priority;

        public bool IsSingleton => this is IGameViewSingleton;

        Camera camera;
        public Camera Camera
        {
            get
            {
                return camera;
            }
        }

        /// <summary>
        /// Sets the camera type, will also create a camera if it doesn't exist
        /// </summary>
        /// <param name="type">The type of camera</param>
        public void SetCameraType(CameraType type)
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

            camera.CameraType = type;
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
        /// The game's state manager
        /// </summary>
        protected StateManager States
        {
            get => manager.Application.States;
        }


        /// <summary>
        /// The state actively using this view
        /// </summary>
        public GameState ParentState
        {
            get;
            internal set;
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

        public virtual void OnPreRender(RenderWindow window)
        {
            
        }

        /// <summary>
        /// Entities that have an event thing
        /// </summary>
        protected IEnumerable<IComponentEventListener> EventComponents
        {
            get
            {
                return CollidableEntities.Where(e => e.HasComponent<IComponentEventListener>() && e.Enabled ).Select(e => e.GetComponent<IComponentEventListener>());
            }
        }

        /// <summary>
        /// Entities that have a collision component
        /// </summary>
        internal Entity[] CollidableEntities
        {
            get
            {
                return Descendants.Where(entity => entity.HasComponent<ICollisionComponent>() && entity.Enabled).ToArray();
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
                return renderable.Where(r => r.Entity.Enabled).OrderBy(e => e.RenderOrder);
            }
        }

        /// <summary>
        /// The game view's priority
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
        /// The view's Id assigned by the ViewManager
        /// </summary>
        public string Id
        {
            get
            {
                return id;
            }
        }

        /// <summary>
        /// Whether or not this view is active
        /// </summary>
        public bool IsActive
        {
            get
            {
                return active || States.ActiveState.ExclusiveView.Current == this;
            }
            set
            {
                if (value)
                {
                    OnActivated();
                    
                }
                else
                {
                    OnDeactivated();
                    //Descendants.ForEach(descendant => descendant.Deactivated());
                }

                active = value;
            }
        }

        /// <summary>
        /// Method called when the view is added to the view manager
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
        /// Method called when the view is activated
        /// </summary>
        public virtual void OnActivated()
        {

        }

        /// <summary>
        /// Method called when the view is deactivated
        /// </summary>
        public virtual void OnDeactivated()
        {

        }

        /// <summary>
        /// Method called when the view is updated
        /// </summary>
        /// <param name="application">The application</param>
        public virtual void OnUpdate(Application application)
        {
            ProcessActions(application);
        }

        /// <summary>
        /// Method called when the view is rendered
        /// </summary>
        /// <param name="window"></param>
        public virtual void OnPostRender(RenderWindow window)
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
                Children.Where(entity => entity.Enabled).Select(entity => entity.GetComponentsInDescendants<IUpdatableComponent>()).ForEach(list => components.AddRange(list));
                return components.OrderByDescending(component => component.UpdatePriority);
            }
        }

        internal void UpdateEntities()
        {
            var updatableComponents = UpdatableComponents;

            foreach (var entity in Descendants.Where(descendant => descendant.Enabled))
            {
                if (entity.IsBeingDestroyed)
                {
                    if (entity.DestroyTick >= entity.LifeTime)
                        entity.Destroy();
                    else
                        entity.DestroyTick += Application.DeltaTime;
                }
            }

            updatableComponents.ForEach(com => com.BeforeUpdate());
            updatableComponents.ForEach(com => com.Update());
            updatableComponents.ForEach(com => com.AfterUpdate());

            UpdateCollisions();
        }

        internal void RenderEntities(RenderWindow window)
        {
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
            {
                OnActivated();
                
            }
                
        }

        /// <summary>
        /// Internal function that sets up the view
        /// </summary>
        /// <exception cref="GameViewInitException">Will be thrown if the view's already initialized</exception>
        internal void Added(StateGameManager manager, string id)
        {
            if (this.manager == null)
            {
                this.manager = manager;
                this.id = id;
                inputService = new UserInputManager();
                OnAdded();
            }
            else
            {
                throw new GameViewInitException();
            }
        }

        public override string ToString()
        {
            return "[GameView " + GetType().Name + "@" + GetType().GUID + "]";
        }

        /// <summary>
        /// Resets the view
        /// </summary>
        public void Reset()
        {
            ClearDelayedActions();

            // Force clear all the entities in this view
            ClearAllChildren();
            
            // Reset all bound inputs
            Input.ClearBindings();

            // Use OnStart to refresh everything.
            OnStart();
        }

        /// <summary>
        /// Spawn an entity under this GameView
        /// </summary>
        /// <returns>The spawned entity</returns>
        public override Entity CreateChild()
        {
            Entity entity = new Entity(this);
            return entity;
        }

        internal void InvokeInput(Application application, Mouse.Button input, InputState state)
        {
            Input.InvokeInput(application, input, state);
            foreach (Entity entity in Children)
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
            foreach (Entity entity in Children)
            {
                entity.Input.InvokeInput(application, input, state);
            }

            foreach (IComponentEventListener com in EventComponents)
            {
                com.InputRecieved(new KeyboardInputAction(state, input));
            }
        }

        internal void Activated()
        {
            Descendants.ForEach(descendant => descendant.Activated());
        }

        internal void Deactivated()
        {
            Descendants.ForEach(descendant => descendant.Deactivated());
        }
    }
}

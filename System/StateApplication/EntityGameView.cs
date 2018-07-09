using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
using Andromeda.Entities;
using Andromeda.Entities.Components;
using SFML.System;
using Andromeda.System.Utility;
using Andromeda.System.Internal;
using Andromeda.Entities.Components.Internal;
using System;
using Andromeda.Entities.Components.UI;
using Andromeda.Entities.Components.Colliders;
using Andromeda.Linq;
using Andromeda.Debugging;
using System.Diagnostics;

namespace Andromeda.System
{
    /// <summary>
    /// A GameView which uses entities
    /// </summary>
    public abstract class EntityGameView : EntityContainer, IGameView
    {
        bool active = true;
        string id;
        StateGameManager manager;
        GameViewPriority priority;

        public bool IsSingleton => this is IGameViewSingleton;

        public virtual void OnCreation()
        {

        }

        /// <summary>
        /// The mouse coordinates
        /// </summary>
        public MouseCoordinates MousePosition
        {
            get => new MouseCoordinates(Application, this);
        }

        public EntityGameView()
        {
            OnCreation();
        }

        private Camera _camera;
        /// <summary>
        /// The camera of this GameView
        /// </summary>
        public Camera Camera
        {
            get
            {
                var existing = UpdatableComponents.OfType<Camera>();
                if (existing.Count() > 1)
                {
                    DebugConsole.Warn("Multiple cameras detected!");
                }

                //if (_camera == null)
                //{
                //    var existing = UpdatableComponents.OfType<Camera>();
                //    if (existing.Count() > 0)
                //    {
                //        _camera = existing.First();
                //    }
                //    else
                //    {
                //        _camera = CreateChild().AddComponent<Camera>();
                //        _camera.Entity.Name = "Camera";
                //        //_camera.Update();
                //        DebugConsole.Warn("Created camera in view");
                //    }
                //}

                return _camera;
            }
        }


        protected void AddCamera()
        {
            if (_camera == null)
                _camera = CreateChild().AddComponent<Camera>();
            else
                DebugConsole.Warn("Camera already exists!");
        }

        /// <summary>
        /// Sets the camera type, will also create a camera if it doesn't exist
        /// </summary>
        /// <param name="type">The type of camera</param>
#if !USE_LEGACY_CAMERA
        [Obsolete]
#endif
        public void SetCameraType(CameraType type)
        {
#if !USE_LEGACY_CAMERA
            Camera.CameraType = CameraType.World;
            Debugging.DebugConsole.Warn("SetCameraType is non-functional.");
#else
            Camera.CameraType = type;
#endif
        }

        private UserInputManager inputService;
        public UserInputManager Input
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
        /// The mouse coordinates
        /// </summary>
        public MouseCoordinates MouseCoordinates => new MouseCoordinates(manager.Application, this);

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
        public IGameState ParentState
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
        protected IEnumerable<IEventListenerComponent> EventComponents
        {
            get
            {
                return CollidableEntities.Where(e => e.HasComponent<IEventListenerComponent>() && e.Enabled).Select(e => e.GetComponent<IEventListenerComponent>());
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
                return renderable.Where(r => r.Entity.Enabled && r.Entity.Visible).OrderBy(e => e.RenderOrder);
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
                    DebugConsole.WriteEngine("Set active (via IsActive): " + this.GetType().Name);
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
                        collider.Entity.WithComponents<ICollisionListener>(listener => listener.Collided(collider2.Entity));
                        collider2.Entity.WithComponents<ICollisionListener>(listener => listener.Collided(collider.Entity));
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
                Children.Where(entity => entity.Enabled).Select(entity => entity.GetComponentsInDescendants<IUpdatableComponent>(true)).ForEach(list => components.AddRange(list));
                return components.OrderBy(component => component.UpdatePriority);
            }
        }

        internal IEnumerable<IInterfaceComponent> InterfaceComponents
        {
            get
            {
                List<IInterfaceComponent> components = new List<IInterfaceComponent>();
                Children.Where(entity => entity.Enabled).Select(entity => entity.GetComponentsInDescendants<IInterfaceComponent>(true)).ForEach(list => components.AddRange(list));
                return components.OrderByDescending(component => component.ZIndex);
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

            updatableComponents.ForEach(com =>
            {
                com.BeforeUpdate();
                com.Update();
                com.AfterUpdate();
            });

            UpdateCollisions();
        }

        [Conditional("DEBUG")]
        internal void RenderDebug(RenderWindow window)
        {
            string cameraString = "";
            UpdatableComponents.ForEach(updatable =>
            {
                cameraString += updatable.Entity.FullName + "\n";
            });

            if (Camera != null)
            {
                RectangleShape rs = new RectangleShape();
                rs.Size = new Vector2f(10, 10);
                rs.Position = Camera.WorldPosition;
                window.Draw(rs);
            }

            Text tx = new Text(cameraString, FontManager.Get("Liberation-Mono"), 10);
            tx.Position = new Vector2f(100, 100);
            window.Draw(tx);
        }

        internal void RenderEntities(RenderWindow window)
        {
            var uiElements = Renderable.OfType<UIComponent>();
            var nonUI = Renderable.Where(item => !(item is UIComponent));

#if !USE_LEGACY_CAMERA
            if (Camera == null)
            {
                _camera = CreateChild().AddComponent<Camera>();
                Camera.Update();
            }

            if (Camera.View != null)
               window.SetView(Camera.View);
            else
                DebugConsole.Warn("View is null for Camera " + Camera.FriendlyId);

            foreach (var component in nonUI)
            {
                component.Draw(window, RenderStates.Default);
            }

            window.SetView(Application.InterfaceView);
            foreach (var component in uiElements)
            {
                component.Draw(window, RenderStates.Default);
            }
#else
            if (Camera != null)
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
#endif


            RenderDebug(window);

        }

        public void Start()
        {
            OnStart();
        }

        /// <summary>
        /// Internal function that sets up the view
        /// </summary>
        /// <exception cref="GameViewInitException">Will be thrown if the view's already initialized</exception>
        public void Added(StateGameManager manager, string id)
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
            DestroyAllChildren();

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

        internal void Activated()
        {
            Descendants.ForEach(descendant => descendant.Activated());
        }

        internal void Deactivated()
        {
            Descendants.ForEach(descendant => descendant.Deactivated());
        }

        public void Update(StateApplication application)
        {
            OnUpdate(application);
            UpdateEntities();
        }

        public void Render(RenderWindow window)
        {
            OnPreRender(window);
            RenderEntities(window);
            OnPostRender(window);
        }

        public void SetParentState(IGameState state)
        {
            ParentState = state;
        }

        public void ProcessInput(Application application, Mouse.Button button, InputState state)
        {
            Input.InvokeInput(application, button, state);
            foreach (Entity entity in Children)
            {
                entity.Input.InvokeInput(application, button, state);
            }

            bool isPreventingFallthrough = false;
            foreach (IEventListenerComponent com in EventComponents)
            {
                if (com is IInteractableInterfaceComponent i)
                {
                    if (!isPreventingFallthrough || i.IsIgnoringFallthroughState)
                        com.InputRecieved(new MouseInputAction(button, state, Mouse.GetPosition(Application.Window)));

                    if (i.HasFallthroughPriority)
                        isPreventingFallthrough = true;

                }
                else
                    com.InputRecieved(new MouseInputAction(button, state, Mouse.GetPosition(Application.Window)));
            }

            foreach (IInterfaceComponent com in InterfaceComponents)
            {
                if (com is IInteractableInterfaceComponent i)
                {
                    if (!isPreventingFallthrough || i.IsIgnoringFallthroughState)
                        i.InputRecieved(new MouseInputAction(button, state, Mouse.GetPosition(Application.Window)));

                    if (i.HasFallthroughPriority)
                        isPreventingFallthrough = true;

                }
            }
        }

        public void ProcessInput(Application application, Keyboard.Key key, InputState state)
        {
            Input.InvokeInput(application, key, state);
            foreach (Entity entity in Children)
            {
                entity.Input.InvokeInput(application, key, state);
            }

            foreach (IEventListenerComponent com in EventComponents)
            {
                com.InputRecieved(new KeyboardInputAction(state, key));
            }
        }
    }
}

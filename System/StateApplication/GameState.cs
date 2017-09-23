using System.Collections.Generic;
using Andromeda2D.Events;
using Andromeda2D.System.Internal;
using Andromeda2D.System.Utility;
using SFML.System;
using System;
using SFML.Audio;
using System.Linq;
using SFML.Window;

namespace Andromeda2D.System
{
    /// <summary>
    /// The base game state class
    /// </summary>
    public abstract class GameState : IGameState
    {
        HashSet<IGameView> views;
        private ExclusiveGameViewProperty exclusiveView;
        private bool mouseGrabbed = false;
        private ViewEvents viewEventQueue;

        bool started;
        public bool HasStarted
        {
            get => started;
        }

        public ViewEvents Events
        {
            get => viewEventQueue;
        }

        Vector2f mouseDelta = new Vector2f();
        /// <summary>
        /// Returns the delta if the mouse constraint is set to center
        /// </summary>
        public Vector2f MouseCenterDelta
        {
            get => mouseDelta;
        }

        [Obsolete]
        public Music BackgroundMusic
        {
            get;
            set;
        }

        public bool IsTempState
        {
            get;
            internal set;
        }

        public UserInputManager Input
        {
            get;
        }

        /// <summary>
        /// The exclusive view to update. Used for stuff like escape menus.
        /// </summary>
        public ExclusiveGameViewProperty ExclusiveView
        {
            get => exclusiveView;
        }

        MouseConstraintType mouseConstraint = MouseConstraintType.Normal;

        public MouseConstraintType MouseConstraint
        {
            get
            {
                return mouseConstraint;
            }
            set
            {
                bool constrainToWindow = MouseConstraintType.ConstrainedWindow == value;


                if (StateManager.ActiveState == this)
                    Application.Window.SetMouseCursorGrabbed(constrainToWindow);

                mouseGrabbed = constrainToWindow;

                mouseConstraint = value;
            }
        }

        public StateApplication Application
        {
            get => StateApplication.Application;
        }

        public StateManager StateManager
        {
            get;
            internal set;
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

        /// <summary>
        /// All the views that can be updated
        /// </summary>
        public IEnumerable<IGameView> UpdatableViewsByPriority
        {
            get
            {
                if (ExclusiveView.Current != null)
                {
                    return new IGameView[] { ExclusiveView.Current };
                }
                else
                    return ActiveViewsByPriority;
            }
        }

        internal void RenderEnd()
        {
            // Set the view back to default, so MouseGrabbed works correctly.
            Application.Window.SetView(Application.Window.DefaultView);
        }

        public IEnumerable<IGameView> ActiveViewsByPriority
        {
            get => Views.Where(view => view.IsActive || ExclusiveView.Current == view).OrderBy(view => view.Priority);
        }

        public IEnumerable<IGameView> Views
        {
            get => views;
        }

        public string Name
        {
            get;
            internal set;
        }

        internal void BeforeUpdate()
        {
            var window = Application.Window;
            var size = window.Size.ToFloat();
            var mousePosition = Mouse.GetPosition(window).ToFloat();

            if (mouseConstraint == MouseConstraintType.ConstrainedCenter)
            {
                mouseDelta = mousePosition - (size / 2);
            }
            else
                mouseDelta = new Vector2f();
        }

        internal void AfterUpdate()
        {
            if (mouseConstraint == MouseConstraintType.ConstrainedCenter && Application.IsFocused)
            {
                var window = Application.Window;
                var size = new Vector2i((int)window.Size.X, (int)window.Size.Y);

                Mouse.SetPosition(size / 2, window);
            }
        }

        public virtual void Update()
        {

        }

        public virtual void Render()
        {

        }

        public IEnumerable<IGameView> GetViewsByName(string viewName)
        {
            return views.Where(view => view.Id == viewName);
        }

        public IEnumerable<ViewType> GetViewsByType<ViewType>() where ViewType : IGameView
        {
            return views.OfType<ViewType>();
        }

        /// <summary>
        /// Finds the first view with the specified name
        /// </summary>
        /// <param name="name">The name of the view</param>
        /// <returns>The view, if it exsits.</returns>
        public IGameView FindFirstView(string name)
        {
            var views = GetViewsByName(name);
            return views.Count() > 0 ? views.First() : null;

        }

        /// <summary>
        /// Finds the first view with the specified type
        /// </summary>
        /// <typeparam name="ViewType">The type of the view</typeparam>
        /// <returns>The view, if it exists.</returns>
        public ViewType FindFirstView<ViewType>() where ViewType : GameView
        {
            var views = GetViewsByType<ViewType>();
            if (views.Count() > 0)
            {
                return views.First();
            }
            else
                return null;

            //return views.Count() > 0 ? views.First() : null;
        }

        internal void Add(GameView view)
        {
            views.Add(view);
        }

        internal void SetName(string name)
        {
            Name = name;
        }

        internal void SetManager(StateManager manager)
        {
            StateManager = manager;
        }

        protected GameState()
        {
            exclusiveView = new ExclusiveGameViewProperty(this);
            views = new HashSet<IGameView>();
            Input = new UserInputManager();
            viewEventQueue = new ViewEvents();
        }


        /// <summary>
        /// Activates this state
        /// </summary>
        public void Activate()
        {
            Activated();
            StateManager.SetActive(Name);

            if (!HasStarted)
            {
                Start();
                started = true;
            }
        }

        public virtual void OnDeactivated()
        {

        }

        internal void Activated()
        {
            Application.Window.SetMouseCursorGrabbed(mouseGrabbed);

            foreach (GameView view in Views)
            {
                // Set the state to this state for the views
                view.ParentState = this;

                if (view.IsActive)
                    view.Activated();
            }
        }

        public virtual void OnActivated()
        {

        }

        public virtual void OnReset()
        {

        }

        /// <summary>
        /// Reset the state
        /// </summary>
        public void Reset()
        {
            if (StateManager.ActiveState == this)
            {
                OnDeactivated();
                OnReset();

                foreach (var view in Views)
                {
                    view.Reset();
                }

                OnActivated();
            }
            else
            {
                OnReset();

                foreach (var view in Views)
                {
                    view.Reset();
                }
            }

        }

        public virtual void Initialize()
        {

        }

        internal void ClearAllViews()
        {
            views.Clear();
        }

        /// <summary>
        /// Starts the views
        /// </summary>
        protected void StartViews()
        {
            views.ForEach(view => view.Start());
        }

        /// <summary>
        /// Start the views
        /// </summary>
        internal abstract void Start();
    }
}
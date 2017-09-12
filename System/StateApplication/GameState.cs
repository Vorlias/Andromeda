using SFML.Audio;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Andromeda2D.System.Utility;
using Andromeda2D.System.Internal;
using Andromeda2D.Events;
using SFML.System;
using SFML.Window;

namespace Andromeda2D.System
{

    /// <summary>
    /// A game state
    /// </summary>
    public class GameState
    {
        HashSet<GameView> views;
        private ExclusiveGameViewProperty exclusiveView;
        private bool mouseGrabbed = false;
        private ViewEvents viewEventQueue;

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

        public Music BackgroundMusic
        {
            get;
            set;
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
        public IEnumerable<GameView> UpdatableViewsByPriority
        {
            get
            {
                if (ExclusiveView.Current != null)
                {
                    return new GameView[] { ExclusiveView.Current };
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

        public IEnumerable<GameView> ActiveViewsByPriority
        {
            get => Views.Where(view => view.IsActive || ExclusiveView.Current == view).OrderBy(view => view.Priority);
        }

        public IEnumerable<GameView> Views
        {
            get => views;
        }

        public string Name
        {
            get;
            internal set;
        }

        public virtual void InitializeViews()
        {

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
                var size = new Vector2i((int) window.Size.X, (int) window.Size.Y);

                Mouse.SetPosition(size / 2, window);
            }
        }

        public virtual void Update()
        {

        }

        public virtual void Render()
        {

        }

        public IEnumerable<GameView> GetViewsByName(string viewName)
        {
            return views.Where(view => view.Id == viewName);
        }

        public IEnumerable<ViewType> GetViewsByType<ViewType>() where ViewType : GameView
        {
            return views.OfType<ViewType>();
        }

        /// <summary>
        /// Finds the first view with the specified name
        /// </summary>
        /// <param name="name">The name of the view</param>
        /// <returns>The view, if it exsits.</returns>
        public GameView FindFirstView(string name)
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
            return views.Count() > 0 ? views.First() : null;
        }

        /// <summary>
        /// Adds a view of the specified type, using the type's name as the view name
        /// </summary>
        /// <typeparam name="ViewType">The type of view</typeparam>
        /// <param name="priority">The priority of the view</param>
        /// <param name="active">Whether or not this view is active</param>
        /// <returns></returns>
        public ViewType AddView<ViewType>(GameViewPriority priority = GameViewPriority.Normal, bool active = true) where ViewType : GameView, new()
        {
            ViewType newView = new ViewType();
            newView.Added(StateManager.GameManager, newView.GetType().Name);
            Add(newView);

            return newView;
        }

        public SingletonViewType AddSingletonView<SingletonViewType>(GameViewPriority priority = GameViewPriority.Normal, bool active = true) where SingletonViewType : GameViewSingleton<SingletonViewType>, new()
        {
            SingletonViewType view = GameViewSingleton<SingletonViewType>.Instance;

            // TODO: Fix this shit, add IsInitialized stuffs.

            if (view.Id == null)
                view.Added(StateManager.GameManager, view.GetType().Name);

            //if (view.manager)
            //view.Added(StateManager.GameManager, view.GetType().Name);
            Add(view);

            return view;
        }

        /// <summary>
        /// Adds a view with the GameViewPriority as Interface
        /// </summary>
        /// <typeparam name="ViewType">The type of the view</typeparam>
        /// <param name="name">The name of the view</param>
        /// <param name="active">Whether or not the view is active</param>
        /// <returns>The view</returns>
        public ViewType AddInterfaceView<ViewType>(string name = null, bool active = true) where ViewType : GameView, new()
        {
            return name != null ? AddView<ViewType>(name, GameViewPriority.Interface, active) : AddView<ViewType>(GameViewPriority.Interface, active);
        }

        /// <summary>
        /// Adds a view with the GameViewPriority as Background
        /// </summary>
        /// <typeparam name="ViewType">The type of the view</typeparam>
        /// <param name="name">The name of the view</param>
        /// <param name="active">Whether or not the view is active</param>
        /// <returns>The view</returns>
        public ViewType AddBackgroundView<ViewType>(string name = null, bool active = true) where ViewType : GameView, new()
        {
            return name != null ? AddView<ViewType>(name, GameViewPriority.Background, active) : AddView<ViewType>(GameViewPriority.Background, active);
        }

        /// <summary>
        /// Adds a new view to this GameState
        /// </summary>
        /// <typeparam name="ViewType">The type of view</typeparam>
        /// <param name="viewName">The name of the view</param>
        /// <param name="priority">The view's priority</param>
        /// <returns>The created view</returns>
        public ViewType AddView<ViewType>(string viewName, GameViewPriority priority = GameViewPriority.Normal, bool active = true) where ViewType : GameView, new()
        {
            ViewType newView = new ViewType();
            newView.Added(StateManager.GameManager, viewName);
            newView.IsActive = active;
            Add(newView);

            return newView;
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
            views = new HashSet<GameView>();
            Input = new UserInputManager();
            viewEventQueue = new ViewEvents();
            
        }

        internal static GameState Create()
        {
            return new GameState();
        }


        /// <summary>
        /// Activates this state
        /// </summary>
        public void Activate()
        {
            Activated();
            StateManager.SetActive(Name);


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

        /// <summary>
        /// Start the views
        /// </summary>
        internal void Start()
        {
            Initialize();
            InitializeViews();
            views.ForEach(view => view.Start());
        }
    }
}

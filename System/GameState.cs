using SFML.Audio;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.System.Utility;
using VorliasEngine2D.System.Internal;

namespace VorliasEngine2D.System
{

    /// <summary>
    /// A game state
    /// </summary>
    public class GameState
    {
        HashSet<GameView> views;
        private ExclusiveGameViewProperty exclusiveView;
        private bool mouseGrabbed = false;

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

        /// <summary>
        /// Whether or not the mouse is locked in this GameState
        /// </summary>
        public bool MouseGrabbed
        {
            set
            {
                if(StateManager.ActiveState == this)
                    Application.Window.SetMouseCursorGrabbed(value);

                mouseGrabbed = value;
            }
            get
            {
                return mouseGrabbed;
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
            get => Views.Where(view => view.IsActive).OrderBy(view => view.Priority);
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

        public virtual void InitViews()
        {

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

        public ViewType AddInterfaceView<ViewType>(string name = null, bool active = true) where ViewType : GameView, new()
        {
            return name != null ? AddView<ViewType>(name, GameViewPriority.Interface, active) : AddView<ViewType>(GameViewPriority.Interface, active);
        }

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
            Init();
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
        }

        public virtual void OnActivated()
        {

        }

        public virtual void Init()
        {

        }

        /// <summary>
        /// Start the views
        /// </summary>
        internal void Start()
        {
            
            InitViews();
            views.ForEach(view => view.Start());
        }
    }
}

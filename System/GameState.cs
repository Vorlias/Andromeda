using SFML.Audio;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.System.Utility;

namespace VorliasEngine2D.System
{
    /// <summary>
    /// A game state
    /// </summary>
    public class GameState
    {
        HashSet<GameView> views;
        private GameView exclusiveView;

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
        public GameView ExclusiveView
        {
            get => exclusiveView;
        }

        /// <summary>
        /// Sets the exlusive view to the specified view
        /// </summary>
        /// <param name="view">The view to set exclusive</param>
        public void SetExclusiveView(GameView view)
        {
            if (views.Contains(view))
            {
                exclusiveView = view;
            }
        }

        /// <summary>
        /// Removes the exclusive view
        /// </summary>
        public void RemoveExclusiveView()
        {
            exclusiveView = null;
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
        /// All the views that can be updated
        /// </summary>
        public IEnumerable<GameView> UpdatableViewsByPriority
        {
            get
            {
                if (exclusiveView != null)
                {
                    return new GameView[] { exclusiveView };
                }
                else
                    return ActiveViewsByPriority;
            }
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
        /// <returns></returns>
        public ViewType AddView<ViewType>(GameViewPriority priority = GameViewPriority.Normal) where ViewType : GameView, new()
        {
            ViewType newView = new ViewType();
            newView.Added(StateManager.GameManager, newView.GetType().Name);
            Add(newView);

            return newView;
        }

        public ViewType AddInterfaceView<ViewType>(string name = null) where ViewType : GameView, new()
        {
            return name != null ? AddView<ViewType>(name, GameViewPriority.Interface) : AddView<ViewType>(GameViewPriority.Interface);
        }

        public ViewType AddBackgroundView<ViewType>(string name = null) where ViewType : GameView, new()
        {
            return name != null ? AddView<ViewType>(name, GameViewPriority.Background) : AddView<ViewType>(GameViewPriority.Background);
        }

        /// <summary>
        /// Adds a new view to this GameState
        /// </summary>
        /// <typeparam name="ViewType">The type of view</typeparam>
        /// <param name="viewName">The name of the view</param>
        /// <param name="priority">The view's priority</param>
        /// <returns>The created view</returns>
        public ViewType AddView<ViewType>(string viewName, GameViewPriority priority = GameViewPriority.Normal) where ViewType : GameView, new()
        {
            ViewType newView = new ViewType();
            newView.Added(StateManager.GameManager, viewName);
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
            StateManager.SetActive(Name);
        }

        public virtual void Deactivated()
        {

        }

        public virtual void Activated()
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

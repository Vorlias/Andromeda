using SFML.Audio;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Andromeda.System.Utility;
using Andromeda.System.Internal;
using Andromeda.Events;
using SFML.System;
using SFML.Window;

namespace Andromeda.System
{

    /// <summary>
    /// A game state in which the views aren't cleared when the state is changed
    /// </summary>
    public class PersistentGameState : GameState
    { 
        public virtual void InitializeViews()
        {

        }

        /// <summary>
        /// Adds a view of the specified type, using the type's name as the view name
        /// </summary>
        /// <typeparam name="ViewType">The type of view</typeparam>
        /// <param name="priority">The priority of the view</param>
        /// <param name="active">Whether or not this view is active</param>
        /// <returns></returns>
        public ViewType AddView<ViewType>(GameViewPriority priority = GameViewPriority.Normal, bool active = true) where ViewType : IGameView, new()
        {
            ViewType newView = new ViewType();
            newView.SetParentState(this);
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
        public ViewType AddInterfaceView<ViewType>(string name = null, bool active = true) where ViewType : IGameView, new()
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
        public ViewType AddBackgroundView<ViewType>(string name = null, bool active = true) where ViewType : IGameView, new()
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
        public ViewType AddView<ViewType>(string viewName, GameViewPriority priority = GameViewPriority.Normal, bool active = true) where ViewType : IGameView, new()
        {
            ViewType newView = new ViewType();
            newView.Added(StateManager.GameManager, viewName);
            newView.IsActive = active;
            Add(newView);

            return newView;
        }

        /// <summary>
        /// Start the views
        /// </summary>
        internal override void Start()
        {
            Initialize();
            InitializeViews();
            Views.ForEach(view => view.Start());
        }
    }
}

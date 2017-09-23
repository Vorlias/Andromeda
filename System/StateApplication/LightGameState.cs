using Andromeda2D.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Andromeda2D.Events;
using Andromeda2D.System.Internal;
using Andromeda2D.System.Utility;

namespace Andromeda.System.StateApplication
{
    /// <summary>
    /// A game state that only holds views while it's running
    /// </summary>
    public abstract class LightGameState : GameState
    {
        protected abstract void InitializeViews();
        protected abstract void OnLightDeactivated();
        protected abstract void OnLightActivated();

        /// <summary>
        /// Adds a new view to this GameState, which will be temporary until the GameState is unloaded
        /// </summary>
        /// <typeparam name="ViewType">The type of view</typeparam>
        /// <param name="viewName">The name of the view</param>
        /// <param name="priority">The view's priority</param>
        /// <returns>The created view</returns>
        public ViewType AddTempView<ViewType>(GameViewConfig config) where ViewType : EntityGameView, new()
        {
            ViewType newView = new ViewType();
            newView.Added(StateManager.GameManager, config.Name);
            newView.IsActive = config.Active;
            Add(newView);

            return newView;
        }

        public sealed override void OnActivated()
        {
            InitializeViews();
            Views.ForEach(view => view.Start());
            OnLightActivated();
        }

        public sealed override void OnDeactivated()
        {
            OnLightDeactivated();
            ClearAllViews();
        }

        internal override void Start()
        {
            Initialize();
            //Views.ForEach(view => view.Start());
        }
    }
}

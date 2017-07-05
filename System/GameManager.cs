using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.System.Utility;
using VorliasEngine2D.System.Experimental;

namespace VorliasEngine2D.System
{

    public class StateGameManager
    {
        Dictionary<string, GameView> states;
        StateApplication application;

        public StateApplication Application
        {
            get
            {
                return application;
            }
        }

        public StateGameManager(StateApplication application)
        {
            states = new Dictionary<string, GameView>();
            this.application = application;
            States = new StateManager(this);
            Views = new ViewManager(this, States);
        }

        internal IEnumerable<GameView> UpdatableViewsByPriority
        {
            get
            {
                return States.ActiveState.UpdatableViewsByPriority;
            }
        }

        /// <summary>
        /// Get the active GameStates by order of priority
        /// </summary>
        internal IEnumerable<GameView> ActiveViewsByPriority
        {
            get
            {
                return States.ActiveState.ActiveViewsByPriority;
            }
        }

        public ViewManager Views
        {
            get;
        }

        public StateManager States
        {
            get;
        }

        /// <summary>
        /// Starts all the states' views
        /// </summary>
        internal void Start()
        {
            States.All.ForEach(state => state.Start());
        }
    }
}

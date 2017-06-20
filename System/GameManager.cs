using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.System.Utility;
using VorliasEngine2D.System.Experimental;

namespace VorliasEngine2D.System
{

    public abstract class GameCollectionService<ManageableType>
    {
        protected Dictionary<string, ManageableType> collection;

        public StateGameManager GameManager
        {
            get;
        }

        public bool Has(string name)
        {
            return collection.ContainsKey(name);
        }

        internal GameCollectionService(StateGameManager parent)
        {
            collection = new Dictionary<string, ManageableType>();
            GameManager = parent;
        }
    }

    public class StateManager : GameCollectionService<GameState>
    {
        /// <summary>
        /// The default state
        /// </summary>
        public GameState Default
        {
            get => collection["Default"];
        }

        public IEnumerable<GameState> All
        {
            get => collection.Select(item => item.Value);
        }

        private GameState activeState;


        /// <summary>
        /// The currently active state
        /// </summary>
        public GameState ActiveState
        {
            get
            {
                if (activeState != null)
                    return activeState;
                else
                    return Default;
            }
        }

        /// <summary>
        /// Gets the state with the specified name
        /// </summary>
        /// <param name="name">The name of the state</param>
        /// <returns>The state</returns>
        public GameState GetState(string name)
        {
            return collection[name];
        }

        /// <summary>
        /// Sets the active state
        /// </summary>
        /// <param name="name"></param>
        public void SetActive(string name)
        {
            if (Has(name))
            {
                if (activeState != null)
                    activeState.Deactivated();

                activeState = GetState(name);
                activeState.Activated();
            }
        }

        /// <summary>
        /// Creates a new generic state
        /// </summary>
        /// <param name="name">The name of the state</param>
        /// <returns>The state</returns>
        public GameState CreateState(string name)
        {
            GameState newState = GameState.Create();
            newState.SetManager(this);
            newState.SetName(name);
            collection.Add(name, newState);
            return newState;
        }

        /// <summary>
        /// Creates a State of the specified type
        /// </summary>
        /// <typeparam name="StateType">The state type</typeparam>
        /// <param name="name">The name of the state</param>
        /// <returns>The state</returns>
        public StateType CreateState<StateType>(string name) where StateType : GameState, new()
        {
            StateType newState = new StateType();
            newState.SetManager(this);
            newState.SetName(name);
            collection.Add(name, newState);
            return newState;
        }

        public StateManager(StateGameManager parent) : base(parent)
        {
            GameState defaultState = GameState.Create();
            defaultState.SetManager(this);
            defaultState.SetName("Default");
            collection.Add("Default", defaultState);
            activeState = defaultState;
        }

        public ViewType AddView<ViewType>(GameState state, string name, GameViewPriority priority = GameViewPriority.Normal) where ViewType : GameView, new()
        {
            ViewType v = new ViewType();
            v.Added(GameManager, name);
            state.Add(v);

            return v;
        }
    }

    public class ViewManager : GameCollectionService<GameView>
    {
        public StateManager States
        {
            get;
        }

        public ViewManager(StateGameManager parent, StateManager states) : base(parent)
        {
            States = states;
        }
    }

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

        /// <summary>
        /// Get the active GameStates by order of priority
        /// </summary>
        public IEnumerable<GameView> ActiveViewsByPriority
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

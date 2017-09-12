using System.Collections.Generic;
using System.Linq;

namespace Andromeda2D.System
{
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
                    activeState.OnDeactivated();

                activeState = GetState(name);
                activeState.Activated();
                activeState.OnActivated();

                foreach (var view in activeState.Views)
                {
                    view.ParentState = activeState;
                }
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
}

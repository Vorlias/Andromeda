using Andromeda.Debugging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Andromeda.System
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

                DebugConsole.WriteEngine("Activate state: " + name);

                if (activeState != null)
                    activeState.OnDeactivated();

                activeState = GetState(name);
                activeState.Activated();
                activeState.OnActivated();

                foreach (var view in activeState.Views)
                {
                    view.SetParentState(activeState);
                }
            }
        }

        /// <summary>
        /// Creates a persistent state of the specified type
        /// </summary>
        /// <typeparam name="StateType">The state type</typeparam>
        /// <param name="name">The name of the state</param>
        /// <returns>The state</returns>
        public StateType CreateState<StateType>(string name) where StateType : GameState, new()
        {
            DebugConsole.WriteEngine("CreateState " + name);
            StateType newState = new StateType();
            newState.SetManager(this);
            newState.SetName(name);
            collection.Add(name, newState);
            newState.IsTempState = false;
            return newState;
        }

        const string TEMP_STATE_ID = "TEMP";

        /// <summary>
        /// Loads a state to the temporary state id
        /// </summary>
        /// <typeparam name="StateType">The state type</typeparam>
        /// <returns>The state</returns>
        public StateType LoadTempState<StateType>() where StateType : GameState, new()
        {
            // Check and remove any old states using {TEMP_STATE_ID}
            if (collection.ContainsKey(TEMP_STATE_ID))
            {
                var state = collection[TEMP_STATE_ID];
                collection.Remove(state.Name);
            }

            StateType newState = new StateType();
            newState.SetManager(this);
            newState.SetName(TEMP_STATE_ID);
            collection.Add(TEMP_STATE_ID, newState);
            newState.IsTempState = true;
            newState.Activate();
            return newState;
        }

        public StateManager(StateGameManager parent) : base(parent)
        {
            PersistentGameState defaultState = new PersistentGameState();
            defaultState.SetManager(this);
            defaultState.SetName("Default");
            defaultState.IsTempState = false;
            collection.Add("Default", defaultState);
            activeState = defaultState;
        }

        public ViewType AddView<ViewType>(GameState state, string name, GameViewPriority priority = GameViewPriority.Normal) where ViewType : EntityGameView, new()
        {
            ViewType v = new ViewType();
            v.Added(GameManager, name);
            state.Add(v);

            return v;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.System.Utility;

namespace VorliasEngine2D.System
{
    public class StateManager
    {
        Dictionary<string, GameState> states;
        StateApplication application;

        public StateApplication Application
        {
            get
            {
                return application;
            }
        }

        public StateManager(StateApplication application)
        {
            states = new Dictionary<string, GameState>();
            this.application = application;
        }

        /// <summary>
        /// Get the GameStates by order of priority
        /// </summary>
        public GameState[] StatesByPriority
        {
            get
            {
                var stateArray = states.Values.ToArray();
                return stateArray.OrderBy(state => state.Priority).ToArray();
            }
        }

        /// <summary>
        /// Get the active GameStates by order of priority
        /// </summary>
        public GameState[] ActiveStatesByPriority
        {
            get
            {
                var stateArray = states.Values.ToArray();
                return stateArray.Where(state => state.IsActive).OrderBy(state => state.Priority).ToArray();
            }
        }

        /// <summary>
        /// Get a GameState by the key
        /// </summary>
        /// <param name="key">The key of the GameState</param>
        /// <returns></returns>
        public GameState this[string key]
        {
            get
            {
                return states[key];
            }
        }

        /// <summary>
        /// Selects the game states of the specified priority
        /// </summary>
        /// <param name="priority">The priority</param>
        /// <returns>Enumerable of game states matching this priority</returns>
        public IEnumerable<GameState> OfPriority(GameStatePriority priority)
        {
            return states.Select(kp => kp.Value).Where(state => state.Priority == priority);
        }

        /// <summary>
        /// Selects the game states of the specified type
        /// </summary>
        /// <typeparam name="T">The type</typeparam>
        /// <returns>Enumerable of game states matching this type</returns>
        public IEnumerable<T> OfType<T>()
        {
            return states.OfType<T>();
        }

        /// <summary>
        /// Adds a state of the specified type and priority to the StateManager using the default constructor
        /// </summary>
        /// <typeparam name="T">The type of the state</typeparam>
        /// <param name="name">The name of the state</param>
        /// <param name="priority">The priority of the state</param>
        public void Add<T>(string name, GameStatePriority priority = GameStatePriority.Normal) where T : GameState, new()
        {
            T state = new T();
            state.Added(this, name);
            state.Priority = priority;
            states.Add(name, state);
        }

        /// <summary>
        /// Creates a game state of the specified type, with the type's name as the name
        /// </summary>
        /// <typeparam name="T">The type of the GameState</typeparam>
        /// <param name="priority">The priority of the GameState</param>
        public void AddDefault<T>(GameStatePriority priority = GameStatePriority.Normal) where T : GameState, new()
        {
            T state = new T();
            state.Added(this, state.GetType().Name);
            states.Add(state.GetType().Name, state);
        }

        /// <summary>
        /// Adds a state to the state manager
        /// </summary>
        /// <param name="name">The name of the state</param>
        /// <param name="state">The state</param>
        public void Add(string name, GameState state)
        {
            states.Add(name, state);
            state.Added(this, name);
        }

        /// <summary>
        /// Call the start method of the states
        /// </summary>
        internal void Start()
        {
            states.Select(v => v.Value).ForEach(state => state.Start());
        }

        /// <summary>
        /// Sets the specified state inactive
        /// </summary>
        /// <param name="name">The name of the state</param>
        public void SetInactive(string name)
        {
            if (states.ContainsKey(name))
            { 
                states[name].IsActive = false;
            }
        }

        /// <summary>
        /// Sets the specified state active
        /// </summary>
        /// <param name="name">The name of the state</param>
        public void SetActive(string name)
        {
            if (states.ContainsKey(name))
            { 
                states[name].IsActive = true;
            }
        }

        /// <summary>
        /// Deactivates all the states
        /// </summary>
        public void DeactivateAll()
        {
            (from keyPair in states where keyPair.Value.IsActive select keyPair.Value).ForEach(value => value.IsActive = false);
        }
    }
}

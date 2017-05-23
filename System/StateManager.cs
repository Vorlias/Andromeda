using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VorliasEngine2D.System
{
    public class StateManager
    {
        Dictionary<string, GameState> states;

        public StateManager()
        {
            states = new Dictionary<string, GameState>();
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
        /// Adds a state of the specified type to the StateManager using the default constructor
        /// </summary>
        /// <typeparam name="T">The type of the state</typeparam>
        /// <param name="name">The name of the state</param>
        public void AddState<T>(string name) where T : GameState, new()
        {
            T state = new T();
            state.Init(this, name);
            states.Add(name, state);
        }

        /// <summary>
        /// Adds a state to the state manager
        /// </summary>
        /// <param name="name">The name of the state</param>
        /// <param name="state">The state</param>
        public void AddState(string name, GameState state)
        {
            states.Add(name, state);
            state.Init(this, name);
        }
    }
}

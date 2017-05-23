using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;

namespace VorliasEngine2D.System
{
    public enum GameStatePriority
    {
        Background,
        Normal,
        Interface,
    }

    public class GameStateInitException : Exception
    {
        public GameStateInitException() : base("GameState already initialized!")
        {

        }
    }

    public class GameState
    {


        bool active;
        string id;
        StateManager manager;
        GameStatePriority priority;

        /// <summary>
        /// The game state's priority
        /// </summary>
        public GameStatePriority Priority
        {
            get
            {
                return priority;
            }
            set
            {
                priority = value;
            }
        }

        /// <summary>
        /// The state's Id assigned by the StateManager
        /// </summary>
        public string Id
        {
            get
            {
                return id;
            }
        }

        /// <summary>
        /// The active state of the state
        /// </summary>
        public bool IsActive
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
            }
        }

        /// <summary>
        /// Method called when the state is added to the state manager
        /// </summary>
        public virtual void OnInit()
        {

        }

        /// <summary>
        /// Method called when the state is activated
        /// </summary>
        public virtual void OnActivated()
        {

        }

        /// <summary>
        /// Method called when the state is deactivated
        /// </summary>
        public virtual void OnDeactivated()
        {

        }

        /// <summary>
        /// Method called when the state is updated
        /// </summary>
        /// <param name="application">The application</param>
        public virtual void OnUpdate(Application application)
        {

        }

        /// <summary>
        /// Method called when the state is rendered
        /// </summary>
        /// <param name="window"></param>
        public virtual void OnRender(RenderWindow window)
        {

        }

        /// <summary>
        /// Internal function that sets up the state
        /// </summary>
        /// <exception cref="GameStateInitException">Will be thrown if the state's already initialized</exception>
        public void Init(StateManager manager, string id)
        {
            if (this.manager == null)
            {
                this.manager = manager;
                this.id = id;
                OnInit();
            }
            else
            {
                throw new GameStateInitException();
            }
        }
    }
}

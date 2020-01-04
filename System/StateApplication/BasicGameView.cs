using Andromeda.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Andromeda.Entities.Components;
using SFML.Graphics;
using SFML.Window;

namespace Andromeda.System
{
    /// <summary>
    /// A basic GameView that doesn't use entities
    /// </summary>
    public abstract class BasicGameView : IGameView
    {
        bool active = true;
        string id;
        StateGameManager manager;
        GameViewPriority priority;

        public void OnActivated()
        {

        }

        public void OnDeactivated()
        {

        }

        /// <summary>
        /// Internal function that sets up the view
        /// </summary>
        /// <exception cref="GameViewInitException">Will be thrown if the view's already initialized</exception>
        public void Added(StateGameManager manager, string id)
        {
            if (this.manager == null)
            {
                this.manager = manager;
                this.id = id;
                inputService = new UserInputManager();
            }
            else
            {
                throw new GameViewInitException();
            }
        }

        private UserInputManager inputService;
        public UserInputManager Input
        {
            get
            {
                return inputService;
            }
        }

        /// <summary>
        /// The view's Id assigned by the ViewManager
        /// </summary>
        public string Id
        {
            get
            {
                return id;
            }
        }

        public bool IsActive
        {
            get;
            set;
        }

        public IGameState ParentState
        {
            get;
            internal set;
        }

        /// <summary>
        /// The game view's priority
        /// </summary>
        public GameViewPriority Priority
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

        public abstract void OnRender(RenderWindow renderWindow);
        public abstract void OnUpdate(StateApplication application);

        public void ProcessInput(Application application, Mouse.Button button, InputState state)
        {
            Input.InvokeInput(application, button, state);
        }

        public void ProcessInput(Application application, Keyboard.Key key, InputState state)
        {
            Input.InvokeInput(application, key, state);
        }

        public void Render(RenderWindow window)
        {
            OnRender(window);
        }

        public virtual void Reset()
        {
            
        }

        public void SetParentState(IGameState state)
        {
            
        }

        public virtual void Start()
        {
            
        }

        public void Update(StateApplication application)
        {
            OnUpdate(application);
        }
    }
}

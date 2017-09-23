using Andromeda2D.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Andromeda2D.Entities.Components;
using SFML.Graphics;

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

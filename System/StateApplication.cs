using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;

namespace VorliasEngine2D.System
{
    public class StateApplication : Application
    {

        StateManager stateManager = new StateManager();

        private UserInputManager inputService = new UserInputManager();
        public UserInputManager InputService
        {
            get
            {
                return inputService;
            }
        }

        /// <summary>
        /// The states of this state application
        /// </summary>
        public StateManager States
        {
            get
            {
                return stateManager;
            }
        }

        /// <summary>
        /// Method called before states are updated
        /// </summary>
        protected virtual void UpdateStart()
        {

        }

        /// <summary>
        /// Method called after states are updated
        /// </summary>
        protected virtual void UpdateEnd()
        {

        }

        /// <summary>
        /// Method called before states are rendered
        /// </summary>
        protected virtual void RenderStart()
        {

        }

        /// <summary>
        /// Method called after states are rendered
        /// </summary>
        protected virtual void RenderEnd()
        {

        }

        protected sealed override void Render()
        {
            RenderStart();

            var states = States.StatesByPriority;
            foreach (GameState state in states)
            {
                state.OnRender(Window);
            }

            RenderEnd();
        }

        protected sealed override void Update()
        {
            UpdateStart();

            var states = States.StatesByPriority;
            foreach (GameState state in states)
            {
                state.OnUpdate(this);
            }

            UpdateEnd();
        }

        public StateApplication(VideoMode mode, string title, Styles styles = Styles.Default) : base(mode, title, styles)
        {
            
        }

        protected sealed override void BeforeStart()
        {
            Window.KeyPressed += Window_KeyPressed;
            Window.MouseButtonPressed += Window_MouseButtonPressed;
            Window.MouseButtonReleased += Window_MouseButtonReleased;
            Window.KeyReleased += Window_KeyReleased;
        }

        private void Window_MouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            InputService.InvokeInput(this, e.Button, InputState.Inactive);
        }

        private void Window_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            InputService.InvokeInput(this, e.Button, InputState.Active);
        }

        private void Window_KeyReleased(object sender, KeyEventArgs e)
        {
            InputService.InvokeInput(this, e.Code, InputState.Inactive);
        }

        private void Window_KeyPressed(object sender, KeyEventArgs e)
        {
            InputService.InvokeInput(this, e.Code, InputState.Active);
        }
    }
}

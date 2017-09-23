using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
using Andromeda2D.System.Internal;

namespace Andromeda2D.System
{
    public class StateApplication : Application
    {
        static StateApplication application;

        /// <summary>
        /// Returns the main StateApplication
        /// </summary>
        public static StateApplication Application
        {
            get
            {
                return application;
            }
        }

        StateGameManager stateManager;

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
        public StateGameManager Game
        {
            get
            {
                return stateManager;
            }
        }

        /// <summary>
        /// The Views of this StateApplication
        /// </summary>
        public ViewManager Views
        {
            get => Game.Views;
        }

        /// <summary>
        /// The States of this StateApplication
        /// </summary>
        public StateManager States
        {
            get => Game.States;
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

            

            var views = Game.ActiveViewsByPriority;
            foreach (IGameView view in views)
            {
                Application.Window.SetView(Application.Window.DefaultView);
                view.Render(Window);
            }

            States.ActiveState.Render();

            States.ActiveState.RenderEnd();

            RenderEnd();
        }

        protected sealed override void Update()
        {
            UpdateStart();

            States.ActiveState.BeforeUpdate();

            States.ActiveState.Update();

            var views = Game.UpdatableViewsByPriority;
            foreach (IGameView view in views)
            {
                view.Update(this);
            }

            UpdateEnd();

            States.ActiveState.AfterUpdate();
        }

        public StateApplication(VideoMode mode, string title, Styles styles = Styles.Default) : base(mode, title, styles)
        {
            stateManager = new StateGameManager(this);
            application = this;
        }

        protected sealed override void BeforeStart()
        {
            Window.SetFramerateLimit(144);
            Window.SetKeyRepeatEnabled(false);
            Window.KeyPressed += Window_KeyPressed;
            Window.MouseButtonPressed += Window_MouseButtonPressed;
            Window.MouseButtonReleased += Window_MouseButtonReleased;
            Window.KeyReleased += Window_KeyReleased;
            Window.TextEntered += Window_TextEntered;
        }

        private void Window_TextEntered(object sender, TextEventArgs e)
        {
            InputService.InvokeTextEntered(this, e.Unicode);

            var activeStateInput = States.ActiveState.Input;
            activeStateInput.InvokeTextEntered(this, e.Unicode);

            if (!activeStateInput.HasTextInputFocus)
            { 
                var states = Game.UpdatableViewsByPriority;
                foreach (GameView state in states)
                {
                    state.Input.InvokeTextEntered(this, e.Unicode);
                    //state.InvokeInput(this, e.Button, InputState.Inactive);
                }
            }
        }

        protected sealed override void AfterStart()
        {
            Game.Start();
        }

        private void Window_MouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            InputService.InvokeInput(this, e.Button, InputState.Inactive);
            States.ActiveState.Input.InvokeInput(this, e.Button, InputState.Inactive);


            var states = Game.UpdatableViewsByPriority;
            foreach (GameView state in states)
            {
                state.InvokeInput(this, e.Button, InputState.Inactive);
            }
        }

        private void Window_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            InputService.InvokeInput(this, e.Button, InputState.Active);
            States.ActiveState.Input.InvokeInput(this, e.Button, InputState.Active);

            var states = Game.UpdatableViewsByPriority;
            foreach (GameView state in states)
            {
                state.InvokeInput(this, e.Button, InputState.Active);
            }
        }

        private void Window_KeyReleased(object sender, KeyEventArgs e)
        {
            InputService.InvokeInput(this, e.Code, InputState.Inactive);
            States.ActiveState.Input.InvokeInput(this, e.Code, InputState.Inactive);


            var states = Game.UpdatableViewsByPriority;
            foreach (GameView state in states)
            {
                state.InvokeInput(this, e.Code, InputState.Inactive);
            }
        }

        private void Window_KeyPressed(object sender, KeyEventArgs e)
        {
            InputService.InvokeInput(this, e.Code, InputState.Active);

            var activeStateInput = States.ActiveState.Input;
            activeStateInput.InvokeInput(this, e.Code, InputState.Active);

            if (!activeStateInput.HasTextInputFocus)
            {
                var states = Game.UpdatableViewsByPriority;
                foreach (GameView state in states)
                {
                    state.InvokeInput(this, e.Code, InputState.Active);
                }
            }
        }
    }
}

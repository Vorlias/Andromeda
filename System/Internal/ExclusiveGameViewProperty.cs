using System.Linq;

namespace Andromeda2D.System.Internal
{
    public class ExclusiveGameViewProperty
    {
        private GameView view;

        /// <summary>
        /// The State associated with this ExclusiveGameViewProperty
        /// </summary>
        public IGameState State
        {
            get;
        }

        /// <summary>
        /// Whether or not ExclusiveView is active.
        /// </summary>
        public bool IsActive => view != null;

        /// <summary>
        /// The current exclusive GameView
        /// </summary>
        public GameView Current
        {
            get => view;
            set
            {
                if (value == null)
                    view = null;
                else if (State.Views.Contains(value))
                    view = value;
            }
        }

        /// <summary>
        /// Removes the exclusive game view, equivalent to .Current = null
        /// </summary>
        public void Reset()
        {
            view = null;
        }

        internal ExclusiveGameViewProperty(IGameState state)
        {
            State = state;
            view = null;
        }
    }
}

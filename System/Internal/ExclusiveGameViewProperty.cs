﻿using System.Linq;

namespace VorliasEngine2D.System.Internal
{
    public class ExclusiveGameViewProperty
    {
        private GameView view;

        /// <summary>
        /// The State associated with this ExclusiveGameViewProperty
        /// </summary>
        public GameState State
        {
            get;
        }

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

        internal ExclusiveGameViewProperty(GameState state)
        {
            State = state;
            view = null;
        }
    }
}

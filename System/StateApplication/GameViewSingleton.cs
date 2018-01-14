using Andromeda.Debugging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda.System
{
    public interface IGameViewSingleton
    {

    }

    /// <summary>
    /// A game view that has only one instance of itself
    /// </summary>
    public abstract class GameViewSingleton<GameViewClass> : EntityGameView, IGameViewSingleton where GameViewClass : EntityGameView, new()
    {
        static GameViewClass singleton;

        static GameViewSingleton()
        {
        }

        static bool started = false;

        /// <summary>
        /// Similar to OnStart() for regular GameViews, but called when the Singleton is initialized
        /// </summary>
        public abstract void OnSingletonStart();


        public sealed override void OnAdded()
        {
            // Do nothing because it doesn't get added
        }


        /// <summary>
        /// Overridden by GameViewSingleton
        /// </summary>
        public sealed override void OnStart()
        {
            if (!started)
            {
                started = true;
                OnSingletonStart();
            }
        }

        public new bool IsSingleton()
        {
            return true;
        }

        public static GameViewClass Instance
        {
            get
            {
                if (singleton == null)
                {
                    singleton = new GameViewClass();
                   // singleton.OnStart();
                }
                    
                return singleton;
            }
        }
    }
}

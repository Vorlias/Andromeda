using Andromeda.System;
using Andromeda2D.Entities.Components;
using System;

namespace Andromeda2D.System
{
    public abstract class InterfaceViewSingleton<InterfaceViewClass> : InterfaceView, IGameViewSingleton where InterfaceViewClass : InterfaceView, new()
    {
        static InterfaceViewClass singleton;

        static InterfaceViewSingleton()
        {
        }

        static bool started = false;

        /// <summary>
        /// Similar to OnStart() for regular GameViews, but called when the Singleton is initialized
        /// </summary>
        public abstract void OnUISingletonStart(UserInterface ui);


        public sealed override void OnAdded()
        {
            // Do nothing because it doesn't get added
        }

        public sealed override void OnInterfaceStart(UserInterface ui)
        {
            if (!started)
            {
                started = true;
                OnUISingletonStart(ui);
            }
        }

        public new bool IsSingleton()
        {
            return true;
        }

        public static InterfaceViewClass Instance
        {
            get
            {
                if (singleton == null)
                {
                    Console.WriteLine("Created singleton (.Instance)");
                    singleton = new InterfaceViewClass();
                    // singleton.OnStart();
                }

                return singleton;
            }
        }
    }
}

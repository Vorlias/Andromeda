using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using VorliasEngine2D.Entities;
using VorliasEngine2D.System.Services;

namespace VorliasEngine2D.System.Internal
{

    /// <summary>
    /// A service that handles the destruction of objects
    /// </summary>
    public class DestructionService : ThreadedService
    {
        class DelayedDestruction
        {
            public IDestroyable Object
            {
                get;
            }

            public float Time
            {
                get;
                set;
            }

            public float EndTime
            {
                get;
            }

            public DelayedDestruction(IDestroyable obj, float endTime)
            {
                Time = 0;
                EndTime = endTime;
                Object = obj;
            }
        }

        List<DelayedDestruction> queuedDestruction;

        public override int Frequency => 100;

        public void Add(IDestroyable destroyable, float time)
        {
            DelayedDestruction newObj = new DelayedDestruction(destroyable, time);
            queuedDestruction.Add(newObj);
        }

        private static DestructionService instance = new DestructionService();
        public static DestructionService Instance
        {
            get => instance;
        }

        private DestructionService()
        {
            queuedDestruction = new List<DelayedDestruction>();
        }

        protected override void Run()
        {
            while (IsRunning)
            {
                foreach (DelayedDestruction td in queuedDestruction.ToList())
                {
                    if (td.Time >= td.EndTime)
                    {
                        queuedDestruction.Remove(td);
                        td.Object.Destroy();
                    }
                    else
                    {
                        td.Time += 1.0f / 10;
                    }
                }

                Thread.Sleep(Frequency);
            }
            Console.WriteLine("Ending DestructionService!");
        }
    }
}

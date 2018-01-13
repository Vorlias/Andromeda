using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Andromeda.System.Services
{
    /// <summary>
    /// A service that runs on a separate thread
    /// </summary>
    public abstract class ThreadedService
    {
        public const int Hz144 = 7,
            Hz60 = 16,
            Hz30 = 33,
            Hz25 = 40,
            Hz15 = 66;

        protected abstract void Run();

        Thread thread;
        public Thread Thread
        {
            get;
        }

        public virtual string Name
        {
            get => GetType().Name;
        }

        /*public static ThreadedService Service
        {
            get;
        }*/

        public abstract int Frequency
        {
            get;
        }

        bool running = false;

        /// <summary>
        /// Whether or not the ThreadedService is running
        /// </summary>
        public bool Running
        {
            get => running;
        }

        private void StartService()
        {
            Console.WriteLine("Running a new ThreadedService: " + Name);
            Run();
        }

        public void Start()
        {
            if (!running)
            {
                running = true;
                ThreadStart start = new ThreadStart(StartService);
                thread = new Thread(start);
                thread.Start();
            }
        }

        public void Terminate()
        {
            running = false;
        }
    }
}

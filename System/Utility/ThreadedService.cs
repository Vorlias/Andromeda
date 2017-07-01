using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace VorliasEngine2D.System.Services
{
    /// <summary>
    /// A service that runs on a separate thread
    /// </summary>
    public abstract class ThreadedService
    {
        protected abstract void Run();

        Thread thread;
        public Thread Thread
        {
            get;
        }

        bool running = false;

        /// <summary>
        /// Whether or not the ThreadedService is running
        /// </summary>
        public bool IsRunning
        {
            get => running;
        }

        public void Start()
        {
            ThreadStart start = new ThreadStart(Run);
            thread = new Thread(start);
            thread.Start();
            running = true;
        }

        public void Terminate()
        {
            running = false;
        }
    }
}

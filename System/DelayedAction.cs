using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda.System
{
    class DelayedAction
    {
        public Action Action
        {
            get;
        }

        public float ElapsedTime
        {
            get;
            set;
        }

        public float TotalTime
        {
            get;
        }

        public DelayedAction(Action action, float time)
        {
            Action = action;
            ElapsedTime = 0.0f;
            TotalTime = time;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Andromeda.System;

namespace Andromeda.Entities.Components.Internal
{
    public interface IEventListenerComponent : IComponent
    {
        /// <summary>
        /// Called when the input event is triggered
        /// </summary>
        /// <param name="inputAction">The input action</param>
        void InputRecieved(UserInputAction inputAction);
    }
}

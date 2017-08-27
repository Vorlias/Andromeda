using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vorlias2D.System;

namespace Vorlias2D.Entities.Components.Internal
{
    public interface IComponentEventListener : IComponent
    {
        /// <summary>
        /// Called when the input event is triggered
        /// </summary>
        /// <param name="inputAction">The input action</param>
        void InputRecieved(UserInputAction inputAction);
    }
}

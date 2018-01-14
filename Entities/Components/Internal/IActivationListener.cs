using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda.Entities.Components.Internal
{
    interface IActivationListener : IComponent
    {
        void Activated();
        void Deactivated();
    }
}

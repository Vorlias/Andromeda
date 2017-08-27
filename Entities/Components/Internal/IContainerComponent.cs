using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vorlias2D.Entities.Components.Internal
{
    interface IContainerComponent
    {
        void ChildAdded(Entity entity);
    }
}

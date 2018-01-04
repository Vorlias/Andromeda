using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda.Entities.Components.Internal
{

    public interface IUpdatableComponent : IComponent
    {
        UpdatePriority UpdatePriority
        {
            get;
        }

        void Update();
        void AfterUpdate();
        void BeforeUpdate();
    }
}

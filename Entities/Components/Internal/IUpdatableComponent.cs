using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VorliasEngine2D.Entities.Components.Internal
{
    interface IUpdatableComponent
    {
        void Update();
        void AfterUpdate();
        void BeforeUpdate();
    }
}

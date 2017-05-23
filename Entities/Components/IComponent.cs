using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VorliasEngine2D.Entities.Components
{
    public interface IComponent
    {
        Entity Entity
        {
            get;
            set;
        }

        string Name
        {
            get;
        }
    }
}

using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda2D.Entities.Components
{
    /// <summary>
    /// Represents a view
    /// </summary>
    interface IViewComponent
    {
        View View
        {
            get;
        }
    }
}

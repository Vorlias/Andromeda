using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andromeda.Entities.Components
{

    public interface IRenderableComponent : Drawable, IComponent
    {
        RenderOrder RenderOrder
        {
            get;
            set;
        }
    }
}

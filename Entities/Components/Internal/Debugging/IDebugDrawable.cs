using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vorlias2D.Entities.Components.Internal
{
    public interface IDebugDrawable
    {
        void DrawDebug(RenderTarget target, RenderStates states);
    }
}

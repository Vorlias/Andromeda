using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VorliasEngine2D.Entities.Components
{
    class SpriteRenderer : Drawable, IComponent
    {
        public string Name
        {
            get
            {
                return "SpriteRenderer";
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            // TODO: Add sprite stuff here
        }
    }
}

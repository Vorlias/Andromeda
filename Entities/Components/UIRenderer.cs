using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace VorliasEngine2D.Entities.Components.Internal
{
    public class UIRenderer : Component, IRenderableComponent
    {
        public UITransform Transform
        {
            get
            {
                return entity.GetComponent<UITransform>();
            }
        }

        private RenderOrder renderOrder;
        public RenderOrder RenderOrder
        {
            get
            {
                return renderOrder;
            }

            set
            {
                renderOrder = value;
            }
        }

        public override bool AllowsMultipleInstances
        {
            get
            {
                return false;
            }
        }

        public override string Name
        {
            get
            {
                return "UIRenderer";
            }
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
        }
    }
}

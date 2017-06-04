using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using VorliasEngine2D.Entities.Components;
using VorliasEngine2D.Entities.Components.Internal;

namespace VorliasEngine2D.Entities
{
    // TODO: Figure out why it requires IRenderableComponent to render child UIRenderer
    public class UserInterface : Component, IContainerComponent, IRenderableComponent
    {
        public override bool AllowsMultipleInstances
        {
            get
            {
                return false;
            }
        }

        public override void OnComponentInit(Entity entity)
        {
            entity.Name = "UserInterface";
        }

        public void ChildAdded(Entity entity)
        {
            entity.AddComponent<UITransform>();
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            
        }

        public override string Name
        {
            get
            {
                return "UserInterface";
            }
        }

        public RenderOrder RenderOrder
        {
            get
            {
                return RenderOrder.Foreground;
            }
            set
            {

            }
        }
    }
}

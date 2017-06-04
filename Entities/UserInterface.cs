using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.Entities.Components;
using VorliasEngine2D.Entities.Components.Internal;

namespace VorliasEngine2D.Entities
{
    public class UserInterface : Component, IContainerComponent
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

        public override string Name
        {
            get
            {
                return "UserInterface";
            }
        }
    }
}

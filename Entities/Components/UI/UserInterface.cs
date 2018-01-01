using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using Andromeda2D.Entities.Components;
using Andromeda2D.Entities.Components.Internal;

namespace Andromeda2D.Entities.Components
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

        /// <summary>
        /// Adds a UIComponent of the specified type to this UserInterface
        /// </summary>
        /// <typeparam name="UIComponentType"></typeparam>
        /// <returns></returns>
        public UIComponentType Add<UIComponentType>() where UIComponentType : UIComponent, new()
        {
            Entity child = Entity.CreateChild();
            return child.AddComponent<UIComponentType>();
        }

        public override void OnComponentInit(Entity entity)
        {
            entity.Name = "UserInterface";
        }

        public void ChildAdded(Entity entity)
        {
            entity.AddComponent<UITransform>();
        }

        public override void OnComponentCopy(Entity source, Entity copy)
        {
            var ui = copy.AddComponent<UserInterface>();
        }

        public override string ToString()
        {
            int count = Entity.GetComponentsInDescendants<UIComponent>().Count;

            if (count == 1)
                return "UserInterface - " + count + " UIComponent";
            else
                return "UserInterface - " + count +" UIComponents";
        }
    }
}

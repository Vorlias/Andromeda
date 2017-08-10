using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.Entities.Components;
using VorliasEngine2D.System.Internal;

namespace VorliasEngine2D.Entities
{
    public class UIEntity : Entity
    {
        /// <summary>
        /// The Transform of the UIEntity
        /// </summary>
        public new UITransform Transform
        {
            get => GetComponent<UITransform>();
        }

        UserInterface parentInterface;

        public UserInterface Interface
        {
            get => parentInterface;
        }

        public override void OnCreate()
        {
            // Add a UITransform
            AddComponent<UITransform>();
        }

        public override void OnParentChanged(EntityContainer newParent)
        {
            if (newParent is Entity)
            {
                var parentEntity = newParent as Entity;
                var userInterfaces = parentEntity.Ancestors.Where(ancestor => ancestor.HasComponent<UserInterface>()).Select(ancestor => ancestor.GetComponent<UserInterface>());

                if (userInterfaces.Count() > 0)
                    parentInterface = userInterfaces.First();
            }
        }
    }
}

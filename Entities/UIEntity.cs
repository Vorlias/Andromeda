using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Andromeda.Entities.Components;
using Andromeda.System;
using Andromeda.System.Internal;

namespace Andromeda.Entities
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

        public new UICoordinates Position
        {
            get => Transform.LocalPosition;
            set => Transform.LocalPosition = value;
        }

        public UICoordinates Size
        {
            get => Transform.LocalSize;
            set => Transform.LocalSize = value;
        }

        UserInterface parentInterface;

        public UserInterface Interface
        {
            get => parentInterface;
        }

        public UIEntity(EntityContainer parent) : base(parent)
        {

        }

        public UIEntity(UserInterface parent) : base(parent.Entity)
        {

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

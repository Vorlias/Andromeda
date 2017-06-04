using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.Entities.Components.Internal;
using VorliasEngine2D.System;

namespace VorliasEngine2D.Entities.Components
{
    public sealed class UITransform : EntityBehaviour
    {
        private Transform transform;

        public UICoordinates Size
        {
            get;
            set;
        }

        public UICoordinates Position
        {
            get;
            set;
        }

        public override bool AllowsMultipleInstances
        {
            get
            {
                return false;
            }
        }

        public override void Update()
        {
            // We want to update the transform to reflect this ;)
            Entity.Transform.Position = Position.Absolute(Application.Window);
            Entity.Transform.Origin = new Vector2f(0, 0);
        }

        public override void OnComponentInit(Entity entity)
        {
           // Create the transform if it doesn't exist.
           entity.FindComponent(out transform, true);
           Position = new UICoordinates(0, 0, 0, 0);
           Size = new UICoordinates(0, 0, 0, 0);
        }

        public override string Name
        {
            get
            {
                return "UITransform";
            }
        }
    }
}

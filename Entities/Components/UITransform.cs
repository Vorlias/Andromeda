using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.Entities.Components.Internal;
using VorliasEngine2D.Serialization;
using VorliasEngine2D.System;

namespace VorliasEngine2D.Entities.Components
{
    /// <summary>
    /// User Interface Transform Component - Overrides the default Transform.
    /// </summary>
    public sealed class UITransform : Component, IUpdatableComponent
    {
        private Transform transform;

        [PersistentProperty("Size", PropertyType = SerializedPropertyType.UICoordinates)]
        public UICoordinates Size
        {
            get;
            set;
        }

        [PersistentProperty("Position", PropertyType = SerializedPropertyType.UICoordinates)]
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

        public void Update()
        {
            // We want to update the transform to reflect this ;)
            Entity.Transform.Position = Position.GlobalAbsolute;
            Entity.Transform.Origin = new Vector2f(0, 0);
        }

        public override void OnComponentInit(Entity entity)
        {
           // Create the transform if it doesn't exist.
           entity.FindComponent(out transform, true);
           Position = new UICoordinates(0, 0, 0, 0);
           Size = new UICoordinates(0, 0, 0, 0);
        }

        public void AfterUpdate()
        {
            
        }

        public void BeforeUpdate()
        {
            
        }

        public override void OnComponentCopy(Entity source, Entity copy)
        {
            var copyTransform = copy.AddComponent<UITransform>();
            copyTransform.Position = Position;
            copyTransform.Size = Size;
        }

        public override string ToString()
        {
            return Name + " - Position: " + Position + ", Size: " + Size;
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

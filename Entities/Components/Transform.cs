using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.Entities.Components.Internal;
using VorliasEngine2D.Serialization;
using VorliasEngine2D.System.Utility;

namespace VorliasEngine2D.Entities.Components
{
    public sealed class PositionConstraint
    {
        public Vector2f Min
        {
            get;
            set;
        }

        public Vector2f Max
        {
            get;
            set;
        }

        public bool IsEnabled
        {
            get
            {
                return (Max - Min) != new Vector2f(0, 0);
            }
        }

        public PositionConstraint()
        {
            Min = new Vector2f(0, 0);
            Max = new Vector2f(0, 0);
        }

        public PositionConstraint(Vector2f min, Vector2f max)
        {
            Min = min;
            Max = max;
        }
    }

    public sealed class Transform : Transformable, IComponent
    {
        private Entity entity;
        public Entity Entity
        {
            get
            {
                return entity;
            }
        }

        public bool AllowsMultipleInstances
        {
            get
            {
                return false;
            }
        }

        public PositionConstraint PositionConstraint
        {
            get;
            set;
        }

        private bool enabled;
        public bool IsEnabled
        {
            get
            {
                return enabled;
            }
        }

        internal void SetEnabled(bool enabled)
        {
            this.enabled = enabled;
        }

        [PersistentProperty("Position")]
        /// <summary>
        /// The position of this transform
        /// </summary>
        public new Vector2f Position
        {
            get
            {
                return base.Position;
            }
            set
            {
                Vector2f newPosition = value;

                if(PositionConstraint != null && PositionConstraint.IsEnabled)
                {
                    newPosition = newPosition.Clamp(PositionConstraint.Min, PositionConstraint.Max);
                }
                    

                base.Position = newPosition;
            }
        }

        [PersistentProperty("Rotation")]
        public new float Rotation
        {
            get
            {
                return base.Rotation;
            }
            set
            {
                base.Rotation = value;
            }
        }

        public string Name
        {
            get
            {
                return "Transform";
            }
        }

        /// <summary>
        /// Called when the component is added to an entity
        /// </summary>
        /// <param name="entity">The entity it is added to</param>
        /// <exception cref="SetEntityInvalidException">Called if the user tries to set it</exception>
        public void ComponentInit(Entity entity)
        {
            if (this.entity == null)
                this.entity = entity;
            else
                throw new SetEntityInvalidException();
        }

        public void OnComponentCopy(Entity source, Entity copy)
        {
            var sourceTransform = source.Transform;
            var copyTransform = copy.Transform;

            // Just set the properties the same :-)
            copyTransform.Origin = sourceTransform.Origin;
            copyTransform.Position = sourceTransform.Position;
            copyTransform.Scale = sourceTransform.Scale;
        }
    }
}

using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Andromeda2D.Entities.Components.Internal;
using Andromeda2D.Serialization;
using Andromeda2D.System.Utility;

namespace Andromeda2D.Entities.Components
{



    public sealed class Transform : Transformable, IComponent
    {
        public enum AxisType
        {
            World,
            Window
        }

        /// <summary>
        /// Flag for enabling local coordinates by default
        /// </summary>
        public const bool USE_LOCAL_COORDINATES_DEFAULT = true;
        public const AxisType ENTITY_DEFAULT_AXIS = AxisType.World;

        bool localCoordinates = USE_LOCAL_COORDINATES_DEFAULT;

        /// <summary>
        /// If the transform is local based (if true, it will be affected by the parent entity's transform)
        /// </summary>
        [SerializableProperty("IsTransformLocal", PropertyType = SerializedPropertyType.Bool)]
        public bool IsTransformLocal
        {
            get
            {
                return localCoordinates;
            }
            set
            {
                localCoordinates = value;
            }
        }

        private AxisType worldAxis = ENTITY_DEFAULT_AXIS;

        /// <summary>
        /// The axis this transform applies to, if Window it will start at 0, 0 with the window - else if World it will start at the world 0, 0
        /// </summary>
        public AxisType Axis
        {
            get => worldAxis;
            set => worldAxis = value;
        }

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

        private Vector2f localPosition = new Vector2f(-1, -1);



        /// <summary>
        /// The local position of this transform
        /// </summary>
        [SerializableProperty("LocalPosition")]
        public Vector2f LocalPosition
        {
            get
            {
                if (IsTransformLocal && Entity.ParentContainer != null)
                    return localPosition;
                else
                {
                    return Position;
                }
                    
            }
            set
            {
                if (IsTransformLocal && Entity.ParentContainer != null)
                    localPosition = value;
                else
                    Position = value;
            }
        }

        /// <summary>
        /// Rotation of this Transform
        /// </summary>
        public float LocalRotation
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

        /// <summary>
        /// Rotation of this transform + the parent transforms (If IsTransformLocal is enabled)
        /// </summary>
        [SerializableProperty("Rotation")]
        public new float Rotation
        {
            get
            {
                if (IsTransformLocal && Entity.Parent != null)
                {
                    return Entity.Parent.Transform.Rotation + base.Rotation;
                }
                else
                    return base.Rotation;
            }
            set
            {
                if (IsTransformLocal && Entity.Parent != null)
                {
                    LocalRotation = Entity.Parent.Transform.Rotation + value;
                }
                else
                    base.Rotation = value;
            }
        }


        /// <summary>
        /// The position of this transform offset from parent transforms (If IsTransformLocal is enabled, otherwise this local position)
        /// </summary>
        [SerializableProperty("Position")]
        public new Vector2f Position
        {
            get
            {
                if (IsTransformLocal && Entity.Parent != null)
                {
                    // The Vector2(0,0) thing is a digusting hack xD - but here because we want to rotate at 0, 0 for local rotation then apply offset rotation
                    return (Origin.Rotate(LocalRotation) + LocalPosition.Rotate(Rotation - LocalRotation) + Entity.Parent.Transform.Position);
                }
                else
                    return base.Position;
            }
            set
            {
                if (IsTransformLocal && Entity.Parent != null)
                {
                    localPosition = value - Entity.Parent.Position;
                    //base.Position = Entity.Parent.Position + value;
                }
                else
                { 
                    Vector2f newPosition = value;

                    if(PositionConstraint != null && PositionConstraint.IsEnabled)
                    {
                        newPosition = newPosition.Clamp(PositionConstraint.Min, PositionConstraint.Max);
                    }
                    

                    base.Position = newPosition;
                }

                //foreach (var child in Entity.Children)
                //{
                //    if (child.Transform.IsTransformLocal)
                //        child.Transform.Position = value;
                //}
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
            copyTransform.IsTransformLocal = sourceTransform.IsTransformLocal;
            
            
            copyTransform.Origin = sourceTransform.Origin;
            copyTransform.Position = sourceTransform.Position;

            //copyTransform.LocalPosition = sourceTransform.LocalPosition;
            copyTransform.Scale = sourceTransform.Scale;
            
        }
    }
}

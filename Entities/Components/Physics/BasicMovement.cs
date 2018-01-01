using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Andromeda2D.Entities.Components.Internal;
using Andromeda2D.System;
using Andromeda2D.System.Utility;
using Andromeda.System;

namespace Andromeda2D.Entities.Components
{
    /// <summary>
    /// A basic entity movement system, doesn't use physics.
    /// </summary>
    [DisallowMultiple]
    public class BasicMovement : Component, IUpdatableComponent
    {
        /* 
         * Note: This is pretty simple for what it is.
         */

        float dampRate = 1.0f,
            accelerateRate = 1.0f,
            speed = 0.0f,
            speedModifier = 0.0f,
            minTrackingDistance = 0.0f;

        bool localMovement = true;

        /// <summary>
        /// If movement is using a direction, will determine if movement is via local space (rotated to entity)
        /// </summary>
        public bool IsMovementLocal
        {
            get => localMovement;
            set => localMovement = value;
        }

        public Vector2f MovementDelta
        {
            get => movementDelta;
            set => Move(value);
        }

        Transform targetTransform;
        Vector2f movementDelta;

        public UpdatePriority UpdatePriority => UpdatePriority.Physics;

        public BasicMovement()
        {

        }

        /// <summary>
        /// Moves this entity towards the target entity
        /// </summary>
        /// <param name="entity">The entity to move towards</param>
        /// <param name="distance">The minimum distance where the movement stops</param>
        public void MoveTo(Entity entity, float distance = 1.0f)
        {
            targetTransform = entity.Transform;
            minTrackingDistance = distance;
        }

        public void Move(Vector2f direction)
        {
            movementDelta = direction;
            targetTransform = null;
        }

        public void AfterUpdate()
        {

        }

        public void BeforeUpdate()
        {

        }

        /// <summary>
        /// The distance between this entity and the target
        /// </summary>
        public float TargetDistance
        {
            get
            {
                if (targetTransform != null)
                    return (targetTransform.Position - Entity.Transform.Position).GetLength();
                else
                    return -1;
            }
        }


        /// <summary>
        /// The target speed to reach while travelling towards the target
        /// </summary>
        public float TargetSpeed
        {
            get => speed;
            set => speed = value;
        }

        /// <summary>
        /// The speed of which deceleration happens
        /// </summary>
        public float DampenRate
        {
            get => dampRate;
            set => dampRate = value;
        }

        /// <summary>
        /// The speed of which acceleration happens
        /// </summary>
        public float AccelerationRate
        {
            get => accelerateRate;
            set => accelerateRate = value;
        }

        /// <summary>
        /// The current speed of this entity
        /// </summary>
        public float Speed
        {
            get => speedModifier;
        }

        /// <summary>
        /// The angle between this entity's position and the other entity's position
        /// </summary>
        public float Angle
        {
            get => (targetTransform.Position.GetAngle(entity.Transform.Position)) - 180;
        }

        public void Update()
        {
            float deltaTime = StateApplication.Application.DeltaTime;

            if (speedModifier < speed)
            {
                speedModifier += accelerateRate;
            }
            else if (speedModifier > speed)
            {
                speedModifier -= dampRate;
            }

            var realMovementDelta = (IsMovementLocal ? movementDelta.Rotate(entity.Transform.Rotation) : movementDelta) * deltaTime;

            // Update entity position
            if (targetTransform != null && TargetDistance > minTrackingDistance)
            {
                Entity.Transform.Position += realMovementDelta + (targetTransform.Position - Entity.Transform.Position).Normalize() * (speedModifier * deltaTime);
            }
            else if (movementDelta != default(Vector2f))
            {
                Entity.Transform.Position += realMovementDelta;
            }
            
        }
    }
}

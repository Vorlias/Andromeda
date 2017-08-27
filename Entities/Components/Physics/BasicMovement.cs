using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vorlias2D.Entities.Components.Internal;
using Vorlias2D.System;
using Vorlias2D.System.Utility;


namespace Vorlias2D.Entities.Components
{
    /// <summary>
    /// A basic entity movement system, doesn't use physics.
    /// </summary>
    public class BasicMovement : Component, IUpdatableComponent
    {
        /* 
         * Note: This is pretty simple for what it is.
         */

        public override bool AllowsMultipleInstances => false;
        public override string Name => "BasicMovement";

        float dampRate = 1.0f,
            accelerateRate = 1.0f,
            speed = 0.0f,
            speedModifier = 0.0f,
            minTrackingDistance = 0.0f;

        Transform targetTransform;

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

            // Update entity position
            if (targetTransform != null && TargetDistance > minTrackingDistance)
            {
                Entity.Transform.Position += (targetTransform.Position - Entity.Transform.Position).Normalize() * (speedModifier * deltaTime);
            }
            
        }
    }
}

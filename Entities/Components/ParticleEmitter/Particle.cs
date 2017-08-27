using SFML.System;
using VorliasEngine2D.System;
using VorliasEngine2D.System.SequenceTypes;
using VorliasEngine2D.System.Utility;

namespace VorliasEngine2D.Entities.Components
{
    /// <summary>
    /// Represents a single particle
    /// </summary>
    class Particle
    {
        float elapsedLifeTime = 0.0f;
        bool alive = true;
        float elapsedRotation = 0.0f;

        public float ElapsedTime
        {
            get => elapsedLifeTime;
        }

        public float ElapsedRotation
        {
            get => elapsedRotation;
        }

        public bool IsAlive
        {
            get => alive;
        }

        internal void Tick()
        {
            float delta = StateApplication.Application.DeltaTime;
            Position += Direction * delta;
            elapsedLifeTime += delta;
            elapsedRotation += Rotation * RotationSpeed * delta;

            if (elapsedLifeTime >= LifeTime)
            {
                alive = false;
            }
        }
        
        public Vector2f Position { get; set; }
        public NumberSequence Size { get; set; }
        public float Speed { get; set; }
        public float LifeTime { get; set; }
        public float Rotation { get; set; }
        public Vector2f Direction { get; set; }
        public float RotationSpeed { get; set; }

        public Particle(Vector2f position, NumberSequence size, float speed, float lifeTime, float rotation, float rotationSpeed, Vector2f direction)
        {
            Position = position;
            Size = size;
            Speed = speed;
            LifeTime = lifeTime;
            Rotation = rotation;
            Direction = direction.Rotate(rotation);
            RotationSpeed = rotationSpeed;
        }
    }
}

using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;
using Andromeda2D.Entities.Components.Internal;
using Andromeda2D.System.SequenceTypes;
using Andromeda2D.System.Types;
using Andromeda2D.System.Utility;

namespace Andromeda2D.Entities.Components
{

    /// <summary>
    /// A component that emits particles
    /// </summary>
    public class ParticleEmitter : TextureComponent, IUpdatableComponent
    {
        public override bool AllowsMultipleInstances => true;

        public override string Name => "ParticleEmitter";

        public UpdatePriority UpdatePriority => UpdatePriority.Normal;

        NumberRange rotation = new NumberRange(0.0f),
            lifeTime = new NumberRange(1.0f),
            speed = new NumberRange(1.0f),
            rotationSpeed = new NumberRange(0.0f);

        NumberSequence size = new NumberSequence(1.0f),
            transparency = 0.0f;

        ColorSequence color = new Color(255, 255, 255);

        Vector2f emissionDirection;

        float spawnRate = 1.0f;
        bool enabled = false;

        /// <summary>
        /// The direction the particles emit towards
        /// </summary>
        public Vector2f EmissionDirection
        {
            get => emissionDirection;
            set => emissionDirection = value;
        }

        /// <summary>
        /// The amount the particles rotate by
        /// </summary>
        public NumberRange Rotation
        {
            get => rotation;
            set => rotation = value;
        }

        /// <summary>
        /// The lifetime of the particles
        /// </summary>
        public NumberRange LifeTime
        {
            get => lifeTime;
            set => lifeTime = value;
        }

        /// <summary>
        /// The speed of rotation
        /// </summary>
        public NumberRange RotationSpeed
        {
            get => rotationSpeed;
            set => rotationSpeed = value;
        }

        /// <summary>
        /// The speed of the particles
        /// </summary>
        public NumberRange Speed
        {
            get => speed;
            set => speed = value;
        }

        public NumberSequence Transparency
        {
            get => transparency;
            set => transparency = value;
        }

        /// <summary>
        /// The colour of the particles
        /// </summary>
        public new ColorSequence Color
        {
            get => color;
            set => color = value;
        }

        /// <summary>
        /// The size of the particles
        /// </summary>
        public NumberSequence Size
        {
            get => size;
            set => size = value;
        }

        /// <summary>
        /// The spawn rate of the particles
        /// </summary>
        public float SpawnRate
        {
            get => spawnRate;
            set => spawnRate = value;
        }

        /// <summary>
        /// Whether or not the ParticleEmitter is enabled
        /// </summary>
        public bool Enabled
        {
            get => enabled;
            set => enabled = value;
        }


        /// <summary>
        /// The particles (internally)
        /// </summary>
        List<Particle> particles = new List<Particle>();

        /// <summary>
        /// Emits particles
        /// </summary>
        /// <param name="particleCount">The amount of particles to emit</param>
        public void Emit(int particleCount = 16)
        {
            for (int i = 0; i < particleCount; i++)
            {
                Particle particle = new Particle(entity.Position, size, speed.Random, lifeTime.Random, rotation.Random, RotationSpeed.Random, emissionDirection);
                particles.Add(particle);
            }
        }

        public void AfterUpdate()
        {
            
        }

        public void BeforeUpdate()
        {
            
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            foreach (var particle in particles.ToArray())
            {
                var currentTime = particle.ElapsedTime / particle.LifeTime;

                var size = particle.Size.GetAtTime(currentTime).Value;

                Sprite sprite = new Sprite(Texture)
                {
                    Origin = new Vector2f(Texture.Size.X / 2, Texture.Size.Y / 2),
                    Rotation = particle.ElapsedRotation,
                    Scale = new Vector2f(size, size),
                    Position = particle.Position,
                };

                Color current = color.GetAtTime(currentTime).Value;

                sprite.Color = new Color(current.R, current.G, current.B, (byte)(255.0f * (1.0f - transparency.GetAtTime(currentTime).Value)));

                target.Draw(sprite);
            }
        }

        public static ParticleEmitter Spawn(Vector2f position)
        {
            Entity explosionEntity = new Entity();
            explosionEntity.Name = "ParticleEmitter";
            explosionEntity.Position = position;
            ParticleEmitter emitter = explosionEntity.AddComponent<ParticleEmitter>();
            return emitter;
        }

        public void Update()
        {
            foreach (var particle in particles.ToArray())
            {
                particle.Tick();

                if (!particle.IsAlive)
                {
                    particles.Remove(particle);
                }
            }
        }
    }
}

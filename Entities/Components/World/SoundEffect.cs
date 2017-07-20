using SFML.Audio;
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
    public class SoundEffect : Component
    {
        public override bool AllowsMultipleInstances => true;
        public override string Name => "SoundEffect";

        SoundBuffer buffer;

        /// <summary>
        /// The sound buffer
        /// </summary>
        public SoundBuffer Buffer
        {
            get => buffer;
            set => buffer = value;
        }

        /// <summary>
        /// The buffer id
        /// </summary>
        public string BufferId
        {
            set
            {
                buffer = SoundManager.Instance.Get(value);
            }

            get
            {
                return SoundManager.Instance.FindId(Buffer);
            }
        }

        Sound sound;

        /// <summary>
        /// The sound
        /// </summary>
        public Sound Sound
        {
            get
            {
                if (sound == null || sound.SoundBuffer != Buffer)
                    sound = new Sound(Buffer);

                return sound;
            }
        }

        float minDist = 100.0f, attenuation = 1.0f;

        public float MinDistance
        {
            get => minDist;
            set => minDist = value;
        }

        public float Attenuation
        {
            get => attenuation;
            set => attenuation = value;
        }

        /// <summary>
        /// Play the sound
        /// </summary>
        public void Play()
        {
            Sound.Position = new Vector3f(entity.Transform.Position.X, 0, entity.Transform.Position.Y);
            //Sound.RelativeToListener = true;

            //Listener.Position = new Vector2f(10, 10);

            //Sound.Attenuation = 0.1f;

            Sound.MinDistance = MinDistance;

            Sound.Play();
        }

        public override void OnComponentInit(Entity entity)
        {
            
        }
    }
}

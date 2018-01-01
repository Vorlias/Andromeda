using SFML.Audio;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Andromeda2D.Entities.Components.Internal;
using Andromeda2D.System;

namespace Andromeda2D.Entities.Components
{
    public class SoundEffect : Component, IDestroyedListener
    {
        string bufferId;

        /// <summary>
        /// The buffer id
        /// </summary>
        public string BufferId
        {
            set
            {
                bufferId = value;
            }

            get
            {
                return bufferId;
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
            //Sound.Position = new Vector3f(entity.Transform.Position.X, 0, entity.Transform.Position.Y);
            //Sound.RelativeToListener = true;

            //Listener.Position = new Vector2f(10, 10);

            //Sound.Attenuation = 0.1f;

            //Sound.MinDistance = MinDistance;

            if (BufferId != null)
            {
                var sound = SoundManager.Instance.GetSound(BufferId);

                sound.Play();
            }

        }

        protected override void OnComponentInit(Entity entity)
        {
            
        }

        public void OnDestroy()
        {

        }
    }
}

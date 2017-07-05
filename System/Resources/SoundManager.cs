using SFML.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VorliasEngine2D.System.Internal;

namespace VorliasEngine2D.System
{
    /// <summary>
    /// Class for managing sounds
    /// </summary>
    public class SoundManager : ResourceManager<SoundBuffer>
    {
        static SoundManager soundManager = new SoundManager();
        public static SoundManager Instance
        {
            get => soundManager;
        }

        /// <summary>
        /// Gets the sound with the specified id
        /// </summary>
        /// <param name="id">The id of the sound</param>
        /// <returns>The sound</returns>
        public Sound GetSound(string id)
        {
            return new Sound(Get(id));
        }

        public Sound GetSoundCopy(string id)
        {
            return new Sound(new SoundBuffer(Get(id)));
        }

        /// <summary>
        /// Loads the sound from a file to the specified id
        /// </summary>
        /// <param name="id">The id</param>
        /// <param name="file">The sound file</param>
        public void LoadToId(string id, string file)
        {
            SoundBuffer buffer = new SoundBuffer(file);
            Add(id, buffer);
        }
    }
}

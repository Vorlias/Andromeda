using SFML.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Andromeda2D.System.Internal;
using Andromeda2D.System.Utility;

namespace Andromeda2D.System
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

        HashSet<Sound> sounds = new HashSet<Sound>();

        /// <summary>
        /// Gets the sound with the specified id
        /// </summary>
        /// <param name="id">The id of the sound</param>
        /// <returns>The sound</returns>
        public Sound GetSound(string id)
        {
            var buffer = Get(id);

            Console.WriteLine("GetSound: " + id);

            if (sounds.Count >= 10)
                sounds.Where(sound => sound.Status == SoundStatus.Stopped).ForEach(sound => { sound.Dispose(); sounds.Remove(sound); });

            var soundCollection = sounds.Where(sound => sound.SoundBuffer == buffer && sound.Status != SoundStatus.Playing);
            if (soundCollection.Count() > 0)
            {
                Console.WriteLine("InArray: " + soundCollection.Count() + ", Retrieve " + sounds.Count);
                var next = soundCollection.First();
                return next;
            }
            else
            {
                Sound test = new Sound(buffer);
                sounds.Add(test);
                Console.WriteLine("InArray: " + soundCollection.Count() + ", Add " + sounds.Count);

                
                return test;
            }

            
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

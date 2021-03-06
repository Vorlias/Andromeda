﻿using SFML.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Andromeda.System.Internal;
using Andromeda.System.Utility;

namespace Andromeda.System
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
        Dictionary<string, Music> music = new Dictionary<string, Music>();
        Music playingMusic;

        bool _musicEnabled = true,
            _soundEnabled = true;

        int _musicVolume = 10,
            _soundVolume = 20;

        /// <summary>
        /// Whether or not music is enabled
        /// </summary>
        public bool MusicEnabled
        {
            get => _musicEnabled;
            set
            {
                if (playingMusic != null)
                    if (value)
                        playingMusic.Play();
                    else
                        playingMusic.Stop();

                _musicEnabled = value;
            }
        }

        /// <summary>
        /// The volume of music (from 0 - 100)
        /// </summary>
        public int MusicVolume
        {
            get
            {
                return _musicVolume;
            }
            set
            {
                _musicVolume = (value > 100 ? 100 : (value < 0 ? 0 : value));
                if (playingMusic != null)
                    playingMusic.Volume = _musicVolume;
            }
        }

        /// <summary>
        /// Whether or not sounds are enabled
        /// </summary>
        public bool SoundEnabled
        {
            get => _soundEnabled;
            set
            {
                _soundEnabled = value;
                foreach (var sound in sounds)
                {
                    sound.Volume = value ? SoundVolume : 0;
                }
            }
        }

        /// <summary>
        /// The volume of sound effects (from 0 - 100)
        /// </summary>
        public int SoundVolume
        {
            get
            {
                return _soundVolume;
            }
            set
            {
                _soundVolume = (value > 100 ? 100 : (value < 0 ? 0 : value));
                foreach (var sound in sounds)
                {
                    sound.Volume = _soundVolume;
                }
            }
        }

        public void StopBackgroundMusic()
        {
            if (playingMusic != null)
                playingMusic.Stop();
        }

        public void PlayBackgroundMusic(string file, bool looped = true)
        {
            Music music = GetMusic(file);
            if (playingMusic != music)
            {
                if (playingMusic != null && playingMusic.Status != SoundStatus.Stopped)
                    playingMusic.Stop();

                playingMusic = music;
                playingMusic.Volume = _musicVolume;
                playingMusic.Loop = looped;

                if (MusicEnabled)
                    playingMusic.Play();
            }
        }

        Music GetMusic(string file)
        {
            if (!music.ContainsKey(file))
            {
                music[file] = new Music(file);
            }

            return music[file];
        }

        /// <summary>
        /// Gets the sound with the specified id
        /// </summary>
        /// <param name="id">The id of the sound</param>
        /// <returns>The sound</returns>
        public Sound GetSound(string id)
        {
            var buffer = Get(id);

            if (sounds.Count >= 10)
                sounds.Where(sound => sound.Status == SoundStatus.Stopped).ForEach(sound => { sound.Dispose(); sounds.Remove(sound); });

            var soundCollection = sounds.Where(sound => sound.SoundBuffer == buffer && sound.Status != SoundStatus.Playing);
            if (soundCollection.Count() > 0)
            {
                var next = soundCollection.First();
                return next;
            }
            else
            {
                Sound test = new Sound(buffer);
                test.Volume = SoundEnabled ? _soundVolume : 0;
                sounds.Add(test);
                return test;
            }
        }

        /// <summary>
        /// Shorthand of SoundManager.Instance.GetSound(id).Play();
        /// </summary>
        /// <param name="id">The id of the sound</param>
        public static void Play(string id)
        {
            Instance.GetSound(id)?.Play();
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

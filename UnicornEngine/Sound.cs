using Microsoft.Xna.Framework.Audio;
using System;

namespace UnicornEngine
{
    public class Sound
    {
        SoundEffectInstance sound;
        public TimeSpan Duration { get; }

        public SoundEffectInstance soundEffectInstance { get { return sound; } }

        public Sound(string name)
        {
            SoundEffect soundeffect = EngineCore.content.Load<SoundEffect>("Sounds/" + name);
            Duration = soundeffect.Duration;
            sound = soundeffect.CreateInstance();
        }

        public void Play()
        {
            sound.Volume = EngineCore.soundVolume / 100;
            sound.Play();
        }

        public void Stop()
        {
            sound.Stop();
        }
    }
}
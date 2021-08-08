using Common.Utilities;
using Common.Utilities.Extensions;
using UnityEngine;

namespace Common.Audio
{
    [CreateAssetMenu(menuName = "Audio Events/Simple")]
    public class SimpleAudioEvent : AudioEvent
    {
        public AudioClip[] clips;
        public RangedFloat volume;
        public RangedFloat pitch;
        
        public override void Play(AudioSource source)
        {
            if (clips.Length == 0) return;

            source.clip = clips.RandomChoice();
            source.volume = volume.GetRandomValue();
            source.pitch = pitch.GetRandomValue();
            
            source.Play();
        }
    }
}

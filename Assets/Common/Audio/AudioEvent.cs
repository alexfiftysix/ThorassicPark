using UnityEngine;

namespace Common.Audio
{
    public abstract class AudioEvent : ScriptableObject
    {
        public abstract void Play(AudioSource source);
    }
}

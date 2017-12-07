using UnityEngine;
using System.Collections.Generic;
using System;
[Serializable]
public abstract class AudioEvent : ScriptableObject
{
	public abstract void Play(AudioSource source);
    public abstract void PlayOneShot(AudioSource source, float volume_received, bool use_volume_received);
}
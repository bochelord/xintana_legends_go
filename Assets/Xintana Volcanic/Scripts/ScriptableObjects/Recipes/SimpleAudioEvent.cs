using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using System;
[CreateAssetMenu(menuName="Audio Events Rad/Simple")]
[Serializable]
public class SimpleAudioEvent : AudioEvent
{
	public AudioClip[] clips;

	public RangedFloat volume;

	[MinMaxRange(0, 2)]
	public RangedFloat pitch;

	public override void Play(AudioSource source)
	{
		if (clips.Length == 0) return;

		source.clip = clips[Random.Range(0, clips.Length)];
		source.volume = Random.Range(volume.minValue, volume.maxValue);
		source.pitch = Random.Range(pitch.minValue, pitch.maxValue);
		source.Play();
	}

    public override void PlayOneShot(AudioSource source,float volume_received, bool use_volume_received){

        if (clips.Length == 0) return;

        //source.clip = clips[Random.Range(0, clips.Length)];
        if (!use_volume_received) { 
            source.volume = Random.Range(volume.minValue, volume.maxValue);
        } 
        else
        {
            source.volume = volume_received;
        }
        float old_pitch = source.pitch;
        source.pitch = Random.Range(pitch.minValue, pitch.maxValue);
        source.PlayOneShot(clips[Random.Range(0, clips.Length)]);

    }
        
        
        
}
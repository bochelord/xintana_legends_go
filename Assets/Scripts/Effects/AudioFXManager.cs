using UnityEngine;
using System.Collections;


public class AudioFXManager : MonoBehaviour
{


    public AudioClip xintanaHurt1;
    public AudioClip xintanaHurt2;
    public AudioClip xintanaDead;
    public AudioClip xintanaIdle;
    public AudioClip xintanaVoiceAttacking;
    public AudioClip xintanaVoiceAttackingAir;
    public AudioClip xintanaCrouch;
    public AudioClip xintanaJump;
    public AudioClip xintanaFootsteps1;
    public AudioClip xintanaFootsteps2;
    public AudioSource effectsplayer;
    
    
    static AudioFXManager instance;
    
    public static AudioFXManager Instance { get { return instance; } }

	// Use this for initialization
	void Start () {
        instance = this;
	}
	
	
    public void PlayHurt1()
    {
        if (xintanaHurt1 )
        {
            //effectPlayer.clip = explosionClip;
            //effectPlayer.Play();

            effectsplayer.PlayOneShot(xintanaHurt1, 0.5f);
        }
    }

    public void PlayHurt2()
    {
        if (xintanaHurt2)
        {
            //effectPlayer.clip = explosionClip;
            //effectPlayer.Play();

            effectsplayer.PlayOneShot(xintanaHurt2, 0.5f);
        }
    }


    public void PlayDead()
    {
        if (xintanaDead)
        {
            //float lastTimeScale = Time.timeScale;
            //Time.timeScale = 1;
            //effectsplayer.clip =xintanaDead;
            //effectsplayer.Play();
            //Time.timeScale = lastTimeScale;

            //if (Time.timeScale != 1)
            //{
            //    effectsplayer.pitch = 1;
            //    //effecteffecsplayer.pitch - Time.timeScale;
            //}

            effectsplayer.PlayOneShot(xintanaDead, 0.8f);

        }
    }


    public void PlayIdle()
    {
        if (xintanaIdle)
        {
            effectsplayer.PlayOneShot(xintanaIdle, 0.2f);
        }
    }


    public void PlayVoiceAttacking()
    {
        if (xintanaVoiceAttacking)
        {
            effectsplayer.PlayOneShot(xintanaVoiceAttacking, 0.3f);
        }
    }

    public void PlayVoiceAttackingAir()
    {
        if (xintanaVoiceAttackingAir)
        {
            effectsplayer.PlayOneShot(xintanaVoiceAttackingAir, 0.3f);
        }
    }


    public void PlayVoiceCrouch()
    {
        if (xintanaCrouch)
        {
            effectsplayer.PlayOneShot(xintanaCrouch, 0.3f);
        }
    }

    public void PlayVoiceJump()
    {
        if (xintanaJump)
        {
            effectsplayer.PlayOneShot(xintanaJump, 0.3f);
        }
    }


    public void PlayVoiceFootsteps1()
    {
        if (xintanaFootsteps1)
        {
            effectsplayer.PlayOneShot(xintanaFootsteps1, 0.1f);
        }
    }
    public void PlayVoiceFootsteps2()
    {
        if (xintanaFootsteps2)
        {
            effectsplayer.PlayOneShot(xintanaFootsteps2, 0.1f);
        }
    }
}


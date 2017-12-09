using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {


    [Header("AudioSources")]
    public AudioSource musicPlayer;
    public AudioSource fxPlayer;
    public AudioSource musicBossPlayer;

    [Header("Volumes")]
    [Range(0, 1)]
    public float generalVolume;

    [Header("")]
    public AudioClip musicLevel1;
    public AudioClip musicLevel2;
    public AudioClip musicLevel3;
    public AudioClip musicBossFight;

    [Header("FX SO")]
    public ScriptableObject xintana_attack_weapon_1;
    public ScriptableObject xintana_attack_weapon_2;
    public ScriptableObject xintana_death;
    public ScriptableObject xintana_hit;

    //cached variables
    private AudioClip tempclip;

    static AudioManager instance;

    public static AudioManager Instance { get { return instance; } }


    private bool audioEnabled;

    // Use this for initialization
    void Start () {
        instance = this;

        musicPlayer.volume = generalVolume;
        fxPlayer.volume = generalVolume;

        musicPlayer.clip = musicLevel1;

        if (Rad_SaveManager.profile.audioEnabled)
        {
            audioEnabled = true;
            musicPlayer.Play();

        } else
        {
            audioEnabled = false;
        }
    }

    public void PlayMusicLevel1()
    {
        if (musicLevel1 && audioEnabled)
        {
            musicPlayer.clip = musicLevel1;
            musicPlayer.Play();
        }
    }


    public void PlayMusicLevel2()
    {
        if (musicLevel2 && audioEnabled)
        {
            musicPlayer.clip = musicLevel2;
            musicPlayer.Play();
        }
    }

    public void PlayMusicLevel3()
    {
        if (musicLevel3 && audioEnabled)
        {
            musicPlayer.clip = musicLevel3;
            musicPlayer.Play();
        }
    }

    public void PlayBossMusic()
    {
        if (musicBossFight && audioEnabled)
        {
            musicPlayer.clip = musicBossFight;
            musicPlayer.Play();
        }
     }


    //public void PlayBossMusicAndPauseMain()
    //{
    //    if (musicBossFight && audioEnabled)
    //    {
    //        musicPlayer.Pause();
    //        musicBossPlayer.clip = musicBossFight;
    //        musicBossPlayer.Play();
    //    }
    //}

    //public void StopBossMusicAndResumeMain()
    //{
    //    if (audioEnabled)
    //    {
    //        musicBossPlayer.Stop();
    //        musicPlayer.UnPause();
    //    }
    //}

    public void Play_XintanaAttack_1()
    {
        if (xintana_attack_weapon_1 && audioEnabled)
        {
            ((SimpleAudioEvent)xintana_attack_weapon_1).PlayOneShot(fxPlayer, generalVolume, true);
        }
    }


    public void Play_XintanaAttack_2()
    {
        if (xintana_attack_weapon_2 && audioEnabled)
        {
            ((SimpleAudioEvent)xintana_attack_weapon_2).PlayOneShot(fxPlayer, generalVolume, true);
        }
    }

    public void Play_XintanaDeath()
    {
        if (xintana_death && audioEnabled)
        {
            ((SimpleAudioEvent)xintana_death).PlayOneShot(fxPlayer, generalVolume, true);
        }
    }

    public void Play_XintanaHit()
    {
        if (xintana_hit && audioEnabled)
        {
            ((SimpleAudioEvent)xintana_hit).PlayOneShot(fxPlayer, generalVolume, true);
        }
    }


}

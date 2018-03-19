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

    [Header("MusicArray")]
    public AudioClip[] musicArray;

    public AudioClip musicBossFight;
    public AudioClip musicMainMenu;
    public AudioClip musicRoulette;
    [Header("FX SO")]
    public ScriptableObject xintana_attack_weapon_1;
    public ScriptableObject xintana_attack_weapon_2;
    public ScriptableObject xintana_death;
    public ScriptableObject xintana_hit;

    [Header("FX")]
    public ScriptableObject YouWin;
    public ScriptableObject YouLose;
    public ScriptableObject Boss;
    public ScriptableObject NextStage;
    public ScriptableObject addScore;
    public ScriptableObject collectCoin;
    public ScriptableObject collectGem;
    //cached variables
    private AudioClip tempclip;

    static AudioManager instance;

    public static AudioManager Instance { get { return instance; } }


    public bool audioEnabled;

    // Use this for initialization
    void Awake()
    {
        if (!Instance)
        {
            instance = this;
        }
        else
        {
            DestroyObject(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        musicPlayer.volume = generalVolume;
        fxPlayer.volume = generalVolume;
        if (Rad_SaveManager.profile.audioEnabled)
        {
            audioEnabled = true;

        }
        else
        {
            audioEnabled = false;
        }
    }

    public void ResumeMusic()
    {
        if (audioEnabled)
        {
            musicPlayer.UnPause();
        }
    }
    public void PlayMusicLevel1()
    {
        musicPlayer.clip = musicArray[0];

        if (audioEnabled)
        {
            musicPlayer.Play();

        }
    }


    public void MuteAudio()
    {
        musicPlayer.mute = true;
        fxPlayer.mute = true;
        musicBossPlayer.mute = true;

    }

    public bool isAudioMuted()
    {
        return musicPlayer.mute;
    }


    public void UnMuteAudio()
    {
        musicPlayer.mute = false;
        fxPlayer.mute = false;
        musicBossPlayer.mute = false;

    }



    public void PlayMusicLevel(int level)
    {
        if (level > musicArray.Length) { level -= musicArray.Length; }
        if (musicArray[level-1] && audioEnabled)
        {
            musicPlayer.clip = musicArray[level - 1];
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

    public void PlayMainMenuMusic()
    {
        if (musicBossFight && audioEnabled)
        {
            musicPlayer.clip = musicMainMenu;
            musicPlayer.Play();
        }
    }
    public void PlayRouletteMusic()
    {
        if (musicBossFight && audioEnabled)
        {
            musicPlayer.clip = musicRoulette;
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

    public void Play_YouWin() {
        if (YouWin && audioEnabled) {
            ((SimpleAudioEvent)YouWin).PlayOneShot(fxPlayer, generalVolume, true);
        }
    }

    public void Play_YouLose() {
        if (YouLose && audioEnabled) {
            ((SimpleAudioEvent)YouLose).PlayOneShot(fxPlayer, generalVolume, true);
        }
    }

    public void Play_Boss() {
        if (Boss && audioEnabled) {
            ((SimpleAudioEvent)Boss).PlayOneShot(fxPlayer, generalVolume, true);
        }
    }

    public void Play_NextStage() {
        if (NextStage && audioEnabled) {
            ((SimpleAudioEvent)NextStage).PlayOneShot(fxPlayer, generalVolume, true);
        }
    }
    public void Play_AddScore()
    {
        if (addScore && audioEnabled)
        {
            fxPlayer.loop = true;
            ((SimpleAudioEvent)addScore).Play(fxPlayer);
        }
    }
    public void Play_CoinCollect()
    {
        if (collectCoin && audioEnabled)
        {
            ((SimpleAudioEvent)collectCoin).Play(fxPlayer);
        }
    }
    public void Stop_AddScore()
    {
        fxPlayer.loop = false;
    }

    public void Play_GemCollect()
    {
        if (collectGem && audioEnabled)
        {
            ((SimpleAudioEvent)collectGem).Play(fxPlayer);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using PaperPlaneTools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {


    public Image audioOn;
    public Image audioOff;

    void Start()
    {
        //yield return new WaitForSeconds(0.2f);
        if (AudioManager.Instance.isAudioMuted())
        {
            audioOff.gameObject.SetActive(true);
            audioOn.gameObject.SetActive(false);
        } 
        else
        {
            audioOn.gameObject.SetActive(true);
            audioOff.gameObject.SetActive(false);
        }
    }

    public void Button_Credits()
    {
        SceneManager.LoadScene("credits");
    }

    public void Button_back()
    {
        SceneManager.LoadScene("XintanaLegendsGo_Menu");
    }

    public void Toggle_Sound()
    {

        if (AudioManager.Instance.isAudioMuted())
        {
            AudioManager.Instance.UnMuteAudio();
            audioOn.gameObject.SetActive(true);
            audioOff.gameObject.SetActive(false);
        }
        else
        {
            AudioManager.Instance.MuteAudio();
            audioOff.gameObject.SetActive(true);
            audioOn.gameObject.SetActive(false);
        }

        
    }





    public void Button_rate()
    {
        RateBox.Instance.Show();
    }


}

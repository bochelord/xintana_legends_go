using System.Collections;
using System.Collections.Generic;
using PaperPlaneTools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using I2.Loc;

public class SettingsManager : MonoBehaviour {


    public Image audioOn;
    public Image audioOff;
    public Dropdown dropdown;

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

        switch (LocalizationManager.CurrentLanguage)
        {
            case "English":
                dropdown.value = 0;
                break;
            case "Spanish":
                dropdown.value = 1;
                break;
            case "Italian":
                dropdown.value = 2;
                break;
            case "Portuguese (Brazil)":
                dropdown.value = 3;
                break;
            case "Arabic":
                dropdown.value = 4;
                break;
            default:
                break;
        }
    }

    public void Button_Credits()
    {
        //SceneManager.LoadScene("credits");
        AsyncLoadLevel.LoadScene("credits");
    }

    public void Button_back()
    {
        //SceneManager.LoadScene("XintanaLegendsGo_Menu");
        AsyncLoadLevel.LoadScene("XintanaLegendsGo_Menu");
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

    public void ChangeLanguageTo()
    {

        switch (dropdown.value)
        {
            case 0:
                if (LocalizationManager.HasLanguage("English")) { LocalizationManager.CurrentLanguage = "English"; }
                break;
            case 1:
                if (LocalizationManager.HasLanguage("Spanish")) { LocalizationManager.CurrentLanguage = "Spanish"; }
                break;
            case 2:
                if (LocalizationManager.HasLanguage("Italian")) { LocalizationManager.CurrentLanguage = "Italian"; }
                break;
            case 3:
                if (LocalizationManager.HasLanguage("Portuguese (Brazil)")) { LocalizationManager.CurrentLanguage = "Portuguese (Brazil)"; }
                break;
            case 4:
                if (LocalizationManager.HasLanguage("Arabic")) { LocalizationManager.CurrentLanguage = "Arabic"; }
                break;
            default:
                break;
        }
    }



    public void Button_rate()
    {
        RateBox.Instance.Show();
    }


}

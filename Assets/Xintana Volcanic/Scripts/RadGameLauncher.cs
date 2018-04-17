using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RadGameLauncher : MonoBehaviour {

	// Use this for initialization
	void Awake () {

#if UNITY_ANDROID || UNITY_IOS
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //we set the language to the device language by default
        switch (Application.systemLanguage)
        {
            case SystemLanguage.Arabic:
                I2.Loc.LocalizationManager.CurrentLanguage = "Arabic";
                
                break;
            case SystemLanguage.Italian:
                I2.Loc.LocalizationManager.CurrentLanguage = "Italian";
                break;
            case SystemLanguage.Spanish:
                I2.Loc.LocalizationManager.CurrentLanguage = "Spanish";
                break;
            case SystemLanguage.Portuguese:
                I2.Loc.LocalizationManager.CurrentLanguage = "Portuguese (Brazil)";
                break;
            case SystemLanguage.English:
                I2.Loc.LocalizationManager.CurrentLanguage = "English";
                break;

        }

        print("Language set to " + I2.Loc.LocalizationManager.CurrentLanguage.ToString());

#endif

        if (ES2.Exists(Rad_SaveManager.xintanaProfileFilename))
        {
            Rad_SaveManager.LoadData();
            SceneManager.LoadScene("XintanaLegendsGo_Menu");
        }
        else
        {
            Rad_SaveManager.SaveData();
            SceneManager.LoadScene("radical_intro");
        }
	}
	
	
}

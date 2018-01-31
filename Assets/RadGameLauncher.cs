using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RadGameLauncher : MonoBehaviour {

	// Use this for initialization
	void Awake () {

#if UNITY_ANDROID || UNITY_IOS
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
#endif

        if (ES2.Exists(Rad_SaveManager.xintanaProfileFilename))
        {
            Rad_SaveManager.LoadData();
            SceneManager.LoadScene("LoadingScreen");
        }
        else
        {
            Rad_SaveManager.SaveData();
            SceneManager.LoadScene("radical_intro");
        }
	}
	
	
}

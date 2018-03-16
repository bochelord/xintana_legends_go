using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour {

	
    public void Button_Credits()
    {
        SceneManager.LoadScene("credits");
    }

    public void Button_back()
    {
        SceneManager.LoadScene("XintanaLegendsGo_Menu");
    }

}

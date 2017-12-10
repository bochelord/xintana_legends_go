using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class RadLauncher : MonoBehaviour {

    public Image Fader;

	// Use this for initialization
	IEnumerator Start () {

        
        Rad_SaveManager.LoadData();
        Rad_SaveManager.SaveData(); //We save in case is a new generated profile...
        Fader.DOFade(0f, 0.7f);


        yield return new WaitForSeconds(10.5f);
        Fader.DOFade(1f, 0.7f);
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene("LoadingScreen");
	}
	
	
}

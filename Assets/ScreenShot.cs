using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour {

    public RectTransform screenshotRect;
    public Texture2D tex;
    public GameObject combinationImage;






    public void TakeDeathScreenshot()
    {
        //on delay 0.25f you get to see the hit FX on xintana
        //on delay 0.45f you get to see the attacking animation of enemy and xinti dead
        //An interesting range would be : 0.20f  - 0.65f

        //float delay = 0.45f;
        float delay = Random.Range(0.20f, 0.65f);
        StartCoroutine(DeathScreenshotDelayed(delay)); 
        
                                                       
    }


    IEnumerator DeathScreenshotDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(DeathScreenshot());
    }


    IEnumerator  DeathScreenshot()
    {
        combinationImage.SetActive(false);
        yield return new WaitForEndOfFrame();
        tex = new Texture2D(Screen.width, Screen.height/3);
        tex.ReadPixels(new Rect(0, Screen.height/2.5f, Screen.width, Screen.height/3), 0, 0);
        tex.Apply();
        combinationImage.SetActive(true);
    }

}

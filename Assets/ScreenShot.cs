using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour {

    public RectTransform screenshotRect;
    public Texture2D tex;
    public GameObject combinationImage;
    public void TakeDeathScreenshot()
    {
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

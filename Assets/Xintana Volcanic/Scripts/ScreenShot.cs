using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenShot : MonoBehaviour {

    //public RectTransform screenshotRect;
    public List<Texture2D> texList = new List<Texture2D>();
    public GameObject combinationImage;
    public int numMaxScreenshotsonMemory = 5;
    




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
        //combinationImage.SetActive(false);
        yield return new WaitForEndOfFrame();
        Texture2D tex = new Texture2D(Screen.width, Screen.height/3);
        tex.ReadPixels(new Rect(0, Screen.height/2.4f, Screen.width, Screen.height/3), 0, 0);
        tex.Apply();
        texList.Add(tex);
        if (texList.Count > numMaxScreenshotsonMemory)
        {
            RemoveOldestScreenshot();
        }
        //combinationImage.SetActive(true);
    }

    public Sprite GetScreenshot()
    {
        texList.Shuffle();

        return Sprite.Create(texList[0], new Rect(0.0f, 0.0f,texList[0].width, texList[0].height), new Vector2(0.5f, 0.5f), 100.0f);
    }

    private void RemoveOldestScreenshot()
    {
        //texList.Remove(texList[texList.Count-1]);//arrays starts at 0...
        texList.RemoveAt(0);
       
    }
}

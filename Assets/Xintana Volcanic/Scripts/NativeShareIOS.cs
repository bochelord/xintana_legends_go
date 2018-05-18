using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NativeShareIOS : MonoBehaviour {

	public GameObject CanvasShareObj;
	private bool isProcessing = false;
	private bool isFocus = false;
	private Rad_GuiManager guiManager;
	private bool didShare = false;
	private XintanaProfile _xintanaProfile;

	private Texture2D _texture;
	


	private void Start(){
		guiManager = FindObjectOfType<Rad_GuiManager>();
        _xintanaProfile = Rad_SaveManager.profile;
	}


	public void ShareBtnPress()
    {
        if (!didShare)
        {
            didShare = true;
            CanvasShareObj.SetActive(true);
            guiManager.FillSharePanel();
            _xintanaProfile.sharedScoreTimes++;
            AnalyticsManager.Instance.Shared_Screen(_xintanaProfile.sharedScoreTimes);
            iosAchievementsManager.Instance.IncrementShareScoreAchievement();
        }

        if (!isProcessing)
        {
            CanvasShareObj.SetActive(true);
            StartCoroutine(ShareScreenshot());
        }
    }

	IEnumerator ShareScreenshot()
    {
        isProcessing = true;
        //count = _xintanaProfile.sharedScoreTimes;

        yield return new WaitForEndOfFrame();


        //ScreenCapture.CaptureScreenshot("screenshot.png", 2);

        // create a texture to pass to encoding
        _texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        // put buffer into texture
        _texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        _texture.Apply(false, false);

        

        //string destination = Path.Combine(Application.persistentDataPath, "xintanalegendsgo_" + count +".jpg");
        string destination = System.IO.Path.Combine(Application.persistentDataPath, "xintanalegendsgo.jpg");
        JPGEncoder encoder = new JPGEncoder(_texture, 75, destination);

        //encoder is threaded; wait for it to finish
        while (!encoder.isDone)
            yield return null;

        Debug.Log("Screendump saved at : " + destination + " | Size: " + encoder.GetBytes().Length + " bytes");


        //count++;

      

        yield return new WaitForSecondsRealtime(0.3f);

		#if UNITY_IOS
			new NativeShare().AddFile(destination).SetSubject(I2.Loc.LocalizationManager.GetTranslation("share_title")).SetText(I2.Loc.LocalizationManager.GetTranslation("share_text")).Share();
		#endif

		yield return new WaitForSecondsRealtime(1);
        

        yield return new WaitUntil(() => isFocus);
        CanvasShareObj.SetActive(false);
        isProcessing = false;
	}


	private void OnApplicationFocus(bool focus)
    {
        isFocus = focus;
    }
}

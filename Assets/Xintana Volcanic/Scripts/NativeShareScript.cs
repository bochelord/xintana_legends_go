﻿using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class NativeShareScript : MonoBehaviour
{
    public GameObject CanvasShareObj;
    private bool isProcessing = false;
    private bool isFocus = false;
    private Rad_GuiManager guiManager;
    private bool didShare = false;
    private XintanaProfile _xintanaProfile;

    private Texture2D _texture;
    private int count = 0;

    private void Start()
    {
        guiManager = FindObjectOfType<Rad_GuiManager>();
        _xintanaProfile = Rad_SaveManager.profile;
        count = _xintanaProfile.sharedScoreTimes;
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
            //AchievementsManager.Instance.IncrementShareScoreAchievement();
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
        count = _xintanaProfile.sharedScoreTimes;

        yield return new WaitForEndOfFrame();


        //ScreenCapture.CaptureScreenshot("screenshot.png", 2);

        // create a texture to pass to encoding
        _texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        // put buffer into texture
        _texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        _texture.Apply(false, false);

        

        //string destination = Path.Combine(Application.persistentDataPath, "xintanalegendsgo_" + count +".jpg");
        string destination = Path.Combine(Application.persistentDataPath, "xintanalegendsgo.jpg");
        JPGEncoder encoder = new JPGEncoder(_texture, 75, destination);

        //encoder is threaded; wait for it to finish
        while (!encoder.isDone)
            yield return null;

        Debug.Log("Screendump saved at : " + destination + " | Size: " + encoder.GetBytes().Length + " bytes");


        //count++;

      

        yield return new WaitForSecondsRealtime(0.3f);

        if (!Application.isEditor)
        {
#if UNITY_ANDROID
            if (GetSDKLevel() < 26) {
                AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
                AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent");
                intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"));
                AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri");
                AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", "file://" + destination);
                intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"),
                    uriObject);
                intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"),
                    I2.Loc.LocalizationManager.GetTranslation("share_text"));
                intentObject.Call<AndroidJavaObject>("setType", "image/jpeg");
                AndroidJavaClass unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
                AndroidJavaObject chooser = intentClass.CallStatic<AndroidJavaObject>("createChooser",
                    intentObject, I2.Loc.LocalizationManager.GetTranslation("share_title"));
                currentActivity.Call("startActivity", chooser);
            }
            else {
                ShareAndroid(I2.Loc.LocalizationManager.GetTranslation("share_text"), I2.Loc.LocalizationManager.GetTranslation("share_score"), destination, null, "image/jpeg", true, I2.Loc.LocalizationManager.GetTranslation("share_title"));
            }
#endif

            yield return new WaitForSecondsRealtime(1);
        }

        yield return new WaitUntil(() => isFocus);
        CanvasShareObj.SetActive(false);
        isProcessing = false;
    }

    private void OnApplicationFocus(bool focus)
    {
        isFocus = focus;
    }

    public int GetSDKLevel() {
        //var clazz = AndroidJNI.FindClass("android.os.Build$VERSION");
        //var fieldID = AndroidJNI.GetStaticFieldID(clazz, "SDK_INT", "I");
        //var sdkLevel = AndroidJNI.GetStaticIntField(clazz, fieldID);
        //Debug.Log(sdkLevel);
        //return sdkLevel;
        using (var version = new AndroidJavaClass("android.os.Build$VERSION"))
        {
            return version.GetStatic<int>("SDK_INT");
        }
    }

#if UNITY_ANDROID
    public static void ShareAndroid(string body, string subject, string url, string[] filePaths, string mimeType, bool chooser, string chooserText) {
        using (AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        using (AndroidJavaObject currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity"))
        using (AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent"))
        using (AndroidJavaObject intentObject = new AndroidJavaObject("android.content.Intent")) {
            using (intentObject.Call<AndroidJavaObject>("setAction", intentClass.GetStatic<string>("ACTION_SEND"))) { }
            using (intentObject.Call<AndroidJavaObject>("setType", mimeType)) { }
            using (intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_SUBJECT"), subject)) { }
            using (intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_TEXT"), body)) { }

            if (!string.IsNullOrEmpty(url)) {
                // attach url
                using (AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri"))
                using (AndroidJavaObject uriObject = uriClass.CallStatic<AndroidJavaObject>("parse", url))
                using (intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObject)) { }
            }
            else if (filePaths != null) {
                // attach extra files (pictures, pdf, etc.)
                using (AndroidJavaClass fileProviderClass = new AndroidJavaClass("android.support.v4.content.FileProvider"))
                using (AndroidJavaObject unityContext = currentActivity.Call<AndroidJavaObject>("getApplicationContext"))
                using (AndroidJavaClass uriClass = new AndroidJavaClass("android.net.Uri"))
                using (AndroidJavaObject uris = new AndroidJavaObject("java.util.ArrayList")) {
                    string packageName = unityContext.Call<string>("getPackageName");
                    string authority = packageName + ".provider";

                    AndroidJavaObject fileObj = new AndroidJavaObject("java.io.File", filePaths[0]);
                    AndroidJavaObject uriObj = fileProviderClass.CallStatic<AndroidJavaObject>("getUriForFile", unityContext, authority, fileObj);

                    int FLAG_GRANT_READ_URI_PERMISSION = intentObject.GetStatic<int>("FLAG_GRANT_READ_URI_PERMISSION");
                    intentObject.Call<AndroidJavaObject>("addFlags", FLAG_GRANT_READ_URI_PERMISSION);

                    using (intentObject.Call<AndroidJavaObject>("putExtra", intentClass.GetStatic<string>("EXTRA_STREAM"), uriObj)) { }
                }
            }

            // finally start application
            if (chooser) {
                AndroidJavaObject jChooser = intentClass.CallStatic<AndroidJavaObject>("createChooser", intentObject, chooserText);
                currentActivity.Call("startActivity", jChooser);
            }
            else {
                currentActivity.Call("startActivity", intentObject);
            }
        }
    }
#endif
}
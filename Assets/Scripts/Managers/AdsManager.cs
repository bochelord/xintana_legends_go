using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour {

    [HideInInspector]
    public bool adViewed = false;
    private Rad_GuiManager _guiManager;
    private LevelManager _levelManager;

    private System.DateTime timeStamp;
    [HideInInspector]
    public int AdsViewed;
    void Awake()
    {
        timeStamp = System.DateTime.Now;
        if(timeStamp.Day > Rad_SaveManager.profile.timeStamp.Day)
        {
            AdsViewed = 0;
        }
        _guiManager = FindObjectOfType<Rad_GuiManager>();
        _levelManager = FindObjectOfType<LevelManager>();
    }
    public void ShowAd()
    {
        if (Advertisement.IsReady() && AdsViewed <=4)
        {
            Advertisement.Show("rewardedVideo",new ShowOptions() { resultCallback = HandleAdResult });
        }
    }

    private void HandleAdResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                AdsViewed++;
                adViewed = true;
                _levelManager.ContinueGame();
                _guiManager.HideAdPanel();
                break;
            case ShowResult.Skipped:
                _guiManager.HideAdPanelAndStartGameOverPanel();
                break;
            case ShowResult.Failed:
                _guiManager.HideAdPanelAndStartGameOverPanel();
                break;

        }
    }
}

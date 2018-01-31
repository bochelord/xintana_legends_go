using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdsManager : MonoBehaviour {

    [HideInInspector]
    public bool adViewed = false;
    private Rad_GuiManager _guiManager;
    private LevelManager _levelManager;
    private AnalyticsManager _analyticsManager;

    private System.DateTime timeStamp;
    [HideInInspector]
    public int AdsViewed;
    void Awake()
    {
        _guiManager = FindObjectOfType<Rad_GuiManager>();
        _levelManager = FindObjectOfType<LevelManager>();
        _analyticsManager = FindObjectOfType<AnalyticsManager>();
    }

    void Start()
    {
        timeStamp = System.DateTime.Now;
        if (timeStamp.Day > Rad_SaveManager.profile.timeStamp.Day)
        {
            AdsViewed = 0;
            Rad_SaveManager.profile.timeStamp = timeStamp;
        }
    }
    public void ShowAdForExtraLife()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideo",new ShowOptions() { resultCallback = HandleResultExtraLife });
        }
    }

    private void HandleResultExtraLife(ShowResult result)
    {
        AnalyticsManager.Instance.AdsViewed_Event(result);
        switch (result)
        {
            case ShowResult.Finished:
                AdsViewed++;
                adViewed = true;
                _levelManager.ContinueGame();
                _guiManager.HideAdPanel();
                _analyticsManager.ResurrectionAd_Event(true);
                break;
            case ShowResult.Skipped:
                _guiManager.HideAdPanelAndStartGameOverPanel();
                break;
            case ShowResult.Failed:
                _guiManager.HideAdPanelAndStartGameOverPanel();
                break;
        }
    }
    private void HandleResultDoubleScore(ShowResult result)
    {
        AnalyticsManager.Instance.AdsViewed_Event(result);
        switch (result)
        {
            case ShowResult.Finished:
                AdsViewed++;
                adViewed = true;
                _guiManager.HideDoubleScorePanel();
                _guiManager.DoubleScore();
                _analyticsManager.DoubleScoreAd_Event(true);
                break;
            case ShowResult.Skipped:
                _guiManager.HideDoubleScorePanel();
                _analyticsManager.DoubleScoreAd_Event(false);
                break;
            case ShowResult.Failed:
                _guiManager.HideDoubleScorePanel();
                break;
        }
    }
    private void HandlResultNoReward(ShowResult result)
    {
        AnalyticsManager.Instance.AdsViewed_Event(result);
        switch (result)
        {
            case ShowResult.Finished:
                Rad_SaveManager.profile.adsSkipped = 0;
                SceneManager.LoadScene("LoadingScreen");
                break;
            case ShowResult.Failed:
                SceneManager.LoadScene("LoadingScreen");
                break;
        }
    }
    public void ShowAdNoReward()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandlResultNoReward });
        }
    }
    public void ShowAdForDoubleScore()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleResultDoubleScore });
        }
    }
}

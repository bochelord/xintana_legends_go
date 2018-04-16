using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class AdsManager : MonoBehaviour {

    [HideInInspector]
    public bool adViewed = false;
    private Rad_GuiManager _guiManager;
    private LevelManager _levelManager;
    private AnalyticsManager _analyticsManager;
    private MainMenuManager _menuManager;
    private ChestRoulette _chestManager;

    private System.DateTime timeStamp;
    [HideInInspector]
    public int AdsViewed;

    private bool _freeShellAdViewed = false;
    void Awake()
    {
        _guiManager = FindObjectOfType<Rad_GuiManager>();
        _levelManager = FindObjectOfType<LevelManager>();
        _analyticsManager = FindObjectOfType<AnalyticsManager>();
        _menuManager = FindObjectOfType<MainMenuManager>();
        _chestManager = FindObjectOfType<ChestRoulette>();
    }

    void Start()
    {
        _freeShellAdViewed = Rad_SaveManager.profile.freeTokenDay;
        timeStamp = System.DateTime.Now;
        if (timeStamp.Day > Rad_SaveManager.profile.timeStamp.Day)
        {
            Rad_SaveManager.profile.freeTokenDay = false; // 1 free token per day
            AdsViewed = 0;
            Rad_SaveManager.profile.timeStamp = timeStamp;
        }
    }
    public void ShowAdForExtraLife()
    {
        if (Advertisement.IsReady() && !Rad_SaveManager.profile.extraLife)
        {
            Advertisement.Show("rewardedVideo",new ShowOptions() { resultCallback = HandleResultExtraLife });
        }
        else
        {
            _levelManager.ContinueGame();
        }
    }

    private void HandleResultExtraLife(ShowResult result)
    {
        AnalyticsManager.Instance.AdsViewed_Event(result);
        switch (result)
        {
            case ShowResult.Finished:
                AdsViewedIncrement();
                adViewed = true;
                _levelManager.ContinueGame();
                _guiManager.HideAdPanel();
                _analyticsManager.ResurrectionAd_Event(true);
                AchievementsManager.Instance.ReviveAdAchievement();
                break;
            case ShowResult.Skipped:
                _guiManager.HideAdPanelAndStartGameOverPanel();
                break;
            case ShowResult.Failed:
                _guiManager.HideAdPanelAndStartGameOverPanel();
                break;
        }
    }
    private void HandleResultDoublePrize(ShowResult result)
    {
        AnalyticsManager.Instance.AdsViewed_Event(result);
        Debug.Log(result);
        switch (result)
        {
            case ShowResult.Finished:
                AdsViewedIncrement();
                adViewed = true;
                _chestManager.closePrizePanel.enabled = true;
                _chestManager.HideDoublePrizePanel();
                _chestManager.DoublePrize();
                _analyticsManager.DoublePrizeAd_Event(true);
                break;
            case ShowResult.Skipped:
                _chestManager.HideDoublePrizePanel();
                _analyticsManager.DoublePrizeAd_Event(false);
                break;
            case ShowResult.Failed:
                _chestManager.HideDoublePrizePanel();
                break;
        }
    }
    private void HandlResultNoReward(ShowResult result)
    {
        AnalyticsManager.Instance.AdsViewed_Event(result);
        switch (result)
        {
            case ShowResult.Finished:
                AdsViewedIncrement();
                Rad_SaveManager.profile.adsSkipped = 0;
                StartCoroutine(FunctionLibrary.CallWithDelay(_levelManager.GameOverPanel, 2f));
                break;
            case ShowResult.Failed:
                StartCoroutine(FunctionLibrary.CallWithDelay(_levelManager.GameOverPanel, 2f));
                break;

            default:
                StartCoroutine(FunctionLibrary.CallWithDelay(_levelManager.GameOverPanel, 2f));
                break;
        }
    }

    private void HandlResultFreeShell(ShowResult result)
    {
        AnalyticsManager.Instance.AdsViewed_Event(result);
        switch (result)
        {
            case ShowResult.Finished:
                AdsViewedIncrement();
                Rad_SaveManager.profile.freeTokenDay = true;
                _freeShellAdViewed = true;
                Rad_SaveManager.profile.shells++;
                _menuManager.HideFreeShellPanel();
                _chestManager.StartRoulettePanel();
                AchievementsManager.Instance.TokenAdAchievement();
                break;

        }
    }

    private void AdsViewedIncrement()
    {
        AdsViewed++;
        Rad_SaveManager.profile.adsViewed++;
        AchievementsManager.Instance.IncrementAdsWatchedAchievements();
    }
    public void ShowAdNoReward()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("video", new ShowOptions() { resultCallback = HandlResultNoReward });
        }
    }
    public void ShowAdForDoublePrize()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandleResultDoublePrize });
        }
    }

    public void ShowAddForFreeShell()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideo", new ShowOptions() { resultCallback = HandlResultFreeShell });
        }
    }
    public bool GetFreeShellAdViewed()
    {
        return _freeShellAdViewed;
    }
}

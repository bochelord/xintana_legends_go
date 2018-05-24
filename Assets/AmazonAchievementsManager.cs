using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

/// <summary>
/// https://developer.amazon.com/docs/gamecircle/unity-setup.html
/// </summary>

public class AmazonAchievementsManager : MonoBehaviour
{
    public static AmazonAchievementsManager Instance;

    private XintanaProfile profile;
    private bool isServiceReady = false;
    void Awake()
    {
        Instance = this;

        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
        //AMAZON INIT
        AGSClient.ServiceReadyEvent += serviceReadyHandler;
        AGSClient.ServiceNotReadyEvent += serviceNotReadyHandler;
        bool usesLeaderboards = true;
        bool usesAchievements = true;
        bool usesWhispersync = false;

        AGSClient.Init(usesLeaderboards, usesAchievements, usesWhispersync);
        //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
    }

    void Start()
    {
        isServiceReady = AGSClient.IsServiceReady();
        profile = Rad_SaveManager.profile;
    }

    /// <summary>
    /// Writes in the Console if Amazon service is not ready.
    /// </summary>
    /// <param name="error"></param>
    private void serviceNotReadyHandler(string error)
    {
        Debug.Log("Service is not ready");
    }

    /// <summary>
    /// Writes in the console if Amazon service is ready.
    /// </summary>
    private void serviceReadyHandler()
    {
        Debug.Log("Service is ready");
    }

    /// <summary>
    /// 
    /// </summary>
    public void LoginAndShowAchievements()
    {
        if (isServiceReady)
        {
            Debug.Log("Login Sucess");
            ShowAchievements();
        }
        else
        {
            Debug.Log("Login failed");
        }
    }

    /// <summary>
    /// AMAZON method to handle Achievement feedback
    /// </summary>
    /// <param name="achievementId"></param>
    private void updateAchievementSucceeded(string achievementId)
    {
        Debug.Log("ACHIVEMENT " + achievementId + " UPDATE HAS SUCCEED");
        return;
    }

    /// <summary>
    /// AMAZON method to handle Achievement feedback
    /// </summary>
    /// <param name="achievementId"></param>
    /// <param name="error"></param>
    private void updateAchievementFailed(string achievementId, string error)
    {
        Debug.Log("ACHIVEMENT " + achievementId + " UPDATE HAS FAILED WITH ERROR: " + error);
        return;
    }

    public void ShowAchievements()
    {
        AGSAchievementsClient.ShowAchievementsOverlay();
        return;
    }

    public void IncrementKillsAchievements()
    {
        //if (AGSClient.IsServiceReady())
        if (AGSPlayerClient.IsSignedIn())
        {
            AGSAchievementsClient.UpdateAchievementSucceededEvent += updateAchievementSucceeded;
            AGSAchievementsClient.UpdateAchievementFailedEvent += updateAchievementFailed;

            int _tempSum = profile.playerKills;
            string _tempString = "";

            switch (_tempSum)
            {
                case 5:
                    _tempString = "achievement_defeat_5_enemies";
                    break;
                case 25:
                    _tempString = "achievement_defeat_25_enemies";
                    break;
                case 50:
                    _tempString = "achievement_defeat_50_enemies";
                    break;
                case 100:
                    _tempString = "achievement_defeat_100_enemies";
                    break;
                case 200:
                    _tempString = "achievement_defeat_200_enemies";
                    break;
                case 500:
                    _tempString = "achievement_defeat_500_enemies";
                    break;
                case 1000:
                    _tempString = "achievement_defeat_1000_enemies";
                    break;
            }
            AGSAchievementsClient.UpdateAchievementProgress(_tempString, 100f);
        }
    }


    public void IncrementGemsComboAchievements(int value)
    {
        if (AGSPlayerClient.IsSignedIn())
        {
            AGSAchievementsClient.UpdateAchievementSucceededEvent += updateAchievementSucceeded;
            AGSAchievementsClient.UpdateAchievementFailedEvent += updateAchievementFailed;

            string _tempString = "";

            if (value >= 3)
            {
                AGSAchievementsClient.UpdateAchievementProgress("achievement_3_gems_combo", 100f);
                _tempString = "achievement_3_gems_combo";
            }

            if (value >= 5)
            {
                AGSAchievementsClient.UpdateAchievementProgress("achievement_5_gems_combo", 100f);
                _tempString = "achievement_5_gems_combo";
            }
            if (value >= 7)
            {
                AGSAchievementsClient.UpdateAchievementProgress("achievement_7_gems_combo", 100f);
                _tempString = "achievement_7_gems_combo";
            }
            if (value >= 9)
            {
                AGSAchievementsClient.UpdateAchievementProgress("achievement_9_gems_combo", 100f);
                _tempString = "achievement_9_gems_combo";
            }
            AGSAchievementsClient.UpdateAchievementProgress(_tempString, 100f);
        }
    }

    public void IncrementLevelAchievements(int value)
    {
        if (AGSPlayerClient.IsSignedIn())
        {
            AGSAchievementsClient.UpdateAchievementSucceededEvent += updateAchievementSucceeded;
            AGSAchievementsClient.UpdateAchievementFailedEvent += updateAchievementFailed;

            int _tempSum = value;
            string _tempString = "";
            switch (_tempSum)
            {
                case 5:
                    _tempString = "achievement_reach_lvl_5";
                    break;
                case 10:
                    _tempString = "achievement_reach_lvl_10";
                    break;
                case 20:
                    _tempString = "achievement_reach_lvl_20";
                    break;
                case 30:
                    _tempString = "achievement_reach_lvl_30";
                    break;
                case 40:
                    _tempString = "achievement_reach_lvl_40";
                    break;
                case 50:
                    _tempString = "achievement_reach_lvl_50";
                    break;
            }
            AGSAchievementsClient.UpdateAchievementProgress(_tempString, 100f);
            profile.playerKills = _tempSum;
        }
    }

    public void IncrementScoreAchievements(float value)
    {
        if (AGSPlayerClient.IsSignedIn())
        {
            AGSAchievementsClient.UpdateAchievementSucceededEvent += updateAchievementSucceeded;
            AGSAchievementsClient.UpdateAchievementFailedEvent += updateAchievementFailed;

            string _tempString = "";

            if (value >= 10000)
            {
                _tempString = "achievement_score_10_000_points";
            }
            if (value >= 250000)
            {
                _tempString = "achievement_score_25_000_points";
            }
            if (value >= 50000)
            {
                _tempString = "achievement_score_50_000_points";
            }
            if (value >= 75000)
            {
                _tempString = "achievement_score_75_000_points";
            }
            if (value >= 100000)
            {
                _tempString = "achievement_score_100_000_points";
            }

            AGSAchievementsClient.UpdateAchievementProgress(_tempString, 100f);
        }
    }

    public void IncrementShareScoreAchievement()
    {
        int _tempValue = profile.sharedScoreTimes;

        if (AGSPlayerClient.IsSignedIn() && _tempValue < 25)
        {
            AGSAchievementsClient.UpdateAchievementSucceededEvent += updateAchievementSucceeded;
            AGSAchievementsClient.UpdateAchievementFailedEvent += updateAchievementFailed;
            AGSAchievementsClient.UpdateAchievementProgress("achievement_share_your_score", 100f);
        }
    }

    public void IncrementCoinsEarnedAchievements(int value)
    {
        if (AGSPlayerClient.IsSignedIn())
        {
            AGSAchievementsClient.UpdateAchievementSucceededEvent += updateAchievementSucceeded;
            AGSAchievementsClient.UpdateAchievementFailedEvent += updateAchievementFailed;

            int _tempSum = profile.coinsEarned + value;
            string _tempString = "";

            if(_tempSum >= 100)
            {
                _tempString = "achievement_earn_100_coins";
            }
            if (_tempSum >= 500)
            {
                _tempString = "achievement_earn_500_coins";
            }
            if (_tempSum >= 1000)
            {
                _tempString = "achievement_earn_1000_coins";
            }
            if (_tempSum >= 5000)
            {
                _tempString = "achievement_earn_5000_coins";
            }
            if (_tempSum >= 10000)
            {
                _tempString = "achievement_earn_10_000_coins";
            }
            AGSAchievementsClient.UpdateAchievementProgress(_tempString, 100f);
        }
    }

    public void IncrementGemsEarnedAchievements(int value)
    {
        if (AGSPlayerClient.IsSignedIn())
        {
            AGSAchievementsClient.UpdateAchievementSucceededEvent += updateAchievementSucceeded;
            AGSAchievementsClient.UpdateAchievementFailedEvent += updateAchievementFailed;

            int _tempSum = profile.gemsCollected + value;
            string _tempString = "";

            if (_tempSum > 1)
            {
                _tempString = "achievement_collect_1_gem";
            }
            if (_tempSum >= 5)
            {
                _tempString = "achievement_collect_5_gems";
            }
            if (_tempSum >= 10)
            {
                _tempString = "achievement_collect_10_gems";
            }
            if (_tempSum >= 20)
            {
                _tempString = "achievement_collect_20_gems";
            }
            if (_tempSum >= 50)
            {
                _tempString = "achievement_collect_50_gems";
            }

            AGSAchievementsClient.UpdateAchievementProgress(_tempString, 100f);
        }
    }

    public void ReviveAdAchievement()
    {
        if (AGSPlayerClient.IsSignedIn())
        {
            AGSAchievementsClient.UpdateAchievementSucceededEvent += updateAchievementSucceeded;
            AGSAchievementsClient.UpdateAchievementFailedEvent += updateAchievementFailed;
            AGSAchievementsClient.UpdateAchievementProgress("achievement_watch_a_revive_ad", 100f);
        }
    }

    public void TokenAdAchievement()
    {
        if (AGSPlayerClient.IsSignedIn())
        {
            AGSAchievementsClient.UpdateAchievementSucceededEvent += updateAchievementSucceeded;
            AGSAchievementsClient.UpdateAchievementFailedEvent += updateAchievementFailed;
            AGSAchievementsClient.UpdateAchievementProgress("achievement_watch_a_token_ad", 100f);
        }
    }

    public void IncrementAdsWatchedAchievements()
    {
        if (AGSPlayerClient.IsSignedIn())
        {
            AGSAchievementsClient.UpdateAchievementSucceededEvent += updateAchievementSucceeded;
            AGSAchievementsClient.UpdateAchievementFailedEvent += updateAchievementFailed;

            int _tempValue = profile.adsViewed;
            string _tempString = "";
            switch (_tempValue)
            {
                case 7:
                    _tempString = "achievement_watch_7_ads";
                    break;
                case 10:
                    _tempString = "achievement_watch_10_ads";
                    break;
                case 25:
                    _tempString = "achievement_watch_25_ads";
                    break;
                case 50:
                    _tempString = "achievement_watch_50_ads";
                    break;
            }
            AGSAchievementsClient.UpdateAchievementProgress(_tempString, 100f);
        }
    }

    public void BuyDoubleScoreAchievement()
    {
        if (AGSPlayerClient.IsSignedIn())
        {
            AGSAchievementsClient.UpdateAchievementSucceededEvent += updateAchievementSucceeded;
            AGSAchievementsClient.UpdateAchievementFailedEvent += updateAchievementFailed;
            AGSAchievementsClient.UpdateAchievementProgress("achievement_buy_a_x2", 100f);
        }
    }

    public void BuyReviveAchievement()
    {
        if (AGSPlayerClient.IsSignedIn())
        {
            AGSAchievementsClient.UpdateAchievementSucceededEvent += updateAchievementSucceeded;
            AGSAchievementsClient.UpdateAchievementFailedEvent += updateAchievementFailed;
            AGSAchievementsClient.UpdateAchievementProgress("achievement_buy_a_revive", 100f);
        }
    }

    public void BuyAllSwordsAchievement()
    {
        if(SIS.DBManager.GetPurchase("si_yellowsword") > 0 && SIS.DBManager.GetPurchase("si_bluesword") > 0 && SIS.DBManager.GetPurchase("si_blacksword") > 0 && SIS.DBManager.GetPurchase("si_greensword") > 0)
        {
            if (AGSPlayerClient.IsSignedIn())
            {
                AGSAchievementsClient.UpdateAchievementSucceededEvent += updateAchievementSucceeded;
                AGSAchievementsClient.UpdateAchievementFailedEvent += updateAchievementFailed;
                AGSAchievementsClient.UpdateAchievementProgress("achievement_buy_all_swords", 100f);
            }
        }
    }
    //TODO - THIS IS THE AMAZOM LEADERBOAR ID: leaderboard_high_score
}



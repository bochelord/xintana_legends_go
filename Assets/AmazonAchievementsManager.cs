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
        //if (PlayGamesPlatform.Instance.localUser.authenticated)
        //{
        //    PlayGamesPlatform.Instance.ShowAchievementsUI();
        //}
        //else
        //{
        //    Debug.Log("Cannot showAcievements, not logged in");
        //}
        return;
    }

    public void IncrementKillsAchievements()
    {
        if (AGSClient.IsServiceReady())
        {
            AGSAchievementsClient.UpdateAchievementSucceededEvent += updateAchievementSucceeded;
            AGSAchievementsClient.UpdateAchievementFailedEvent += updateAchievementFailed;

            int _tempSum = profile.playerKills;

            switch (_tempSum)
            {
                case 5:
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_defeat_5_enemies", 100f);
                    break;

                case 25:
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_defeat_25_enemies", 100f);
                    break;

                case 50:
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_defeat_50_enemies", 100f);
                    break;

                case 100:
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_defeat_100_enemies", 100f);
                    break;

                case 200:
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_defeat_200_enemies", 100f);
                    break;

                case 500:
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_defeat_500_enemies", 100f);
                    break;

                case 1000:
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_defeat_1000_enemies", 100f);
                    break;

                default:
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_defeat_5_enemies", 100f);
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_defeat_25_enemies", 100f);
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_defeat_50_enemies", 100f);
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_defeat_100_enemies", 100f);
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_defeat_200_enemies", 100f);
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_defeat_500_enemies", 100f);
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_defeat_1000_enemies", 100f);
                    break;
            }
        }
    }

    public void IncrementGemsComboAchievements(int value)
    {
        if (AGSClient.IsServiceReady())
        {
            AGSAchievementsClient.UpdateAchievementSucceededEvent += updateAchievementSucceeded;
            AGSAchievementsClient.UpdateAchievementFailedEvent += updateAchievementFailed;
            if (value >= 3)
            {
                AGSAchievementsClient.UpdateAchievementProgress("achievement_3_gems_combo", 100f);
            }

            if (value >= 5)
            {
                AGSAchievementsClient.UpdateAchievementProgress("achievement_5_gems_combo", 100f);
            }
            if (value >= 7)
            {
                AGSAchievementsClient.UpdateAchievementProgress("achievement_7_gems_combo", 100f);
            }
            if (value >= 9)
            {
                AGSAchievementsClient.UpdateAchievementProgress("achievement_9_gems_combo", 100f);
            }
        }
    }

    public void IncrementLevelAchievements(int value)
    {
        if (AGSClient.IsServiceReady())
        {
            AGSAchievementsClient.UpdateAchievementSucceededEvent += updateAchievementSucceeded;
            AGSAchievementsClient.UpdateAchievementFailedEvent += updateAchievementFailed;

            int _tempSum = value;

            switch (_tempSum)
            {
                case 5:
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_reach_lvl_5", 100f);
                    break;

                case 10:
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_reach_lvl_10", 100f);
                    break;

                case 20:
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_reach_lvl_20", 100f);
                    break;

                case 30:
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_reach_lvl_30", 100f);
                    break;

                case 40:
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_reach_lvl_40", 100f);
                    break;

                case 50:
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_reach_lvl_50", 100f);
                    break;

                default:
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_reach_lvl_5", 100f);
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_reach_lvl_10", 100f);
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_reach_lvl_20", 100f);
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_reach_lvl_30", 100f);
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_reach_lvl_40", 100f);
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_reach_lvl_50", 100f);
                    break;
            }

            profile.playerKills = _tempSum;
        }
    }

    public void IncrementScoreAchievements(float value)
    {
        if (AGSClient.IsServiceReady())
        {
            AGSAchievementsClient.UpdateAchievementSucceededEvent += updateAchievementSucceeded;
            AGSAchievementsClient.UpdateAchievementFailedEvent += updateAchievementFailed;

            if (value >= 10000)
            {
                AGSAchievementsClient.UpdateAchievementProgress("achievement_score_10_000_points", 100f);
            }
            if (value >= 250000)
            {
                AGSAchievementsClient.UpdateAchievementProgress("achievement_score_25_000_points", 100f);
            }
            if (value >= 50000)
            {
                AGSAchievementsClient.UpdateAchievementProgress("achievement_score_50_000_points", 100f);
            }
            if (value >= 75000)
            {
                AGSAchievementsClient.UpdateAchievementProgress("achievement_score_75_000_points", 100f);
            }
            if (value >= 100000)
            {
                AGSAchievementsClient.UpdateAchievementProgress("achievement_score_100_000_points", 100f);
            }
        }
    }

    public void IncrementShareScoreAchievement()
    {
        int _tempValue = profile.sharedScoreTimes;

        if (AGSClient.IsServiceReady() && _tempValue < 25)
        {
            AGSAchievementsClient.UpdateAchievementSucceededEvent += updateAchievementSucceeded;
            AGSAchievementsClient.UpdateAchievementFailedEvent += updateAchievementFailed;
            AGSAchievementsClient.UpdateAchievementProgress("achievement_share_your_score", 100f);
        }
    }

    public void IncrementCoinsEarnedAchievements(int value)
    {
        if (AGSClient.IsServiceReady())
        {
            AGSAchievementsClient.UpdateAchievementSucceededEvent += updateAchievementSucceeded;
            AGSAchievementsClient.UpdateAchievementFailedEvent += updateAchievementFailed;

            int _tempSum = profile.coinsEarned + value;

            if(_tempSum >= 100)
            {
                AGSAchievementsClient.UpdateAchievementProgress("achievement_earn_100_coins", 100f);
            }
            if (_tempSum >= 500)
            {
                AGSAchievementsClient.UpdateAchievementProgress("achievement_earn_500_coins", 100f);
            }
            if (_tempSum >= 1000)
            {
                AGSAchievementsClient.UpdateAchievementProgress("achievement_earn_1000_coins", 100f);
            }
            if (_tempSum >= 5000)
            {
                AGSAchievementsClient.UpdateAchievementProgress("achievement_earn_5000_coins", 100f);
            }
            if (_tempSum >= 10000)
            {
                AGSAchievementsClient.UpdateAchievementProgress("achievement_earn_10_000_coins", 100f);
            }
        }
    }

    public void IncrementGemsEarnedAchievements(int value)
    {
        if (AGSClient.IsServiceReady())
        {
            AGSAchievementsClient.UpdateAchievementSucceededEvent += updateAchievementSucceeded;
            AGSAchievementsClient.UpdateAchievementFailedEvent += updateAchievementFailed;

            int _tempSum = profile.gemsCollected + value;

            if (_tempSum > 1)
            {
                AGSAchievementsClient.UpdateAchievementProgress("achievement_collect_1_gem", 100f);
            }
            if (_tempSum >= 5)
            {
                AGSAchievementsClient.UpdateAchievementProgress("achievement_collect_5_gems", 100f);
            }
            if (_tempSum >= 10)
            {
                AGSAchievementsClient.UpdateAchievementProgress("achievement_collect_10_gems", 100f);
            }
            if (_tempSum >= 20)
            {
                AGSAchievementsClient.UpdateAchievementProgress("achievement_collect_20_gems", 100f);
            }
            if (_tempSum >= 50)
            {
                AGSAchievementsClient.UpdateAchievementProgress("achievement_collect_50_gems", 100f);
            }
        }
    }

    public void ReviveAdAchievement()
    {
        if (AGSClient.IsServiceReady())
        {
            AGSAchievementsClient.UpdateAchievementSucceededEvent += updateAchievementSucceeded;
            AGSAchievementsClient.UpdateAchievementFailedEvent += updateAchievementFailed;
            AGSAchievementsClient.UpdateAchievementProgress("achievement_watch_a_revive_ad", 100f);
        }
    }

    public void TokenAdAchievement()
    {
        if (AGSClient.IsServiceReady())
        {
            AGSAchievementsClient.UpdateAchievementSucceededEvent += updateAchievementSucceeded;
            AGSAchievementsClient.UpdateAchievementFailedEvent += updateAchievementFailed;
            AGSAchievementsClient.UpdateAchievementProgress("achievement_watch_a_token_ad", 100f);
        }
    }

    public void IncrementAdsWatchedAchievements()
    {
        if (AGSClient.IsServiceReady())
        {
            AGSAchievementsClient.UpdateAchievementSucceededEvent += updateAchievementSucceeded;
            AGSAchievementsClient.UpdateAchievementFailedEvent += updateAchievementFailed;

            int _tempValue = profile.adsViewed;

            switch (_tempValue)
            {
                case 7:
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_watch_7_ads", 100f);
                    break;
                case 10:
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_watch_10_ads", 100f);
                    break;
                case 25:
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_watch_25_ads", 100f);
                    break;
                case 50:
                        AGSAchievementsClient.UpdateAchievementProgress("achievement_watch_50_ads", 100f);
                    break;
                default:
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_watch_7_ads", 100f);
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_watch_10_ads", 100f);
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_watch_25_ads", 100f);
                    AGSAchievementsClient.UpdateAchievementProgress("achievement_watch_50_ads", 100f);
                    break;
            }
        }
    }

    public void BuyDoubleScoreAchievement()
    {
        if (AGSClient.IsServiceReady())
        {
            AGSAchievementsClient.UpdateAchievementSucceededEvent += updateAchievementSucceeded;
            AGSAchievementsClient.UpdateAchievementFailedEvent += updateAchievementFailed;
            AGSAchievementsClient.UpdateAchievementProgress("achievement_buy_a_x2", 100f);
        }
    }

    public void BuyReviveAchievement()
    {
        if (AGSClient.IsServiceReady())
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
            if (AGSClient.IsServiceReady())
            {
                AGSAchievementsClient.UpdateAchievementSucceededEvent += updateAchievementSucceeded;
                AGSAchievementsClient.UpdateAchievementFailedEvent += updateAchievementFailed;
                AGSAchievementsClient.UpdateAchievementProgress("achievement_buy_all_swords", 100f);
            }
        }
    }
    //TODO - THIS IS THE AMAZOM LEADERBOAR ID: leaderboard_high_score
}



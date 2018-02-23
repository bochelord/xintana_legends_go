using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class AchievementsManager : MonoBehaviour
{
    public static AchievementsManager Instance;

    private XintanaProfile profile;

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        profile = Rad_SaveManager.profile;
    }

    public void LoginAndShowAchievements()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.Log("Login Sucess");
                ShowAchievements();
            }
            else
            {
                Debug.Log("Login failed");
            }
        });
    }

    public void ShowAchievements()
    {
        if (PlayGamesPlatform.Instance.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ShowAchievementsUI();
        }
        else
        {
            Debug.Log("annot showAcievements, not logged in");
        }
    }

    public void IncrementKillsAchievements()
    {
        if (Social.localUser.authenticated)
        {
            int _tempSum = profile.playerKills;

            switch (_tempSum)
            {
                case 5:
                    PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_defeat_5_enemies, 100.0f, (bool success) =>
                    {
                        Debug.Log("Defeat 5 enemies: " +
                                    success);
                    });
                    break;

                case 25:
                    PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_defeat_25_enemies, 100.0f, (bool success) =>
                    {
                        Debug.Log("Defeat 25 enemies: " +
                                    success);
                    });
                    break;

                case 50:
                    PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_defeat_50_enemies, 100.0f, (bool success) =>
                    {
                        Debug.Log("Defeat 50 enemies: " +
                                    success);
                    });
                    break;

                case 100:
                    PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_defeat_100_enemies, 100.0f, (bool success) =>
                    {
                        Debug.Log("Defeat 100 enemies: " + success);
                    });
                    break;

                case 200:
                    PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_defeat_200_enemies, 100.0f, (bool success) =>
                    {
                        Debug.Log("Defeat 200 enemies: " + success);
                    });
                    break;

                case 500:
                    PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_defeat_500_enemies, 100.0f, (bool success) =>
                    {
                        Debug.Log("Defeat 500 enemies: " + success);
                    });
                    break;

                case 1000:
                    PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_defeat_1000_enemies, 100.0f, (bool success) =>
                    {
                        Debug.Log("Defeat 1000 enemies: " + success);
                    });
                    break;

                default:
                    PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_defeat_5_enemies, 1, (bool success) =>
                    {
                        Debug.Log("Defeat 5 Enemies Increment: " + success);
                    });
                    PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_defeat_25_enemies, 1, (bool success) =>
                    {
                        Debug.Log("Defeat 25 Enemies Increment: " + success);
                    });
                    PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_defeat_50_enemies, 1, (bool success) =>
                    {
                        Debug.Log("Defeat 50 Enemies Increment: " + success);
                    });
                    PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_defeat_100_enemies, 1, (bool success) =>
                    {
                        Debug.Log("Defeat 100 Enemies Increment: " + success);
                    });
                    PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_defeat_200_enemies, 1, (bool success) =>
                    {
                        Debug.Log("Defeat 200 Enemies Increment: " + success);
                    });
                    PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_defeat_500_enemies, 1, (bool success) =>
                    {
                        Debug.Log("Defeat 500 Enemies Increment: " + success);
                    });
                    PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_defeat_1000_enemies, 1, (bool success) =>
                    {
                        Debug.Log("Defeat 1000 Enemies Increment: " + success);
                    });
                    break;
            }

        }
    }
    public void IncrementGemsComboAchievements(int value)
    {
        if (Social.localUser.authenticated)
        {
            if (value >= 3)
            {
                PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_3_gems_combo, 100.0f, (bool success) =>
                {
                    Debug.Log("3 gems combo: " + success);
                });
            }
            if (value >= 5)
            {
                PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_5_gems_combo, 100.0f, (bool success) =>
                {
                    Debug.Log("5 gems combo: " + success);
                });
            }
            if (value >= 7)
            {
                PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_7_gems_combo, 100.0f, (bool success) =>
                {
                    Debug.Log("7 gems combo: " + success);
                });
            }
            if (value >= 9)
            {
                PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_9_gems_combo, 100.0f, (bool success) =>
                {
                    Debug.Log("9 gems combo: " + success);
                });
            }
        }
    }
    public void IncrementLevelAchievements(int value)
    {
        if (Social.localUser.authenticated)
        {
            int _tempSum = value;

            switch (_tempSum)
            {
                case 5:
                    PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_reach_lvl_5, 100.0f, (bool success) =>
                    {
                        Debug.Log("Reach lvl 5: " +
                                    success);
                    });
                    break;

                case 10:
                    PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_reach_lvl_10, 100.0f, (bool success) =>
                    {
                        Debug.Log("Reach lvl 10: " +
                                    success);
                    });
                    break;

                case 20:
                    PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_reach_lvl_20, 100.0f, (bool success) =>
                    {
                        Debug.Log("Reach lvl 20: " +
                                    success);
                    });
                    break;

                case 30:
                    PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_reach_lvl_30, 100.0f, (bool success) =>
                    {
                        Debug.Log("Reach lvl 30: " +
                                    success);
                    });
                    break;

                case 40:
                    PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_reach_lvl_40, 100.0f, (bool success) =>
                    {
                        Debug.Log("Reach lvl 40: " +
                                    success);
                    });
                    break;

                case 50:
                    PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_reach_lvl_50, 100.0f, (bool success) =>
                    {
                        Debug.Log("Reach lvl 50: " +
                                    success);
                    });
                    break;

                default:
                    PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_reach_lvl_5, value, (bool success) =>
                    {
                        Debug.Log("Reach lvl 5 Increment: " + success);
                    });
                    PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_reach_lvl_10, value, (bool success) =>
                    {
                        Debug.Log("Reach lvl 10 Increment: " + success);
                    });
                    PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_reach_lvl_20, value, (bool success) =>
                    {
                        Debug.Log("Reach lvl 20 Increment: " + success);
                    });
                    PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_reach_lvl_30, value, (bool success) =>
                    {
                        Debug.Log("Reach lvl 30 Increment: " + success);
                    });
                    PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_reach_lvl_40, value, (bool success) =>
                    {
                        Debug.Log("Reach lvl 40 Increment: " + success);
                    });
                    PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_reach_lvl_50, value, (bool success) =>
                    {
                        Debug.Log("Reach lvl 50 Increment: " + success);
                    });
                    break;
            }

            profile.playerKills = _tempSum;
        }
    }
    public void IncrementScoreAchievements(float value)
    {
        if (Social.localUser.authenticated)
        {
            if (value >= 10000)
            {
                PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_score_10_000_points, 100.0f, (bool success) =>
                {
                    Debug.Log("10k score: " + success);
                });
            }
            if (value >= 250000)
            {
                PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_score_25_000_points, 100.0f, (bool success) =>
                {
                    Debug.Log("25k score: " + success);
                });
            }
            if (value >= 50000)
            {
                PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_score_50_000_points, 100.0f, (bool success) =>
                {
                    Debug.Log("50k score: " + success);
                });
            }
            if (value >= 75000)
            {
                PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_score_75_000_points, 100.0f, (bool success) =>
                {
                    Debug.Log("75k score: " + success);
                });
            }
            if (value >= 100000)
            {
                PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_score_100_000_points, 100.0f, (bool success) =>
                {
                    Debug.Log("100k score: " + success);
                });
            }
        }
    }
    public void IncrementShareScoreAchievement()
    {
        int _tempValue = profile.sharedScoreTimes;
        if (_tempValue < 25)
        {
            PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_share_your_score, 1, (bool success) =>
            {
                Debug.Log("Share Score increment: " + success);
            });
        }
        else
        {
            PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_share_your_score, 100.0f, (bool success) =>
            {
                Debug.Log("Share score: " + success);
            });
        }
    }
    public void IncrementCoinsEarnedAchievements(int value)
    {
        if (Social.localUser.authenticated)
        {
            int _tempSum = profile.coinsEarned + value;

            if(_tempSum >= 100)
            {
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_earn_100_coins, value, (bool success) =>
                {
                    Debug.Log("100 coins Earned: " + success);
                });
            }
            if (_tempSum >= 500)
            {
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_earn_500_coins, value, (bool success) =>
                {
                    Debug.Log("500 coins Earned: " + success);
                });
            }
            if (_tempSum >= 1000)
            {
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_earn_1000_coins, value, (bool success) =>
                {
                    Debug.Log("1000 coins Earned: " + success);
                });
            }
            if (_tempSum >= 5000)
            {
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_earn_5000_coins, value, (bool success) =>
                {
                    Debug.Log("5000 coins Earned: " + success);
                });
            }
            if (_tempSum >= 10000)
            {
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_earn_10_000_coins, value, (bool success) =>
                {
                    Debug.Log("10000 coins Earned: " + success);
                });
            }
        }
    }
    public void IncrementGemsEarnedAchievements(int value)
    {
        if (Social.localUser.authenticated)
        {
            int _tempSum = profile.gemsCollected + value;

            if (_tempSum >= 5)
            {
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_collect_5_gems, value, (bool success) =>
                {
                    Debug.Log("5 gems Earned: " + success);
                });
            }
            if (_tempSum >= 10)
            {
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_collect_10_gems, value, (bool success) =>
                {
                    Debug.Log("10 gems Earned: " + success);
                });
            }
            if (_tempSum >= 20)
            {
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_collect_20_gems, value, (bool success) =>
                {
                    Debug.Log("20 gems Earned: " + success);
                });
            }
            if (_tempSum >= 50)
            {
                PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_collect_50_gems, value, (bool success) =>
                {
                    Debug.Log("50 gems Earned: " + success);
                });
            }
        }
    }
    public void ReviveAdAchievement()
    {
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_watch_a_revive_ad, 100f, (bool success) =>
            {
                Debug.Log("Revive ad watched: " + success);
            });

        }
    }
    public void TokenAdAchievement()
    {
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_watch_a_token_ad, 100f, (bool success) =>
            {
                Debug.Log("Token ad watched: " + success);
            });

        }
    }
    public void IncrementAdsWatchedAchievements()
    {
        if (Social.localUser.authenticated)
        {
            int _tempValue = profile.adsViewed;
            switch (_tempValue)
            {
                case 7:
                    PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_watch_7_ads, 100.0f, (bool success) =>
                    {
                        Debug.Log("7 ads viewed: " + success);
                    });
                    break;
                case 10:
                    PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_watch_10_ads, 100.0f, (bool success) =>
                    {
                        Debug.Log("10 ads viewed: " + success);
                    });
                    break;
                case 25:
                    PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_watch_25_ads, 100.0f, (bool success) =>
                    {
                        Debug.Log("25 ads viewed: " + success);
                    });
                    break;
                case 50:
                    PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_watch_50_ads, 100.0f, (bool success) =>
                    {
                        Debug.Log("50 ads viewed: " + success);
                    });
                    break;
                default:
                    PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_watch_7_ads, 1, (bool success) =>
                    {
                        Debug.Log("7 ads viewed increment: " + success);
                    });
                    PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_watch_10_ads, 1, (bool success) =>
                    {
                        Debug.Log("10 ads viewed increment: " + success);
                    });
                    PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_watch_25_ads, 1, (bool success) =>
                    {
                        Debug.Log("25 ads viewed increment: " + success);
                    });
                    PlayGamesPlatform.Instance.IncrementAchievement(GPGSIds.achievement_watch_50_ads, 1, (bool success) =>
                    {
                        Debug.Log("50 ads viewed increment: " + success);
                    });
                    break;
            }
        }
    }
    public void BuyDoubleScoreAchievement()
    {
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_buy_a_x2, 100f, (bool success) =>
            {
                Debug.Log("Double score bought " + success);
            });
        }
    }
    public void BuyReviveAchievement()
    {
        if (Social.localUser.authenticated)
        {
            PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_buy_a_revive, 100f, (bool success) =>
            {
                Debug.Log("Revive bought " + success);
            });
        }
    }
    public void BuyAllSwordsAchievement()
    {
        if(SIS.DBManager.GetPurchase("si_yellowsword") > 0 && SIS.DBManager.GetPurchase("si_bluesword") > 0 && SIS.DBManager.GetPurchase("si_blacksword") > 0 && SIS.DBManager.GetPurchase("si_greensword") > 0)
        {
            if (Social.localUser.authenticated)
            {
                PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_buy_all_swords, 100f, (bool success) =>
                {
                    Debug.Log("All swords bought " + success);
                });
            }

        }
    }
}



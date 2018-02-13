using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Analytics;
using UnityEngine.Advertisements;

public class AnalyticsManager : MonoBehaviour
{

    static AnalyticsManager instance;

    public static AnalyticsManager Instance { get { return instance; } }

    void Awake()
    {
        instance = this;
    }
    
    /// <summary>
    /// Save the player score, number of fights and world reached.
    /// </summary>
    /// <param name="playerScore"></param>
    /// <param name="numFights"></param>
    /// <param name="worldReached"></param>
    public void GameOver_Event(int playerScore, int numFights, int worldReached)
    {
        Analytics.CustomEvent("Game Over", new Dictionary<string, object>
        {
            {"Player Score", playerScore },
            {"Fights", numFights },
            {"World Reached", worldReached }
        });
    }

    /// <summary>
    /// Which Device model has the player.
    /// </summary>
    public void DeviceModel_Event()
    {
        Analytics.CustomEvent("DeviceModel", new Dictionary<string, object>
        {
            {"device",SystemInfo.deviceModel }
        });
    }

    /// <summary>
    /// Which item has the player bought
    /// </summary>
    /// <param name="itemName"></param>
    public void Item_Bought_Event(string itemName)
    {
        Analytics.CustomEvent("Item_Bought", new Dictionary<string, object>
        {
            {"item bought", itemName }
        });
    }

    /// <summary>
    /// Has the player shared the score screen?
    /// </summary>
    public void Shared_Screen(int amount)
    {
        Analytics.CustomEvent("Shared_Screen", new Dictionary<string, object>
        {
            {"shared screen", "Yes" },
            {"times shared", amount }
        });
    }

    /// <summary>
    /// Which item won the player
    /// </summary>
    /// <param name="itemName"></param>
    /// <param name="amount"></param>
    public void ChestPrice_Event(string itemName, int amount)
    {
        Analytics.CustomEvent("Chest_Price", new Dictionary<string, object>
        {
            {itemName, amount }
        });
    }
    //===================================================================================================================
    //ADS
    //===================================================================================================================
    /// <summary>
    /// How many Ads has watched the player in 1 game run.
    /// </summary>
    /// <param name="value"></param>
    public void AdsViewed_Event(ShowResult value)
    {
        Analytics.CustomEvent("Ads_Viewed", new Dictionary<string, object>
        {
            {"ads viewed",value }

        });
    }

    /// <summary>
    /// The total number of ads a player has watched since app installation to uninstall.
    /// </summary>
    /// <param name="value"></param>
    public void TotalWatchedAds_Event(int value)
    {
        Analytics.CustomEvent("Total_Watched_Ads", new Dictionary<String, object>
        {
            { "total watched ads", value }
        });
    }

    /// <summary>
    /// Has the player watched the Resurrection Ads event? Will let us know if the Player is interested in this ad.
    /// </summary>
    /// <param name="watched"></param>
    public void ResurrectionAd_Event(bool watched)
    {
        Analytics.CustomEvent("Resurrection_Ad_Event", new Dictionary<String, object>
        {
            { "resurrection ad watched", watched }
        });
    }

    /// <summary>
    /// Has the player watched the Double Score Ad? Will let us know if the player is interested in this ad.
    /// </summary>
    /// <param name="watched"></param>
    public void DoubleScoreAd_Event(bool watched)
    {
        Analytics.CustomEvent("DoubleScore_Ad_Event", new Dictionary<String, object>
        {
            { "double score ad watched", watched }
        });
    }

    public void DoublePrizeAd_Event(bool watched)
    {
        Analytics.CustomEvent("DoublePrice_Ad_Event", new Dictionary<String, object>
        {
            { "double price ad watched", watched }
        });
    }
    //===================================================================================================================
    //TIME
    //===================================================================================================================

    /// <summary>
    /// TimePlayed will save the time the player has spent playing 1 game, and by 1 game means launching and then later closing the game app.
    /// </summary>
    public void TimePlayed()
    {
        Analytics.CustomEvent("Time_Played", new Dictionary<string, object>
        {
            {"time played",Time.time }
        });
    }

    /// <summary>
    /// The total amount of time the player has spent playing the game since day1 until uninstall.
    /// </summary>
    /// <param name="totalTime"></param>
    public void TotalPlayedTime(DateTime totalTime)
    {
        Analytics.CustomEvent("Total_Played", new Dictionary<string, object>
        {
            {"total played time", totalTime }
        });
    }



    
}

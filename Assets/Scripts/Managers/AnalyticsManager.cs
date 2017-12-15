using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Analytics;

public class AnalyticsManager : MonoBehaviour
{

    static AnalyticsManager instance;

    public static AnalyticsManager Instance { get { return instance; } }

    void Awake()
    {
        instance = this;
    }




    public void GameOver_Event(int playerScore, int numFights, int worldReached)
    {
        Analytics.CustomEvent("Game Over", new Dictionary<string, object>
        {
            {"Player Score", playerScore },
            {"Fights", numFights },
            {"World Reached", worldReached }
        });
    }

    public void DeviceModel_Event()
    {

        Analytics.CustomEvent("DeviceModel", new Dictionary<string, object>
        {
            {"device",SystemInfo.deviceModel }
        });


    }

    public void Item_Bought_Event(string itemName)
    {
        Analytics.CustomEvent("Item_Bought", new Dictionary<string, object>
        {
            {"item bought", itemName }
        });
    }

    public void Shared_Screen()
    {
        Analytics.CustomEvent("Shared_Screen", new Dictionary<string, object>
        {
            {"shared screen", "Yes" }
        });
    }
}

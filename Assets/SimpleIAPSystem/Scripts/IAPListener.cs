/*  This file is part of the "Simple IAP System" project by Rebound Games.
 *  You are only allowed to use these resources if you've bought them from the Unity Asset Store.
 * 	You shall not license, sublicense, sell, resell, transfer, assign, distribute or
 * 	otherwise make available to any third party the Service or the Content. */

using UnityEngine;

#if UNITY_PURCHASING
using UnityEngine.Purchasing;
#endif

namespace SIS
{
    /// <summary>
    /// Script that listens to purchases and other IAP events:
    /// here we tell our game what to do when these events happen.
    /// <summary>
    public class IAPListener : MonoBehaviour
    {
        //subscribe to the most important IAP events
        private void OnEnable()
        {
            IAPManager.purchaseSucceededEvent += HandleSuccessfulPurchase;
            IAPManager.purchaseFailedEvent += HandleFailedPurchase;
            ShopManager.itemSelectedEvent += HandleSelectedItem;
            ShopManager.itemDeselectedEvent += HandleDeselectedItem;
        }


        private void OnDisable()
        {
			IAPManager.purchaseSucceededEvent -= HandleSuccessfulPurchase;
			IAPManager.purchaseFailedEvent -= HandleFailedPurchase;
			ShopManager.itemSelectedEvent -= HandleSelectedItem;
			ShopManager.itemDeselectedEvent -= HandleDeselectedItem;
        }


        /// <summary>
        /// Handle the completion of purchases, be it for products or virtual currency.
        /// Most of the IAP logic is handled internally already, such as adding products or currency to the inventory.
        /// However, this is the spot for you to implement your custom game logic for instantiating in-game products etc.
        /// </summary>
        public void HandleSuccessfulPurchase(string id)
        {
            if (IAPManager.isDebug) Debug.Log("IAPListener reports: HandleSuccessfulPurchase: " + id);

            //differ between ids set in the IAP Settings editor
            switch (id)
            {
                //section for in app purchases
                case "si_x2":
                    //the user bought the item "coins", show appropriate feedback
                    ShowMessage("Your score will be double!!!");
                    Rad_SaveManager.profile.doubleScorePurchased++;
                    Rad_SaveManager.profile.doubleScore = true;
                    AnalyticsManager.Instance.Item_Bought_Event("Double Score");
                    AchievementsManager.Instance.BuyDoubleScoreAchievement();
                    break;
                case "si_1up":
                    ShowMessage("Now you have an extra life in combat!");
                    Rad_SaveManager.profile.extraLife = true;
                    Rad_SaveManager.profile.extraLifePurchased++;
                    AnalyticsManager.Instance.Item_Bought_Event("Extra Life");
                    AchievementsManager.Instance.BuyReviveAchievement();
                    break;
                case "si_noads":
                    Rad_SaveManager.profile.noAds = true;
                    ShowMessage("No more ads!");
                    AnalyticsManager.Instance.Item_Bought_Event("No ads");
                    break;

                case "si_1gem":
                    Rad_SaveManager.profile.gems++;
                    DBManager.IncreaseFunds("gems", 1);
                    ShowMessage("1 Gem Purchased !");
                    AnalyticsManager.Instance.Item_Bought_Event("1 Gem");
                    AchievementsManager.Instance.IncrementGemsEarnedAchievements(1);
                    break;
                case "si_5gems":
                    Rad_SaveManager.profile.gems += 5;
                    DBManager.IncreaseFunds("gems", 5);
                    ShowMessage("5 Gems Purchased !");
                    AnalyticsManager.Instance.Item_Bought_Event("5 Gems");
                    AchievementsManager.Instance.IncrementGemsEarnedAchievements(5);
                    break;
                case "si_10gems":
                    Rad_SaveManager.profile.gems += 10;
                    DBManager.IncreaseFunds("gems", 10);
                    AnalyticsManager.Instance.Item_Bought_Event("10 Gems");
                    AchievementsManager.Instance.IncrementGemsEarnedAchievements(10);
                    ShowMessage("10 Gems Purchased !");
                    break;
                case "si_20gems":
                    Rad_SaveManager.profile.gems += 20;
                    DBManager.IncreaseFunds("gems", 20);
                    AnalyticsManager.Instance.Item_Bought_Event("20 Gems");
                    AchievementsManager.Instance.IncrementGemsEarnedAchievements(20);
                    ShowMessage("20 Gems Purchased !");
                    break;
                case "si_supportdevs":
                    ShowMessage("Eternal gratitude...now! Thx");
                    AnalyticsManager.Instance.Item_Bought_Event("Eternal Gratitude");
                    break;
                case "si_yellowsword":
                    ShowMessage("Slow time when fighting.");
                    DBManager.IncreasePurchase("si_yellowsword_gems", 1);
                    AnalyticsManager.Instance.Item_Bought_Event("Yellow sword");
                    AchievementsManager.Instance.BuyAllSwordsAchievement();
                    break;
                case "si_bluesword":
                    ShowMessage("Press any gem, break any gem.");
                    DBManager.IncreasePurchase("si_bluesword_gems", 1);
                    AnalyticsManager.Instance.Item_Bought_Event("Blue sword");
                    AchievementsManager.Instance.BuyAllSwordsAchievement();
                    break;
                case "si_greensword":
                    ShowMessage("Drain life per kill.");
                    DBManager.IncreasePurchase("si_greensword_gems", 1);
                    AnalyticsManager.Instance.Item_Bought_Event("Green sword");
                    AchievementsManager.Instance.BuyAllSwordsAchievement();
                    break;
                case "si_blacksword":
                    ShowMessage("All Crits while active.");
                    DBManager.IncreasePurchase("si_blacksword_gems", 1);
                    AnalyticsManager.Instance.Item_Bought_Event("Black sword");
                    AchievementsManager.Instance.BuyAllSwordsAchievement();
                    break;
                case "si_yellowsword_gems":
                    ShowMessage("Slow time when fighting.");
                    DBManager.IncreasePurchase("si_yellowsword", 1);
                    AnalyticsManager.Instance.Item_Bought_Event("Yellow sword");
                    AchievementsManager.Instance.BuyAllSwordsAchievement();
                    break;
                case "si_bluesword_gems":
                    ShowMessage("Press any gem, break any gem.");
                    DBManager.IncreasePurchase("si_bluesword", 1);
                    AnalyticsManager.Instance.Item_Bought_Event("Blue sword");
                    AchievementsManager.Instance.BuyAllSwordsAchievement();
                    break;
                case "si_greensword_gems":
                    ShowMessage("Drain life per kill.");
                    DBManager.IncreasePurchase("si_greensword", 1);
                    AnalyticsManager.Instance.Item_Bought_Event("Green sword");
                    AchievementsManager.Instance.BuyAllSwordsAchievement();
                    break;
                case "si_blacksword_gems":
                    ShowMessage("All Crits while active.");
                    DBManager.IncreasePurchase("si_blacksword", 1);
                    AnalyticsManager.Instance.Item_Bought_Event("Black sword");
                    AchievementsManager.Instance.BuyAllSwordsAchievement();
                    break;

            }
            Rad_SaveManager.SaveData();
        }

        //just shows a message via our ShopManager component,
        //but checks for an instance of it first
        void ShowMessage(string text)
        {
            if (ShopManager.GetInstance())
                ShopManager.ShowMessage(text);
        }

        //called when an purchaseFailedEvent happens,
        //we do the same here
        void HandleFailedPurchase(string error)
        {
            if (ShopManager.GetInstance())
                ShopManager.ShowMessage(error);
        }


        //called when a purchased shop item gets selected
        void HandleSelectedItem(string id)
        {
            if (IAPManager.isDebug) Debug.Log("Selected: " + id);
        }


        //called when a selected shop item gets deselected
        void HandleDeselectedItem(string id)
        {
            if (IAPManager.isDebug) Debug.Log("Deselected: " + id);
        }

    }
}
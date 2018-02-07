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
                case "shop_item_00":
                    //the user bought the item "coins", show appropriate feedback
                    ShowMessage("Your score will be double!!!");
                    DBManager.IncreasePurchase("shop_item_00", 1);
                    Rad_SaveManager.profile.doubleScore = true;
                    AnalyticsManager.Instance.Item_Bought_Event("Double Score");
                    break;
                case "shop_item_01":
                    DBManager.IncreasePurchase("shop_item_01", 1);
                    ShowMessage("Now you have an extra life in combat!");
                    Rad_SaveManager.profile.extraLife = true;
                    AnalyticsManager.Instance.Item_Bought_Event("Extra Life");
                    break;
                case "shop_item_02":
                    DBManager.IncreasePurchase("shop_item_02", 1);
                    Rad_SaveManager.profile.noAds = true;
                    ShowMessage("No more ads!");
                    AnalyticsManager.Instance.Item_Bought_Event("No ads");
                    break;
                case "shop_item_03":
                    DBManager.IncreasePurchase("shop_item_03", 1);
                    ShowMessage("Eternal gratitude...now! Thx");
                    AnalyticsManager.Instance.Item_Bought_Event("Eternal Gratitude");
                    break;
                case "shop_item_04":
                    //DBManager.IncreasePurchase("shop_item_04", 1);
                    Rad_SaveManager.profile.tokens++;
                    ShowMessage("1 Gem Purchased !");
                    AnalyticsManager.Instance.Item_Bought_Event("1 Gem");
                    break;
                case "shop_item_05":
                    //DBManager.IncreasePurchase("shop_item_05", 1);
                    Rad_SaveManager.profile.tokens += 5;
                    ShowMessage("5 Gems Purchased !");
                    AnalyticsManager.Instance.Item_Bought_Event("5 Gems");
                    break;
                case "shop_item_06":
                    // DBManager.IncreasePurchase("shop_item_06", 1);
                    Rad_SaveManager.profile.tokens += 10;
                    AnalyticsManager.Instance.Item_Bought_Event("10 Gems");
                    ShowMessage("10 Gems Purchased !");
                    break;
                case "shop_item_07":
                    //DBManager.IncreasePurchase("shop_item_07", 1);
                    Rad_SaveManager.profile.tokens += 20;
                    AnalyticsManager.Instance.Item_Bought_Event("20 Gems");
                    ShowMessage("20 Gems Purchased !");
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
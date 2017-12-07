/*  This file is part of the "Simple IAP System" project by Rebound Games.
 *  You are only allowed to use these resources if you've bought them from the Unity Asset Store.
 * 	You shall not license, sublicense, sell, resell, transfer, assign, distribute or
 * 	otherwise make available to any third party the Service or the Content. */

#if UNITY_PURCHASING && PLAYFAB_STEAM
using UnityEngine;
using UnityEngine.Purchasing.Extension;
using PlayFab;
using PlayFab.ClientModels;
using Steamworks;

namespace SIS
{
    /// <summary>
    /// Represents the public interface of the underlying store system for the Steamworks API.
    /// </summary>
    public class PlayfabSteamStore : PlayfabStore
    {
        #pragma warning disable 0414
        protected Callback<MicroTxnAuthorizationResponse_t> microTxnAuthorizationResponse;
        #pragma warning restore 0414


        public override void Initialize(IStoreCallback callback)
        {
            storeId = "Steam";
            this.callback = callback;

            microTxnAuthorizationResponse = Callback<MicroTxnAuthorizationResponse_t>.Create(OnMicroTxnAuthorizationResponse);
        }


		public override void OnPurchaseResult(PayForPurchaseResult result)
		{
			Debug.LogError("Status: " + result.Status + ", Currency: " + result.PurchaseCurrency +
		        ", Price: " + result.PurchasePrice + ", ProviderData: " + result.ProviderData +
		        ", PageURL: " + result.PurchaseConfirmationPageURL);
		}


        private void OnMicroTxnAuthorizationResponse(MicroTxnAuthorizationResponse_t pCallback)
        {
            Debug.LogError("OnMicroTxnAuthorizationResponse: " + pCallback.m_unAppID + ", " + pCallback.m_ulOrderID + ", " + pCallback.m_bAuthorized);

			ConfirmPurchaseRequest request = new ConfirmPurchaseRequest()
			{
				OrderId = orderId
			};

			PlayFabClientAPI.ConfirmPurchase(request, OnPurchaseSucceeded, OnPurchaseFailed);
        }
    }
}
#endif
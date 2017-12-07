﻿/*  This file is part of the "Simple IAP System" project by Rebound Games.
 *  You are only allowed to use these resources if you've bought them from the Unity Asset Store.
 * 	You shall not license, sublicense, sell, resell, transfer, assign, distribute or
 * 	otherwise make available to any third party the Service or the Content. */

using UnityEngine;
using UnityEngine.SceneManagement;

namespace SIS
{
    /// <summary>
    /// Simple script that contains methods for testing purposes.
    /// You shouldn't implement this script in production versions.
    /// <summary>
    public class DebugCalls : MonoBehaviour
    {
        void Start()
        {
            //switch over to the virtual reality supported scene if VR mode is enabled
            if (SceneManager.GetActiveScene().name == "AllSelection" && UnityEngine.VR.VRSettings.enabled)
                SceneManager.LoadScene("VerticalVR");
        }


        /// <summary>
        /// Deletes all data saved in prefs, for ensuring a clean test state.
        /// <summary>
        public void Reset()
        {
            if (DBManager.GetInstance())
            {
                DBManager.ClearAll();
                DBManager.GetInstance().Init();
            }
        }


        /// <summary>
        /// Increases player level by 1 which unlocks new shop items.
        /// <summary>
        public void LevelUp()
        {
            if (DBManager.GetInstance())
            {
                int level = DBManager.IncreasePlayerData("level", 1);

                if (ShopManager.GetInstance())
                    ShopManager.RefreshAll();

                Debug.Log("Leveled up to level: " + level +
                          "! Shop Manager tried to unlock new items.");
            }
        }
    }
}

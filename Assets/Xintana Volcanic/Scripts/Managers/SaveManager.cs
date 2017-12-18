using UnityEngine;
using System.Collections;

public static class SaveManager : object
{
    //public static int coinAmmount = 1500;                   //The ammount of coins the player has
    //public static int level = 1;                     //The best distance the player has reached
    //public static int lastCoins = 0;
    //public static int lastDistance = 0;                     //The last distance played.
    //public static int currentScore = 0;
    //public static int bestScore = 0;                        //The last score played.
    //public static int currentSkinID = 0;                    //The current submarine skin ID (0 is the default skin)
    //public static int skin2Unlocked = 0;                    //Hold the skin 2 owned state
    //public static int audioEnabled = 1;

    // XINTANA VALUES ===================================
    public static float xintanaLife = 10f;
    public static float checkpointXPosition = -18.28f;
    public static float checkpointYPosition = -4.9f;
    public static int level = 2;
    public static int sword = 0;
    public static int finalBossStarted = 0;
    // ==================================================

    //public static int[] missionID;                          //Mission 1, 2 and 3 ID
    //public static int[] missionData;                        //Mission 1, 2 and 3 Data

    //public static string missionDataString = "";            //Saved mission data string

    //Loads the player data
    public static void LoadData()
    {
        //If found the coin ammount data, load the datas
        if (!PlayerPrefs.HasKey("Level"))
            SaveData();
        else
        {
            //coinAmmount = PlayerPrefs.GetInt("Coin ammount");
            
            //lastCoins = PlayerPrefs.GetInt("Last Coins");
            //bestScore = PlayerPrefs.GetInt("Best Score");
            //currentScore = PlayerPrefs.GetInt("Score");
            //audioEnabled = PlayerPrefs.GetInt("AudioEnabled");

            xintanaLife = PlayerPrefs.GetFloat("Life");
            checkpointXPosition = PlayerPrefs.GetFloat("XCoordinate");
            checkpointYPosition = PlayerPrefs.GetFloat("YCoordinate");
            level = PlayerPrefs.GetInt("Level");
            sword = PlayerPrefs.GetInt("Sword");
            finalBossStarted = PlayerPrefs.GetInt("FinalFight");
        }

        //if (!PlayerPrefs.HasKey("Skin ID"))
        //{
        //    PlayerPrefs.SetInt("Skin ID", currentSkinID);
        //    PlayerPrefs.SetInt("Skin 2 Unlocked", skin2Unlocked);

        //    PlayerPrefs.Save();
        //}
        //else
        //{
        //    currentSkinID = PlayerPrefs.GetInt("Skin ID");
        //    skin2Unlocked = PlayerPrefs.GetInt("Skin 2 Unlocked");
        //}
    }
    //Saves the player data
    public static void SaveData()
    {
        //PlayerPrefs.SetInt("Coin Ammount", coinAmmount);
        //PlayerPrefs.SetInt("Level", level);
        //PlayerPrefs.SetInt("Last Coins", lastCoins);
        //PlayerPrefs.SetInt("Best Score", bestScore);
        //PlayerPrefs.SetInt("Score", currentScore);
        //PlayerPrefs.SetInt("AudioEnabled", audioEnabled);

        // XINTANA ==============================================
        PlayerPrefs.SetFloat("Life",xintanaLife);
        PlayerPrefs.SetFloat("XCoordinate", checkpointXPosition);
        PlayerPrefs.SetFloat("YCoordinate", checkpointYPosition);
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.SetInt("Sword", sword);
        PlayerPrefs.SetInt("FinalFight", finalBossStarted);
        // ======================================================
        PlayerPrefs.Save();
    }
    //Loads the mission data
    //public static void LoadMissionData()
    //{
    //    missionID = new int[3] { -1, -1, -1 };
    //    missionData = new int[3] { 0, 0, 0 };

    //    if (!PlayerPrefs.HasKey("Missions"))
    //    {
    //        SaveMissionData();
    //    }
    //    else
    //    {
    //        missionDataString = PlayerPrefs.GetString("Missions");

    //        missionID[0] = PlayerPrefs.GetInt("Mission1");
    //        missionID[1] = PlayerPrefs.GetInt("Mission2");
    //        missionID[2] = PlayerPrefs.GetInt("Mission3");

    //        missionData[0] = PlayerPrefs.GetInt("Mission1Data");
    //        missionData[1] = PlayerPrefs.GetInt("Mission2Data");
    //        missionData[2] = PlayerPrefs.GetInt("Mission3Data");
    //    }
    //}
    //Saves the mission data
    //public static void SaveMissionData()
    //{
    //    PlayerPrefs.SetInt("Mission1", missionID[0]);
    //    PlayerPrefs.SetInt("Mission2", missionID[1]);
    //    PlayerPrefs.SetInt("Mission3", missionID[2]);

    //    PlayerPrefs.SetInt("Mission1Data", missionData[0]);
    //    PlayerPrefs.SetInt("Mission2Data", missionData[1]);
    //    PlayerPrefs.SetInt("Mission3Data", missionData[2]);

    //    PlayerPrefs.SetString("Missions", missionDataString);
    //}
    //Reset mission data
    //public static void ResetMissions()
    //{
    //    missionID = new int[3] { -1, -1, -1 };
    //    missionData = new int[3] { 0, 0, 0 };

    //    missionDataString = "";

    //    SaveMissionData();
    //}

    public static void ResetValues()
    {

        //PlayerPrefs.SetInt("Coin ammount", 0);
        //PlayerPrefs.SetInt("Level", 0);
        //PlayerPrefs.SetInt("Last Coins", 0);
        //PlayerPrefs.SetInt("Best Score", 0);
        //PlayerPrefs.SetInt("Score", 0);

        PlayerPrefs.SetFloat("Life", 0);
        PlayerPrefs.SetFloat("XCoordinate", 0);
        PlayerPrefs.SetFloat("YCoordinate", 0);
        PlayerPrefs.SetInt("Level", 0);
        PlayerPrefs.SetInt("Sword", 0);
        PlayerPrefs.SetInt("FinalFight", 0);
        PlayerPrefs.Save();
    }

}
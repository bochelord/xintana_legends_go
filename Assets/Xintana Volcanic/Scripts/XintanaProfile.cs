using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class XintanaProfile : MonoBehaviour {

    #region Variables

    public string gameVersion = "";
    public int profileID;

    public int gems;
    public int shells;

    public int highscore;
    public int coinsEarned;
    public int extraLifePurchased;
    public int doubleScorePurchased;
    public int playerKills;
    public int gemsCollected;
    public int gemsComboLength;
    public int level;

    public int doubleScoreOwned;
    public int extraLifeOwned;

    public float experience;
    public WeaponType weaponEquipped;
    public int adsViewed;
    public int adsSkipped;
    public bool audioEnabled;
    public bool extraLife;
    public bool noAds;
    public bool doubleScore;
    public bool firstTimePlayed;
    public bool freeTokenDay;
    public bool reviveAdWatched;
    public bool tokenAdWatched;
    public System.DateTime timeStamp;
    public int sharedScoreTimes;
    //TODO creat purchases done and equipped_items;
    #endregion

    #region contructor

    public XintanaProfile GenerateXintanaProfile(int id)
    {
        XintanaProfile profile = new XintanaProfile
        {
#if !UNITY_EDITOR
        profile.gameVersion = Application.version;
#else
            gameVersion = "Debugversion",
#endif
            profileID = id,
            highscore = 0,
            gems = 0,
            shells = 0,
            adsViewed = 0,
            adsSkipped = 0,
            playerKills = 0,
            gemsCollected = 0,
            gemsComboLength = 0,
            coinsEarned = 0,
            extraLifePurchased = 0,
            doubleScorePurchased = 0,
            doubleScoreOwned = 0,
            extraLifeOwned = 0,
            level = 1,
            experience = 0,
            weaponEquipped = WeaponType.red, // TODO check if this one will be the default weapon
            audioEnabled = true,
            extraLife = false,
            noAds = false,
            doubleScore = false,
            firstTimePlayed = true,
            freeTokenDay = false,
            tokenAdWatched = false,
            reviveAdWatched = false,
            timeStamp = System.DateTime.Now,
            sharedScoreTimes = 0
        };
        return profile;
    }
    #endregion
}

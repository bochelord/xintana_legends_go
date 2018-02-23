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
        XintanaProfile profile = new XintanaProfile();
#if !UNITY_EDITOR
        profile.gameVersion = Application.version;
#else
        profile.gameVersion = "Debugversion";
#endif
        profile.profileID = id;
        profile.highscore = 0;
        profile.gems = 0;
        profile.shells = 0;
        profile.adsViewed = 0;
        profile.adsSkipped = 0;
        profile.playerKills = 0;
        profile.gemsCollected = 0;
        profile.gemsComboLength = 0;
        profile.coinsEarned = 0;
        profile.extraLifePurchased = 0;
        profile.doubleScorePurchased = 0;
        profile.doubleScoreOwned = 0;
        profile.extraLifeOwned = 0;
        profile.level = 1;
        profile.experience = 0;
        profile.weaponEquipped = WeaponType.red; // TODO check if this one will be the default weapon
        profile.audioEnabled = true;
        profile.extraLife = false;
        profile.noAds = false;
        profile.doubleScore = false;
        profile.firstTimePlayed = true;
        profile.freeTokenDay = false;
        profile.tokenAdWatched = false;
        profile.reviveAdWatched = false;
        profile.timeStamp = System.DateTime.Now;
        profile.sharedScoreTimes = 0;
        return profile;
    }
    #endregion
}

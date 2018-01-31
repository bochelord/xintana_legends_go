using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class XintanaProfile : MonoBehaviour {

    #region Variables

    public string gameVersion = "";
    public int profileID;
    public int highscore;
    public int tokens;
    public int timesKilledByZazuc;
    public int timesKilledByMakula;
    public int timesKilledByKogi;
    public int timesKilledByBlackKnight; //TODO add variable for any type of enemy
    public int adsViewed;
    public int adsSkipped;
    public bool audioEnabled;
    public bool extraLife;
    public bool noAds;
    public bool doubleScore;
    public System.DateTime timeStamp;
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
        profile.tokens = 0;
        profile.adsViewed = 0;
        profile.adsSkipped = 0;
        profile.audioEnabled = true;
        profile.extraLife = false;
        profile.noAds = false;
        profile.doubleScore = false;
        profile.timesKilledByBlackKnight = 0;
        profile.timesKilledByKogi = 0;
        profile.timesKilledByMakula = 0;
        profile.timesKilledByZazuc = 0;
        profile.timeStamp = System.DateTime.Now;
        return profile;
    }
    #endregion
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class XintanaProfile : MonoBehaviour {

    #region Variables

    public string gameVersion = "";
    public int profileID;
    public int highscore;
    public int tokens;
    public bool audioEnabled;
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
        profile.audioEnabled = true;

        return profile;
    }
    #endregion
}

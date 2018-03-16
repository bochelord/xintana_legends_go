using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KogiReward : MonoBehaviour {

    private LevelManager _levelManager;

    void OnEnable()
    {
        _levelManager = FindObjectOfType<LevelManager>();
    }

    public void BUTTON_SpawnKogiReward()
    {
        _levelManager.SpawnKogiReward();
    }
}

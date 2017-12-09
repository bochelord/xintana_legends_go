using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Rad_GuiManager : MonoBehaviour {

    [Header("Panels")]
    public GameObject gmeOverPanel;
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject shop;

    [Header("Text")]
    public Text enemyText;
    public Text scoreText;

    [Header("Share Panel")]
    public Text kogiAmount;
    public Text zazuAmount;
    public Text makulaAmount;
    public Text scorePlayer;
    public Text worldReached;

    private LevelManager _levelManager;

    private void Awake()
    {
        _levelManager = FindObjectOfType<LevelManager>();
    }




    private void Update()
    {
        scoreText.text = _levelManager.GetPlayerScoreUI().ToString();
    }



    /// <summary>
    /// called from Button, set main menu panel on and turns off the rest
    /// </summary>
    public void MainMenuPanelOn()
    {
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    /// <summary>
    /// called frm button, turns on settings
    /// </summary>
    public void SettingsPanelOn()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void GameOverPanelOn()
    {
        gmeOverPanel.SetActive(true);
        gmeOverPanel.transform.DOLocalMoveX(0f, 1f).SetEase(Ease.OutBack);
    }


    public void GameOverPanelOff()
    {
        //gmeOverPanel.transform.DOLocalMoveX(663f, 1f);
        gmeOverPanel.transform.localPosition = new Vector3(663f, 0, 0);
        gmeOverPanel.SetActive(false);
        
    }
    public void FillSharePanel()
    {
        kogiAmount.text = _levelManager.GetKogiKilled().ToString();
        zazuAmount.text = _levelManager.GetZazuKilled().ToString();
        makulaAmount.text = _levelManager.GetMakulaKilled().ToString();
        int _tempScore = (int)_levelManager.GetPlayerScore();
        scorePlayer.text = _tempScore.ToString();
        worldReached.text = "World " + _levelManager.GetWorldNumber().ToString();
        
    }

    public void Button_OpenShop()
    {
        shop.SetActive(true);
    }

    public void Button_CloseShop()
    {
        shop.SetActive(false);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Rad_GuiManager : MonoBehaviour {

    [Header("Panels")]
    public GameObject gmeOverPanel;
    public GameObject playerGameOverPanel;
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject shop;
    public GameObject viewAdPanel;
    [Header("Text")]
    public Text enemyText;
    public Text scoreText;
    public Text worldText;
    [Header ("Player Game Over")]
    public Text pKogiAmount;
    public Text pZazuAmount;
    public Text pMakulaAmount;
    public Text pScorePlayer;
    public Text pWorldReached;
    public Text pFightsNumber;
    [Header("Share Panel")]
    public Text kogiAmount;
    public Text zazuAmount;
    public Text makulaAmount;
    public Text scorePlayer;
    public Text worldReached;
    public Text fightsNumber;
    [Header("ViewAdsPanel")]
    public Text timeCountdown;


    private LevelManager _levelManager;
    private float _timerCountdown = 5f;
    private bool timerCountdownAdOn = false;
    private bool _scorePanelOn = false;

    private int _pScorePlayer;
    private int _pZazuAmount;
    private int _pKogiAmount;
    private int _pMakulaAmount;
    private int _pWorldReached;
    private int _pFightsNumber;

    private void Awake()
    {
        _levelManager = FindObjectOfType<LevelManager>();
    }

    private void Update()
    {
        scoreText.text = _levelManager.GetPlayerScoreUI().ToString();
        worldText.text = _levelManager.GetCurrentWorldNumber().ToString();

        if (timerCountdownAdOn)
        {
            _timerCountdown -= Time.deltaTime;
            timeCountdown.text = Mathf.Round(_timerCountdown).ToString();
            if (_timerCountdown < 0)
            {
                Button_CloseViewAddPanel();
            }
        }

        if (_scorePanelOn)
        {
            UpdateScorePanelUI();
        }
    }

    private void UpdateScorePanelUI()
    {
        if (pScorePlayer)
        {
            pScorePlayer.text = _pScorePlayer.ToString();
        }
        if (pKogiAmount)
        {
            pKogiAmount.text = _pKogiAmount.ToString();
        }
        if (pZazuAmount)
        {
            pZazuAmount.text = _pZazuAmount.ToString();
        }
        if (pMakulaAmount)
        {
            pMakulaAmount.text = _pMakulaAmount.ToString();
        }
        if (pWorldReached)
        {
            pWorldReached.text = _pWorldReached.ToString();
        }
        if (pFightsNumber)
        {
            pFightsNumber.text = _pFightsNumber.ToString();
        }

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

    /// <summary>
    /// turns de game over panel on
    /// </summary>
    public void GameOverPanelOn()
    {
        playerGameOverPanel.SetActive(false);
        gmeOverPanel.SetActive(true);
        gmeOverPanel.transform.DOLocalMoveX(0f, 1f).SetEase(Ease.OutBack);

    }

    public void PlayerGameOverPanelOn()
    {
        playerGameOverPanel.SetActive(true);
        StartCoroutine(FillGameOverPanel());
    }
    /// <summary>
    /// Turns de Game over panell off
    /// </summary>
    public void GameOverPanelOff()
    {
        //gmeOverPanel.transform.DOLocalMoveX(663f, 1f);
        gmeOverPanel.transform.localPosition = new Vector3(663f, 0, 0);
        gmeOverPanel.SetActive(false);
        
    }
    /// <summary>
    /// fill all the info needed in the Share Panel
    /// </summary>
    public void FillSharePanel()
    {
        kogiAmount.text = _levelManager.GetKogiKilled().ToString();
        zazuAmount.text = _levelManager.GetZazuKilled().ToString();
        makulaAmount.text = _levelManager.GetMakulaKilled().ToString();
        int _tempScore = (int)_levelManager.GetPlayerScore();
        scorePlayer.text = _tempScore.ToString();
        worldReached.text = "World " + _levelManager.GetCurrentWorldNumber().ToString();
        fightsNumber.text = (_levelManager.GetTotalEnemyKilled()+1).ToString() + " Fights"; // This is plus one cause the current fight that player lost also counts although he didn't kill the enemy...
    }

    /// <summary>
    /// called from button
    /// </summary>
    public void Button_CloseViewAddPanel()
    {
        timerCountdownAdOn = false;
        _timerCountdown = 5;
        viewAdPanel.transform.DOLocalMoveY(1000f, 1f).SetEase(Ease.OutBack);
        viewAdPanel.SetActive(false);
        PlayerGameOverPanelOn();
    }
    IEnumerator FillGameOverPanel()
    {
        _scorePanelOn = true; 
        playerGameOverPanel.transform.DOLocalMoveX(0f, 1f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(1);
        DOTween.To(() => _pScorePlayer, x => _pScorePlayer = x, (int)_levelManager.GetPlayerScore(), 1f);
        yield return new WaitForSeconds(1);
        DOTween.To(() => _pWorldReached, x => _pWorldReached = x, _levelManager.GetCurrentWorldNumber(), 1f);
        yield return new WaitForSeconds(0.5f);
        DOTween.To(() => _pFightsNumber, x => _pFightsNumber = x, _levelManager.GetTotalEnemyKilled()+1, 1f);
        yield return new WaitForSeconds(0.5f);
        DOTween.To(() => _pKogiAmount, x => _pKogiAmount = x, _levelManager.GetKogiKilled(), 1f);
        yield return new WaitForSeconds(0.5f);

        DOTween.To(() => _pZazuAmount, x => _pZazuAmount = x, _levelManager.GetZazuKilled(), 1f);
        yield return new WaitForSeconds(0.5f);
        DOTween.To(() => _pMakulaAmount, x => _pMakulaAmount = x, _levelManager.GetMakulaKilled(), 1f);
        yield return new WaitForSeconds(0.5f);
        _scorePanelOn = false; //so we stop constantly refreshing this panel
    }

    public void ShowAdPanel()
    {
        viewAdPanel.SetActive(true);
        timerCountdownAdOn = true;
        viewAdPanel.transform.DOLocalMoveY(0f, 1f).SetEase(Ease.OutBack);
    }
    public void HideAdPanel()
    {
        timerCountdownAdOn = false;
        _timerCountdown = 5;
        viewAdPanel.transform.DOLocalMoveY(1000f, 1f).SetEase(Ease.OutBack);
        viewAdPanel.SetActive(false);
    }

    public void HideAdPanelAndStartGameOverPanel()
    {
        viewAdPanel.transform.DOLocalMoveY(1000f, 1f).SetEase(Ease.OutBack);
        viewAdPanel.SetActive(false);
        PlayerGameOverPanelOn();
    }
    public void Button_OpenShop()
    {
        //shop.SetActive(true);
        shop.transform.DOLocalMoveX(0f, 0.75f).SetEase(Ease.OutBack);
    }

    public void Button_CloseShop()
    {
        //shop.SetActive(false);
        shop.transform.DOLocalMoveX(840f, 0.75f).SetEase(Ease.OutBack);
    }
}
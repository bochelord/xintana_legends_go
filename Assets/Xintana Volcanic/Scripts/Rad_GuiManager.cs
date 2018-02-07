using System.Collections;
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
    public GameObject ContinueNoAdPanel;
    public GameObject pausePanel;
    public GameObject menuButtons;
    public GameObject doublePricePanel;
    public GameObject roulettePanel;
    public GameObject pricePanel;
    public GameObject noGemsPanel;
    [Header("PlaceHolders")]
    public Transform midScreen;
    public Transform gemPosition;
    [Header("Text")]
    public Text enemyText;
    public Text scoreText;
    public Text highScoreText;
    public Text worldText;
    public Text gemsText;
    [Header("Icons")]
    public GameObject doubleScoreIcon;
    public GameObject extraLifeIcon;
    [Header ("Player Game Over")]
    public Text pKogiAmount;
    public Text pZazuAmount;
    public Text pMakulaAmount;
    public Text pScorePlayer;
    public Text pWorldReached;
    public Text pFightsNumber;
    public GameObject x2Text;
    public GameObject gem;
    public GameObject HighScoreFxPrefab;
    [Header("Share Panel")]
    public Text kogiAmount;
    public Text zazuAmount;
    public Text makulaAmount;
    public Text scorePlayer;
    public Text worldReached;
    public Text fightsNumber;
    [Header("ViewAdsPanel")]
    public Text timeCountdown;
    public Text timeCountDownContinuePanel;

    [Header("Roulette")]
    public GameObject chest1;
    public GameObject chest2;
    public GameObject chest3;
    public Text rouletteTitle;
    public Transform posChest1;
    public Transform posChest2;
    public Transform posChest3;
    public Transform posTitle;
    public Button backButton;
    public Text gemsTextRoulette;
    [Header("Price Panel")]
    public Text priceText;
    

    private AnalyticsManager _analyticsManager;
    private AdsManager _adsManager;
    private SIS.ShopManager _shopManager;
    private LevelManager _levelManager;
    private ChestRoulette _chestManager;

    private float _timerCountdown = 5f;
    private bool timerCountdownAdOn = false;
    private bool timerContdownContinue = false;
    private bool _scorePanelOn = false;
    private bool _doubleScorePanelOn = false;
    private bool _pricePanelOn = false;
    private bool _doublePrice = false;
    private bool _mainMenu = false;
    private bool rouletteOn = false;
    private bool _updateScore = false;
    private int _scorePlayer = 0;
    private int _pDoublePrice = 0;
    private int _pScorePlayer;
    private int _pHighScore;
    private int _pZazuAmount;
    private int _pKogiAmount;
    private int _pMakulaAmount;
    private int _pWorldReached;
    private int _pFightsNumber;
    private Coroutine gameOverPanelCoroutine;
    private Coroutine doublePricePanelCoroutine;
    private void Awake()
    {
        _analyticsManager = FindObjectOfType<AnalyticsManager>();
        _levelManager = FindObjectOfType<LevelManager>();
        _shopManager = FindObjectOfType<SIS.ShopManager>();
        _adsManager = FindObjectOfType<AdsManager>();
        _chestManager = FindObjectOfType<ChestRoulette>();
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

        if (timerContdownContinue)
        {
            _timerCountdown -= Time.deltaTime;
            timeCountDownContinuePanel.text = Mathf.Round(_timerCountdown).ToString();
            if (_timerCountdown < 0)
            {
                HideContinuePanel();
            }
        }
        if (_scorePanelOn)
        {
            UpdateScorePanelUI();
        }
        if (_pricePanelOn)
        {
            UpdatePricePanel();
        }
        if (rouletteOn)
        {
            gemsTextRoulette.text = Rad_SaveManager.profile.gems.ToString();
        }
        if (_doublePrice && _chestManager.priceType == chestType.coins)
        {
            priceText.text = _pDoublePrice.ToString() + " Coins !!";
        }
        else if (_doublePrice &&_chestManager.priceType == chestType.gems)
        {
            priceText.text = _pDoublePrice.ToString() + " Gems !!";
        }
        if (_mainMenu)
        {
            gemsText.text = Rad_SaveManager.profile.gems.ToString();
        }

        if(scoreText && _updateScore)
        {
            scoreText.text = _levelManager.GetPlayerScoreUI().ToString();
        }
    }
    private void UpdatePricePanel()
    {
        if (priceText && _chestManager.priceType == chestType.coins)
        {
            priceText.text = _chestManager.priceAmount.ToString() + " Coins !!";
        }
        else if (priceText && _chestManager.priceType == chestType.gems)
        {
            priceText.text = _chestManager.priceAmount.ToString() + " Gems !!";
        }else if(priceText && _chestManager.priceType == chestType.empty)
        {
            priceText.text = " OOHHHH !!";
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
        if (highScoreText)
        {
            highScoreText.text = _pHighScore.ToString();
        }

    }

    public void UpdateIcons()
    {
        if (SIS.DBManager.GetPurchase("shop_item_00") > 0)
        {
            doubleScoreIcon.SetActive(true);
        }
        else
        {
            doubleScoreIcon.SetActive(false);
        }

        if (SIS.DBManager.GetPurchase("shop_item_01") > 0)
        {
            extraLifeIcon.SetActive(true);
        }
        else
        {
            extraLifeIcon.SetActive(false);
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
        _mainMenu = true;
        playerGameOverPanel.SetActive(false);
        gmeOverPanel.SetActive(true);
        gmeOverPanel.transform.DOLocalMoveX(0f, 1f).SetEase(Ease.OutBack);
        if (_doubleScorePanelOn)
        {
            HideDoublePricePanel();
        }
        if(gameOverPanelCoroutine != null)
        {
            StopCoroutine(gameOverPanelCoroutine);
        }
    }

    public void PlayerGameOverPanelOn()
    {
        playerGameOverPanel.SetActive(true);
        gameOverPanelCoroutine = StartCoroutine(FillGameOverPanel());


    }

    /// <summary>
    /// Called from button, playerGameOverPanel closeButton in cartoon scene
    /// </summary>
    public void Button_CloseScorePanel()
    {
        _mainMenu = true;
        playerGameOverPanel.transform.DOMoveX(1100f, 1f).SetEase(Ease.OutBack);
        pausePanel.transform.DOMoveX(midScreen.position.x,1).SetEase(Ease.OutBack);
        if (menuButtons)
        {
            menuButtons.transform.DOLocalMoveY(-556f, 1).SetEase(Ease.OutBack);

        }

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
        Rad_SaveManager.profile.adsSkipped++;
        PlayerGameOverPanelOn();
        _analyticsManager.ResurrectionAd_Event(false);

    }

    public void Button_ShowAdExtraLife()
    {
        timerCountdownAdOn = false;
        _adsManager.ShowAdForExtraLife();
    }
   public void PausePanelOn()
    {
        if (pausePanel)
        {
            pausePanel.transform.DOLocalMoveX(1650f, 1f).SetEase(Ease.OutBack);
            _mainMenu = true;
        }

    }
    public void PausePanelOff()
    {
        if (pausePanel)
        {
            pausePanel.transform.DOLocalMoveX(-1650f, 1f).SetEase(Ease.OutBack);
            _mainMenu = false;
        }

    }

    public void OptionsPanelOn()
    {
        if (settingsPanel)
        {
            pausePanel.transform.DOLocalMoveX(-1650f, 1f).SetEase(Ease.OutBack);
            settingsPanel.transform.DOLocalMoveX(1650f, 1f).SetEase(Ease.OutBack);
        }
    }

    public void OptionsPanelOff()
    {
        if (settingsPanel)
        {
            pausePanel.transform.DOLocalMoveX(1650f, 1f).SetEase(Ease.OutBack);
            settingsPanel.transform.DOLocalMoveX(-1650f, 1f).SetEase(Ease.OutBack);
        }
    }

    private IEnumerator refreshHighScore(int newHighScore) {
        yield return new WaitForSeconds(0.60f);
        _pHighScore = 0;
        DOTween.To(() => _pHighScore, x => _pHighScore = x, newHighScore, 1f);
    }

    IEnumerator FillGameOverPanel()
    {
        _scorePanelOn = true;
        _pHighScore = Rad_SaveManager.profile.highscore;
        _pScorePlayer = 0;
        x2Text.SetActive(false); 
        playerGameOverPanel.transform.DOLocalMoveX(0f, 1f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(1);
        DOTween.To(() => _pScorePlayer, x => _pScorePlayer = x, (int)_levelManager.GetPlayerScore(), 1f).OnComplete(()=> 
        {
            if (SIS.DBManager.GetPurchase("shop_item_00") > 0)
            {
                x2Text.SetActive(true);
                x2Text.transform.DOShakeScale(2, 0.5f, 2, 25, true);
                DOTween.To(() => _pScorePlayer, x => _pScorePlayer = x, (int)_levelManager.GetPlayerScore() * 2, 1f);
                SIS.DBManager.RemovePurchase("shop_item_00");
                SIS.DBManager.RemovePurchaseUI("shop_item_00");
                Rad_SaveManager.profile.doubleScore = false;
                _levelManager.SetPlayerScore((int)_levelManager.GetPlayerScore() * 2);
            }
        });

        yield return new WaitForSeconds(2.5f);
        if(Rad_SaveManager.profile.highscore< _levelManager.GetPlayerScore())
        {
            Rad_SaveManager.profile.highscore = (int)_levelManager.GetPlayerScore();
            Rad_SaveManager.SaveData();
            GameObject clone_HighScoreFxPrefab;
            clone_HighScoreFxPrefab = Instantiate(HighScoreFxPrefab);
            clone_HighScoreFxPrefab.transform.SetParent(playerGameOverPanel.transform);
            clone_HighScoreFxPrefab.transform.position = highScoreText.transform.position;
            StartCoroutine(refreshHighScore(Rad_SaveManager.profile.highscore));
        }
        DOTween.To(() => _pWorldReached, x => _pWorldReached = x, _levelManager.GetCurrentWorldNumber(), 1f).OnComplete(()=> 
        {
            if (_pWorldReached > 1)
            {
                SpawnGem();
                Rad_SaveManager.profile.gems++;
                Rad_SaveManager.SaveData();
            }
        });

        yield return new WaitForSeconds(1f);
        DOTween.To(() => _pFightsNumber, x => _pFightsNumber = x, _levelManager.GetTotalEnemyKilled()+1, 1f);

        yield return new WaitForSeconds(1.5f);
        _scorePanelOn = false; //so we stop constantly refreshing this panel
    }

    private void SpawnGem()
    {
        gem.SetActive(true);
        gem.transform.DOScale(3, 1).OnComplete(()=> 
        {
            gem.transform.DOScale(1, 0);
            gem.SetActive(false);
        });
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
    public void ShowContinuePanel()
    {
        timerContdownContinue = true;
        ContinueNoAdPanel.SetActive(true);
        ContinueNoAdPanel.transform.DOLocalMoveY(0f, 1f).SetEase(Ease.OutBack);
    }

    public void HideContinuePanel()
    {
        timerContdownContinue = false;
        _timerCountdown = 5;
        ContinueNoAdPanel.transform.DOLocalMoveY(1000f, 1f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            ContinueNoAdPanel.SetActive(false);
            _levelManager.ContinueGame();
        });
    }

    public void ShowDoublePricePanel()
    {
        doublePricePanelCoroutine = StartCoroutine(ShowDoublePriceCoroutine(2));
    }

    IEnumerator ShowDoublePriceCoroutine(float time)
    {
        yield return new WaitForSeconds(time);
        doublePricePanel.SetActive(true);
        _doubleScorePanelOn = true;
        doublePricePanel.transform.DOLocalMoveY(0f, 1f).SetEase(Ease.OutBack);
        //pricePanel.SetActive(false);
    }
    public void Button_DoublePrice()
    {
        _adsManager.ShowAdForDoublePrice();
    }
    public void DoublePrice()
    {
        StartCoroutine(DoublePriceFromAd());
    }
    IEnumerator DoublePriceFromAd()
    {
        backButton.enabled = true;
        _pricePanelOn = false;
        _doublePrice = true;
        DOTween.To(() =>_pDoublePrice, x => _pDoublePrice = x,(int)_chestManager.priceAmount * 2, 2f );
        yield return new WaitForSeconds(2);
        _chestManager.UpdateDoublePrice();
        _doublePrice = false;
    }

    public void HideDoublePricePanel()
    {
        //pricePanel.SetActive(true);
        doublePricePanel.transform.DOLocalMoveY(1000f, 1f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            _doubleScorePanelOn = false;
            doublePricePanel.SetActive(false);

        });

    }
    public void Button_HideDoublePricePanel()
    {
        _analyticsManager.DoublePriceAd_Event(false);
        HideDoublePricePanel();
        Rad_SaveManager.profile.adsSkipped++;
        backButton.enabled = true;
    }

    public void Button_ShowRoulettePanel()
    {
        backButton.enabled = false;
        rouletteOn = true;
        if (Rad_SaveManager.profile.gems > 0)
        {
            roulettePanel.transform.DOLocalMoveX(0f, 0.75f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                rouletteTitle.transform.DOMove(posTitle.position, 1f).SetEase(Ease.OutBack).OnComplete(() =>
                {
                    chest1.transform.DOMove(posChest1.position, 1f).SetEase(Ease.OutBack).OnComplete(() =>
                    {
                        chest2.transform.DOMove(posChest2.position, 1f).SetEase(Ease.OutBack).OnComplete(() =>
                        {
                            chest3.transform.DOMove(posChest3.position, 1f).SetEase(Ease.OutBack).OnComplete(() =>
                            {
                                _chestManager.RestartChestGenerations();
                            });
                        });
                    });
                });
            });
        }
        else
        {
            ShowNoGemsCoroutine(1.5f);
        }

    }
    public void ShowNoGemsCoroutine(float time)
    {
        StartCoroutine(ShowNoGemsPanelCoroutine(time));
    }
    IEnumerator ShowNoGemsPanelCoroutine(float time)
    {
        ShowNoGemsPanel();
        yield return new WaitForSeconds(time);
        HideNoGemsPanel();
    }

    private void ShowNoGemsPanel()
    {
        noGemsPanel.transform.DOLocalMoveY(0f, 1f).SetEase(Ease.OutBack);
    }
    private void HideNoGemsPanel()
    {
        noGemsPanel.transform.DOLocalMoveY(1000f, 1f).SetEase(Ease.OutBack);
    }
    public void HideRoulette()
    {
        StopDoublePriceCoroutine();
        rouletteOn = false;
        HidePricePanel();
        roulettePanel.transform.DOLocalMoveX(-840f, 0.75f).SetEase(Ease.OutBack);
        rouletteTitle.transform.DOLocalMoveX(3000f, 0.75f).SetEase(Ease.OutBack);
        chest1.transform.DOLocalMoveX(2100f, 0.75f).SetEase(Ease.OutBack);
        chest2.transform.DOLocalMoveX(-2110f, 0.75f).SetEase(Ease.OutBack);
        chest3.transform.DOLocalMoveX(2110f, 0.75f).SetEase(Ease.OutBack);
        _chestManager.CloseChests();
    }
    public void StopDoublePriceCoroutine()
    {
        if (doublePricePanelCoroutine != null)
        {
            StopCoroutine(doublePricePanelCoroutine);
        }
    }
    public void ShowPricePanel()
    {
        StartCoroutine(ShowPricePanelCoroutine(1));
    }

  IEnumerator ShowPricePanelCoroutine(float time)
    {

        yield return new WaitForSeconds(time);
        _pricePanelOn = true;
        pricePanel.transform.DOLocalMoveY(0f, 1f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            if (_chestManager.priceAmount > 0)
            {
                ShowDoublePricePanel();
            }
            else
            {
                backButton.enabled = true;
            }

        });
    }

    public void HidePricePanel()
    {
        _pricePanelOn = false;
        pricePanel.transform.DOLocalMoveY(1000f, 1f).SetEase(Ease.OutBack);
    }
}

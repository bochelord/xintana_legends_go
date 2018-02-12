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
    public GameObject startRoulettePanel;
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
    public Text scorePlayer;
    public Text worldReached;
    public Text fightsNumber;
    public Image screenshotImage;
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
    public Text prizeText;
    public GameObject rerollButton;

    private AnalyticsManager _analyticsManager;
    private AdsManager _adsManager;
    private SIS.ShopManager _shopManager;
    private LevelManager _levelManager;
    private ChestRoulette _chestManager;
    private ScreenShot _screenshot;

    private float _timerCountdown = 5f;
    private bool timerCountdownAdOn = false;
    private bool timerContdownContinue = false;
    private bool _scorePanelOn = false;
    private bool _doubleScorePanelOn = false;
    private bool _prizePanelOn = false;
    private bool _doublePrize = false;
    private bool _mainMenu = false;
    private bool rouletteOn = false;
    private bool _updateScore = false;
    private bool _spawnPrize = false;

    private int _pDoublePrize = 0;
    private float _prizeSpawnTime = 0;
    private int _pScorePlayer;
    private int _pHighScore;
    private int _pWorldReached;
    private int _pFightsNumber;
    private int _coinsToSpawn = 20;
    private int _coinsSpawned = 0;

    private Coroutine gameOverPanelCoroutine;
    private Coroutine doublePricePanelCoroutine;
    private void Awake()
    {
        _analyticsManager = FindObjectOfType<AnalyticsManager>();
        _levelManager = FindObjectOfType<LevelManager>();
        _shopManager = FindObjectOfType<SIS.ShopManager>();
        _adsManager = FindObjectOfType<AdsManager>();
        _chestManager = FindObjectOfType<ChestRoulette>();
        _screenshot = FindObjectOfType<ScreenShot>();

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
        if (_prizePanelOn)
        {
            UpdatePricePanel();
        }
        if (rouletteOn)
        {
            gemsTextRoulette.text = Rad_SaveManager.profile.gems.ToString();
        }
        if (_doublePrize && _chestManager.prizeType == chestType.coins)
        {
            prizeText.text = _pDoublePrize.ToString() + " Coins !!";
        }
        else if (_doublePrize &&_chestManager.prizeType == chestType.gems)
        {
            prizeText.text = _pDoublePrize.ToString() + " Gems !!";
        }
        if (_mainMenu)
        {
            gemsText.text = Rad_SaveManager.profile.gems.ToString();
        }

        if(scoreText && _updateScore)
        {
            scoreText.text = _levelManager.GetPlayerScoreUI().ToString();
        }
        if (_spawnPrize)
        {
            _prizeSpawnTime += Time.deltaTime;
            if(_prizeSpawnTime > 0.1f && _coinsSpawned < _coinsToSpawn)
            {
                _coinsSpawned++;
                _prizeSpawnTime = 0;
                SpawnPrizeUI();
            }
            else if(_coinsSpawned > _coinsToSpawn)
            {
                _coinsSpawned = 0;
                _spawnPrize = false;
            }
        }
    }

    private void UpdatePricePanel()
    {
        if (prizeText && _chestManager.prizeType == chestType.coins)
        {
            prizeText.text = _chestManager.prizeAmount.ToString() + " Coins !!";
        }
        else if (prizeText && _chestManager.prizeType == chestType.gems)
        {
            prizeText.text = _chestManager.prizeAmount.ToString() + " Gems !!";
        }else if(prizeText && _chestManager.prizeType == chestType.empty)
        {
            prizeText.text = "  You chose poorly!!";
        }
    }
    private void UpdateScorePanelUI()
    {
        if (pScorePlayer)
        {
            pScorePlayer.text = _pScorePlayer.ToString();
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
        if (SIS.DBManager.GetPurchase("si_x2") > 0)
        {
            doubleScoreIcon.SetActive(true);
        }
        else
        {
            doubleScoreIcon.SetActive(false);
        }

        if (SIS.DBManager.GetPurchase("si_1up") > 0)
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
            HideDoublePrizePanel();
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
        int _tempScore = (int)_levelManager.GetPlayerScore();
        scorePlayer.text = _tempScore.ToString();
        worldReached.text = "World " + _levelManager.GetCurrentWorldNumber().ToString();
        fightsNumber.text = "Fight " + (_levelManager.GetTotalEnemyKilled()+1).ToString() ; // This is plus one cause the current fight that player lost also counts although he didn't kill the enemy...

        screenshotImage.sprite = _screenshot.GetScreenshot();

        //screenshotImage.sprite = Sprite.Create(_screenshot.tex, new Rect(0.0f, 0.0f, _screenshot.tex.width, _screenshot.tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        screenshotImage.preserveAspect = true;
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
            if (SIS.DBManager.GetPurchase("si_x2") > 0)
            {
                x2Text.SetActive(true);
                x2Text.transform.DOShakeScale(2, 0.5f, 2, 25, true);
                DOTween.To(() => _pScorePlayer, x => _pScorePlayer = x, (int)_levelManager.GetPlayerScore() * 2, 1f);
                SIS.DBManager.RemovePurchase("si_x2");
                SIS.DBManager.RemovePurchaseUI("si_x2");
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
        _spawnPrize = false;
        _coinsSpawned = 0;
        doublePricePanel.SetActive(true);
        _doubleScorePanelOn = true;
        doublePricePanel.transform.DOLocalMoveY(0f, 1f).SetEase(Ease.OutBack);
        //pricePanel.SetActive(false);
    }
    public void Button_DoublePrize()
    {
        _adsManager.ShowAdForDoublePrize();
    }
    public void DoublePrize()
    {
        StartCoroutine(DoublePrizeFromAd());
    }
    IEnumerator DoublePrizeFromAd()
    {
        _prizePanelOn = false;
        _doublePrize = true;
        DOTween.To(() =>_pDoublePrize, x => _pDoublePrize = x,(int)_chestManager.prizeAmount * 2, 1f );
        yield return new WaitForSeconds(1);
        _chestManager.UpdateDoublePrize();
        _doublePrize = false;
        backButton.enabled = true;
    }

    public void HideDoublePrizePanel()
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
        HideDoublePrizePanel();
        Rad_SaveManager.profile.adsSkipped++;
        backButton.enabled = true;
    }

    public void Button_ShowRoulettePanel()
    {
        backButton.enabled = false;
        rouletteOn = true;
        if (Rad_SaveManager.profile.gems > 0)
        {
            StartRoulettePanel();
        }
        else
        {
            ShowNoGemsCoroutine(1.5f);
        }

    }
    private void StartRoulettePanel()
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
                            ShowStartRoulettePanel();
                        });
                    });
                });
            });
        });
    }
    public void ShowStartRoulettePanel()
    {
        startRoulettePanel.transform.DOLocalMoveY(0f, 1f).SetEase(Ease.OutBack);
    }
    public void HideStartRoulettePanel()
    {
        startRoulettePanel.transform.DOLocalMoveY(1000f, 1f).SetEase(Ease.OutBack);
    }
    public void Button_StartRoulettePanel()
    {
        _chestManager.RestartChestGenerations();
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
    public void ShowPrizePanel()
    {
        StartCoroutine(ShowPricePanelCoroutine(1));
    }

  IEnumerator ShowPricePanelCoroutine(float time)
    {
        if(Rad_SaveManager.profile.gems > 0)
        {
            rerollButton.SetActive(true);
        }
        else
        {
            rerollButton.SetActive(false);
        }
        yield return new WaitForSeconds(time);
        _prizePanelOn = true;
        pricePanel.transform.DOLocalMoveY(0f, 1f).SetEase(Ease.OutBack).OnComplete(() =>
        {

            if (_chestManager.prizeAmount > 0)
            {
                ShowDoublePricePanel();
            }
            else
            {
                backButton.enabled = true;
            }
            _chestManager.UpdatePlayerData();
            _spawnPrize = true;
        });
    }
    public void SetCointToSpawn(int value)
    {
        _coinsToSpawn = value;
    }
    private void SpawnPrizeUI()
    {
        switch (_chestManager.prizeType)
        {
            case chestType.coins:
                _chestManager.SpawnCoinAndMoveItToEndPosition();
                break;
            case chestType.gems:
                _chestManager.SpawnGemAndMoveItToEndPosition();
                break;
        }
    }
    public void HidePricePanel()
    {
        _prizePanelOn = false;
        pricePanel.transform.DOLocalMoveY(1000f, 1f).SetEase(Ease.OutBack);
    }
    public void SetSpawnPrize(bool value)
    {
        _spawnPrize = value;
    }
}

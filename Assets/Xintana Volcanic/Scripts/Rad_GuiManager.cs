using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Rad_GuiManager : MonoBehaviour {

    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject scorePanel;
    public GameObject shop;
    public GameObject viewAdPanel;
    public GameObject ContinueNoAdPanel;
    public GameObject doublePricePanel;
    public GameObject roulettePanel;
    public GameObject pricePanel;
    public GameObject noGemsPanel;
    public GameObject startRoulettePanel;
    [Header("Sliders")]
    public Slider experienceSlider;
    public Slider powerUpSlider;
   // public Image powerUpFill;
    [Header("PlaceHolders")]
    public Transform midScreen;
    public Transform gemPosition;
    [Header("Text")]
    public Text enemyText;
    public Text scoreText;
    public Text highScoreText;
    public Text worldText;
    public Text gemsText;
    public Text playerLevelText;
    public Text enemyLevelText;
    [Header("Icons")]
    public GameObject doubleScoreIcon;
    public GameObject extraLifeIcon;
    [Header ("Score Panel")]
    public Text pScorePlayer;
    public Text highScorePlayer;
    public Text pWorldReached;
    public Text pFightsNumber;
    public Slider expScoreSlider;
    public Text levelScoreText;
    public Text attackScoreValue;
    public Text attackIncrease;
    public Text hpValue;
    public Text hpIncrease;
    public Text shareText;
    public GameObject shareButton;
    public GameObject x2Text;
    public GameObject HighScoreFxPrefab;
    public Button closeButton;
    [Header("Share Panel")]
    public Text scorePlayer;
    public Text worldReached;
    public Text fightsNumber;
    public Text levelPlayerText;
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
    public Button closePrizePanel;
    [Header("Main Menu")]
    public Text attackValue;
    public Text highscoreMainMenuText;
    public Text hpText;
    public Text levelText;
    public Text expText;
    public Slider expSlider;
    public GameObject[] itemsImages;
    [Header("Xintana Weapons Images")]
    public GameObject redXintana;
    public GameObject greenXintana;
    public GameObject blueXintana;
    public GameObject yellowXintana;
    public GameObject blackXintana;
    [Header("Power Up")]
    public GameObject powerUpButton;
    private AnalyticsManager _analyticsManager;
    private AdsManager _adsManager;
    private SIS.ShopManager _shopManager;
    private LevelManager _levelManager;
    private ChestRoulette _chestManager;
    private ScreenShot _screenshot;
    private PlayerManager _playerManager;

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
    private bool _changeBrithness = false;
    private bool _powerUpOn = false;

    private float _tempBrightness;
    private int _pDoublePrize = 0;
    private float _prizeSpawnTime = 0;
    private int _pScorePlayer;
    private int _pHighScore;
    private int _pWorldReached;
    private int _pFightsNumber;
    private int _coinsToSpawn = 20;
    private int _coinsSpawned = 0;
    private int _tempLevel;
    private float _attackValue;
    private float _hpValue;
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
        _playerManager = FindObjectOfType<PlayerManager>();

    }

    private void Start()
    {
        _tempLevel = _playerManager.level; //conflicts with timing
        float _tempExp = Rad_SaveManager.profile.experience;
        AddExperienceToSlider(_tempExp);
        if(Rad_SaveManager.profile.weaponEquipped != WeaponType.red)
        {
            powerUpSlider.gameObject.SetActive(true);
        }
       UpdatePowerUpColorSlider();
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

        if (playerLevelText)
        {
            playerLevelText.text = _playerManager.level.ToString();
        }

        if(enemyLevelText && _levelManager.enemyController)
        {
            enemyLevelText.text = _levelManager.enemyController.level.ToString();
        }
        //TODO TODO :: Check the code below since the powerUpFill does not exist on the Canvas. Probably we need to make a bar

        //if (powerUpFill.GetComponent<_2dxFX_HSV>() && _changeBrithness)
        //{
        //    powerUpFill.GetComponent<_2dxFX_HSV>()._ValueBrightness = _tempBrightness;
        //}

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
        if (highScorePlayer)
        {
            highScorePlayer.text = _pHighScore.ToString();
        }
        if (levelScoreText)
        {
            levelScoreText.text = "Level " + _playerManager.level.ToString();
        }

        if (attackScoreValue)
        {
            attackScoreValue.text = _playerManager.attack.ToString("f2");
        }
        if (hpValue)
        {
            hpValue.text = _playerManager.GetMaxLife().ToString("f2");
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

    void UpdatePowerUpColorSlider()
    {
        switch (_playerManager.weaponEquipped)
        {
            //case WeaponType.blac:
            //    powerUpSlider.fillRect.GetComponent<Image>().color = new Color(0, 0, 0);
            //    break;
            //case WeaponType.blue:
            //    powerUpSlider.fillRect.GetComponent<Image>().color = new Color(0, 0, 255);
            //    break;
            //case WeaponType.green:
            //    powerUpSlider.fillRect.GetComponent<Image>().color = new Color(0, 255, 0);
            //    break;
            case WeaponType.red:
                powerUpSlider.transform.parent.transform.DOLocalMoveY(-500, 0, false);
                break;
            //case WeaponType.yellow:
            //    powerUpSlider.fillRect.GetComponent<Image>().color = new Color(255, 255, 0);
            //    break;
        }
    }

    /// <summary>
    /// turns de game over panel on
    /// </summary>
    public void CloseScorePanelAndMainMenuOn()
    {

        _scorePanelOn = false;
        SetMainMenuStats();
        _mainMenu = true;
        scorePanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        mainMenuPanel.transform.DOLocalMoveX(0f, 1f).SetEase(Ease.OutBack);
        if (_doubleScorePanelOn)
        {
            HideDoublePrizePanel();
        }
        if(gameOverPanelCoroutine != null)
        {
            StopCoroutine(gameOverPanelCoroutine);
        }
        CheckInventory();
    }

    public void PlayerGameOverPanelOn()
    {
        scorePanel.SetActive(true);
        closeButton.gameObject.SetActive(false);
        gameOverPanelCoroutine = StartCoroutine(FillGameOverPanel());


    }


    /// <summary>
    /// Turns de Game over panell off
    /// </summary>
    public void GameOverPanelOff()
    {
        //gmeOverPanel.transform.DOLocalMoveX(663f, 1f);
        mainMenuPanel.transform.localPosition = new Vector3(663f, 0, 0);
        mainMenuPanel.SetActive(false);
        
    }
    /// <summary>
    /// fill all the info needed in the Share Panel
    /// </summary>
    public void FillSharePanel()
    {
        int _tempScore = (int)_levelManager.GetPlayerScore();
        scorePlayer.text = _tempScore.ToString();
        worldReached.text = "World " + _levelManager.GetCurrentWorldNumber().ToString();
        fightsNumber.text = "Fight " + (_levelManager.GetTotalEnemyKilled()).ToString() ; // This is plus one cause the current fight that player lost also counts although he didn't kill the enemy...
        levelPlayerText.text = "Level " + _playerManager.level.ToString();
        screenshotImage.sprite = _screenshot.GetScreenshot();

        //screenshotImage.sprite = Sprite.Create(_screenshot.tex, new Rect(0.0f, 0.0f, _screenshot.tex.width, _screenshot.tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        screenshotImage.preserveAspect = true;
    }

    /// <summary>
    /// called from button
    /// </summary>
    public void Button_CloseViewAddPanel()
    {
        _levelManager.adsSkipped++;
        timerCountdownAdOn = false;
        _timerCountdown = 5;
        viewAdPanel.transform.DOLocalMoveY(1000f, 1f).SetEase(Ease.OutBack);
        viewAdPanel.SetActive(false);
        Rad_SaveManager.profile.adsSkipped++;
        _levelManager.VanishPlayer();
        StartCoroutine(FunctionLibrary.CallWithDelay(PlayerGameOverPanelOn,2f));
        _analyticsManager.ResurrectionAd_Event(false);

    }

    public void Button_ShowAdExtraLife()
    {
        timerCountdownAdOn = false;
        _adsManager.ShowAdForExtraLife();
    }

    private IEnumerator refreshHighScore(int newHighScore) {
        yield return new WaitForSeconds(0.60f);
        _pHighScore = 0;
        DOTween.To(() => _pHighScore, x => _pHighScore = x, newHighScore, 1f);
    }

    IEnumerator FillGameOverPanel()
    {

        _scorePanelOn = true;
        StartCoroutine(refreshHighScore(Rad_SaveManager.profile.highscore));
        expScoreSlider.value = Rad_SaveManager.profile.experience / ((_playerManager.level + 1)*_playerManager.GetExperienceToLevelUp());
        _pScorePlayer = 0;
        x2Text.SetActive(false);
        scorePanel.transform.DOLocalMoveX(0f, 0.5f).SetEase(Ease.OutBack);
        yield return new WaitForSeconds(0.5f);
        DOTween.To(() => _pScorePlayer, x => _pScorePlayer = x, (int)_levelManager.GetPlayerScore(), 0.5f).OnComplete(()=> 
        {
            if (SIS.DBManager.GetPurchase("si_x2") > 0)
            {
                x2Text.SetActive(true);
                x2Text.transform.DOShakeScale(2, 0.5f, 2, 25, true);
                DOTween.To(() => _pScorePlayer, x => _pScorePlayer = x, (int)_levelManager.GetPlayerScore() * 2, 0.5f);
                SIS.DBManager.RemovePurchase("si_x2");
                SIS.DBManager.RemovePurchaseUI("si_x2");
                Rad_SaveManager.profile.doubleScore = false;
                _levelManager.SetPlayerScore((int)_levelManager.GetPlayerScore() * 2);
            }
        });

        yield return new WaitForSeconds(1f);
        if(Rad_SaveManager.profile.highscore< _levelManager.GetPlayerScore())
        {
            Rad_SaveManager.profile.highscore = (int)_levelManager.GetPlayerScore();
            Rad_SaveManager.SaveData();
            GameObject clone_HighScoreFxPrefab;
            clone_HighScoreFxPrefab = Instantiate(HighScoreFxPrefab);
            clone_HighScoreFxPrefab.transform.SetParent(scorePanel.transform);
            clone_HighScoreFxPrefab.transform.position = highScorePlayer.transform.position;
            StartCoroutine(refreshHighScore(Rad_SaveManager.profile.highscore));
        }
        DOTween.To(() => _pWorldReached, x => _pWorldReached = x, _levelManager.GetCurrentWorldNumber(), 0.5f).OnComplete(()=> 
        {
            DOTween.To(() => _pFightsNumber, x => _pFightsNumber = x, _levelManager.GetTotalEnemyKilled() , 0.5f).OnComplete(()=> 
            {
                UpdateExperience();
            });
        });
    }

    private void UpdateExperience()
    {
        if(_playerManager.GetTotalExpPerGame() + Rad_SaveManager.profile.experience >= _playerManager.GetExperienceToLevelUp())
        {
            
            DOTween.To(() => expScoreSlider.value, x => expScoreSlider.value = x, 1 , 0.51f).OnComplete(() =>
            {
                DOTween.To(() => expScoreSlider.value, x => expScoreSlider.value = x, 0, 0f).OnComplete(() =>
                {
                    _attackValue = _playerManager.attack;
                    _hpValue = _playerManager.GetMaxLife();
                    _playerManager.LevelUpAndUpdateExperience();
                    IncreaseStatsValue();
                    StartCoroutine(FunctionLibrary.CallWithDelay(UpdateExperience, 0.5f));
                });   

            });
        }else if (!_playerManager.GetExperienceAddedFromProfile())
        {
            float _tempValue = (_playerManager.GetTotalExpPerGame() + Rad_SaveManager.profile.experience) / _playerManager.GetExperienceToLevelUp();
            DOTween.To(() => expScoreSlider.value, x => expScoreSlider.value = x, _tempValue, 0.5f);
            SetScoreButtonsOn();
            _playerManager.SavePlayerStats();
            Rad_SaveManager.SaveData();
        }
        else
        {
            float _tempValue = _playerManager.GetTotalExpPerGame() / _playerManager.GetExperienceToLevelUp();
            DOTween.To(() => expScoreSlider.value, x => expScoreSlider.value = x, _tempValue, 0.5f);
            SetScoreButtonsOn();
            _playerManager.SavePlayerStats();
            Rad_SaveManager.SaveData();
        }


        return;

    }
    private void SetScoreButtonsOn()
    {
        Debug.Log("sdfasdfasdfas>>>>>>>>>>>>>>>>>>>");
        closeButton.gameObject.SetActive(true);
        shareButton.SetActive(true);
        shareText.DOFade(0, 1f).OnComplete(() => 
        {
            shareText.DORestart();
        });
    }

    private void IncreaseStatsValue()
    {
        attackIncrease.gameObject.SetActive(true);
        attackIncrease.text = "+" + (_playerManager.attack - _attackValue).ToString("f2");
        hpIncrease.gameObject.SetActive(true);
        hpIncrease.text = "+" + (_playerManager.GetMaxLife() - _hpValue).ToString("f2");
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
        CheckInventory();
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

    public void ShowDoublePrizePanel()
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
        _coinsToSpawn = _chestManager.prizeAmount;
        _spawnPrize = true;
        _prizePanelOn = false;
        _doublePrize = true;
        DOTween.To(() =>_pDoublePrize, x => _pDoublePrize = x,(int)_chestManager.prizeAmount * 2, 1f );
        yield return new WaitForSeconds(1);
        _chestManager.UpdateDoublePrize();
        _doublePrize = false;
        _coinsToSpawn = 0;
        _spawnPrize = false;
    }

    public void HideDoublePrizePanel()
    {
        //pricePanel.SetActive(true);
        doublePricePanel.transform.DOLocalMoveY(1000f, 1f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            closePrizePanel.enabled = true;
            _doubleScorePanelOn = false;
            doublePricePanel.SetActive(false);

        });

    }
    public void Button_HideDoublePricePanel()
    {
        _analyticsManager.DoublePrizeAd_Event(false);
        HideDoublePrizePanel();
        Rad_SaveManager.profile.adsSkipped++;
    }

    public void Button_ShowRoulettePanel()
    {

        if (Rad_SaveManager.profile.gems > 0)
        {
            backButton.enabled = false;
            rouletteOn = true;
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
            ShowStartRoulettePanel();
        });
    }

    public void ChestPresentation()
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

        yield return new WaitForSeconds(time);

        _prizePanelOn = true;
        pricePanel.transform.DOLocalMoveY(0f, 1f).SetEase(Ease.OutBack).OnComplete(() =>
        {

            if (_chestManager.prizeAmount > 0)
            {
                closePrizePanel.enabled = false; // we wait for double prize ad
                ShowDoublePrizePanel();
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
    public void Button_HidePricePanel()
    {
        backButton.enabled = true;
        _prizePanelOn = false;
        pricePanel.transform.DOLocalMoveY(1000f, 1f).SetEase(Ease.OutBack);

    }
    public void Button_CheckRerollButton()
    {
        if (Rad_SaveManager.profile.gems > 0)
        {
            rerollButton.SetActive(true);
        }
        else
        {
            rerollButton.SetActive(false);
        }
    }
    public void SetSpawnPrize(bool value)
    {
        _spawnPrize = value;
    }

    public void SetMainMenuStats()
    {
        attackValue.text = _playerManager.attack.ToString("f2");
        highscoreMainMenuText.text = Rad_SaveManager.profile.highscore.ToString();
        hpText.text = _playerManager.GetMaxLife().ToString("f2");
        levelText.text = "Level " + _playerManager.level.ToString();

        float _tempValue = _playerManager.experience / _playerManager.GetExperienceToLevelUp();
        expSlider.value =  _tempValue; 

        //Debug.Log(_playerManager.experience);
        //Debug.Log(experienceSlider.value);
        //Debug.Log(_playerManager.GetMaxExperience());
        //Debug.Log(_playerManager.experience / _playerManager.GetMaxExperience());
    }

    public void AddPowerUpSlider(float value)
    {
        if (!_powerUpOn)
        {
            float _tempValue = value / 10;
            if (powerUpSlider.value + _tempValue >= 1 && powerUpSlider.value != 1)
            {
                _changeBrithness = true;
                //TODO add poer Up button
                //add some tweens, shakes and effects
                PowerUpSliderBrightness();
                powerUpSlider.value = 1;
                //we will reset the value once the player use the power up
                if (_playerManager.weaponEquipped != WeaponType.red)
                {
                    ShowPowerUpButton();
                }

            }
            else
            {
                powerUpSlider.value += _tempValue;
            }
        }


    }

    private void PowerUpSliderBrightness()
    {
        DOTween.To(() => _tempBrightness, x => _tempBrightness = x, 1f, 0.5f).OnComplete(() =>
        {
            DOTween.To(() => _tempBrightness, x => _tempBrightness = x, 2f, 0.5f).OnComplete(() =>
            {
                PowerUpSliderBrightness();
            });
        });
    }

    public void RemovePowerUpSliderValueForPowerUpTime(float time)
    {
        DOTween.To(() => powerUpSlider.value, x => powerUpSlider.value = x, 0, time).OnComplete(()=> 
        {
            HidePowerUpButton();
        });
    }
    public void Button_PowerUp()
    {
        if(_levelManager.state == GameState.Running)
        {
            _playerManager.StartPowerUp();
        }
    }
    void ShowPowerUpButton()
    {
        powerUpButton.transform.DOLocalMoveY(180,2).SetEase(Ease.OutBack);
        powerUpButton.transform.DOScale(2, 2);
    }
    public void HidePowerUpButton()
    {
        powerUpButton.transform.DOLocalMoveX(-900, 1).SetEase(Ease.OutBack);
        powerUpButton.transform.DOScale(0.5f, 1);
    }
    public void AddExperienceToSlider(float value)
    {
        float _tempValue = value / ((_tempLevel + 1) * _playerManager.pointsPerLevel);

        if (experienceSlider.value + _tempValue >= 1)
        {
            float _tempRestValue = 1 - experienceSlider.value;
            _tempValue -= _tempRestValue;
            experienceSlider.value = _tempValue;
            _tempLevel++;
        }
        else
        {
            experienceSlider.value += _tempValue;
        }
    }
    public void CheckInventory()
    {
        CheckXintanaWeapon();
        for (int i = 0; i < itemsImages.Length; i++)
        {
            InventorySlot item = itemsImages[i].GetComponent<MainMenuInventory>().item;
            if(SIS.DBManager.GetPurchase("si_1up") > 0 && item == InventorySlot.ExtraLife)
            {
                itemsImages[i].SetActive(true);
            }
            else if (SIS.DBManager.GetPurchase("si_x2") > 0 && item == InventorySlot.DoubleScore)
            {
                itemsImages[i].SetActive(true);
            }
            else if (SIS.DBManager.GetPurchase("si_noads") > 0 && item == InventorySlot.NoAds)
            {
                itemsImages[i].SetActive(true);
            }
            else if (SIS.DBManager.GetPurchase("si_supportdevs") > 0 && item == InventorySlot.EthernalGrattitude)
            {
                itemsImages[i].SetActive(true);
            }
        }
    }
    public void CheckXintanaWeapon()
    {

        switch (Rad_SaveManager.profile.weaponEquipped)
        {
            case WeaponType.black:
                redXintana.SetActive(false);
                greenXintana.SetActive(false);
                blueXintana.SetActive(false);
                yellowXintana.SetActive(false);
                blackXintana.SetActive(true);
                break;
            case WeaponType.blue:
                redXintana.SetActive(false);
                greenXintana.SetActive(false);
                blueXintana.SetActive(true);
                yellowXintana.SetActive(false);
                blackXintana.SetActive(false);
                break;
            case WeaponType.green:
                redXintana.SetActive(false);
                greenXintana.SetActive(true);
                blueXintana.SetActive(false);
                yellowXintana.SetActive(false);
                blackXintana.SetActive(false);
                break;
            case WeaponType.red:

                redXintana.SetActive(true);
                greenXintana.SetActive(false);
                blueXintana.SetActive(false);
                yellowXintana.SetActive(false);
                blackXintana.SetActive(false);
                break;
            case WeaponType.yellow:
                redXintana.SetActive(false);
                greenXintana.SetActive(false);
                blueXintana.SetActive(false);
                yellowXintana.SetActive(true);
                blackXintana.SetActive(false);
                break;
        }

    }
    public void Button_EquipRedWeapon()
    {
        Rad_SaveManager.profile.weaponEquipped = WeaponType.red;
        CheckXintanaWeapon();
    }
    public void Button_EquipYellowWeapon()
    {
        Rad_SaveManager.profile.weaponEquipped = WeaponType.yellow;
        CheckXintanaWeapon();
    }
    public void Button_EquipGreenWeapon()
    {
        Rad_SaveManager.profile.weaponEquipped = WeaponType.green;
        CheckXintanaWeapon();
    }
    public void Button_EquipBlueWeapon()
    {
        Rad_SaveManager.profile.weaponEquipped = WeaponType.blue;
        CheckXintanaWeapon();
    }
    public void Button_EquipBlackWeapon()
    {
        Rad_SaveManager.profile.weaponEquipped = WeaponType.black;
        CheckXintanaWeapon();
    }

    public void SetPowerUpOn(bool value)
    {
        _powerUpOn = value;
    }
}

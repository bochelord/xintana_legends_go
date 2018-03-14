using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Rad_GuiManager : MonoBehaviour {

    [Header("Panels")]
    public GameObject scorePanel;
    public GameObject viewAdPanel;
    public GameObject ContinueNoAdPanel;

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
    public Text expScoreText;
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

    [Header("Power Up")]
    public GameObject powerUpButton;
    public Text PowerUpText;

    private AnalyticsManager _analyticsManager;
    private AdsManager _adsManager;
    private SIS.ShopManager _shopManager;
    private LevelManager _levelManager;
    private ScreenShot _screenshot;
    private PlayerManager _playerManager;
    private AudioManager _audioManager;

    private float _timerCountdown = 5f;
    private float _timerPowerUp = 1f;
    private float _powerUpTime = 1f;
    private bool timerCountdownAdOn = false;
    private bool timerContdownContinue = false;
    private bool _scorePanelOn = false;


    private bool _updateScore = false;
    private bool _powerUpOn = false;

    private int _pScorePlayer;
    private int _pHighScore;
    private int _pWorldReached;
    private int _pFightsNumber;

    private int _tempLevel;
    private float _attackValue;
    private float _hpValue;
    private AndroidLeaderboard _leaderboardManager;
    private Coroutine gameOverPanelCoroutine;

    private void Awake()
    {
        _analyticsManager = FindObjectOfType<AnalyticsManager>();
        _levelManager = FindObjectOfType<LevelManager>();
        _shopManager = FindObjectOfType<SIS.ShopManager>();
        _adsManager = FindObjectOfType<AdsManager>();
        _screenshot = FindObjectOfType<ScreenShot>();
        _playerManager = FindObjectOfType<PlayerManager>();
        _audioManager = FindObjectOfType<AudioManager>();
        _leaderboardManager = FindObjectOfType<AndroidLeaderboard>();
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
        if (_powerUpOn && _levelManager.state == GameState.Running)
        {
            _timerPowerUp -= (Time.deltaTime / _powerUpTime);
            powerUpSlider.value = _timerPowerUp;
            if(_timerPowerUp <= 0)
            {
                _playerManager.StopPowerUp();
                _powerUpOn = false;
                _timerPowerUp = 1f;
                powerUpSlider.fillRect.GetComponent<Image>().color = new Color(255, 255, 255);
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



        if(scoreText && _updateScore)
        {
            scoreText.text = _levelManager.GetPlayerScoreUI().ToString();
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
        if (expScoreText)
        {
            //TODO MAKE IT DYNAMIC -> tweenable
            expScoreText.text = _playerManager.experience.ToString() + "/" + _playerManager.GetExperienceToLevelUp().ToString();
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
        if (_playerManager)
        {
            switch (_playerManager.weaponEquipped)
            {
                case WeaponType.black:
                    powerUpSlider.fillRect.GetComponent<Image>().color = new Color(0, 0, 0);
                    break;
                case WeaponType.blue:
                    powerUpSlider.fillRect.GetComponent<Image>().color = new Color(255, 255, 255);
                    powerUpSlider.fillRect.GetComponent<_2dxFX_ColorChange>()._HueShift = 131;
                    break;
                case WeaponType.green:
                    powerUpSlider.fillRect.GetComponent<Image>().color = new Color(255, 255, 255);
                    powerUpSlider.fillRect.GetComponent<_2dxFX_ColorChange>()._HueShift = 190;
                    break;
                case WeaponType.red:
                    powerUpSlider.fillRect.GetComponent<Image>().color = new Color(255, 255, 255);
                    powerUpSlider.transform.parent.transform.DOLocalMoveY(-500, 0, false);
                    break;
                case WeaponType.yellow:
                    powerUpSlider.fillRect.GetComponent<Image>().color = new Color(255, 255, 255);
                    powerUpSlider.fillRect.GetComponent<_2dxFX_ColorChange>()._HueShift = 303;
                    break;
            }
        }

    }

    /// <summary>
    /// turns de game over panel on
    /// </summary>
    public void CloseScorePanelAndMainMenuOn()
    {
        //menuCanvas.SetActive(true);
        //_scorePanelOn = false;
        //SetMainMenuStats();
        //_mainMenu = true;
        //scorePanel.SetActive(false);
        //mainMenuPanel.SetActive(true);
        //_audioManager.PlayMainMenuMusic();
        //mainMenuPanel.transform.DOLocalMoveX(0f, 1f).SetEase(Ease.OutBack).OnComplete(()=> 
        //{
        //    inGameCanvas.SetActive(false);
        //});
        //if (_doubleScorePanelOn)
        //{
        //    HideDoublePrizePanel();
        //}
        //if(gameOverPanelCoroutine != null)
        //{
        //    StopCoroutine(gameOverPanelCoroutine);
        //}
        //CheckInventory();
        SceneManager.LoadScene("XintanaLegendsGo_Menu");

    }

    public void PlayerGameOverPanelOn()
    {
        scorePanel.SetActive(true);
        closeButton.gameObject.SetActive(false);
        gameOverPanelCoroutine = StartCoroutine(FillGameOverPanel());

    }


    ///// <summary>
    ///// Turns de Game over panell off
    ///// </summary>
    //public void GameOverPanelOff()
    //{
    //    //gmeOverPanel.transform.DOLocalMoveX(663f, 1f);
    //    mainMenuPanel.transform.localPosition = new Vector3(663f, 0, 0);
    //    mainMenuPanel.SetActive(false);
        
    //}
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
                Rad_SaveManager.profile.doubleScorePurchased--;
                if(Rad_SaveManager.profile.doubleScorePurchased <= 0)
                {
                    SIS.DBManager.RemovePurchase("si_x2");
                    SIS.DBManager.RemovePurchaseUI("si_x2");
                    Rad_SaveManager.profile.doubleScore = false;
                }
                x2Text.SetActive(true);
                x2Text.transform.DOShakeScale(2, 0.5f, 2, 25, true);
                DOTween.To(() => _pScorePlayer, x => _pScorePlayer = x, (int)_levelManager.GetPlayerScore() * 2, 0.5f);
                _levelManager.SetPlayerScore((int)_levelManager.GetPlayerScore() * 2);
            }
        });

        yield return new WaitForSeconds(1f);
        if(Rad_SaveManager.profile.highscore< _levelManager.GetPlayerScore())
        {
            Rad_SaveManager.profile.highscore = (int)_levelManager.GetPlayerScore();
            Rad_SaveManager.SaveData();

            _leaderboardManager.OnAddScoreToLeaderBorad();

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
    //public void Button_OpenShop()
    //{

    //    //shopCanvas.SetActive(true);
    //    //shop.transform.DOLocalMoveX(0f, 0.75f).SetEase(Ease.OutBack);
    //    SceneManager.LoadScene("XintanaLegendsGo_Shop");

    //    shopCanvas.SetActive(true);
    //    menuCanvas.SetActive(false);
    //    shop.transform.DOLocalMoveX(0f, 0.75f).SetEase(Ease.OutBack);
    //}

    //public void Button_CloseShop()
    //{
    //    CheckInventory();
    //    shop.transform.DOLocalMoveX(840f, 0.75f).SetEase(Ease.OutBack).OnComplete(()=> 
    //    {
    //        shopCanvas.SetActive(false);
    //    });
    //    menuCanvas.SetActive(true);
    //}
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

    public void AddPowerUpSlider(float value)
    {
        if (!_powerUpOn)
        {
            float _tempValue = value / 10;
            if (powerUpSlider.value + _tempValue >= 1 && powerUpSlider.value != 1)
            {
                //TODO add poer Up button
                //add some tweens, shakes and effects
                PunchPowerUpSlider();
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

    private void PunchPowerUpSlider()
    {
        //DOTween.To(() => _tempBrightness, x => _tempBrightness = x, 1f, 0.5f).OnComplete(() =>
        //{
        //    DOTween.To(() => _tempBrightness, x => _tempBrightness = x, 2f, 0.5f).OnComplete(() =>
        //    {
        //        PowerUpSliderBrightness();
        //    });
        //});
        powerUpSlider.transform.parent.transform.DOPunchScale(new Vector3(0.05f, 0.05f, 0.05f),1,4,0.4f).OnComplete(() => 
        {
            powerUpButton.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 1, 2, 0.4f);
            powerUpSlider.transform.parent.transform.DORestart();
        });
    }

    public void RemovePowerUpSliderValueForPowerUpTime(float time)
    {
        _powerUpTime = time;
        _powerUpOn = true;
        //DOTween.To(() => powerUpSlider.value, x => powerUpSlider.value = x, 0, time).OnComplete(()=> 
        //{
        //    HidePowerUpButton();
        //    powerUpSlider.fillRect.GetComponent<Image>().color = new Color(255, 255, 255);
        //});
    }
    public void Button_PowerUp()
    {
        if(_levelManager.state == GameState.Running)
        {
            _playerManager.StartPowerUp();
            powerUpSlider.fillRect.GetComponent<Image>().color = new Color(0, 0, 255);
            powerUpSlider.transform.parent.transform.DOKill();
            powerUpButton.transform.DOKill();
            HidePowerUpButton();
        }
    }
    void ShowPowerUpButton()
    {
        powerUpButton.transform.DOLocalMoveY(180,2).SetEase(Ease.OutBack);
        powerUpButton.transform.DOScale(2, 2);
    }
    public void HidePowerUpButton()
    {
        powerUpButton.transform.DOLocalMoveY(-900, 1).SetEase(Ease.OutBack);
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
    public void SetAndActivatePowerUpText(string _value)
    {
        PowerUpText.gameObject.SetActive(true);
        PowerUpText.text = _value;
        PowerUpText.transform.DOPunchScale(new Vector3(1, 1, 1), 2, 5, 0).OnComplete(() =>
        {
            PowerUpText.gameObject.SetActive(false);
        });
    }
    public void SetPowerUpOn(bool value)
    {
        _powerUpOn = value;
    }
}

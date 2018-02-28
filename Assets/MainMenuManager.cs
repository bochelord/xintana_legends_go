using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    [Header("Panels")]
    public GameObject roulettePanel;
    public GameObject noShellsPanel;
    public GameObject freeShellpanel;
    public GameObject blockButtonsPanel;
    [Header("Main Menu")]
    public Text attackValue;
    public Text highscoreMainMenuText;
    public Text hpText;
    public Text levelText;
    public Text shellsText;
    public Text expText;
    public Text gemsText;
    public Slider expSlider;
    public GameObject[] itemsImages;
    [Header("Xintana Weapons Images")]
    public GameObject redXintana;
    public GameObject greenXintana;
    public GameObject blueXintana;
    public GameObject yellowXintana;
    public GameObject blackXintana;

    private bool _mainMenu = false;

    private ChestRoulette _chestManager;
    private AudioManager _audioManager;
    private AnalyticsManager _analyticsManager;
    private AdsManager _adsManager;
    private PlayerManager _playerManager;
    
    void Awake()
    {
        _chestManager = FindObjectOfType<ChestRoulette>();
        _analyticsManager = FindObjectOfType<AnalyticsManager>();
        _adsManager = FindObjectOfType<AdsManager>();
        _audioManager = FindObjectOfType<AudioManager>();
        _playerManager = FindObjectOfType<PlayerManager>();
    }
    void Start()
    {
        CheckInventory();
        SetMainMenuStats();
    }
    // Update is called once per frame
    void Update ()
    {
        if (_mainMenu)
        {
            gemsText.text = Rad_SaveManager.profile.gems.ToString();
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
    public void CheckInventory()
    {
        CheckXintanaWeapon();
        for (int i = 0; i < itemsImages.Length; i++)
        {
            InventorySlot item = itemsImages[i].GetComponent<MainMenuInventory>().item;
            if (SIS.DBManager.GetPurchase("si_1up") > 0 && item == InventorySlot.ExtraLife)
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
            else if (SIS.DBManager.GetPurchase("si_yellowsword") > 0 && item == InventorySlot.YellowWeapon)
            {
                itemsImages[i].SetActive(true);
            }
            else if (SIS.DBManager.GetPurchase("si_greensword") > 0 && item == InventorySlot.GreenWeapon)
            {
                itemsImages[i].SetActive(true);
            }
            else if (SIS.DBManager.GetPurchase("si_bluesword") > 0 && item == InventorySlot.BlueWeapon)
            {
                itemsImages[i].SetActive(true);
            }
            else if (SIS.DBManager.GetPurchase("si_blacksword") > 0 && item == InventorySlot.BlackWeapon)
            {
                itemsImages[i].SetActive(true);
            }
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

    public void Button_ShowRoulettePanel()
    {

        if (Rad_SaveManager.profile.shells > 0)
        {
            _chestManager.StartRoulettePanel();
        }
        else if (!_adsManager.GetFreeShellAdViewed())
        {
            ShowFreeShellPanel();
        }
        else
        {
            ShowNoShellsCoroutine(1.5f);
        }

    }
    public void Button_ShowFreeShellAd()
    {
        _adsManager.ShowAddForFreeShell();
    }
    public void Button_Shop()
    {
        SceneManager.LoadScene("XintanaLegendsGo_Shop");   
    }
    public void Button_Play()
    {
        SceneManager.LoadScene("combinationDisplay_safe_portrait");
    }

    public void ShowFreeShellPanel()
    {
        blockButtonsPanel.SetActive(true);
        freeShellpanel.transform.DOLocalMoveY(0f, 1f).SetEase(Ease.OutBack);
    }
    public void HideFreeShellPanel()
    {
        blockButtonsPanel.SetActive(false);
        freeShellpanel.transform.DOLocalMoveY(1000f, 1f).SetEase(Ease.OutBack);
    }

    public void HideRoulette()
    {
        _chestManager.StopDoublePriceCoroutine();
        _audioManager.PlayMainMenuMusic();
        roulettePanel.transform.DOLocalMoveX(-840f, 0.75f).SetEase(Ease.OutBack);
        _chestManager.rouletteTitle.transform.DOLocalMoveX(3000f, 0.75f).SetEase(Ease.OutBack);
        _chestManager.chest1.transform.DOLocalMoveX(2100f, 0.75f).SetEase(Ease.OutBack);
        _chestManager.chest2.transform.DOLocalMoveX(-2110f, 0.75f).SetEase(Ease.OutBack);
        _chestManager.chest3.transform.DOLocalMoveX(2110f, 0.75f).SetEase(Ease.OutBack);
        _chestManager.CloseChests();
        SetMainMenuStats();
    }
    public void SetMainMenuStats()
    {
        attackValue.text = _playerManager.attack.ToString("f2");
        highscoreMainMenuText.text = Rad_SaveManager.profile.highscore.ToString();
        hpText.text = _playerManager.GetMaxLife().ToString("f2");
        levelText.text = "Level " + _playerManager.level.ToString();
        shellsText.text = Rad_SaveManager.profile.shells.ToString();
        expText.text = _playerManager.experience.ToString() + "/" + _playerManager.GetExperienceToLevelUp().ToString();
        float _tempValue = _playerManager.experience / _playerManager.GetExperienceToLevelUp();
        expSlider.value = _tempValue;
    }

    public void ShowNoShellsCoroutine(float time)
    {
        StartCoroutine(ShowNoShellsPanelCoroutine(time));
    }
    IEnumerator ShowNoShellsPanelCoroutine(float time)
    {
        ShowNoShellsPanel();
        yield return new WaitForSeconds(time);
        HideNoShellsPanel();
    }

    private void ShowNoShellsPanel()
    {
        noShellsPanel.transform.DOLocalMoveY(0f, 1f).SetEase(Ease.OutBack);
    }
    private void HideNoShellsPanel()
    {
        noShellsPanel.transform.DOLocalMoveY(1000f, 1f).SetEase(Ease.OutBack);
    }
}

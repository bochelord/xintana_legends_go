using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum RouletteState { READY, SPIN, WAITING}
public class ChestRoulette : MonoBehaviour {

    public RouletteState state = RouletteState.READY;
    public Sprite[] available_chest_contents;
    public Rad_Chest[] chests;
    public Transform chestContainer;
    public int numChests;
    public bool chestRotate = false;
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
    public Text shellsTextRoulette;
    [Header("Price Panel")]
    public int prizeAmount;
    public GameObject coinsImage;
    public GameObject gemsImage;
    public GameObject weaponImage;
    public GameObject shellsImage;
    public PrizesListScriptableObject.PrizeListClass prizeType;
    public Transform coinsEndPosition;
    public Transform gemEndPositon;
    public Transform shellsEndPosition;
    public Text prizeText;
    public GameObject rerollButton;
    public GameObject goToShopButton;
    public Button closePrizePanel;
    [Header(" Prizes Scriptable Object")]
    public PrizesListScriptableObject prizesList;
    [Header("Panels")]
    public GameObject pricePanel;
    public GameObject doublePricePanel;
    public GameObject startRoulettePanel;
    
    private MainMenuManager _menuManager;
    private ParticlePooler _particlePooler;
    private CoinsPooler _coinsPooler;

    private List<Vector2> initialPosition = new List<Vector2>();

    private bool canOpen;
    private bool _spawnPrize = false;
    private bool _prizePanelOn = false;
    private bool _doublePrize = false;
    public bool rouletteOn = false;
    private bool _doubleScorePanelOn = false;
    private bool shellPrizeAdded = false;
    private int _coinsToSpawn = 20;
    private int _coinsSpawned = 0;
    private int _pDoublePrize = 0;

    private float _prizeSpawnTime = 0;
    private AnalyticsManager _analyticsManager;
    private AdsManager _adsManager;

    private Coroutine doublePricePanelCoroutine;
    // Use this for initialization
    void Start ()
    {

        
        _menuManager = FindObjectOfType<MainMenuManager>();
        _particlePooler = FindObjectOfType<ParticlePooler>();
        _coinsPooler = FindObjectOfType<CoinsPooler>();
        _analyticsManager = FindObjectOfType<AnalyticsManager>();
        _adsManager = FindObjectOfType<AdsManager>();
    }
	
    void Update()
    {
        if (_doublePrize && prizeType.categoryType == PrizeType.COINS)
        {
            prizeText.text = _pDoublePrize.ToString() + " Coins !!";
        }
        else if (_doublePrize && prizeType.categoryType == PrizeType.GEMS)
        {
            prizeText.text = _pDoublePrize.ToString() + " Gems !!";
        }
        else if (_doublePrize && prizeType.categoryType == PrizeType.SHELLS)
        {
            prizeText.text = _pDoublePrize.ToString() + " Shells !!";
        }
        if (rouletteOn)
        {
            shellsTextRoulette.text = Rad_SaveManager.profile.shells.ToString();
        }

        if (_prizePanelOn)
        {
            UpdatePricePanel();
        }
        if (_spawnPrize)
        {
            _prizeSpawnTime += Time.deltaTime;
            if (_prizeSpawnTime > 0.1f && _coinsSpawned < _coinsToSpawn)
            {
                _coinsSpawned++;
                _prizeSpawnTime = 0;
                SpawnPrizeUI();
            }
            else if (_coinsSpawned > _coinsToSpawn)
            {
                _coinsSpawned = 0;
                _spawnPrize = false;
            }
        }
    }

    public void RestartChestGenerations()
    {
        for (int i = 0; i < 3; i++)
        {
            initialPosition.Add(chests[i].transform.position);
            shellPrizeAdded = false;
        }

        if (Rad_SaveManager.profile.shells > 0)
        {
            GeneratePrizes();
            StartCoroutine(StartChestRotation(1.25f));
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="position">position where coin wll be spawned</param>
    public void SpawnCoinAndMoveItToEndPosition()
    {
        GameObject obj =_coinsPooler.GetPooledCoin();
        obj.transform.position = coinsImage.transform.position;
        obj.SetActive(true);
        obj.transform.DOMove(coinsEndPosition.position, 0.5f, false).OnComplete(() => 
        {
            AudioManager.Instance.Play_CoinCollect();
            SpawnCoinCollectedParticle(obj.transform.position);
            _coinsPooler.RemoveElement(obj.transform);
        });
    }

    public void SpawnGemAndMoveItToEndPosition()
    {
        GameObject obj = _coinsPooler.GetPooledGem();
        obj.transform.position = gemsImage.transform.position;
        obj.SetActive(true);
        obj.transform.DOMove(gemEndPositon.position, 0.5f, false).OnComplete(() =>
        {
            AudioManager.Instance.Play_GemCollect();
            SpawnGemCollectedParticle(obj.transform.position);
            _coinsPooler.RemoveElement(obj.transform);
        });
    }

    public void SpawnShellAndMoveItToEndPosition()
    {
        GameObject obj = _coinsPooler.GetPooledShell();
        obj.transform.position = shellsImage.transform.position;
        obj.SetActive(true);
        obj.transform.DOMove(shellsEndPosition.position, 0.5f, false).OnComplete(() =>
        {
            AudioManager.Instance.Play_GemCollect();
            SpawnGemCollectedParticle(obj.transform.position);
            _coinsPooler.RemoveElement(obj.transform);
        });
    }
    IEnumerator StartChestRotation(float time)
    {
        state = RouletteState.SPIN;
        rerollButton.SetActive(false);
        SetSpawnPrize(false);
        StopDoublePriceCoroutine();
        ShowChestPrizes();
        yield return new WaitForSeconds(time);
        CloseChests();
        yield return new WaitForSeconds(0.5f);
        float _randomTime = Random.Range(1, 1.8f);
        chestRotate = true;
        Rad_SaveManager.profile.shells--;
        yield return new WaitForSeconds(_randomTime);
        chestRotate = false;
        ResetRotationChest();
        canOpen = true;
        backButton.enabled = false;
        state = RouletteState.WAITING;
    }

    private void ShowChestPrizes()
    {
        for (int i = 0; i < chests.Length; i++)
        {
            chests[i].PreShowPRize();
        }
    }
    public void CloseChests()
    {
        for (int i = 0; i < chests.Length; i++)
        {
            chests[i].CloseChest();
        }
    }
    private void ResetRotationChest()
    {
        for (int i = 0; i < chests.Length; i++)
        {
            chests[i].ResetChestRotation();
        }

    }
    /// <summary>
    /// Function that generate the Prizes for all the chests that are included on the list (in principle three)
    /// Possible contents:
    ///     - gold (x amount)
    ///     - herbs (+1 life? + healthbar?)
    ///     - gems (+ another token to play so the gems are the tokens)
    ///     - shells (+ Score? + )
    ///     - nothing (sadness)
    /// Percentage separation is as follows:
    /// - 1 chest empty
    /// - 2 chests prize
    /// </summary>

    private void GeneratePrizes()
    {
        bool weaponIn = false; // so, in case we add any weapon in prizes, we only add one
        //1 coin
        //1 gem
        //1 random between coin shell weapon power up ?
        for (int i = 0; i < chests.Length; i++)
        {
            int randomPrize = RadUtils.d100();
            if (randomPrize == 1 && !weaponIn)
            //if (randomPrize > 1 && !weaponIn)
            {
                int _rWeapon = Random.Range(0, prizesList.weaponsItemsList.Count);
                chests[i].prize = prizesList.weaponsItemsList[_rWeapon];
            }
            else if (randomPrize == 2)
            {
                int _rGem = Random.Range(0, prizesList.gemsItemsList.Count);
                chests[i].prize = prizesList.gemsItemsList[_rGem];
            }
            else if (randomPrize % 2 == 0 && Rad_SaveManager.profile.shells <= 2 && !shellPrizeAdded)
            {
                shellPrizeAdded = true;

                int _rtoken = Random.Range(0, prizesList.tokensItemsList.Count);
                chests[i].prize = prizesList.tokensItemsList[_rtoken];
            }
            else
            {

                int _rGold = Random.Range(0, prizesList.coinsItemsList.Count);
                chests[i].prize = prizesList.coinsItemsList[_rGold];
            }
            chests[i].CloseChest();
        }

    }
    private void RandomChestPositions()
    {
        int randomIndex = 0;
        for (int i = 0; i < chests.Length; i++)
        {
            randomIndex = Random.Range(0, initialPosition.Count);
            chests[i].transform.position = initialPosition[randomIndex];
            initialPosition.RemoveAt(randomIndex);
        }
    }
    public void UpdateDoublePrize()
    {
        if (prizeType.categoryType == PrizeType.COINS)
        {
            SIS.DBManager.IncreaseFunds("coins", prizeAmount);
        }
        else if (prizeType.categoryType == PrizeType.GEMS)
        {
            Rad_SaveManager.profile.gems += prizeAmount;
        }
        else if(prizeType.categoryType == PrizeType.SHELLS)
        {
            Rad_SaveManager.profile.shells += prizeAmount;
        }
        Rad_SaveManager.SaveData();
    }




    public void SpawnGoldParticleChest(Transform item)
    {
        StartCoroutine(SpawnCoinsParticle(item));
    }
    public void SpawnEmptyParticleChest(Transform item)
    {
        StartCoroutine(SpawnEmptyParticle(item));
    }
    public void SpawnGemParticleChest(Transform item)
    {
        StartCoroutine(SpawnGemParticle(item));
    }

    IEnumerator SpawnCoinsParticle(Transform item)
    {
        GameObject obj = _particlePooler.GetPooledGoldParticle();
        obj.SetActive(true);
        Debug.Log(obj);
        obj.transform.position = item.transform.position;
        yield return new WaitForSeconds(1);
        _particlePooler.RemoveElement(obj.transform);
    }
    IEnumerator SpawnGemParticle(Transform item)
    {
        GameObject obj = _particlePooler.GetPooledGemParticle();
        obj.SetActive(true);
        obj.transform.position = item.transform.position;
        yield return new WaitForSeconds(1);
        _particlePooler.RemoveElement(obj.transform);
    }
    IEnumerator SpawnEmptyParticle(Transform item)
    {
        GameObject obj = _particlePooler.GetPooledEmptyParticle();
        obj.SetActive(true);
        obj.transform.position = item.transform.position;
        yield return new WaitForSeconds(1);
        _particlePooler.RemoveElement(obj.transform);
    }
    IEnumerator SpawnCoinCollected(Vector3 position)
    {
        GameObject obj = _particlePooler.GetPooledCoinCollectedParticle();
        obj.SetActive(true);
        obj.transform.position = position;
        yield return new WaitForSeconds(1);
        _particlePooler.RemoveElement(obj.transform);
    }
    IEnumerator SpawnGemCollected(Vector3 position)
    {
        GameObject obj = _particlePooler.GetPooledGemCollectedParticle();
        obj.SetActive(true);
        obj.transform.position = position;
        yield return new WaitForSeconds(1);
        _particlePooler.RemoveElement(obj.transform);
    }
    public void UpdatePlayerData()
    {

        switch (prizeType.categoryType)
        {
            case PrizeType.COINS:
                SIS.DBManager.IncreaseFunds("coins", prizeAmount);
                break;
            case PrizeType.GEMS:
                Rad_SaveManager.profile.gems += prizeAmount;
                break;
            case PrizeType.SHELLS:
                Rad_SaveManager.profile.shells += prizeAmount;
                break;
            case PrizeType.WEAPON:
                switch (prizeType.weaponType)
                {
                    case WeaponType.black:
                        if (!SIS.DBManager.isPurchased("si_blacksword"))
                        {
                            SIS.DBManager.IncreasePurchase("si_blacksword", 1);
                        }
                        else
                        {
                            SIS.DBManager.IncreaseFunds("coins", 1750);
                        }
                        
                        //SIS.DBManager.IncreasePurchase("si_blacksword_gems", 1);
                        break;
                    case WeaponType.blue:
                        if (!SIS.DBManager.isPurchased("si_bluesword"))
                        {
                            SIS.DBManager.IncreasePurchase("si_bluesword", 1);
                        }
                        else
                        {
                            SIS.DBManager.IncreaseFunds("coins", 3750);
                        }
                            
                        //SIS.DBManager.IncreasePurchase("si_bluesword_gems", 1);
                        break;
                    case WeaponType.green:
                        if (!SIS.DBManager.isPurchased("si_greensword"))
                        {
                            SIS.DBManager.IncreasePurchase("si_greensword", 1);
                        }
                        else
                        {
                            SIS.DBManager.IncreaseFunds("coins", 1250);
                        }
                            
                        //SIS.DBManager.IncreasePurchase("si_greensword_gems", 1);
                        break;
                    case WeaponType.yellow:
                        if (!SIS.DBManager.isPurchased("si_yellowsword"))
                        {
                            SIS.DBManager.IncreasePurchase("si_yellowsword", 1);
                        }
                        else
                        {
                            SIS.DBManager.IncreaseFunds("coins", 2500);
                        }
                            
                        //SIS.DBManager.IncreasePurchase("si_yellowword_gems", 1);
                        break;
                }
                break;
        }
        Rad_SaveManager.SaveData();
    }
    private void UpdatePricePanel()
    {
        if (prizeText && prizeType.categoryType == PrizeType.COINS)
        {
            prizeText.text = prizeAmount.ToString() + " Coins !!";
        }
        else if (prizeText && prizeType.categoryType == PrizeType.GEMS)
        {
            prizeText.text = prizeAmount.ToString() + " Gems !!";
        }
        else if (prizeText && prizeType.categoryType == PrizeType.SHELLS)
        {
            prizeText.text = prizeAmount.ToString() + " Shells !!";
        }
        else if (prizeText && prizeType.categoryType == PrizeType.WEAPON)
        {
            prizeText.text = prizeAmount.ToString() + " " + prizeType.description;
        }
        //else if(prizeText && _chestManager.prizeType == chestType.empty)
        //{
        //    prizeText.text = "  You chose poorly!!";
        //}
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
        _coinsToSpawn = prizeAmount;
        _spawnPrize = true;
        _prizePanelOn = false;
        _doublePrize = true;
        DOTween.To(() => _pDoublePrize, x => _pDoublePrize = x, (int)prizeAmount * 2, 1f);
        yield return new WaitForSeconds(1);
        UpdateDoublePrize();
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
    private void SpawnPrizeUI()
    {
        switch (prizeType.categoryType)
        {
            case PrizeType.COINS:
                SpawnCoinAndMoveItToEndPosition();
                break;
            case PrizeType.GEMS:
                SpawnGemAndMoveItToEndPosition();
                break;
            case PrizeType.SHELLS:
                SpawnShellAndMoveItToEndPosition();
                break;

        }
    }
    public void ChestPresentation()
    {
        rouletteTitle.transform.DOMove(posTitle.position, 0.75f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            chest1.transform.DOMove(posChest1.position, 0.75f).SetEase(Ease.OutBack).OnComplete(() =>
            {
                chest2.transform.DOMove(posChest2.position, 0.75f).SetEase(Ease.OutBack).OnComplete(() =>
                {
                    chest3.transform.DOMove(posChest3.position, 0.75f).SetEase(Ease.OutBack).OnComplete(() =>
                    {
                        RestartChestGenerations();
                    });
                });
            });
        });
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

            if (prizeAmount > 0 && prizeType.categoryType != PrizeType.WEAPON)
            {
                closePrizePanel.enabled = false; // we wait for double prize ad
                ShowDoublePrizePanel();
            }
            UpdatePlayerData();
            _spawnPrize = true;
        });
    }
    public void SetCointToSpawn(int value)
    {
        _coinsToSpawn = value;
    }

    public void Button_HidePricePanel()
    {

        backButton.enabled = true;
        _prizePanelOn = false;
        pricePanel.transform.DOLocalMoveY(1000f, 1f).SetEase(Ease.OutBack);

    }
    public void Button_CheckRerollButton()
    {
        if (Rad_SaveManager.profile.shells > 0)
        {
            rerollButton.SetActive(true);
            goToShopButton.SetActive(false);
        }
        else
        {
            rerollButton.SetActive(false);
            goToShopButton.SetActive(true);
        }
        state = RouletteState.READY;
    }
    public void StartRoulettePanel()
    {
        rouletteOn = true;
        backButton.enabled = false;
        AudioManager.Instance.PlayRouletteMusic();
        _menuManager.roulettePanel.transform.DOLocalMoveX(0f, 0.75f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            ShowStartRoulettePanel();
        });
    }
    public void ChangeChestPrizes(Rad_Chest _chest)
    {
        int randomCHest = Random.Range(0, 1);
        Rad_Chest tempChest = _chest;

        if(_chest.prize == chests[0].prize)
        {
            switch (randomCHest)
            {
                case 0:
                    _chest.prize = chests[1].prize;
                    break;
                case 1:
                    _chest.prize = chests[2].prize;
                    break;
            }
        }
        if (_chest.prize == chests[1].prize)
        {
            switch (randomCHest)
            {
                case 0:
                    _chest.prize = chests[0].prize;
                    break;
                case 1:
                    _chest.prize = chests[2].prize;
                    break;
            }
        }
        if (_chest.prize == chests[2].prize)
        {
            switch (randomCHest)
            {
                case 0:
                    _chest.prize = chests[1].prize;
                    break;
                case 1:
                    _chest.prize = chests[0].prize;
                    break;
            }
        }
    }
    public void SetSpawnPrize(bool value)
    {
        _spawnPrize = value;
    }
    public void SpawnCoinCollectedParticle(Vector3 position)
    {
        StartCoroutine(SpawnCoinCollected(position));
    }
    public void SpawnGemCollectedParticle(Vector3 position)
    {
        StartCoroutine(SpawnGemCollected(position));
    }
    public void ShowStartRoulettePanel()
    {
        startRoulettePanel.transform.DOLocalMoveY(0f, 1f).SetEase(Ease.OutBack);
    }
    public void HideStartRoulettePanel()
    {
        startRoulettePanel.transform.DOLocalMoveY(1000f, 1f).SetEase(Ease.OutBack);
        _menuManager.CheckInventory();
    }
    public void SetCanOpen(bool value) { canOpen = value; }
    public bool GetCanOpen() { return canOpen; }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChestRoulette : MonoBehaviour {


    public Sprite[] available_chest_contents;
    public Rad_Chest[] chests;
    public Transform chestContainer;
    public int numChests;
    public bool chestRotate = false;

    [Header("Price Panel")]
    public int prizeAmount;
    public GameObject coinsImage;
    public GameObject gemsImage;
    public GameObject weaponImage;
    public GameObject shellsImage;
    public PrizesListScriptableObject.PrizeListClass prizeType;
    public Transform coinsEndPosition;
    public Transform gemEndPositon;
    [Header("Prizes Scriptable Object")]
    public PrizesListScriptableObject prizesList;

    private WeaponType weaponType;
    private Rad_GuiManager _guiManager;
    private ParticlePooler _particlePooler;
    private CoinsPooler _coinsPooler;
    private List<Vector2> initialPosition = new List<Vector2>();
    private bool canOpen;
    

    // Use this for initialization
    void Start ()
    {
        _guiManager = FindObjectOfType<Rad_GuiManager>();
        _particlePooler = FindObjectOfType<ParticlePooler>();
        _coinsPooler = FindObjectOfType<CoinsPooler>();
    }
	

    public void RestartChestGenerations()
    {
        for (int i = 0; i < 3; i++)
        {
            initialPosition.Add(chests[i].transform.position);
        }

        if (Rad_SaveManager.profile.shells > 0)
        {
            GeneratePrizes();
            StartCoroutine(StartChestRotation(2));
        }
        else
        {
            _guiManager.ShowNoShellsCoroutine(1.5f);
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
    IEnumerator StartChestRotation(float time)
    {
        _guiManager.rerollButton.SetActive(false);
        _guiManager.SetSpawnPrize(false);
        _guiManager.StopDoublePriceCoroutine();
        ShowChestPrizes();
        yield return new WaitForSeconds(time);
        CloseChests();
        yield return new WaitForSeconds(1);
        int _randomTime = Random.Range(3, 6);
        chestRotate = true;
        Rad_SaveManager.profile.shells--;
        yield return new WaitForSeconds(_randomTime);
        chestRotate = false;
        ResetRotationChest();
        canOpen = true;
        _guiManager.backButton.enabled = false;

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
        for (int i = 0; i < chests.Length; i++)
        {
            int randomPrize = RadUtils.d100();
            if(randomPrize == 1 && !weaponIn)
            {
                int _rWeapon = Random.Range(0, prizesList.weaponsItemsList.Count);
                chests[i].prize = prizesList.weaponsItemsList[_rWeapon];
            }
            else if(randomPrize == 2)
            {
                int _rGem = Random.Range(0, prizesList.gemsItemsList.Count);
                chests[i].prize = prizesList.gemsItemsList[_rGem];
            }
            else if( randomPrize % 2 == 0)
            {
                int _rGold = Random.Range(0, prizesList.coinsItemsList.Count);
                chests[i].prize = prizesList.coinsItemsList[_rGold];
            }
            else
            {
                int _rtoken = Random.Range(0, prizesList.tokensItemsList.Count);
                chests[i].prize = prizesList.tokensItemsList[_rtoken];
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
        Debug.Log(obj);
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
                switch (weaponType)
                {
                    //TODO ADD WEAPONS TO IAPURCHASES DB
                }
                break;

        }
        Rad_SaveManager.SaveData();
    }
    public void SpawnCoinCollectedParticle(Vector3 position)
    {
        StartCoroutine(SpawnCoinCollected(position));
    }
    public void SpawnGemCollectedParticle(Vector3 position)
    {
        StartCoroutine(SpawnGemCollected(position));
    }
    public void SetCanOpen(bool value) { canOpen = value; }
    public bool GetCanOpen() { return canOpen; }
}

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
    public GameObject nothingImage;
    public chestType prizeType;
    public Transform coinsEndPosition;
    public Transform gemEndPositon;

    [Header("Flash")]
    public Image flashImage;

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

        if (Rad_SaveManager.profile.gems > 0)
        {
            GeneratePrizes();
            StartCoroutine(StartChestRotation(2));
        }
        else
        {
            _guiManager.ShowNoGemsCoroutine(1.5f);
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
        _guiManager.SetSpawnPrize(false);
        _guiManager.HidePricePanel();
        _guiManager.StopDoublePriceCoroutine();
        ShowChestPrizes();
        yield return new WaitForSeconds(time);
        CloseChests();
        yield return new WaitForSeconds(1);
        int _randomTime = Random.Range(3, 6);
        chestRotate = true;
        Rad_SaveManager.profile.gems--;
        yield return new WaitForSeconds(_randomTime);
        chestRotate = false;
        ResetRotationChest();
        canOpen = true;
        _guiManager.backButton.enabled = false;

    }
    public void Flash()
    {
        flashImage.DOFade(1, 0.1f).OnComplete(() => 
        {
            flashImage.DOFade(0, 0.1f);
        });
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
        for (int i = 0; i < chests.Length; i++)
        {
            if(chests[i].type == chestType.coins)
            {
                GeneratePriceAmountGold(chests[i]);
            }
            else if(chests[i].type == chestType.gems)
            {
                GeneratePriceAmountGems(chests[i]);
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
        if (prizeType == chestType.coins)
        {
            SIS.DBManager.IncreaseFunds("coins", prizeAmount);
        }
        else if (prizeType == chestType.gems)
        {
            Rad_SaveManager.profile.gems += prizeAmount;
        }

        Rad_SaveManager.SaveData();
    }
    private void GeneratePriceAmountGold(Rad_Chest chest)
    {
        float diceroll = 0;
        diceroll = RadUtils.d100();

        if(diceroll < 5)
        {
            chest.amount = 1500;
        }
        else if(diceroll >= 5 && diceroll < 20)
        {
            chest.amount = 750;
        }
        else if (diceroll >= 20 && diceroll < 60)
        {
            chest.amount = 125;
        }
        else if (diceroll >= 60)
        {
            chest.amount = 50;
        }
    }
    private void GeneratePriceAmountGems(Rad_Chest chest)
    {
        float diceroll = 0;
        diceroll = RadUtils.d100();

        if (diceroll < 5)
        {
            chest.amount = 3;
        }
        else if (diceroll >= 5 && diceroll < 20)
        {
            chest.amount = 2;
        }
        else if (diceroll >= 20)
        {
            chest.amount = 1;
        }
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
        switch (prizeType)
        {
            case chestType.coins:
                SIS.DBManager.IncreaseFunds("coins", prizeAmount);
                break;
            case chestType.gems:
                Rad_SaveManager.profile.gems += prizeAmount;
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

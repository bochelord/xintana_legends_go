using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChestRoulette : MonoBehaviour {


    public Sprite[] available_chest_contents;
    public Rad_Chest[] chests;
    public int numChests;

    [Header("Price Panel")]
    public int priceAmount;
    public GameObject coinsImage;
    public GameObject gemsImage;
    public GameObject nothingImage;
    public chestType priceType;
    [Header("Flash")]
    public Image flashImage;

    private Rad_GuiManager _guiManager;
    private ParticlePooler _pooler;
    private List<Vector2> initialPosition = new List<Vector2>();
    private bool canOpen;


    // Use this for initialization
    void Start ()
    {
        _guiManager = FindObjectOfType<Rad_GuiManager>();
        _pooler = FindObjectOfType<ParticlePooler>();
    }
	

    public void RestartChestGenerations()
    {
        _guiManager.HidePricePanel();
        _guiManager.StopDoublePriceCoroutine();
        if (Rad_SaveManager.profile.tokens > 0)
        {
            Rad_SaveManager.profile.tokens--;
            for (int i = 0; i < 3; i++)
            {
                initialPosition.Add(chests[i].transform.position);
            }

            GeneratePrizes();
            RandomChestPositions();
            canOpen = true;
            _guiManager.backButton.enabled = false;
        }
        else
        {
            _guiManager.ShowNoTokensCoroutine(1.5f);
        }
    }

    public void Flash()
    {
        flashImage.DOFade(1, 0.1f).OnComplete(() => 
        {
            flashImage.DOFade(0, 0.1f);
        });
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
    public void UpdateDoublePrice()
    {
        if (priceType == chestType.coins)
        {
            SIS.DBManager.IncreaseFunds("coins", priceAmount);
        }
        else if (priceType == chestType.gems)
        {
            Rad_SaveManager.profile.tokens += priceAmount;
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

    public void CloseChests()
    {
        for (int i = 0; i < chests.Length; i++)
        {
            chests[i].CloseChest();
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
        GameObject obj = _pooler.GetPooledGoldParticle();
        obj.SetActive(true);
        Debug.Log(obj);
        obj.transform.position = item.transform.position;
        yield return new WaitForSeconds(1);
        _pooler.RemoveElement(obj.transform);
    }
    IEnumerator SpawnGemParticle(Transform item)
    {
        GameObject obj = _pooler.GetPooledGemParticle();
        obj.SetActive(true);
        Debug.Log(obj);
        obj.transform.position = item.transform.position;
        yield return new WaitForSeconds(1);
        _pooler.RemoveElement(obj.transform);
    }
    IEnumerator SpawnEmptyParticle(Transform item)
    {
        GameObject obj = _pooler.GetPooledEmptyParticle();
        obj.SetActive(true);
        Debug.Log(obj);
        obj.transform.position = item.transform.position;
        yield return new WaitForSeconds(1);
        _pooler.RemoveElement(obj.transform);
    }
    public void SetCanOpen(bool value) { canOpen = value; }
    public bool GetCanOpen() { return canOpen; }
}

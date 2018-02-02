using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum chestType { coins, herb, shells, empty, gems}
public class Rad_Chest : MonoBehaviour {

    public GameObject chestClosed;
    public GameObject chestOpen;
    public GameObject gold;
    public GameObject gems;
    public chestType type;
    public int amount;

    private ChestRoulette _chestManager;
    private Rad_GuiManager _guiManager;

    private void Start()
    {
        _chestManager = FindObjectOfType<ChestRoulette>();
        _guiManager = FindObjectOfType<Rad_GuiManager>();

    }
    public void OpenChest()
    {
        if (_chestManager.GetCanOpen())
        {
            this.transform.DOShakeRotation(2, 20, 2, 25, false).OnComplete(() =>
            {
                chestClosed.SetActive(false);
                chestOpen.SetActive(true);
                _chestManager.SetCanOpen(false);
                _chestManager.priceAmount = amount;
                _chestManager.priceType = type;
                UpdatePrice();
                _guiManager.ShowPricePanel();
                switch (type)
                {
                    case chestType.coins:
                        _chestManager.SpawnGoldParticleChest(this.transform);
                        break;
                    case chestType.gems:
                        _chestManager.SpawnGemParticleChest(this.transform);
                        break;
                    case chestType.empty:
                        _chestManager.SpawnEmptyParticleChest(this.transform);
                        break;
                }
            });
        }
    }

    public void CloseChest()
    {
        chestClosed.SetActive(true);
        chestOpen.SetActive(false);
        gold.SetActive(false);
        gems.SetActive(false);
    }
   
    private void UpdatePrice()
    {
        if (type == chestType.coins)
        {
            SIS.DBManager.IncreaseFunds("coins", amount);
            _chestManager.nothingImage.SetActive(false);
            _chestManager.gemsImage.SetActive(false);
            _chestManager.coinsImage.SetActive(true);
            gold.SetActive(true);
        }
        else if (type == chestType.gems)
        {

            Rad_SaveManager.profile.tokens += amount;
            _chestManager.nothingImage.SetActive(false);
            _chestManager.gemsImage.SetActive(true);
            _chestManager.coinsImage.SetActive(false);
            gems.SetActive(true);
        }
        else if (type == chestType.empty)
        {

            _chestManager.nothingImage.SetActive(false);
            _chestManager.gemsImage.SetActive(false);
            _chestManager.coinsImage.SetActive(false);
        }
    }
}

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
    public Transform midPoint;
    public float rotationSpeed;

    private ChestRoulette _chestManager;
    private Rad_GuiManager _guiManager;
    private ParticlePooler _pooler;

    private void Start()
    {
        _pooler = FindObjectOfType<ParticlePooler>();
        _chestManager = FindObjectOfType<ChestRoulette>();
        _guiManager = FindObjectOfType<Rad_GuiManager>();

    }
    private void Update()
    {
        if (_chestManager.chestRotate)
        {
            this.transform.RotateAround(midPoint.position, Vector3.forward, rotationSpeed * Time.deltaTime);

        }
    }
    public void OpenChest()
    {

        if (_chestManager.GetCanOpen())
        {
            _chestManager.SetCanOpen(false);
            this.transform.DOShakeRotation(2, 20, 2, 25, false).OnComplete(() =>
            {
                chestClosed.SetActive(false);
                chestOpen.SetActive(true);
                _chestManager.prizeAmount = amount;
                _chestManager.prizeType = type;
                UpdatePrize();
                _guiManager.ShowPrizePanel();
            });
        }
    }
    public void ResetChestRotation()
    {
        this.transform.DOLocalRotate(new Vector3(0, 0, 0), 0);
    }
    public void PreShowPRize()
    {
        chestClosed.SetActive(false);
        chestOpen.SetActive(true);
        switch (type)
        {
            case chestType.coins:
                gold.SetActive(true);
                break;

            case chestType.gems:
                gems.SetActive(true);
                break;
        }
    }
    public void CloseChest()
    {
        chestClosed.SetActive(true);
        chestOpen.SetActive(false);
        gold.SetActive(false);
        gems.SetActive(false);
    }
   

    private void UpdatePrize()
    {
        _guiManager.SetCointToSpawn(amount);

        switch (type)
        {
            case chestType.coins:

                _chestManager.nothingImage.SetActive(false);
                _chestManager.gemsImage.SetActive(false);
                _chestManager.coinsImage.SetActive(true);
                AnalyticsManager.Instance.ChestPrice_Event("Coins", amount);
                _chestManager.SpawnGoldParticleChest(this.transform);
                gold.SetActive(true);
                break;

            case chestType.gems:

                AnalyticsManager.Instance.ChestPrice_Event("Gems", amount);
                _chestManager.nothingImage.SetActive(false);
                _chestManager.gemsImage.SetActive(true);
                _chestManager.coinsImage.SetActive(false);
                _chestManager.SpawnGemParticleChest(this.transform);
                gems.SetActive(true);
                break;

            case chestType.empty:

                AnalyticsManager.Instance.ChestPrice_Event("Empty", amount);
                _chestManager.nothingImage.SetActive(false);
                _chestManager.gemsImage.SetActive(false);
                _chestManager.coinsImage.SetActive(false);
                _chestManager.SpawnEmptyParticleChest(this.transform);
                break;
        }
        Rad_SaveManager.SaveData();
    }


}

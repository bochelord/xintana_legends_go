using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public enum chestType { coins, herb, shells, empty, gems}
public class Rad_Chest : MonoBehaviour {

    public GameObject chestClosed;
    public GameObject chestOpen;
    public GameObject gold;
    public GameObject gems;
    public GameObject shells;
    public GameObject weapon;
    public PrizesListScriptableObject.PrizeListClass prize;

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
                _chestManager.prizeAmount = prize.itemValue;
                _chestManager.prizeType = prize;
                
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
        switch (prize.categoryType)
        {
            case PrizeType.COINS:
                gold.SetActive(true);
                break;

            case PrizeType.GEMS:
                gems.SetActive(true);
                break;

            case PrizeType.SHELLS:
                shells.SetActive(true);
                break;

            case PrizeType.WEAPON:
                weapon.GetComponent<Image>().sprite = prize.itemSprite;
                weapon.SetActive(true);
                break;

        }
    }
    public void CloseChest()
    {
        chestClosed.SetActive(true);
        chestOpen.SetActive(false);
        gold.SetActive(false);
        gems.SetActive(false);
        shells.SetActive(false);
        weapon.SetActive(false);

    }


    private void UpdatePrize()
    {
        _guiManager.SetCointToSpawn(prize.itemValue);

        switch (_chestManager.prizeType.categoryType)
        {

            case PrizeType.COINS:
                _chestManager.weaponImage.SetActive(false);
                _chestManager.gemsImage.SetActive(false);
                _chestManager.coinsImage.SetActive(true);
                AnalyticsManager.Instance.ChestPrice_Event("Coins", prize.itemValue);
                _chestManager.SpawnGoldParticleChest(this.transform);
                _chestManager.shellsImage.SetActive(false);
                gold.SetActive(true);
                break;

            case PrizeType.GEMS:
                AnalyticsManager.Instance.ChestPrice_Event("Gems", prize.itemValue);
                _chestManager.weaponImage.SetActive(false);
                _chestManager.gemsImage.SetActive(true);
                _chestManager.coinsImage.SetActive(false);
                _chestManager.SpawnGemParticleChest(this.transform);
                _chestManager.shellsImage.SetActive(false);
                gems.SetActive(true);
                break;

            case PrizeType.WEAPON:
                AnalyticsManager.Instance.ChestPrice_Event("Weapon", prize.itemValue);
                _chestManager.weaponImage.GetComponent<Image>().sprite = prize.itemSprite;
                _chestManager.weaponImage.SetActive(true);
                _chestManager.gemsImage.SetActive(false);
                _chestManager.coinsImage.SetActive(false);
                _chestManager.shellsImage.SetActive(false);
                weapon.SetActive(true);
                //TODO WEAPON PARTICLES
                break;
            case PrizeType.SHELLS:
                _chestManager.weaponImage.SetActive(false);
                _chestManager.gemsImage.SetActive(false);
                _chestManager.coinsImage.SetActive(false);
                _chestManager.SpawnEmptyParticleChest(this.transform);
                _chestManager.shellsImage.SetActive(true);
                shells.SetActive(true);
                AnalyticsManager.Instance.ChestPrice_Event("Shells", prize.itemValue);
                //TODO SHELLS PARTICLES
                 
                break;
        }

        Rad_SaveManager.SaveData();
    }


}

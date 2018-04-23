using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class KogiReward : MonoBehaviour {

    public GameObject kogi;
    private GameObject _player;
    public Sprite coinImage;

    public PrizesListScriptableObject kogiRewardsList;
    private PrizesListScriptableObject.PrizeListClass kogiReward;
    private EnemiesPooler _enemyPooler;
    private CoinsPooler _coinsPooler;
    private LevelManager _levelManager;
    private Canvas _canvas;
    void OnEnable()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        _coinsPooler = FindObjectOfType<CoinsPooler>();
        _enemyPooler = FindObjectOfType<EnemiesPooler>();
        _player = _levelManager.player;
        //_canvas = FindObjectOfType<Canvas>();


    }

    public void BUTTON_SpawnKogiReward()
    {
        //_levelManager.SpawnKogiReward();
        _levelManager.SpawnKogiRewardTween();
        AudioManager.Instance.playKogiAppears();
    }

    ///// <summary>
    ///// New Method to manage how and what Kogi will spawn when clicked/touched.
    ///// </summary>
    //public void SpawnKogiRewardTween()
    //{
    //    GenerateKogiReward();
    //    _enemyPooler.RemoveElement(this.transform.parent);
    //    _levelManager.kogiSpawned = false;
    //}

    //private void GenerateKogiReward()
    //{
    //    bool _price = Random.value > 0.5f;
    //    //Debug.Log("_price: " + _price);
    //    int _randomprice;
    //    if (_price)
    //    {
    //        _randomprice = Random.Range(0, kogiRewardsList.coinsItemsList.Count);
    //        //Debug.Log("_randomprice: " + _randomprice.ToString());
    //        kogiReward = kogiRewardsList.coinsItemsList[_randomprice];
    //        //Debug.Log("kogiReward: " + kogiReward.ToString());
    //    }
    //    else
    //    {
    //        _randomprice = Random.Range(0, kogiRewardsList.tokensItemsList.Count);
    //        //Debug.Log("_randomprice: " + _randomprice.ToString());
    //        kogiReward = kogiRewardsList.tokensItemsList[_randomprice];
    //        //Debug.Log("kogiReward: " + kogiReward.ToString());
    //    }

    //    StartCoroutine(ShowKogiRewardAndSendToTarget(kogi, _player));
    //}

    //IEnumerator ShowKogiRewardAndSendToTarget(GameObject fromTarget, GameObject toTarget)
    //{
    //    Debug.Log("IEnumerator launched!");

    //    List<GameObject> coinsArray = new List<GameObject>();

    //    Vector2 posConverted;

        
    //    for (int i = 0; i < 5; i++)
    //    {
    //        GameObject tempCoin = null;
    //        if (kogiReward.categoryType == PrizeType.COINS)
    //        {
    //            tempCoin = _coinsPooler.GetPooledCoin();
    //        }
    //        else if (kogiReward.categoryType == PrizeType.GEMS)
    //        {
    //            tempCoin = _coinsPooler.GetPooledGem();
    //        }
    //        else if (kogiReward.categoryType == PrizeType.SHELLS)
    //        {
    //            tempCoin = _coinsPooler.GetPooledShell();
    //        }


    //        tempCoin.SetActive(true);

    //        tempCoin.transform.position = fromTarget.transform.position;
    //        tempCoin.transform.SetParent(_canvas.transform);
    //        tempCoin.transform.localScale = new Vector3(1, 1, 1);
    //        coinsArray.Add(tempCoin);

    //        Debug.Log("tempCoin: " + tempCoin.name);
    //    }

    //    for (int i = 0; i < 5; i++)
    //    {
    //        toTarget.transform.position = Random.insideUnitCircle;
    //        posConverted = RectTransformUtility.WorldToScreenPoint(null, toTarget.transform.position);
    //        coinsArray[i].transform.DOMove(posConverted, 0.5f, false);
    //        yield return new WaitForSeconds(0.1f);
    //    }

    //    for (int i = 0; i < 5; i++)
    //    {
    //        posConverted = RectTransformUtility.WorldToScreenPoint(null, toTarget.transform.position);
    //        coinsArray[i].transform.DOMove(posConverted, 0.5f, false).OnComplete(() =>
    //        {
    //            Debug.Log("ONCOMPLETE!!");
    //            _coinsPooler.RemoveElement(coinsArray[i].transform);
    //        });
    //        yield return new WaitForSeconds(0.1f);
    //    }

    //    //yield return null;

    //}

}

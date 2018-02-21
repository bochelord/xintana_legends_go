using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsPooler : Pooler {

    public GameObject coinPrefab;
    public GameObject gemPrefab;
    public GameObject shellPrefab;

    private List<GameObject> pooledShells;
    private List<GameObject> pooledCoins;
    private List<GameObject> pooledGems;

    public Transform parent;
	// Use this for initialization
	public override void Start ()
    {
        pooledCoins = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(coinPrefab);
            obj.transform.parent = parent;
            obj.transform.position = Vector3.zero;
            obj.SetActive(false);
            pooledCoins.Add(obj);
        }

        pooledGems = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(gemPrefab);
            obj.transform.parent = parent;
            obj.transform.position = Vector3.zero;
            obj.SetActive(false);
            pooledGems.Add(obj);
        }

        pooledShells = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(shellPrefab);
            obj.transform.parent = parent;
            obj.transform.position = Vector3.zero;
            obj.SetActive(false);
            pooledShells.Add(obj);
        }
    }
	
    public GameObject GetPooledCoin()
    {
        List<GameObject> activeCoins = new List<GameObject>();
        foreach (GameObject coin in pooledCoins)
        {
            if (!coin.activeInHierarchy)
            {
                activeCoins.Add(coin);
            }
        }

        int index = Random.Range(0, activeCoins.Count);

        if (activeCoins.Count == 0) { Debug.LogError("godverdomme"); }

        return activeCoins[index];
    }

    public GameObject GetPooledGem()
    {
        List<GameObject> activeGems = new List<GameObject>();
        foreach (GameObject gem in pooledGems)
        {
            if (!gem.activeInHierarchy)
            {
                activeGems.Add(gem);
            }
        }

        int index = Random.Range(0, activeGems.Count);

        if (activeGems.Count == 0) { Debug.LogError("godverdomme"); }

        return activeGems[index];
    }

    public GameObject GetPooledShell()
    {
        List<GameObject> activeShell = new List<GameObject>();
        foreach (GameObject shell in pooledShells)
        {
            if (!shell.activeInHierarchy)
            {
                activeShell.Add(shell);
            }
        }

        int index = Random.Range(0, pooledShells.Count);

        if (activeShell.Count == 0) { Debug.LogError("godverdomme"); }

        return activeShell[index];
    }

    public override void RemoveElement(Transform item)
    {
        item.transform.position = new Vector3(0, 0, 0);
        item.gameObject.SetActive(false);
    }
}

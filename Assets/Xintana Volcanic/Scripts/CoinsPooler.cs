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

        for (int i = 0; i < pooledCoins.Count; i++)
        {
            if (!pooledCoins[i].activeInHierarchy)
            {
                return pooledCoins[i];
            }
        }

        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(coinPrefab);
            obj.transform.parent = current.transform;
            pooledCoins.Add(obj);
            return obj;
        }
        return null;
    }

    public GameObject GetPooledGem()
    {
        for (int i = 0; i < pooledGems.Count; i++)
        {
            if (!pooledGems[i].activeInHierarchy)
            {
                return pooledGems[i];
            }
        }

        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(gemPrefab);
            obj.transform.parent = current.transform;
            pooledGems.Add(obj);
            return obj;
        }
        return null;
    }

    public GameObject GetPooledShell()
    {
        for (int i = 0; i < pooledShells.Count; i++)
        {
            if (!pooledShells[i].activeInHierarchy)
            {
                return pooledShells[i];
            }
        }

        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(shellPrefab);
            obj.transform.parent = current.transform;
            pooledGems.Add(obj);
            return obj;
        }
        return null;
    }

    public override void RemoveElement(Transform item)
    {
        item.transform.position = new Vector3(0, 0, 0);
        item.gameObject.SetActive(false);
    }
}

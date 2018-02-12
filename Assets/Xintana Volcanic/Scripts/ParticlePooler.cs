using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePooler : Pooler {

    public GameObject goldChestParticle;
    public GameObject gemsChestParticle;
    public GameObject emptyChestParticle;
    public GameObject deadEnemyParticle;
    public GameObject coinCollectedParticle;
    public GameObject gemCollectedParticle;
    public int amount;

    private List<GameObject> pooledGold;
    private List<GameObject> pooledGems;
    private List<GameObject> pooledEmpty;
    private List<GameObject> pooledDeadEnemy;
    private List<GameObject> pooledCoinCollected;
    private List<GameObject> pooledGemCollected;
    // Use this for initialization

    public override void Start ()
    {

        pooledGold = new List<GameObject>();
        for (int j = 0; j < amount; j++)
        {
            GameObject obj = (GameObject)Instantiate(goldChestParticle);
            obj.transform.parent = this.transform;
            obj.transform.position = Vector3.zero;
            obj.SetActive(false);
            pooledGold.Add(obj);
        }
        pooledGems = new List<GameObject>();
        for (int j = 0; j < amount; j++)
        {
            GameObject obj = (GameObject)Instantiate(gemsChestParticle);
            obj.transform.parent = this.transform;
            obj.transform.position = Vector3.zero;
            obj.SetActive(false);
            pooledGems.Add(obj);
        }
        pooledEmpty = new List<GameObject>();
        for (int j = 0; j < amount; j++)
        {
            GameObject obj = (GameObject)Instantiate(emptyChestParticle);
            obj.transform.parent = this.transform;
            obj.transform.position = Vector3.zero;
            obj.SetActive(false);
            pooledEmpty.Add(obj);
        }
        pooledDeadEnemy = new List<GameObject>();
        for (int j = 0; j < amount; j++)
        {
            GameObject obj = (GameObject)Instantiate(deadEnemyParticle);
            obj.transform.parent = this.transform;
            obj.transform.position = Vector3.zero;
            obj.SetActive(false);
            pooledDeadEnemy.Add(obj);
        }
        pooledCoinCollected = new List<GameObject>();
        for (int j = 0; j < amount; j++)
        {
            GameObject obj = (GameObject)Instantiate(coinCollectedParticle);
            obj.transform.parent = this.transform;
            obj.transform.position = Vector3.zero;
            obj.SetActive(false);
            pooledCoinCollected.Add(obj);
        }
        pooledGemCollected = new List<GameObject>();
        for (int j = 0; j < amount; j++)
        {
            GameObject obj = (GameObject)Instantiate(gemCollectedParticle);
            obj.transform.parent = this.transform;
            obj.transform.position = Vector3.zero;
            obj.SetActive(false);
            pooledGemCollected.Add(obj);
        }
    }
    public GameObject GetPooledGemCollectedParticle()
    {
        for (int i = 0; i < pooledGemCollected.Count; i++)
        {
            if (!pooledGemCollected[i].activeInHierarchy)
            {
                return pooledGemCollected[i];
            }
        }
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(gemCollectedParticle);
            obj.transform.parent = current.transform;
            pooledGemCollected.Add(obj);
            return obj;
        }

        return null;
    }
    public GameObject GetPooledCoinCollectedParticle()
    {
        for (int i = 0; i < pooledCoinCollected.Count; i++)
        {
            if (!pooledCoinCollected[i].activeInHierarchy)
            {
                return pooledCoinCollected[i];
            }
        }
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(coinCollectedParticle);
            obj.transform.parent = current.transform;
            pooledCoinCollected.Add(obj);
            return obj;
        }

        return null;
    }
    public GameObject GetPooledGoldParticle()
    {
        for (int i = 0; i < pooledGold.Count; i++)
        {
            if (!pooledGold[i].activeInHierarchy)
            {
                return pooledGold[i];
            }
        }
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(goldChestParticle);
            obj.transform.parent = current.transform;
            pooledGold.Add(obj);
            return obj;
        }

        return null;
    }

    public GameObject GetPooledGemParticle()
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
            GameObject obj = (GameObject)Instantiate(gemsChestParticle);
            obj.transform.parent = current.transform;
            pooledGems.Add(obj);
            return obj;
        }

        return null;
    }

    public GameObject GetPooledEmptyParticle()
    {
        for (int i = 0; i < pooledEmpty.Count; i++)
        {
            if (!pooledEmpty[i].activeInHierarchy)
            {
                return pooledEmpty[i];
            }
        }
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(emptyChestParticle);
            obj.transform.parent = current.transform;
            pooledEmpty.Add(obj);
            return obj;
        }

        return null;
    }
    public GameObject GetPooledDeadEnemyParticle()
    {
        for (int i = 0; i < pooledDeadEnemy.Count; i++)
        {
            if (!pooledDeadEnemy[i].activeInHierarchy)
            {
                return pooledDeadEnemy[i];
            }
        }
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(deadEnemyParticle);
            obj.transform.parent = current.transform;
            pooledDeadEnemy.Add(obj);
            return obj;
        }

        return null;
    }
    public override void RemoveElement(Transform item)
    {
        base.RemoveElement(item);
    }
}

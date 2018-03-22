using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePooler : Pooler {

    public GameObject goldChestParticle;
    public GameObject gemsChestParticle;
    public GameObject emptyChestParticle;
    public GameObject[] deadEnemyParticle;
    public GameObject coinCollectedParticle;
    public GameObject gemCollectedParticle;
    public GameObject healPlayerParticle;
    public GameObject shellCollectedParticle;
    public GameObject criticsPowerUpParticle;
    public GameObject freezePowerUpParticle;
    public GameObject healPowerUpParticle;
    public GameObject gemsPowerUpParticle;
    public GameObject[] hitParticles;
    public int amount;
    public int powerUpAmount;
    public int deadParticleAmount;
    //public int hitAmount;
    private List<GameObject> pooledGold;
    private List<GameObject> pooledGems;
    private List<GameObject> pooledEmpty;
    private List<GameObject> pooledDeadEnemy;
    private List<GameObject> pooledCoinCollected;
    private List<GameObject> pooledGemCollected;
    private List<GameObject> pooledHealPlayer;
    private List<GameObject> pooledShellCollected;
    private List<GameObject> pooledCriticsPowerUp;
    private List<GameObject> pooledFreezePowerUp;
    private List<GameObject> pooledHealPowerUp;
    private List<GameObject> pooledGemsPowerUp;
    private List<GameObject> pooledHits;

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
        for (int i = 0; i < deadEnemyParticle.Length; i++)
        {
            for (int j = 0; j < deadParticleAmount; j++)
            {
                GameObject obj = (GameObject)Instantiate(deadEnemyParticle[i]);
                obj.transform.parent = this.transform;
                obj.transform.position = Vector3.zero;
                obj.SetActive(false);
                pooledDeadEnemy.Add(obj);
            }
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
        pooledHealPlayer = new List<GameObject>();
        for (int j = 0; j < amount; j++)
        {
            GameObject obj = (GameObject)Instantiate(healPlayerParticle);
            obj.transform.parent = this.transform;
            obj.transform.position = Vector3.zero;
            obj.SetActive(false);
            pooledHealPlayer.Add(obj);
        }
        pooledShellCollected = new List<GameObject>();
        for (int j = 0; j < amount; j++)
        {
            GameObject obj = (GameObject)Instantiate(shellCollectedParticle);
            obj.transform.parent = this.transform;
            obj.transform.position = Vector3.zero;
            obj.SetActive(false);
            pooledShellCollected.Add(obj);
        }
        pooledFreezePowerUp = new List<GameObject>();
        for (int j = 0; j < amount; j++)
        {
            GameObject obj = (GameObject)Instantiate(freezePowerUpParticle);
            obj.transform.parent = this.transform;
            obj.transform.position = Vector3.zero;
            obj.SetActive(false);
            pooledFreezePowerUp.Add(obj);
        }
        pooledGemsPowerUp = new List<GameObject>();
        for (int j = 0; j < amount; j++)
        {
            GameObject obj = (GameObject)Instantiate(gemsPowerUpParticle);
            obj.transform.parent = this.transform;
            obj.transform.position = Vector3.zero;
            obj.SetActive(false);
            pooledGemsPowerUp.Add(obj);
        }
        pooledCriticsPowerUp = new List<GameObject>();
        for (int j = 0; j < amount; j++)
        {
            GameObject obj = (GameObject)Instantiate(criticsPowerUpParticle);
            obj.transform.parent = this.transform;
            obj.transform.position = Vector3.zero;
            obj.SetActive(false);
            pooledCriticsPowerUp.Add(obj);
        }
        pooledHealPowerUp = new List<GameObject>();
        for (int j = 0; j < amount; j++)
        {
            GameObject obj = (GameObject)Instantiate(healPowerUpParticle);
            obj.transform.parent = this.transform;
            obj.transform.position = Vector3.zero;
            obj.SetActive(false);
            pooledHealPowerUp.Add(obj);
        }

        pooledHits = new List<GameObject>();
        for (int j = 0; j < hitParticles.Length; j++)
        {
            GameObject obj = (GameObject)Instantiate(hitParticles[j]);
            obj.transform.parent = this.transform;
            obj.transform.position = Vector3.zero;
            obj.SetActive(false);
            pooledHits.Add(obj);
        }

    }
    public GameObject GetHitParticle()
    {
        for (int i = 0; i < pooledHits.Count; i++)
        {
            if (!pooledHits[i].activeInHierarchy)
            {
                return pooledHits[i];
            }
        }
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(hitParticles[Random.Range(0, hitParticles.Length - 1)]);
            obj.transform.parent = current.transform;
            pooledHits.Add(obj);
            return obj;
        }

        return null;
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
    public GameObject GetPooledShellCollectedParticle()
    {
        for (int i = 0; i < pooledShellCollected.Count; i++)
        {
            if (!pooledShellCollected[i].activeInHierarchy)
            {
                return pooledShellCollected[i];
            }
        }
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(shellCollectedParticle);
            obj.transform.parent = current.transform;
            pooledShellCollected.Add(obj);
            return obj;
        }

        return null;
    }
    public GameObject GetPooledHealPlayerParticle()
    {
        for (int i = 0; i < pooledHealPlayer.Count; i++)
        {
            if (!pooledHealPlayer[i].activeInHierarchy)
            {
                return pooledHealPlayer[i];
            }
        }
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(healPlayerParticle);
            obj.transform.parent = current.transform;
            pooledHealPlayer.Add(obj);
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
        int _randomParticle = Random.Range(0, deadEnemyParticle.Length);
        if(!pooledDeadEnemy[_randomParticle].activeInHierarchy)
        {
            return pooledDeadEnemy[_randomParticle];

        }

        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(deadEnemyParticle[_randomParticle]);
            obj.transform.parent = current.transform;
            pooledDeadEnemy.Add(obj);
            return obj;
        }

        return null;
    }
    public GameObject GetPooledHealParticle()
    {
        for (int i = 0; i < pooledHealPowerUp.Count; i++)
        {
            if (!pooledHealPowerUp[i].activeInHierarchy)
            {
                return pooledHealPowerUp[i];
            }
        }
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(healPowerUpParticle);
            obj.transform.parent = current.transform;
            pooledHealPowerUp.Add(obj);
            return obj;
        }

        return null;
    }
    public GameObject GetPooledGemsPowerUpParticle()
    {
        for (int i = 0; i < pooledGemsPowerUp.Count; i++)
        {
            if (!pooledGemsPowerUp[i].activeInHierarchy)
            {
                return pooledGemsPowerUp[i];
            }
        }
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(gemsPowerUpParticle);
            obj.transform.parent = current.transform;
            pooledGemsPowerUp.Add(obj);
            return obj;
        }

        return null;
    }
    public GameObject GetPooledCriticsParticle()
    {
        for (int i = 0; i < pooledCriticsPowerUp.Count; i++)
        {
            if (!pooledCriticsPowerUp[i].activeInHierarchy)
            {
                return pooledCriticsPowerUp[i];
            }
        }
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(criticsPowerUpParticle);
            obj.transform.parent = current.transform;
            pooledCriticsPowerUp.Add(obj);
            return obj;
        }

        return null;
    }
    public GameObject GetPooledFreezeParticle()
    {
        for (int i = 0; i < pooledFreezePowerUp.Count; i++)
        {
            if (!pooledFreezePowerUp[i].activeInHierarchy)
            {
                return pooledFreezePowerUp[i];
            }
        }
        if (willGrow)
        {
            GameObject obj = (GameObject)Instantiate(freezePowerUpParticle);
            obj.transform.parent = current.transform;
            pooledFreezePowerUp.Add(obj);
            return obj;
        }

        return null;
    }
    public override void RemoveElement(Transform item)
    {
        base.RemoveElement(item);
    }

    public IEnumerator RemoveElement(Transform item, float delay)
    {
        
        yield return new WaitForSeconds(delay);
        base.RemoveElement(item);
    }
}

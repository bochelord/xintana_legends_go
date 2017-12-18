using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPooler : Pooler {

    //public GameObject zazucPrefab;
    //public GameObject malukaPrefab;
    //public GameObject kogiPrefab;

    public GameObject[] enemiesPrefabs;
    public GameObject[] bossPrefab;
    public int amountPooledPerType;


    private List<GameObject> pooledBoss;

    public override void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int j = 0; j < enemiesPrefabs.Length; j++)
        {
            for (int i = 0; i < amountPooledPerType; i++)
            {
                GameObject obj = (GameObject)Instantiate(enemiesPrefabs[j]);
                obj.transform.parent = current.transform;
                obj.transform.position = Vector3.zero;
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
        pooledBoss = new List<GameObject>();
        for (int j = 0; j < bossPrefab.Length; j++)
        {
            for (int i = 0; i < amountPooledPerType; i++)
            {
                GameObject obj = (GameObject)Instantiate(bossPrefab[j]);
                obj.transform.parent = current.transform;
                obj.transform.position = Vector3.zero;
                obj.SetActive(false);
                pooledBoss.Add(obj);
            }
        }
    }

    public override GameObject GetPooledObject()
    {
        List<GameObject> activeEnemies = new List<GameObject>();
        foreach(GameObject enemyobj in pooledObjects){
            if (!enemyobj.activeInHierarchy)
            {
                activeEnemies.Add(enemyobj);
            }
        }

        int index = Random.Range(0, activeEnemies.Count);

        if (activeEnemies.Count == 0) { Debug.LogError("godverdomme"); }

        return activeEnemies[index];

    }

    public GameObject GetBossObject()
    {
        List<GameObject> activeEnemies = new List<GameObject>();
        foreach (GameObject enemyobj in pooledBoss)
        {
            if (!enemyobj.activeInHierarchy)
            {
                activeEnemies.Add(enemyobj);
            }
        }

        int index = Random.Range(0, activeEnemies.Count);

        if (activeEnemies.Count == 0) { Debug.LogError("godverdomme"); }

        return activeEnemies[index];
    }
    public override void RemoveElement(Transform item)
    {
        base.RemoveElement(item);
    }
}

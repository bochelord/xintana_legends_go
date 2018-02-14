using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPooler : Pooler {

    public GameObject[] enemiesPrefabs;
    //public GameObject[] enemiesPrefabsWorld1;
    //public GameObject[] enemiesPrefabsWorld2;
    //public GameObject[] enemiesPrefabsWorld3;
    //public GameObject[] enemiesPrefabsWorld4;
    //public GameObject[] enemiesPrefabsWorld5;
    //public GameObject[] enemiesPrefabsWorld6;
    //public GameObject[] enemiesPrefabsWorld7;
    //public GameObject[] enemiesPrefabsWorld8;

    public GameObject[] bossPrefab;
    public int amountPooledPerType;


    private List<GameObject> pooledBoss;

    //private List<GameObject>[] pooledObjArrayList = new List<GameObject>[7]; //index indicates the currentworld

    private LevelManager levelManager;

    public override void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();

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

    public  GameObject GetPooledObject(int appearInWorld)
    {
        List<GameObject> retEnemies = new List<GameObject>();
        foreach (GameObject enemyobj in pooledObjects)
        {
            if (!enemyobj.activeInHierarchy && appearInWorld == enemyobj.GetComponentInChildren<EnemyController>().appearsOnWorld)
            {
                retEnemies.Add(enemyobj);
            }
        }

        int index = Random.Range(0, retEnemies.Count);

        if (retEnemies.Count == 0) { Debug.LogError("godverdomme"); }

        return retEnemies[index];


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




    //private void GeneratepooledObjects()
    //{


    //    List<GameObject> listToProcess;

    //    for (int i = 0; i < pooledObjArrayList.Length; i++)
    //    {
    //        listToProcess = pooledObjArrayList[levelManager.GetCurrentWorldNumber()];//we get the list of enemies for that world







    //        GameObject obj = (GameObject)Instantiate(listToProcess[]);

    //        obj.transform.parent = current.transform;
    //        obj.transform.position = Vector3.zero;
    //        obj.SetActive(false);
    //        pooledObjects.Add(obj);
    //    }
    //}
}

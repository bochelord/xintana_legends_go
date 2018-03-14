using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPooler : Pooler {

    public GameObject[] enemiesPrefabs;


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

        if (retEnemies.Count == 0)
        {
            for (int i = 0; i < enemiesPrefabs.Length; i++)
            {
                if (appearInWorld == enemiesPrefabs[i].GetComponentInChildren<EnemyController>().appearsOnWorld)
                {
                    GameObject obj = (GameObject)Instantiate(enemiesPrefabs[i]);
                    obj.transform.parent = current.transform;
                    obj.transform.position = Vector3.zero;
                    obj.SetActive(false);
                    retEnemies.Add(obj);
                }

            }
        }

        AddEnemyToPokedex(retEnemies[index]);

        return retEnemies[index];


    }
    private void AddEnemyToPokedex(GameObject _obj)
    {
        EnemyController _tempController;
        if(_tempController = _obj.GetComponent<EnemyController>())
        {
            _tempController = _obj.GetComponent<EnemyController>();
        }else
        {
            _tempController = _obj.GetComponentInChildren<EnemyController>();
        }
        
        if (!Rad_SaveManager.pokedex.enemiesKnown[_tempController.type])
        {
            Rad_SaveManager.pokedex.enemiesKnown[_tempController.type] = true;
        }
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

        AddEnemyToPokedex(activeEnemies[index]);
        return activeEnemies[index];
    }
    public override void RemoveElement(Transform item)
    {
        base.RemoveElement(item);
    }


}

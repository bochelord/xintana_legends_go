using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPooler : Pooler {

    //public GameObject[] enemiesPrefabs;
    public XintanaEnemiesBestiary enemiesList;

    public GameObject[] bossPrefab;
    public int amountPooledPerType;
    public Transform kogiParent;

    private List<GameObject> pooledBoss;
    private List<GameObject> pooledKogiBounty;
    //private List<GameObject>[] pooledObjArrayList = new List<GameObject>[7]; //index indicates the currentworld

    private LevelManager _levelManager;

    public override void Start()
    {
        _levelManager = FindObjectOfType<LevelManager>();

        pooledObjects = new List<GameObject>();
        for (int j = 0; j < enemiesList.xintanaEnemies.Count; j++)
        {
            for (int i = 0; i < amountPooledPerType; i++)
            {

                if (!isThisinTheArray(enemiesList.xintanaEnemies[j].prefab, bossPrefab))
                {
                    GameObject obj = (GameObject)Instantiate(enemiesList.xintanaEnemies[j].prefab);
                    obj.transform.parent = this.transform;
                    obj.transform.position = Vector3.zero;

                    if (obj.GetComponent<EnemyController>())
                    {
                        obj.GetComponent<EnemyController>().appearsOnWorld = enemiesList.xintanaEnemies[j].appearsInWorld;
                        obj.GetComponent<EnemyController>().nameID = enemiesList.xintanaEnemies[j].nameId;
                    }
                    else if (obj.GetComponentInChildren<EnemyController>())
                    {
                        obj.GetComponentInChildren<EnemyController>().appearsOnWorld = enemiesList.xintanaEnemies[j].appearsInWorld;
                        obj.GetComponentInChildren<EnemyController>().nameID = enemiesList.xintanaEnemies[j].nameId;
                    }
                    else
                    {
                        Debug.LogError("Enemy Pooler is trying to setup enemies but this one: " + obj.name + " does not have EnemyController attached (nor its children)");
                    }
                    
                    //obj.GetComponent<EnemyController>().type = enemiesList.xintanaEnemies[j].type;
                    obj.SetActive(false);
                    pooledObjects.Add(obj);
                }
                
            }
        }

        pooledKogiBounty = new List<GameObject>();

        for (int i = 0; i < 2; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);
            obj.transform.parent = kogiParent.transform;
            obj.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            obj.transform.position = Vector3.zero;
            obj.SetActive(false);
            pooledKogiBounty.Add(obj);
        }


        pooledBoss = new List<GameObject>();
        for (int j = 0; j < bossPrefab.Length; j++)
        {
            for (int i = 0; i < amountPooledPerType; i++)
            {
                GameObject obj = (GameObject)Instantiate(bossPrefab[j]);
                obj.transform.parent = this.transform;

                obj.transform.position = Vector3.zero;
                obj.SetActive(false);
                pooledBoss.Add(obj);
                pooledObjects.Remove(obj); //we remove it from the normal enemy list...
            }
        }
    }

    public override GameObject GetPooledObject()
    {
        List<GameObject> activeEnemies = new List<GameObject>();

        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && !isThisinTheArray(pooledObjects[i], bossPrefab))//we only get the enemies that are not bosses...
            {
                activeEnemies.Add(pooledObjects[i]);
            }
        }


        //foreach (GameObject enemyobj in pooledObjects) {
        //    if (!enemyobj.activeInHierarchy && !isThisinTheArray(enemyobj,bossPrefab))//we only get the enemies that are not bosses...
        //    {
        //        activeEnemies.Add(enemyobj);
        //    }
        //}

        int index = Random.Range(0, activeEnemies.Count);

        if (activeEnemies.Count == 0) { Debug.LogError("godverdomme"); }

        return activeEnemies[index];

    }

    public GameObject GetPooledObject(int appearInWorld)
    {

        List<GameObject> retEnemies = new List<GameObject>();
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy && isEnemyInWorld(appearInWorld, pooledObjects[i].GetComponentInChildren<EnemyController>().appearsOnWorld))
            {
                retEnemies.Add(pooledObjects[i]);
            }
        }


        //foreach (GameObject enemyobj in pooledObjects)
        //{
        //    if (!enemyobj.activeInHierarchy && isEnemyInWorld(appearInWorld, enemyobj.GetComponentInChildren<EnemyController>().appearsOnWorld))
        //    {
        //        retEnemies.Add(enemyobj);
        //    }
        //}

        int index = Random.Range(0, retEnemies.Count);

        if (retEnemies.Count == 0)
        {
            for (int i = 0; i < enemiesList.xintanaEnemies.Count; i++)
            {
                if (isEnemyInWorld(appearInWorld, enemiesList.xintanaEnemies[i].appearsInWorld))
                {
                    GameObject obj = (GameObject)Instantiate(enemiesList.xintanaEnemies[i].prefab);
                    obj.transform.parent = current.transform;
                    obj.transform.position = Vector3.zero;
                    obj.SetActive(false);
                    retEnemies.Add(obj);
                }

            }
        }

        if (retEnemies[index] != null)
        {
            AddEnemyToPokedex(retEnemies[index]);
        } else
        {
            Debug.Log("ARF it does not exist! tried:" + retEnemies[index]);
        }

        return retEnemies[index];


    }
    private void AddEnemyToPokedex(GameObject _obj)
    {
        EnemyController _tempController;
        if (_tempController = _obj.GetComponent<EnemyController>())
        {
            _tempController = _obj.GetComponent<EnemyController>();
        } else
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

        for (int i = 0; i < pooledBoss.Count; i++)
        {
            if (!pooledBoss[i].activeInHierarchy)
            {
                activeEnemies.Add(pooledBoss[i]);
            }
        }

        //foreach (GameObject enemyobj in pooledBoss)
        //{
        //    if (!enemyobj.activeInHierarchy)
        //    {
        //        activeEnemies.Add(enemyobj);
        //    }
        //}

        int index = Random.Range(0, activeEnemies.Count);

        if (activeEnemies.Count == 0) { Debug.LogError("godverdomme"); }

        AddEnemyToPokedex(activeEnemies[index]);
        return activeEnemies[index];
    }

    public GameObject GetBossObject(int appearinWorld)
    {
        List<GameObject> inactiveBosses = new List<GameObject>();
        for (int i = 0; i < pooledBoss.Count; i++)
        {
            if (!pooledBoss[i].activeInHierarchy)
            {
                inactiveBosses.Add(pooledBoss[i]);
            }
        }
        //foreach (GameObject enemyobj in pooledBoss)
        //{
        //    if (!enemyobj.activeInHierarchy)
        //    {
        //        inactiveBosses.Add(enemyobj);
        //    }
        //}


        bool found = false;
        GameObject ret = null;
        for (int i = 0; i < inactiveBosses.Count; i++)
        {
            if (isEnemyInWorld(appearinWorld, inactiveBosses[i].GetComponentInChildren<EnemyController>().appearsOnWorld))
            {
                found = true;
                ret = inactiveBosses[i];
            }

            if (found)
            {
                i = inactiveBosses.Count;
            }
        }

        if (ret == null)
        {
            Debug.LogError("No boss found for world: "+ appearinWorld);
        }

        return ret;

    }



    public GameObject GetPooledKogiBounty()
    {
        List<GameObject> activeKogi = new List<GameObject>();
        for (int i = 0; i < pooledKogiBounty.Count; i++)
        {
            if (!pooledKogiBounty[i].activeInHierarchy)
            {
                activeKogi.Add(pooledKogiBounty[i]);
            }
        }
        //foreach (GameObject kogi in pooledKogiBounty)
        //{
        //    if (!kogi.activeInHierarchy)
        //    {
        //        activeKogi.Add(kogi);
        //    }
        //}

        int index = Random.Range(0, activeKogi.Count);

        if (activeKogi.Count == 0) { Debug.LogError("godverdomme"); }

        return activeKogi[index];
    }
    public override void RemoveElement(Transform item)
    {
        if (!item.GetComponentInChildren<KogiReward>())
        {
            item.transform.SetParent(this.transform);
        }
        //Reset it's position
        item.transform.position = new Vector3(0, 0, 0);
        item.gameObject.SetActive(false);
    }


    private bool isThisinTheArray(GameObject element, GameObject[] array)
    {
        bool ret = false;

        for (int i = 0; i < array.Length; i++)
        {
            if (element == array[i])
            {
                ret = true;
            }
        }

        return ret;
    }


    private bool isEnemyInWorld(int worldEnemy, int[] worlds)
    {
        bool ret = false;

        for (int i = 0; i < worlds.Length; i++)
        {
            if (worldEnemy == worlds[i])
            {
                ret = true;
            }
        }

        return ret;
    }

}
